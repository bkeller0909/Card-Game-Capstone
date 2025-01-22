using System.Collections.Generic;
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
    public List<GameObject> CreatedButtons = new List<GameObject>();
    public GameObject firstPosition;
    private Vector3 space = new Vector3(2f, 0f, 0f);
    public float remainingTime;
    public bool startTimer;
    public int enumAmount;

    private void Start()
    {
        startTimer = false;
        player = GetComponent<PlayerManager>();
        remainingTime = GameManager.Instance.timerQTE;
        index = -1;
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
            }
        }

        CheckAvailability();
    }

    private GameObject RandomizeBTN()
    {
        int button = UnityEngine.Random.Range(0, Buttons.Count);
        return Buttons[button];
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
    private void AssignSprite()
    {
        if (RandoBTN == QTEButtonType.Y)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = Y;
        }
        else if (RandoBTN == QTEButtonType.X)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = X;
        }
        else if (RandoBTN == QTEButtonType.B)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = B;
        }
        else if (RandoBTN == QTEButtonType.A)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = A;
        }
        else if (RandoBTN == QTEButtonType.UP)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = DU;
        }
        else if (RandoBTN == QTEButtonType.LEFT)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = DL;
        }
        else if (RandoBTN == QTEButtonType.RIGHT)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = DR;
        }
        else if (RandoBTN == QTEButtonType.DOWN)
        {
            Buttons[0].GetComponent<SpriteRenderer>().sprite = DD;
        }
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
                if (actualObject.GetComponent<QTEButton>().dir)
                {
                    actualObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Skull");
                }
            }
            else if (caster == p2)
            {
                actualObject.layer = LayerMask.NameToLayer("The Stag");
                actualObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
                if (actualObject.GetComponent<QTEButton>().dir)
                {
                    actualObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("The Stag");
                }
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

    public void CreateV2(int qteAmount, PlayerManager caster)
    {

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
            DestroyQTEBTN();
            index = -1;
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
}
