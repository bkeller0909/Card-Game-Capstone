using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        QTE Manager
//  Date Created: 01/16/2025
//  Purpose:      Manage the creation and success check of all the Quick time Events of each player
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class QTEHandler : MonoBehaviour
{
    //Index that keeps track of the created buttons of the current QTE System
    private int createdBTNIndex;
    //enum variable that can store the current randomized qte button on the sequence
    private QTEButtonType RandoBTN;

    [Header("QTE Sprites")]
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteA;
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteB;
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteY;
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteX;
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteDownInput;
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteLeftInput;
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteRightInput;
    [Tooltip("Variable for every sprite of each buttons")]
    [SerializeField] private Sprite QTESpriteUpInput;

    [Header("Object References Needed")]
    [Tooltip("Reference of player 1 for comparison")]
    [SerializeField] private PlayerManager p1;
    [Tooltip("Reference of player 2 for comparison")]
    [SerializeField] private PlayerManager p2;
    [Tooltip("List of all buttons on scene for Player 1")]
    [SerializeField] private List<GameObject> Buttons = new List<GameObject>();

    [Header("Used Buttons and Values")]
    [Tooltip("List of all buttons used for QTE for Player 1")]
    public List<GameObject> CreatedButtons = new List<GameObject>();
    [Tooltip("Value that counts how many QTE buttons are pressed correctly")]
    [SerializeField] private int QTECounter;
    [Tooltip("Value that tracks the percentage of the Succesful QTE pressed")]
    [SerializeField] private int QTEPercent;

    [Header("Timer Values")]
    public float remainingTime;
    public bool startTimer;

    [Header("Debug Values")]
    [SerializeField] private TMP_Text qteCheck;
    [SerializeField] private TMP_Text qteCheckPercent;
    //debug value for making the direction sprites be the same size
    private Vector3 dirScale = new Vector3(0.02f, 0.02f, 0.02f);
    //debug value for making the button sprites be the same size
    private Vector3 btnScale = new Vector3(0.03f, 0.03f, 0.03f);

    //everything bellow 50% is low, everything below 100% is mid, everyting 100% is full
    //everything bellow 50% is low, above 50% is mid (completed if there is only 2 options)

    private InputHandler playerInput;
    private bool qteInput = false;

    public bool resetLoop = false;


    private void Start()
    {
        //start the timer at false
        startTimer = false;
        //get the timer amount from the Game Manager
        remainingTime = GameManager.Instance.timerQTE;
        //set the qte button index to -1 as to not offset the out of bounce counter
        createdBTNIndex = -1;
        //set the amount of qte pressed correctly to 0 since you havent yet done a qte
        QTECounter = 0;
        playerInput = gameObject.GetComponentInParent<InputHandler>();
    }

    /// <summary>
    /// Main loop of QTE Hanlder
    /// </summary>
    void Update()
    {
        if(resetLoop)
        {
            resetLoop = false;
            playerInput.gameObject.GetComponent<PlayerManager>().playerInput.SwitchCurrentActionMap("Card");
            playerInput.gameObject.GetComponentInChildren<CameraPositionChange>().GetInputForced(1);
        }


        //main loop of the QTE Handler

        //check if the timer has started
        if (startTimer)
        {
            //if the timer is running then tick down based on Time.deltaTime
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            //if the timer is done then set the remaining time to 0, the timer check to false and remove the QTEButtons that where present
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                startTimer = false;
                DisableQTEButtonsTimer();
            }
        }

        if (playerInput.debugQTE)
        {
            if (!qteInput)
            {
                qteInput = true;
                //Create(14, gameObject.GetComponent<PlayerManager>());
                //playerInput.gameObject.GetComponent<PlayerManager>().playerInput.SwitchCurrentActionMap("Card");
            }
        }
        else
        {
            qteInput = false;
        }

        //function that checks the values of each QTE Button on the sequence to determine which is currently active (more info in the function)
        CheckAvailability();

        //debug string value for cehcking correct amount of QTE
        //qteCheck.text = QTECounter.ToString();

        //function that checks the percentage of correct QTE for the card evaluation results
        //EvauateQTEResults();
    }

    /// <summary>
    /// This function is used to randomize the buttons that are gonna be used in the QTE sequence, first we get a range between 0 and 8 with 0 being eclusive
    /// that way we can get a value between 1 and 8 which represents each possible input we have for the QTE, then we run a loop for each possible input (loop of 8)
    /// and we do a check to determine if the randomize value matches that of the global enum QTEButtonType by casting it in the if check, if it matches then we
    /// return that enum value so it can be assigned in the Create function
    /// </summary>
    private static QTEButtonType RandomizeBTN()
    {
        //get a rando value between 1 and 8
        int button = UnityEngine.Random.Range(0, 8);
        //check for each possible button type based on the randomzied value
        for (int i = 0; i < 8; i++)
        {
            if (button == (int)QTEButtonType.Y)
            {
                //if check is succesful then return the correct button type
                return QTEButtonType.Y;
            }
            else if (button == (int)QTEButtonType.X)
            {
                return QTEButtonType.X;
            }
            else if (button == (int)QTEButtonType.B)
            {
                return QTEButtonType.B;
            }
            else if (button == (int)QTEButtonType.A)
            {
                return QTEButtonType.A;
            }
            else if (button == (int)QTEButtonType.UP)
            {
                return QTEButtonType.UP;
            }
            else if (button == (int)QTEButtonType.LEFT)
            {
                return QTEButtonType.LEFT;
            }
            else if (button == (int)QTEButtonType.RIGHT)
            {
                return QTEButtonType.RIGHT;
            }
            else if (button == (int)QTEButtonType.DOWN)
            {
                return QTEButtonType.DOWN;
            }
        }
        return QTEButtonType.A;
        //return QTEButtons[button];
    }

    /// <summary>
    /// This function is used to assign values to the buttons that are being used in the scene for the QTE, we run an if check that checks what button they are supposed
    /// to be based on the randomized value, then it assigns the correct sprite, the correct keycode and for now the correct size (size is debuged for now with temp sprites)
    /// the 2 values the function takes in are the index of the buttons that are to be used on the scene and the randomized enum value that needs to check for the corect value
    /// </summary>
    private void AssignSprite(int indexBTN, QTEButtonType RandomizeBTN)
    {
        //check if the randomize value matches the correct button type
        if (RandomizeBTN == QTEButtonType.Y)
        {
            //if check is succesful then assign proper sprite, keycode and scale (scale is debug only due to the current sprites)
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteY;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.Y;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = btnScale;
        }
        else if (RandomizeBTN == QTEButtonType.X)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteX;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.X;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = btnScale;
        }
        else if (RandomizeBTN == QTEButtonType.B)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteB;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.B;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = btnScale;
        }
        else if (RandomizeBTN == QTEButtonType.A)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteA;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.A;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = btnScale;
        }
        else if (RandomizeBTN == QTEButtonType.UP)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteUpInput;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.UpArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
        else if (RandomizeBTN == QTEButtonType.LEFT)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteLeftInput;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.LeftArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
        else if (RandomizeBTN == QTEButtonType.RIGHT)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteRightInput;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.RightArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
        else if (RandomizeBTN == QTEButtonType.DOWN)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = QTESpriteDownInput;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.DownArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
    }

    /// <summary>
    /// This Function is the one that "Creates" or assignes the buttons that will be used for the QTE Sequence, this function handles most of the proccess with the sequence 
    /// which includes, assigning the proper buttons, fitting them into the proper list for evaluation, runs the randomize and assignsprite functions, ensures that the correct
    /// caster is seeing the correct QTE Sequence on their screen, sets the timer for the QTE Sequence and sets the index of the created Sequence, the 2 values the function
    /// takes in are the QTE Amount so that it knows how many buttons to create and the caster it has to display it to
    /// </summary>
    public void Create(int qteAmount, PlayerManager caster)
    {
        //set the proper action map for QTE Action
        caster.playerInput.SwitchCurrentActionMap("QTE");
        //loop the amount of times equeal to the amount of QTE that need to be in the sequence
        for (int i = 0; i < qteAmount; i++)
        {
            //set the counter to 0 since you havent completed any input
            QTECounter = 0;
            //set the randomized button 
            RandoBTN = RandomizeBTN();
            //assign the button properly
            AssignSprite(i, RandoBTN);
            //activete the proper button on the sequence
            Buttons[i].SetActive(true);
            //assign the proper caster to the button input checks
            Buttons[i].GetComponent<QTEButton>().playerQTE = caster;
            Buttons[i].GetComponent<QTEButton>().wasPressed = false;
            //check to verify whcih caster is the one that needs this QTE Sequence
            if (caster == p1)
            {
                //assign the buttons to the correct layers so that it can be showned properly (this might change later once in the game scene the handler uses different buttons)
                Buttons[i].layer = LayerMask.NameToLayer("The Skull");
                Buttons[i].transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Skull");
                Buttons[i].transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("The Skull");
                Buttons[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Skull");
            }
            else if (caster == p2)
            {
                //assign the buttons to the correct layers so that it can be showned properly (this might change later once in the game scene the handler uses different buttons)
                Buttons[i].layer = LayerMask.NameToLayer("The Stag");
                Buttons[i].transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
                Buttons[i].transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("The Stag");
                Buttons[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
            }
            //add the sequence to a list for evaluation
            CreatedButtons.Add(Buttons[i]);
            //then set every button to "Inactive" so the sequence can satrt where it supposed to
            Buttons[i].GetComponent<QTEButton>().enabled = false;
        }
        //once all buttons are ready activate the first button on the sequence added
        CreatedButtons[0].GetComponent<QTEButton>().enabled = true;
        //set the timer
        remainingTime = GameManager.Instance.timerQTE;
        //start the timer
        startTimer = true;
        //set the index to the proper value to start
        createdBTNIndex = 0;
    }

    /// <summary>
    /// This Function is responsible for checking the status of each button on the QTE sequence, this function knows whcih button is the current "Active" button and which
    /// button follows on the sequence, it is responsible for actiating the next button on the sequence when the current one has been pressed, first it checks that the list
    /// of buttons is not empty, then it checks that the index is properly set, then it checks if the current active button has been pressed, if so then it checks if it was 
    /// a correct input, if so then it will se the proper counter, then it disables the button that was just pressed and activates the next button on the sequence and changes
    /// the index, Once the entire list has finished it checks that the index matches that of the list, and returns the index back to its starting value and removes the buttons
    /// </summary>
    public void CheckAvailability()
    {
        //check if the list has been filled with the sequence
        if (CreatedButtons.Count >= 0)
        {
            //check if the index is properly set
            if (createdBTNIndex >= 0)
            {
                //check to see if the current active button has been pressed
                if (CreatedButtons[createdBTNIndex].GetComponent<QTEButton>().pressed)
                {
                    //check if the input was correct
                    if (CreatedButtons[createdBTNIndex].GetComponent<QTEButton>().correctPress)
                    {
                        //mark it in the counter as a correct input
                        QTECounter = QTECounter + 1;
                    }
                    //make the current button inactive
                    CreatedButtons[createdBTNIndex].GetComponent<QTEButton>().enabled = false;
                    //up the index to the next button
                    createdBTNIndex += 1;
                    //check if the list is over
                    if (createdBTNIndex <= CreatedButtons.Count - 1)
                    {
                        //if the list is not over then set the next button to active
                        CreatedButtons[createdBTNIndex].GetComponent<QTEButton>().enabled = true;
                    }
                }
            }
        }

        //if the list is completed and the index matches that check
        if (CreatedButtons.Count == createdBTNIndex)
        {
            //set the index back to default
            createdBTNIndex = -1;
            //remove the buttons since the QTE Sequence has been completed
            //StartCoroutine(DisableQTEButtons());
        }

    }

    /// <summary>
    /// This function turns the correct amount of QTE inputs of the entire QTE Sequence into a percentage so that we can use it for card results
    /// </summary>
    public void EvauateQTEResults()
    {
        //check if the sequence exists
        if (CreatedButtons.Count > 0)
        {
            //convert the values of the completed sequence into a percentage of 100%
            QTEPercent = QTECounter * 100 / CreatedButtons.Count;
            //debug check to see in what state the result ends with, below 50%, above 50% and 100%
            if (QTEPercent < 50)
            {
                qteCheckPercent.text = "Below 50%";
            }
            else if (QTEPercent >= 50 && QTEPercent < 99)
            {
                qteCheckPercent.text = "Above 50%";
            }
            else if (QTEPercent == 100)
            {
                qteCheckPercent.text = "100%";
            }
        }
    }

    /// <summary>
    /// This function runs only when the QTE sequence needs to be removed becuase the timer reached 0
    /// </summary>
    public void DisableQTEButtonsTimer()
    {
        //go through each button of the QTE Sequence and turn it back to the defualt state for future use
        foreach (GameObject button in CreatedButtons)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            button.GetComponent<QTEButton>().pressed = false;
            button.SetActive(false);
        }
        //set the timer to 0 
        remainingTime = 0;
        //stop the timer from going
        startTimer = false;
        //clear the buttons from the sequence
        CreatedButtons.Clear();
        //set the index back to default
        createdBTNIndex = -1;
    }


    /// <summary>
    /// this coroutine runs when the player succesfully finished the QTE Sequence and the buttons need to dissapear, will run regardless of how many he got correct
    /// </summary>
    IEnumerator DisableQTEButtons()
    {
        //wait 2 seconds for debug and maybe even for later so you can see your results
        yield return new WaitForSeconds(2);
        //go through each button of the QTE Sequence and turn it back to the defualt state for future use
        foreach (GameObject button in CreatedButtons)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            button.GetComponent<QTEButton>().pressed = false;
            button.SetActive(false);
        }
        //set the timer to 0 
        remainingTime = 0;
        //stop the timer from going
        startTimer = false;
        //clear the buttons from the sequence
        CreatedButtons.Clear();
        //set the index back to default
        createdBTNIndex = -1;

    }
}
