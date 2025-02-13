using System.Collections.Generic;
using UnityEngine;

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
                        break;
                    }
                }
                break;
            }
        }
    }


    /*public void InitializeCards()
    {
        for (int i = 0; i < emptySlots.Length; i++)
        {
            if (emptySlots[i])
            {
                GameObject card = Instantiate(cardPrefab, cardSlots[i].position, Quaternion.identity);
                cards.Add(card.GetComponent<CardSelect>());
                emptySlots[i] = false;
            }
        }
    }*/
}
