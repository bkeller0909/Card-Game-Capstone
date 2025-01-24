using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        QTE Button
//  Date Created: 01/16/2025
//  Purpose:      Script to crontrol the functionallity of QTE usage
//  Instance?     No
//-----------------------------------------------------------------
/// </summary>
public class QTEButton : MonoBehaviour
{
    // If you wish to test the new input actions I made for you. Un-comment everything and try it out.
    // ABXY are WASD on the keyboard and the DPad is the arrow keys. 

    public PlayerManager playerQTE;
    private InputActionMap actionQTE;
    [SerializeField] private InputActionAsset inputActionAsset;

    public bool dir;
    public Color correct = Color.green;
    public Color incorrect = Color.red;
    public bool pressed = false;
    public bool correctPress = false;
    public KeyCode AssignedBTN;             // replace this with IsActionPressed() in the if statements.
    public InputAction CurrentBTN;


    private void Awake()
    {
        actionQTE = inputActionAsset.FindActionMap("QTE", true);
        //dir = true;
    }

    private void Start()
    {
        //CurrentBTN = playerQTE.playerInput.GetComponent<InputAction>();
        //playerQTE = GetComponent<PlayerManager>();
        //actionQTE = inputActionAsset.FindActionMap("QTE", true);
        //dir = true;
    }

    void Update()
    {
        /*if (playerQTE.playerInput.actions["South"].triggered || playerQTE.playerInput.actions["East"].triggered
            || playerQTE.playerInput.actions["West"].triggered || playerQTE.playerInput.actions["North"].triggered
            || playerQTE.playerInput.actions["DPad Down"].triggered || playerQTE.playerInput.actions["DPad Up"].triggered
            || playerQTE.playerInput.actions["DPad Left"].triggered || playerQTE.playerInput.actions["DPad Right"].triggered)*/

        //if (pressed)
        //{
        //    gameObject.GetComponent<QTEButton>().enabled = false;
        //}

        if (IsActionPressed())
        {
            if (testCheck() == playerQTE.playerInput.actions["South"] && AssignedBTN == KeyCode.A)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (testCheck() == playerQTE.playerInput.actions["North"] && AssignedBTN == KeyCode.Y)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (testCheck() == playerQTE.playerInput.actions["East"] && AssignedBTN == KeyCode.B)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (testCheck() == playerQTE.playerInput.actions["West"] && AssignedBTN == KeyCode.X)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (testCheck() == playerQTE.playerInput.actions["DPad Down"] && AssignedBTN == KeyCode.DownArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (testCheck() == playerQTE.playerInput.actions["DPad Up"] && AssignedBTN == KeyCode.UpArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (testCheck() == playerQTE.playerInput.actions["DPad Left"] && AssignedBTN == KeyCode.LeftArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (testCheck() == playerQTE.playerInput.actions["DPad Right"] && AssignedBTN == KeyCode.RightArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else
            //else if (!CurrentBTN.triggered)
            {
                pressed = true;
                correctPress = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                if (AssignedBTN == KeyCode.RightArrow || AssignedBTN == KeyCode.LeftArrow || AssignedBTN == KeyCode.UpArrow || AssignedBTN == KeyCode.DownArrow)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if (Input.GetKeyDown(AssignedBTN))
        //    {
        //        pressed = true;
        //        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
        //        if (dir)
        //        {
        //            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
        //        }
        //    }
        //    else if (!Input.GetKeyDown(AssignedBTN))
        //    {
        //        pressed = true;
        //        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
        //        if (dir)
        //        {
        //            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Checks if the action map is triggered.
    /// </summary>
    /// <returns>True is action is triggered. False if not.</returns>
    private bool IsActionPressed()
    {
        if (playerQTE.playerInput.actions["South"].triggered || playerQTE.playerInput.actions["East"].triggered || playerQTE.playerInput.actions["West"].triggered
            || playerQTE.playerInput.actions["North"].triggered || playerQTE.playerInput.actions["DPad Down"].triggered || playerQTE.playerInput.actions["DPad Up"].triggered
            || playerQTE.playerInput.actions["DPad Left"].triggered || playerQTE.playerInput.actions["DPad Right"].triggered)
        {
            return true;
        }
        return false;
    }


    private InputAction testCheck()
    {
        if (playerQTE.playerInput.actions["South"].triggered)
        {
            return playerQTE.playerInput.actions["South"];
        }
        else if (playerQTE.playerInput.actions["East"].triggered)
        {
            return playerQTE.playerInput.actions["East"];
        }
        else if (playerQTE.playerInput.actions["West"].triggered)
        {
            return playerQTE.playerInput.actions["West"];
        }
        else if (playerQTE.playerInput.actions["North"].triggered)
        {
            return playerQTE.playerInput.actions["North"];
        }
        else if (playerQTE.playerInput.actions["DPad Down"].triggered)
        {
            return playerQTE.playerInput.actions["DPad Down"];
        }
        else if (playerQTE.playerInput.actions["DPad Up"].triggered)
        {
            return playerQTE.playerInput.actions["DPad Up"];
        }
        else if (playerQTE.playerInput.actions["DPad Left"].triggered)
        {
            return playerQTE.playerInput.actions["DPad Left"];
        }
        else if (playerQTE.playerInput.actions["DPad Right"].triggered)
        {
            return playerQTE.playerInput.actions["DPad Right"];
        }
        return null;

    }

    private InputAction BTNCheck()
    {
        for (int i = 0; i < actionQTE.actions.Count; i++)
        {
            Debug.Log(actionQTE.actions[i]);
            if (actionQTE.actions[i].triggered)
            {
                return actionQTE.actions[i];
            }
        }
        return null;
    }
}
