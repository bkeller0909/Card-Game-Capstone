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

    [Tooltip("player using the QTE")]
    public PlayerManager playerQTE;
    //needed for actionMap
    private InputActionMap actionQTE;
    [Tooltip("needed to set/find action map")]
    [SerializeField] private InputActionAsset inputActionAsset;
    //colors for visual representation only
    private Color correct = Color.green;
    private Color incorrect = Color.red;
    [Tooltip("check to verify if the button was pressed")]
    public bool pressed = false;
    [Tooltip("Check to verify if the input was correct")]
    public bool correctPress = false;
    [Tooltip("Assigned value for identifiyng the button")]
    public KeyCode AssignedBTN;

    private void Awake()
    {
        //set the proper action map on awake as a precuasing, might not be working 100% of time, requires further testing with action maps
        actionQTE = inputActionAsset.FindActionMap("QTE", true);
    }

    /// <summary>
    /// On the update function we are running the main confirm loop of the script, in here all the main functionality of the button runs
    /// first we check the function IsActionPressed() to check uif the input pressed is a valid one (see function for more details) then
    /// we check if the function BTNCheck() has a valid value and if that value matches with the assigned button value (see function for more details)
    /// and we also check if the assigned keycode matches the buttons keycode (the keycode gets assigned upon the spawing of the button) then
    /// we set all the info the button will have confirming that the button was pressed and if it was a correct input or not
    /// </summary>
    void Update()
    {
        //check function for possible input
        if (IsActionPressed())
        {
            //verify the input matches the button
            if (BTNCheck() == playerQTE.playerInput.actions["South"] && AssignedBTN == KeyCode.A)
            {
                //set the value of the button to pressed and if the input was correct set it to correct
                pressed = true;
                correctPress = true;
                //visual representation of correct value
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (BTNCheck() == playerQTE.playerInput.actions["North"] && AssignedBTN == KeyCode.Y)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (BTNCheck() == playerQTE.playerInput.actions["East"] && AssignedBTN == KeyCode.B)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (BTNCheck() == playerQTE.playerInput.actions["West"] && AssignedBTN == KeyCode.X)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (BTNCheck() == playerQTE.playerInput.actions["DPad Down"] && AssignedBTN == KeyCode.DownArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (BTNCheck() == playerQTE.playerInput.actions["DPad Up"] && AssignedBTN == KeyCode.UpArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (BTNCheck() == playerQTE.playerInput.actions["DPad Left"] && AssignedBTN == KeyCode.LeftArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else if (BTNCheck() == playerQTE.playerInput.actions["DPad Right"] && AssignedBTN == KeyCode.RightArrow)
            {
                pressed = true;
                correctPress = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else
            {
                //set the value of the button to pressed and if the input was incorrect set it to incorrect
                pressed = true;
                correctPress = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //visual representation of correct value
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                if (AssignedBTN == KeyCode.RightArrow || AssignedBTN == KeyCode.LeftArrow || AssignedBTN == KeyCode.UpArrow || AssignedBTN == KeyCode.DownArrow)
                {
                    //visual representation of incorrect value
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                }
            }
        }
    }

    /// <summary>
    /// This function checks that whatever input we pressed with the controller, it is one of the 8 possible inputs, that way if the player 
    /// pressed anything else it wont count as an error, this ensures that the only possible contact with the QTE system is with the proper
    /// inputs, This script is called on update before any other button check is verified.
    /// </summary>
    private bool IsActionPressed()
    {
        //checks to see if any of the inputs made are the possible QTE input options, if so return true
        if (playerQTE.playerInput.actions["South"].triggered || playerQTE.playerInput.actions["East"].triggered || playerQTE.playerInput.actions["West"].triggered
            || playerQTE.playerInput.actions["North"].triggered || playerQTE.playerInput.actions["DPad Down"].triggered || playerQTE.playerInput.actions["DPad Up"].triggered
            || playerQTE.playerInput.actions["DPad Left"].triggered || playerQTE.playerInput.actions["DPad Right"].triggered)
        {
            return true;
        }
        //if the input is not that of a possible button then return false
        return false;
    }



    /// <summary>
    /// This Function exists to check and return the correct input the player pressed, that way we can properly identify the QTE Button
    /// It is called in update when checking for the input confirmation so that the QTE manager has the right info about each button.
    /// </summary>
    private InputAction BTNCheck()
    {
        //the if checks verify the button pressed
        if (playerQTE.playerInput.actions["South"].triggered)
        {
            //and the return in turn returns the proper button
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
        //if nothing was pressed then the input returns as null
        return null;
    }
}
