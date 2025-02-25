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
    //player manager reference
    [Tooltip("player using the QTE")]
    public PlayerManager playerQTE;

    //needed for actionMap
    private InputActionMap actionQTE;

    //reference for action map indentification
    [Tooltip("needed to set/find action map")]
    [SerializeField] private InputActionAsset inputActionAsset;

    //colors for visual representation only (Temporary)
    private Color correct = Color.green;
    private Color incorrect = Color.red;

    //check if a button was pressed
    [Tooltip("check to verify if the button was pressed")]
    public bool pressed = false;

    //check if the pressed button was correct
    [Tooltip("Check to verify if the input was correct")]
    public bool correctPress = false;

    //assigned keycode for button correction
    [Tooltip("Assigned value for identifiyng the button")]
    public KeyCode AssignedBTN;

    //reference for controller input
    [SerializeField] private InputHandler playerInput;

    //controller input check to avoid multiple input
    public bool wasPressed = false;

    //animator reference for Hand animations
    Animator animator;

    private void Awake()
    {
        //set the proper action map on awake as a precuasing, might not be working 100% of time, requires further testing with action maps
        actionQTE = inputActionAsset.FindActionMap("QTE", true);
        //find the animator for the hands in the player object
        animator = playerQTE.GetComponentInChildren<Animator>();
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
        //check if a valid input is being pressed
        if (IsActionPressed())
        {
            //run the check to see what input was the one checked and ensure that it was a correct input or incorrect
            BTNCheck();
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
            || playerQTE.playerInput.actions["North"].triggered || playerQTE.playerInput.actions["DPadDown"].triggered || playerQTE.playerInput.actions["DPadUp"].triggered
            || playerQTE.playerInput.actions["DPadLeft"].triggered || playerQTE.playerInput.actions["DPadRight"].triggered)
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
    private void BTNCheck()
    {
        if (playerQTE.playerInput.actions["South"].WasPressedThisFrame()) //playerInput.Abtn
        {
            if (!wasPressed) // check to ensure double input wont happen
            {
                wasPressed = true; //set back to firt input
                if (AssignedBTN == KeyCode.A)  //if checks to see what input on keycode is correct
                {
                    pressed = true; //set the button to pressed
                    correctPress = true; //set it to correct input
                    gameObject.transform.GetChild(0).gameObject.SetActive(true); //activate the backdrop 
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct; //set the back drop to the corect color
                    ResetTriggers(); //reset animation triggers
                    animator.SetTrigger("QTE1"); //play animation for that input
                }
                else
                {
                    pressed = true; //set the button to pressed
                    correctPress = false; //set the button to incorrect input
                    gameObject.transform.GetChild(0).gameObject.SetActive(true); //activate the backdrop 
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect; //set the back drop to the incorrect color
                    if (AssignedBTN == KeyCode.RightArrow || AssignedBTN == KeyCode.LeftArrow || AssignedBTN == KeyCode.UpArrow || AssignedBTN == KeyCode.DownArrow) //temporary check for arrow image backdrop
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                        gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                    }
                }
            }
        }
        else if (playerQTE.playerInput.actions["East"].WasPressedThisFrame()) //playerInput.Bbtn
        {
            if (!wasPressed)
            {
                wasPressed = true;
                if (AssignedBTN == KeyCode.B)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger("QTE2");
                }
                else
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
        }
        else if (playerQTE.playerInput.actions["West"].WasPressedThisFrame()) //playerInput.Xbtn
        {
            if (!wasPressed)
            {
                wasPressed = true;
                if (AssignedBTN == KeyCode.X)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger("QTE3");
                }
                else
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
        }
        else if (playerQTE.playerInput.actions["North"].WasPressedThisFrame()) //playerInput.Ybtn
        {
            if (!wasPressed)
            {
                wasPressed = true;
                if (AssignedBTN == KeyCode.Y)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger("QTE4");
                }
                else
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
        }
        else if (playerQTE.playerInput.actions["DPadDown"].WasPressedThisFrame()) //playerInput.DownBtn
        {
            if (!wasPressed)
            {
                wasPressed = true;
                if (AssignedBTN == KeyCode.DownArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger("QTE5");
                }
                else
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
        }
        else if (playerQTE.playerInput.actions["DPadUp"].WasPressedThisFrame()) //playerInput.UpBtn
        {
            if (!wasPressed)
            {
                wasPressed = true;
                if (AssignedBTN == KeyCode.UpArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger("QTE6");
                }
                else
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
        }
        else if (playerQTE.playerInput.actions["DPadLeft"].WasPressedThisFrame()) //playerInput.LeftBtn
        {
            if (!wasPressed)
            {
                wasPressed = true;
                if (AssignedBTN == KeyCode.LeftArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger("QTE2");
                }
                else
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
        }
        else if (playerQTE.playerInput.actions["DPadRight"].WasPressedThisFrame()) //playerInput.RightBtn
        {
            if (!wasPressed)
            {
                wasPressed = true;
                if (AssignedBTN == KeyCode.RightArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger("QTE1");
                }
                else
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
        }
        else
        {
            wasPressed = false;
        }
        //if nothing was pressed then the input returns as null

    }
    void ResetTriggers()
    {
        //reset animation triggers
        animator.ResetTrigger("IDLE");
        animator.ResetTrigger("QTE1");
        animator.ResetTrigger("QTE2");
        animator.ResetTrigger("QTE3");
        animator.ResetTrigger("QTE4");
        animator.ResetTrigger("QTE5");
        animator.ResetTrigger("QTE6");
    }
}
