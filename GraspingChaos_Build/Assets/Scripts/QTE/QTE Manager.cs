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

    public PlayerManager player;
    public PlayerManager p1, p2; //temp
    public List<GameObject> Buttons = new List<GameObject>();
    public List<GameObject> CreatedButtons = new List<GameObject>();
    public GameObject firstPosition;
    private Vector3 space = new Vector3(2f, 0f, 0f);
    public float remainingTime;
    public bool startTimer;
    public bool checkSituation = false;

    private void Start()
    {
        startTimer = false;
        player = GetComponent<PlayerManager>();
        remainingTime = GameManager.Instance.timerQTE;
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
            CheckAvailability();
        }

        if (checkSituation)
        {
            DestroyQTEBTN();
        }
    }

    private GameObject RandomizeBTN()
    {
        int button = Random.Range(0, Buttons.Count);
        return Buttons[button];
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
        //DestroyQTEBTN();
    }

    //CHANGE FROM FOR LOOP TO SIMPLE IF CHECKS TO ENSURE THERE ARE NO ERRORS IN CHECKUP
    public void CheckAvailability()
    {
        if (startTimer)
        {
            if (CreatedButtons[0] != null)
            {
                for (int i = 0; i < CreatedButtons.Count; i++)
                {
                    if (CreatedButtons[i].GetComponent<QTEButton>().pressed)
                    {
                        CreatedButtons[i].GetComponent<QTEButton>().enabled = false;
                        if (i != CreatedButtons.Count - 1)
                        {
                            CreatedButtons[i + 1].GetComponent<QTEButton>().enabled = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    public void DestroyQTEBTN()
    {
        for (int i = 0; i < CreatedButtons.Count; i++)
        {
            Destroy(CreatedButtons[i], GameManager.Instance.timerQTE);
        }
    }

}
