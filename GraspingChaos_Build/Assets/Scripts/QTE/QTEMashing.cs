using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEMashing : MonoBehaviour
{

    //player manager reference
    [Tooltip("player using the QTE")]
    public PlayerManager playerQTE;

    //needed for actionMap
    private InputActionMap actionQTE;

    //reference for action map indentification
    [Tooltip("needed to set/find action map")]
    [SerializeField] private InputActionAsset inputActionAsset;

    //assigned keycode for button correction
    [Tooltip("Assigned value for identifiyng the button")]
    public KeyCode AssignedBTN;

    //controller input check to avoid multiple input
    public bool wasPressed = false;

    //colors for visual representation only (Temporary)
    private Color correct = Color.green;
    private Color incorrect = Color.red;

    //check if a button was pressed
    [Tooltip("check to verify if the button was pressed")]
    public bool pressed = false;

    //check if the pressed button was correct
    [Tooltip("Check to verify if the input was correct")]
    public bool correctPress = false;

    //animator reference for Hand animations
    Animator animator;

    string storageQTE;


    private void Awake()
    {
        //set the proper action map on awake as a precuasing, might not be working 100% of time, requires further testing with action maps
        actionQTE = inputActionAsset.FindActionMap("QTE", true);
        //find the animator for the hands in the player object
        animator = playerQTE.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if a valid input is being pressed
        if (IsActionPressed())
        {
            BTNCheck();
        }
    }

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


    private void BTNCheck()
    {
        if (playerQTE.playerInput.actions["South"].WasPressedThisFrame()) //playerInput.Abtn
        {
            if (!wasPressed) // check to ensure double input wont happen
            {
                wasPressed = true; //set back to firt input
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.A)  //if checks to see what input on keycode is correct
                {
                    pressed = true; //set the button to pressed
                    correctPress = true; //set it to correct input
                    gameObject.transform.GetChild(0).gameObject.SetActive(true); //activate the backdrop 
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct; //set the back drop to the corect color
                    ResetTriggers(); //reset animation triggers
                    animator.SetTrigger(RandomnizeHandAnim()); //play animation for that input
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
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.B)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger(RandomnizeHandAnim());
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
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.X)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger(RandomnizeHandAnim());
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
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.Y)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger(RandomnizeHandAnim());
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
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.DownArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger(RandomnizeHandAnim());
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
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.UpArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger(RandomnizeHandAnim());
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
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.LeftArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger(RandomnizeHandAnim());
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
                playerQTE.gameObject.GetComponent<QTEHandler>().inputTracker++;
                if (AssignedBTN == KeyCode.RightArrow)
                {
                    pressed = true;
                    correctPress = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                    ResetTriggers();
                    animator.SetTrigger(RandomnizeHandAnim());
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


    private string RandomnizeHandAnim()
    {
        int rand = Random.Range(1, 7);
        if(rand == 1)
        {
            storageQTE = "QTE1";
            return "QTE1";
        }
        else if(rand == 2)
        {
            storageQTE = "QTE2";
            return "QTE2";
        }
        else if(rand == 3)
        {
            storageQTE = "QTE3";
            return "QTE3";
        }
        else if (rand == 4)
        {
            storageQTE = "QTE4";
            return "QTE4";
        }
        else if (rand == 5)
        {
            storageQTE = "QTE5";
            return "QTE5";
        }
        else if (rand == 6)
        {
            storageQTE = "QTE6";
            return "QTE6";
        }

        return null;
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
