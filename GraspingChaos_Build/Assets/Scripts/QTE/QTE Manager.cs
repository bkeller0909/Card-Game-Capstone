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
//  Instance?     YES
//-----------------------------------------------------------------
/// </summary>
public class QTEManager : MonoBehaviour
{
    private static QTEManager instance;
    public static QTEManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QTEManager>();
            }
            return instance;
        }
    }

    public int index;
    public QTEButtonType RandoBTN;
    public Sprite A, B, Y, X, DU, DL, DR, DD;
    public PlayerManager player;
    public PlayerManager p1, p2; //temp
    public List<GameObject> Buttons = new List<GameObject>();
    public List<GameObject> PreviuosButtons = new List<GameObject>();
    public List<GameObject> CreatedButtons = new List<GameObject>();
    public Vector3 dirScale = new Vector3(0.1278096f, 0.1278096f, 0.1278096f);
    public GameObject firstPosition;
    private Vector3 space = new Vector3(2f, 0f, 0f);
    public float remainingTime;
    public bool startTimer;
    public int enumAmount;
    public int QTECounter;
    public TMP_Text qteCheck;
    public TMP_Text qteCheckPercent;
    public int intpercent;

    //everything bellow 50% is low, everything below 100% is mid, everyting 100% is full
    //everything bellow 50% is low, above 50% is mid (completed if there is only 2 options)

    private void Start()
    {
        startTimer = false;
        player = GetComponent<PlayerManager>();
        remainingTime = GameManager.Instance.timerQTE;
        index = -1;
        QTECounter = 0;
    }

    //private void OnEnable()
    //{
    //    player.playerInput.actions["SwapModes"].performed += SwitchActionMap;
    //}

    //private void SwitchActionMap(InputAction.CallbackContext context)
    //{
    //    player.playerInput.actions.FindAction("Player").Disable();
    //    player.playerInput.actions.FindAction("QTE").Enable();
    //    checkSituation = true;
    //}


    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                startTimer = false;
                DisableQTEButtonsTimer();
            }
        }

        CheckAvailability();

        qteCheck.text = QTECounter.ToString();

        EvauateQTEResults();
    }

    private GameObject RandomizeBTN()
    {
        int button = UnityEngine.Random.Range(0, PreviuosButtons.Count);
        return PreviuosButtons[button];
    }

    private static QTEButtonType RandomizeBTNV2()
    {
        int button = UnityEngine.Random.Range(0, 8);
        for (int i = 0; i < 8; i++)
        {
            if (button == (int)QTEButtonType.Y)
            {
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

    //TAKE IN A INDEX VALUE
    private void AssignSprite(int indexBTN)
    {
        if (RandoBTN == QTEButtonType.Y)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = Y;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.Y;
        }
        else if (RandoBTN == QTEButtonType.X)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = X;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.X;
        }
        else if (RandoBTN == QTEButtonType.B)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = B;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.B;
        }
        else if (RandoBTN == QTEButtonType.A)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = A;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.A;
        }
        else if (RandoBTN == QTEButtonType.UP)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = DU;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.UpArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
        else if (RandoBTN == QTEButtonType.LEFT)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = DL;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.LeftArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
        else if (RandoBTN == QTEButtonType.RIGHT)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = DR;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.RightArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
        else if (RandoBTN == QTEButtonType.DOWN)
        {
            Buttons[indexBTN].GetComponent<SpriteRenderer>().sprite = DD;
            Buttons[indexBTN].GetComponent<QTEButton>().AssignedBTN = KeyCode.DownArrow;
            Buttons[indexBTN].GetComponent<QTEButton>().transform.localScale = dirScale;
        }
    }

    public void CreateV2(int qteAmount, PlayerManager caster)
    {
        caster.playerInput.SwitchCurrentActionMap("QTE");
        for (int i = 0; i < qteAmount; i++)
        {
            QTECounter = 0;
            RandoBTN = RandomizeBTNV2();
            AssignSprite(i);
            Buttons[i].SetActive(true);
            Buttons[i].GetComponent<QTEButton>().playerQTE = caster;
            if (caster == p1)
            {
                Buttons[i].layer = LayerMask.NameToLayer("The Skull");
                Buttons[i].transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Skull");
                Buttons[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Skull");
            }
            else if (caster == p2)
            {
                Buttons[i].layer = LayerMask.NameToLayer("The Stag");
                Buttons[i].transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
                Buttons[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
            }
            CreatedButtons.Add(Buttons[i]);
            Buttons[i].GetComponent<QTEButton>().enabled = false;
            firstPosition.transform.position += space;
        }
        CreatedButtons[0].GetComponent<QTEButton>().enabled = true;
        remainingTime = GameManager.Instance.timerQTE;
        startTimer = true;
        index = 0;
    }

    public void Create(int qteAmount, PlayerManager caster)
    {
        //remainingTime = GameManager.Instance.timerQTE;
        caster.playerInput.SwitchCurrentActionMap("QTE");
        // caster.playerInput.SwitchCurrentControlScheme("Gamepad");
        GameObject hold = new GameObject();
        firstPosition.transform.position = new Vector3(-5.21f, -1.19f, 0);
        for (int i = 0; i < qteAmount; i++)
        {
            //RandoBTN = RandomizeBTNV2();
            hold = RandomizeBTN();
            GameObject actualObject = Instantiate(hold, firstPosition.transform.position, Quaternion.identity);
            actualObject.GetComponent<QTEButton>().playerQTE = caster;
            //actualObject.GetComponent<testingQTEBTN>().player = caster;  //testing
            if (caster == p1)
            {
                actualObject.layer = LayerMask.NameToLayer("The Skull");
                actualObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Skull");
                //if (actualObject.GetComponent<QTEButton>().dir)
                //{
                //    actualObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Skull");
                //}
            }
            else if (caster == p2)
            {
                actualObject.layer = LayerMask.NameToLayer("The Stag");
                actualObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
                //if (actualObject.GetComponent<QTEButton>().dir)
                //{
                //    actualObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
                //}
            }
            CreatedButtons.Add(actualObject);
            actualObject.GetComponent<QTEButton>().enabled = false;
            firstPosition.transform.position += space;
        }
        CreatedButtons[0].GetComponent<QTEButton>().enabled = true;
        startTimer = true;
        index = 0;
        //DestroyQTEBTN();
    }

    //CHANGE FROM FOR LOOP TO SIMPLE IF CHECKS TO ENSURE THERE ARE NO ERRORS IN CHECKUP
    public void CheckAvailability()
    {
        if (CreatedButtons.Count >= 0)
        {
            if (index >= 0)
            {
                if (CreatedButtons[index].GetComponent<QTEButton>().pressed)
                {
                    if (CreatedButtons[index].GetComponent<QTEButton>().correctPress)
                    {
                        QTECounter = QTECounter + 1;
                    }
                    CreatedButtons[index].GetComponent<QTEButton>().enabled = false;
                    index += 1;
                    if (index <= CreatedButtons.Count - 1)
                    {
                        CreatedButtons[index].GetComponent<QTEButton>().enabled = true;
                    }
                }
            }
        }

        if (CreatedButtons.Count == index)
        {
            StartCoroutine(DisableQTEButtons());
        }

    }

    public void EvauateQTEResults()
    {
        intpercent = QTECounter * 100 / CreatedButtons.Count;
        if (intpercent < 50)
        {
            qteCheckPercent.text = "Below 50%";
        }
        else if (intpercent >= 50 && intpercent < 99)
        {
            qteCheckPercent.text = "Above 50%";
        }
        else if (intpercent == 100)
        {
            qteCheckPercent.text = "100%";
        }
    }

    public void DestroyQTEBTN()
    {
        for (int i = 0; i < CreatedButtons.Count; i++)
        {
            //Destroy(CreatedButtons[i], GameManager.Instance.timerQTE);
            Destroy(CreatedButtons[i]);
        }
    }

    public void DisableQTEButtonsTimer()
    {
        foreach (GameObject button in CreatedButtons)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            button.GetComponent<QTEButton>().pressed = false;
            button.SetActive(false);
        }
        remainingTime = 0;
        startTimer = false;
        CreatedButtons.Clear();
        index = -1;
    }

    IEnumerator DisableQTEButtons()
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject button in CreatedButtons)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            button.GetComponent<QTEButton>().pressed = false;
            button.SetActive(false);
        }
        remainingTime = 0;
        startTimer = false;
        CreatedButtons.Clear();
        index = -1;

    }
}
