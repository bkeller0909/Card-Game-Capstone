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
    Animator playerAnimator, enemyAnimator;

    [SerializeField] private Sprite failA;
    [SerializeField] private Sprite failB;
    [SerializeField] private Sprite failX;
    [SerializeField] private Sprite failY;
    [SerializeField] private Sprite failDown;
    [SerializeField] private Sprite failUp;
    [SerializeField] private Sprite failRight;
    [SerializeField] private Sprite failLeft;

    [SerializeField] private Sprite winA;
    [SerializeField] private Sprite winB;
    [SerializeField] private Sprite winX;
    [SerializeField] private Sprite winY;
    [SerializeField] private Sprite winDown;
    [SerializeField] private Sprite winUp;
    [SerializeField] private Sprite winRight;
    [SerializeField] private Sprite winLeft;

    private void Awake()
    {
        //set the proper action map on awake as a precuasing, might not be working 100% of time, requires further testing with action maps
        actionQTE = inputActionAsset.FindActionMap("QTE", true);
    }

    private void Start()
    {
        //find the animator for the hands in the player object
        if (playerQTE.playerNum == PlayerType.PLAYER1)
        {
            playerAnimator = playerQTE.skullHands.GetComponent<Animator>();
            enemyAnimator = playerQTE.stagHands.GetComponent<Animator>();
        }
        else if (playerQTE.playerNum == PlayerType.PLAYER2)
        {
            playerAnimator = playerQTE.stagHands.GetComponent<Animator>();
            enemyAnimator = playerQTE.skullHands.GetComponent<Animator>();
        }
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winA; // set the new sprite
                    ResetTriggers(); //reset animation triggers
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true; //set the button to pressed
                    correctPress = false; //set the button to incorrect input
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winB; // set the new sprite
                    ResetTriggers();
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true;
                    correctPress = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winX; // set the new sprite
                    ResetTriggers();
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true;
                    correctPress = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winY; // set the new sprite
                    ResetTriggers();
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true;
                    correctPress = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winDown; // set the new sprite
                    ResetTriggers();
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true;
                    correctPress = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winUp; // set the new sprite
                    ResetTriggers();
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true;
                    correctPress = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winLeft; // set the new sprite
                    ResetTriggers();
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true;
                    correctPress = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = winRight; // set the new sprite
                    ResetTriggers();
                    string temp = playerQTE.GetComponent<QTEHandler>().GetUniqueHandAnimation();
                    playerAnimator.SetTrigger(temp); //play animation for that input
                    enemyAnimator.SetTrigger(temp); //play animation for that input
                }
                else
                {
                    pressed = true;
                    correctPress = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = FindIncorrectSprite(AssignedBTN); // set the new sprite
                }
            }
        }
        else
        {
            wasPressed = false;
        }
        //if nothing was pressed then the input returns as null

    }

    private Sprite FindIncorrectSprite(KeyCode currentInput)
    {
        switch(currentInput)
        {
            case KeyCode.A:
                return failA;
           case KeyCode.B:
                return failB;
            case KeyCode.X:
                return failX;
            case KeyCode.Y:
                return failY;
            case KeyCode.DownArrow:
                return failDown;
            case KeyCode.UpArrow:
                return failUp;
            case KeyCode.LeftArrow:
                return failLeft;
            case KeyCode.RightArrow:
                return failRight;
        }
        return null;
    }



    void ResetTriggers()
    {
        //reset animation triggers
        playerAnimator.ResetTrigger("IDLE");
        playerAnimator.ResetTrigger("QTE1");
        playerAnimator.ResetTrigger("QTE2");
        playerAnimator.ResetTrigger("QTE3");
        playerAnimator.ResetTrigger("QTE4");
        playerAnimator.ResetTrigger("QTE5");
        playerAnimator.ResetTrigger("QTE6");
        playerAnimator.ResetTrigger("QTE7");
        playerAnimator.ResetTrigger("QTE8");
        playerAnimator.ResetTrigger("QTE9");
        playerAnimator.ResetTrigger("QTE10");

        enemyAnimator.ResetTrigger("IDLE");
        enemyAnimator.ResetTrigger("QTE1");
        enemyAnimator.ResetTrigger("QTE2");
        enemyAnimator.ResetTrigger("QTE3");
        enemyAnimator.ResetTrigger("QTE4");
        enemyAnimator.ResetTrigger("QTE5");
        enemyAnimator.ResetTrigger("QTE6");
        enemyAnimator.ResetTrigger("QTE7");
        enemyAnimator.ResetTrigger("QTE8");
        enemyAnimator.ResetTrigger("QTE9");
        enemyAnimator.ResetTrigger("QTE10");
    }
}
