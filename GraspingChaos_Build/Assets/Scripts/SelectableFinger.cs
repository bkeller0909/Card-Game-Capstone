using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Contributors: Sebastian
//  Title:        SelectableFinger
//  Date Created: 01/09/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Navigation ability between fingers and finger joints. This is organized by using a multi dimensionl array
/// that takes the fingers as columns and the joints of the finger as a row. The ability to fully select the finger or joint
/// can be toggled. Each selected finger or joint is highlighted by changing the colour of the gamee object through the use of the
/// array.
/// </summary>
public class SelectableFinger : MonoBehaviour
{
    private PlayerManager player;
    private PlayerState playerState;

    [Tooltip("Color for the selected object.")]
    public Color selectedColor = Color.red;
    public Color deselect = Color.white;

    public int currentFingerIndexP1 = 0;         // columns
    private int currentFingerJointIndexP1 = 0;    // rows

    //used for enemy as of now for testing, likely will be altered for specific situations or maybe deleted depending on game loop
    private int currentFingerIndexP2 = 0;         // columns
    private int currentFingerJointIndexP2 = 0;    // rows

    private bool toggleFullFingerSelect = true; // start by fully selecting fingers

    public bool playerOneHands = true;

    private InputHandler playerInput;

    private bool wasPressed = false;
    private bool dmgAndHpPressed = false;
    private bool switchPresses = false;

    private bool setState = true;

    private Color basicColour;

    void Start()
    {
        selectedColor = Color.white;
        player = GetComponent<PlayerManager>();
        playerInput = GetComponent<InputHandler>();
        playerState = GetComponent<PlayerState>();
        GameManager.Instance.currentCaster = GameManager.Instance.player2;
        //UpdateSelection();
    }

    public void SetUpAuras()
    {
        if (GameManager.Instance.player1 == player)
        {
            GameManager.Instance.defaultColorP1 = GameManager.Instance.player1.fingers[5].GetComponentInChildren<SkinnedMeshRenderer>().material.GetColor("_AuraColour");
            GameManager.Instance.defaultColorP1 = GameManager.Instance.player1.visualFingers[5].GetComponentInChildren<SkinnedMeshRenderer>().material.GetColor("_AuraColour");
        }
        else if (GameManager.Instance.player2 == player)
        {
            GameManager.Instance.defaultColorP2 = GameManager.Instance.player2.fingers[5].GetComponentInChildren<SkinnedMeshRenderer>().material.GetColor("_AuraColour");
            GameManager.Instance.defaultColorP2 = GameManager.Instance.player2.visualFingers[5].GetComponentInChildren<SkinnedMeshRenderer>().material.GetColor("_AuraColour");
        }
    }

    /// <summary>
    /// Updates the current selected finger and also the finger joint. 
    /// Visually represent the change by changing the colour of the selected game object.
    /// </summary>
    /// <param name="currentFinger">Horizontal index for the finger.</param>
    /// <param name="currentFingerJoint">Vertical index for the finger joint.</param>
    /// 
    //MoveSelection has been altered as of now for testing both players, will likely get altered or changed to fit the game loop
    public void MoveSelection(int currentFingerP1, int currentFingerJointP1, int currentFingerP2, int currentFingerJointP2)
    {
        if (player.playerOneHands)
        {
            //currentFingerP1 = currentFingerIndexP1;
            //currentFingerJointP1 = currentFingerJointIndexP1;
            currentFingerP2 = -1;
            currentFingerJointP2 = -1;
            // deselect all finger joints for the finger
            for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
            {
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.visualFingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
                }
            }

            // update the index
            currentFingerIndexP1 = Mathf.Clamp(currentFingerIndexP1 + currentFingerP1, 0, GameManager.Instance.player1.fingers.Count - 1); // clamp to stay in bounds
            currentFingerJointIndexP1 = Mathf.Clamp(currentFingerJointIndexP1 + currentFingerJointP1, 0, GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count - 1);
        }
        else
        {
            //used for enemy as of now for testing, likely will be altered for specific situations or maybe deleted depending on game loop
            currentFingerP1 = -1;
            currentFingerJointP1 = -1;
            //currentFingerP2 = currentFingerIndexP2;
            //currentFingerJointP2 = currentFingerIndexP2;
            // deselect all finger joints for the finger
            for (int i = 0; i < GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints.Count; i++)
            {
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.visualFingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                }
            }

            // update the index
            currentFingerIndexP2 = Mathf.Clamp(currentFingerIndexP2 + currentFingerP2, 0, GameManager.Instance.player2.fingers.Count - 1); // clamp to stay in bounds
            currentFingerJointIndexP2 = Mathf.Clamp(currentFingerJointIndexP2 + currentFingerJointP2, 0, GameManager.Instance.player1.fingers[currentFingerIndexP2].fingerJoints.Count - 1);
        }

        // apply a new selected finger
        UpdateSelection();
    }

    /// <summary>
    /// Switch between fully selecting a finger, and the single joints on the finger.
    /// </summary>
    private void ToggleFingerSelection()
    {
        if (player.playerOneHands)
        {
            // only reset the finger if we are not already on a joint
            if (toggleFullFingerSelect)
            {

                // deselect all joints in the current finger if switching from full column selection
                for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
                {
                    if (GameManager.Instance.player1 == player)
                    {
                        SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
                    }
                    else if (GameManager.Instance.player2 == player)
                    {
                        SetObjectColor(GameManager.Instance.player1.visualFingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
                    }
                    //playerState.currentFingerName = player.fingers[currentFingerIndexP1].finger;
                }
            }

            // toggle the selection state
            toggleFullFingerSelect = !toggleFullFingerSelect;

            // if switching to the joint selection, stay on the same finger
            if (toggleFullFingerSelect)
            {
                currentFingerJointIndexP1 = 0;  // select the first joint of the finger
            }
            else
            {
                currentFingerIndexP1 = Mathf.Clamp(currentFingerIndexP1, 0, GameManager.Instance.player1.fingers.Count - 1); // copied the clamp to stay in bounds
                currentFingerJointIndexP1 = 0;  // reset to the first cube in the column
            }
        }
        else
        {
            //used for enemy as of now for testing, likely will be altered for specific situations or maybe deleted depending on game loop
            // only reset the finger if we are not already on a joint
            if (toggleFullFingerSelect)
            {

                // deselect all joints in the current finger if switching from full column selection
                for (int i = 0; i < GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints.Count; i++)
                {
                    if (GameManager.Instance.player1 == player)
                    {
                        SetObjectColor(GameManager.Instance.player2.visualFingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                    }
                    else if (GameManager.Instance.player2 == player)
                    {
                        SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                    }
                    //playerState.currentFingerName = player.fingers[currentFingerIndexP2].finger;
                }
            }

            // toggle the selection state
            toggleFullFingerSelect = !toggleFullFingerSelect;

            // if switching to the joint selection, stay on the same finger
            if (toggleFullFingerSelect)
            {
                currentFingerJointIndexP2 = 0;  // select the first joint of the finger
            }
            else
            {
                currentFingerIndexP2 = Mathf.Clamp(currentFingerIndexP2, 0, GameManager.Instance.player2.fingers.Count - 1); // copied the clamp to stay in bounds
                currentFingerJointIndexP2 = 0;  // reset to the first cube in the column
            }
        }

        // update the selection after toggling
        UpdateSelection();
    }

    /// <summary>
    /// Visually updates the finger for if the entire finger is selected or just the joint.
    /// </summary>
    public void UpdateSelection()
    {
        if (player.playerOneHands)
        {
            // highlight the entire finger
            for (int i = 0; i < GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints.Count; i++)
            {
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.visualFingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                }
            }

            if (toggleFullFingerSelect)
            {
                // highlight the entire finger
                for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
                {
                    selectedColor = GameManager.Instance.defaultColorP2;
                    if (GameManager.Instance.player1 == player)
                    {
                        SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], selectedColor);
                    }
                    else if (GameManager.Instance.player2 == player)
                    {
                        SetObjectColor(GameManager.Instance.player1.visualFingers[currentFingerIndexP1].fingerJoints[i], selectedColor);
                    }
                }
            }
            else
            {
                // highlight the selected joint of the finger
                selectedColor = GameManager.Instance.defaultColorP2;
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[currentFingerJointIndexP1], selectedColor);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.visualFingers[currentFingerIndexP1].fingerJoints[currentFingerJointIndexP1], selectedColor);
                }
            }
        }
        else
        {
            //used for enemy as of now for testing, likely will be altered for specific situations or maybe deleted depending on game loop
            // highlight the entire finger
            for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
            {
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.visualFingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP2);
                }
            }
            DeselectForFake();
            if (toggleFullFingerSelect)
            {
                // highlight the entire finger
                for (int i = 0; i < GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints.Count; i++)
                {
                    selectedColor = GameManager.Instance.defaultColorP1;
                    if (GameManager.Instance.player1 == player)
                    {
                        SetObjectColor(GameManager.Instance.player2.visualFingers[currentFingerIndexP2].fingerJoints[i], selectedColor);
                    }
                    else if (GameManager.Instance.player2 == player)
                    {
                        SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], selectedColor);
                    }
                }
            }
            else
            {
                // highlight the selected joint of the finger
                selectedColor = GameManager.Instance.defaultColorP1;
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.visualFingers[currentFingerIndexP2].fingerJoints[currentFingerJointIndexP2], selectedColor);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[currentFingerJointIndexP2], selectedColor);
                }
            }
        }
    }

    /// <summary>
    /// Changes the colour of the game object to show if it's selected or not.
    /// </summary>
    /// <param name="obj">The game object whose colour is being changed.</param>
    /// <param name="color">The colour that will be applied.</param>
    private void SetObjectColor(GameObject obj, Color color)
    {
        SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            renderer.material.SetColor("_AuraColour", color);
        }
    }

    public void SelectFinger()
    {
        if (player.playerOneHands)
        {
            playerState.currentFingerName = player.fingers[currentFingerIndexP1].finger;
        }
        else
        {
            playerState.currentFingerName = player.fingers[currentFingerIndexP2].finger;
        }
    }

    public void DeselectOnPick()
    {
        if (player.playerOneHands)
        {
            for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
            {
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player1.visualFingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
                }
            }
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints.Count; i++)
            {
                if (GameManager.Instance.player1 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.visualFingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                }
                else if (GameManager.Instance.player2 == player)
                {
                    SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], GameManager.Instance.defaultColorP2);
                }
            }
        }
    }

    public void DeselectForFake()
    {
        for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
        {
            if (GameManager.Instance.player1 == player)
            {
                SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
            }
            else if (GameManager.Instance.player2 == player)
            {
                SetObjectColor(GameManager.Instance.player1.visualFingers[currentFingerIndexP1].fingerJoints[i], GameManager.Instance.defaultColorP1);
            }
        }
    }

    public void UnToggleEnemyHands(PlayerManager player)
    {
        if (player == GameManager.Instance.player1)
        {
            for (int i = 0; i < GameManager.Instance.player2.fingers.Count; i++)
            {
                for (int w = 0; w < GameManager.Instance.player2.fingers[i].fingerJoints.Count; w++)
                {
                    SetObjectColor(GameManager.Instance.player2.visualFingers[i].fingerJoints[w], GameManager.Instance.defaultColorP2);
                }
            }
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.player1.fingers.Count; i++)
            {
                for (int w = 0; w < GameManager.Instance.player1.fingers[i].fingerJoints.Count; w++)
                {
                    SetObjectColor(GameManager.Instance.player1.visualFingers[i].fingerJoints[w], GameManager.Instance.defaultColorP1);
                }
            }
        }
    }
}
