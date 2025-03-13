using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//----------------------------------------------------------------
//  Author:       Sebastian
//  Co-Auther:    Keller
//  Title:        CardsObjectPool
//  Date Created: 02/09/2025
//  Instance:     yes
//-----------------------------------------------------------------

/// <summary>
/// Initializes the object pool for the cards in the scene.
/// Rotates the cards to their respective player.
/// </summary>
public class CardsObjectPool : MonoBehaviour
{

    private static CardsObjectPool instance;
    public static CardsObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardsObjectPool>();
            }

            if (!instance)
            {
                Debug.LogError("No Game Manager Present !!!");
            }

            return instance;

        }
    }

    private CardDealing getCard;
    private CardHandSlot cardSlots;
    float p1rotation = -180;
    float p2rotation = 0;
    float rightBend = 24.291f;
    public int[] allcardAmounts;

    public List<GameObject> objPoolCards = new List<GameObject>();

    public List<GameObject> cardsCurrentlyInHand = new List<GameObject>();

    //needed for the game Debugger script - important for testing complex scenarios
    public List<GameObject> cardsCurrentlyInHandP1 = new List<GameObject>();
    //needed for the game Debugger script - important for testing complex scenarios
    public List<GameObject> cardsCurrentlyInHandP2 = new List<GameObject>();

    // to return a gameobject that is the proper card that returns from the dealing.
    // based on the chance to be dealt the card

    // move the card to the proper card hand slot position for the specified player
    // fill the card lost of CardHandSlot
    // Turn empty to false
    Scene currentScene;
    private string sceneCheckName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        getCard = new CardDealing();
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(this);
    }

    private void Start()
    {
        allcardAmounts = new int[ActiveSpellCards.Instance.AmtOfCards];
        ResetPoolDistributionValues();
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneCheckName = currentScene.name;

        if (sceneCheckName == GameManager.Instance.ln_MainMenuName)
        {
            foreach (GameObject pooledCards in objPoolCards)
            {
                if (pooledCards.activeSelf)
                {
                    pooledCards.SetActive(false);
                }
            }
        }
    }

    public void ResetPoolDistributionValues()
    {
        for (int i = 0; i < allcardAmounts.Length; i++)
        {
            allcardAmounts[i] = 0;
        }
    }

    public SpellCard FireBoltRound1(PlayerManager player)
    {
        cardSlots = player.GetComponentInChildren<CardHandSlot>();
        foreach (GameObject pooledCards in objPoolCards)
        {
            if (pooledCards.GetComponent<SpellCard>().spellName == SpellNames.FireBolt && !pooledCards.activeSelf)
            {
                allcardAmounts[(int)SpellNames.FireBolt] += 1;
                if (player == GameManager.Instance.player1)
                {
                    pooledCards.transform.eulerAngles = new Vector3(rightBend, p1rotation, transform.eulerAngles.z);
                    //7 is layer skull
                    pooledCards.transform.GetChild(0).gameObject.layer = 7;
                }
                else if (player == GameManager.Instance.player2)
                {
                    pooledCards.transform.eulerAngles = new Vector3(rightBend, p2rotation, transform.eulerAngles.z);
                    //6 is layer stag
                    pooledCards.transform.GetChild(0).gameObject.layer = 6;
                }
                pooledCards.SetActive(true);
                for (int i = 0; i < cardSlots.emptySlots.Length; i++)
                {
                    if (cardSlots.emptySlots[i])
                    {
                        pooledCards.transform.position = cardSlots.cardSlots[i].transform.position;
                        cardSlots.cards.Add(pooledCards.GetComponent<CardSelect>());
                        cardSlots.emptySlots[i] = false;
                        cardsCurrentlyInHand.Add(pooledCards);
                        if (player == GameManager.Instance.player1)
                        {
                            cardsCurrentlyInHandP1.Add(pooledCards);
                        }
                        else if (player == GameManager.Instance.player2)
                        {
                            cardsCurrentlyInHandP2.Add(pooledCards);
                        }
                        return pooledCards.GetComponent<SpellCard>();
                    }
                }
                break;
            }
        }
        return null;
    }


    public SpellCard ScriptedDealing(PlayerManager player, SpellNames spell1, SpellNames spell2, SpellNames spell3, SpellNames spell4, SpellNames spell5)
    {
        cardSlots = player.GetComponentInChildren<CardHandSlot>();
        foreach (GameObject pooledCards in objPoolCards)
        {
            if (pooledCards.GetComponent<SpellCard>().spellName == spell1 || pooledCards.GetComponent<SpellCard>().spellName == spell2 ||
                pooledCards.GetComponent<SpellCard>().spellName == spell3 || pooledCards.GetComponent<SpellCard>().spellName == spell4 ||
                pooledCards.GetComponent<SpellCard>().spellName == spell5 && !pooledCards.activeSelf)
            {
                SpellCard card = pooledCards.GetComponent<SpellCard>();
                allcardAmounts[(int)card.spellName] += 1;
                if (card.type == SpellType.ATTACK)
                {
                    player.attackCardAmount++;
                }
                else if (card.type == SpellType.RESTORATION)
                {
                    player.restCardAmount++;
                }
                else if (card.type == SpellType.RING)
                {
                    player.ringCardAmount++;
                }

                if (player == GameManager.Instance.player1)
                {
                    pooledCards.transform.eulerAngles = new Vector3(rightBend, p1rotation, transform.eulerAngles.z);
                    //7 is layer skull
                    pooledCards.transform.GetChild(0).gameObject.layer = 7;
                }
                else if (player == GameManager.Instance.player2)
                {
                    pooledCards.transform.eulerAngles = new Vector3(rightBend, p2rotation, transform.eulerAngles.z);
                    //6 is layer stag
                    pooledCards.transform.GetChild(0).gameObject.layer = 6;
                }
                pooledCards.SetActive(true);
                for (int i = 0; i < cardSlots.emptySlots.Length; i++)
                {
                    if (cardSlots.emptySlots[i])
                    {
                        pooledCards.transform.position = cardSlots.cardSlots[i].transform.position;
                        cardSlots.cards.Add(pooledCards.GetComponent<CardSelect>());
                        cardSlots.emptySlots[i] = false;
                        cardsCurrentlyInHand.Add(pooledCards);
                        if (player == GameManager.Instance.player1)
                        {
                            cardsCurrentlyInHandP1.Add(pooledCards);
                        }
                        else if (player == GameManager.Instance.player2)
                        {
                            cardsCurrentlyInHandP2.Add(pooledCards);
                        }
                        return pooledCards.GetComponent<SpellCard>();
                    }
                }
                break;
            }
        }
        return null;
    }


    public void SetCardsFromPool(PlayerManager player, SpellCard card)
    {
        cardSlots = player.GetComponentInChildren<CardHandSlot>();

        foreach (GameObject pooledCards in objPoolCards)
        {
            if (pooledCards.GetComponent<SpellCard>().spellName == card.spellName && !pooledCards.activeSelf)//card.spellName)
            {
                allcardAmounts[(int)card.spellName] += 1;
                if (player == GameManager.Instance.player1)
                {
                    pooledCards.transform.eulerAngles = new Vector3(rightBend, p1rotation, transform.eulerAngles.z);
                    //7 is layer skull
                    pooledCards.transform.GetChild(0).gameObject.layer = 7;
                }
                else if (player == GameManager.Instance.player2)
                {
                    pooledCards.transform.eulerAngles = new Vector3(rightBend, p2rotation, transform.eulerAngles.z);
                    //6 is layer stag
                    pooledCards.transform.GetChild(0).gameObject.layer = 6;
                }
                pooledCards.SetActive(true);
                for (int i = 0; i < cardSlots.emptySlots.Length; i++)
                {
                    if (cardSlots.emptySlots[i])
                    {
                        // pooledCards.transform.position = cardSlots.cardSlots[i].transform.position;
                        cardSlots.cards.Add(pooledCards.GetComponent<CardSelect>());
                        cardSlots.emptySlots[i] = false;
                        cardsCurrentlyInHand.Add(pooledCards);
                        pooledCards.GetComponent<CardTravelHandler>().CardTravel(0.2f, 5f, pooledCards.transform, cardSlots.cardSlots[i].transform);

                        if (player == GameManager.Instance.player1)
                        {
                            cardsCurrentlyInHandP1.Add(pooledCards);
                        }
                        else if (player == GameManager.Instance.player2)
                        {
                            cardsCurrentlyInHandP2.Add(pooledCards);
                        }
                        break;
                    }
                }
                break;
            }
        }
    }
}
