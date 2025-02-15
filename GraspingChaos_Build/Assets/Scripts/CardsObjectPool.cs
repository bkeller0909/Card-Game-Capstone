using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------
//  Author:       Sebastian
//  Co-Auther:    Keller
//  Title:        CardsObjectPool
//  Date Created: 02/09/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Initializes the object pool for the cards in the scene.
/// Rotates the cards to their respective player.
/// </summary>
public class CardsObjectPool : MonoBehaviour
{
    private CardDealing getCard;
    private CardHandSlot cardSlots;
    float p1rotation = 90;
    float p2rotation = -90;
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

    private void Awake()
    {
        getCard = new CardDealing();
    }

    private void Start()
    {
        allcardAmounts = new int[ActiveSpellCards.Instance.AmtOfCards];
        for (int i = 0; i < allcardAmounts.Length; i++)
        {
            allcardAmounts[i] = 0;
        }
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
                }
                else if (player == GameManager.Instance.player2)
                {
                    pooledCards.transform.eulerAngles = new Vector3(rightBend, p2rotation, transform.eulerAngles.z);
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
                        break;
                    }
                }
                break;
            }
        }
    }
}
