using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller, Sebastian
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
    [Tooltip("Color for the selected object.")]
    public Color selectedColor = Color.green;
    public Color deselect = Color.white;

    private Color defaultColor;

    private int currentFingerIndexP1 = 0;         // columns
    private int currentFingerJointIndexP1 = 0;    // rows

    //used for enemy as of now for testing, likely will be altered for specific situations or maybe deleted depending on game loop
    private int currentFingerIndexP2 = 0;         // columns
    private int currentFingerJointIndexP2 = 0;    // rows

    private bool toggleFullFingerSelect = true; // start by fully selecting fingers

    private bool playerOneHands = true;



    void Start()
    {
        defaultColor = gameObject.GetComponentInChildren<Renderer>().material.color;
        GameManager.Instance.currentCaster = GameManager.Instance.player2;
        UpdateSelection();
    }

    void Update()
    {
        //set up for testing, change it to controller input, this swaps the select of each players hands
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (GameManager.Instance.currentCaster == GameManager.Instance.player2)
            {
                GameManager.Instance.currentCaster = GameManager.Instance.player1;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (GameManager.Instance.currentCaster == GameManager.Instance.player1)
            {
                GameManager.Instance.currentCaster = GameManager.Instance.player2;
            }
        }


        //damage and heal for both players with the above check, also swap fro controller support
        if (playerOneHands)
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                //fingersP2[currentFingerIndexP2].remove = true;
                GameManager.Instance.player1.fingers[currentFingerIndexP1].remove = true;
            }
            else if (Input.GetKeyUp(KeyCode.H))
            {
                GameManager.Instance.player1.fingers[currentFingerIndexP1].add = true;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                //fingersP2[currentFingerIndexP2].remove = true;
                GameManager.Instance.player2.fingers[currentFingerIndexP1].remove = true;
            }
            else if (Input.GetKeyUp(KeyCode.H))
            {
                GameManager.Instance.player2.fingers[currentFingerIndexP1].add = true;
            }
        }


        // the button to toggle between full finger selection or joint selection
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleFingerSelection();
        }

        // we only want to select full fingers individually left and right on our hand
        if (toggleFullFingerSelect)
        {
            if (InputManager.instance.playerControls.Player.NavigateFingerLeft.WasPressedThisFrame())
            {
                MoveSelection(-1, 0, -1, 0); // move left
            }
            else if (InputManager.instance.playerControls.Player.NavigateFingerRight.WasPressedThisFrame())
            {
                MoveSelection(1, 0, 1, 0); // move right
            }
        }

        // once we select a finger only move up and down the finger for each joint
        else
        {
            if (InputManager.instance.playerControls.Player.NavigateFingerUp.WasPressedThisFrame())
            {
                MoveSelection(0, -1, 0, -1); // move up
            }
            else if (InputManager.instance.playerControls.Player.NavigateFingerDown.WasPressedThisFrame())
            {
                MoveSelection(0, 1, 0, 1); // move down
            }
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
    private void MoveSelection(int currentFingerP1, int currentFingerJointP1, int currentFingerP2, int currentFingerJointP2)
    {
        if (playerOneHands)
        {
            //currentFingerP1 = currentFingerIndexP1;
            //currentFingerJointP1 = currentFingerJointIndexP1;
            currentFingerP2 = -1;
            currentFingerJointP2 = -1;
            // deselect all finger joints for the finger
            for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
            {
                SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], defaultColor);
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
                SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], defaultColor);
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
        if (playerOneHands)
        {
            // only reset the finger if we are not already on a joint
            if (toggleFullFingerSelect)
            {

                // deselect all joints in the current finger if switching from full column selection
                for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
                {
                    SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], defaultColor);
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
                    SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], defaultColor);
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
    private void UpdateSelection()
    {
        if (playerOneHands)
        {
            // highlight the entire finger
            for (int i = 0; i < GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints.Count; i++)
            {
                SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], deselect);
            }

            if (toggleFullFingerSelect)
            {
                // highlight the entire finger
                for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
                {
                    SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], selectedColor);
                }
            }
            else
            {
                // highlight the selected joint of the finger
                SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[currentFingerJointIndexP1], selectedColor);
            }
        }
        else
        {
            //used for enemy as of now for testing, likely will be altered for specific situations or maybe deleted depending on game loop
            // highlight the entire finger
            for (int i = 0; i < GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints.Count; i++)
            {
                SetObjectColor(GameManager.Instance.player1.fingers[currentFingerIndexP1].fingerJoints[i], deselect);
            }

            if (toggleFullFingerSelect)
            {
                // highlight the entire finger
                for (int i = 0; i < GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints.Count; i++)
                {
                    SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[i], selectedColor);
                }
            }
            else
            {
                // highlight the selected joint of the finger
                SetObjectColor(GameManager.Instance.player2.fingers[currentFingerIndexP2].fingerJoints[currentFingerJointIndexP2], selectedColor);
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
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
