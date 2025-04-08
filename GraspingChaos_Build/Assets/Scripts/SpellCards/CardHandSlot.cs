using System.Collections.Generic;
using UnityEngine;


//----------------------------------------------------------------
//  Author:       Keller
//  Contributors: Sebastian
//  Title:        CardHandSlot
//  Date Created: 01/07/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Keep track of the index of selected card to smooth navigation of the controller.
/// </summary>
public class CardHandSlot : MonoBehaviour
{
    private PlayerManager player;
    private PlayerState playerState;

    [SerializeField] private GameObject cardPrefab;
    public Transform cardSpawnTransform;

    [Tooltip("List of all the card objects for the player's Spell Hand.")]
    public List<CardSelect> cards = new List<CardSelect>();         // List to keep track of cards in the hand.
    [Tooltip("List for cards when they are selected. Keep EMPTY in editor.")]
    public List<CardSelect> selectedCards = new List<CardSelect>(); // List for selected cards.
    [Tooltip("Slot positions for the cards.")]
    public Transform[] cardSlots;                                   // Slot positions for each card.
    [Tooltip("Determines if a slot is empty. TRUE in editor.")]
    public bool[] emptySlots;                                       // Bool for the slot position being empty.

    public SpellCard whatCard;

    public int currentHoverIndex = 0;                              // Index of the currently hovered card.
    private readonly int maxSelectedCards = 3;                      // Maximum number of cards that can be selected.

    private InputHandler playerInput;
    private bool cardNavPressed = false;

    private CameraPositionChange camPosScript;
    private bool finalPressed = false;

    public CardsObjectPool ALLCards;

    public List<CardSelect> RemovingCards = new List<CardSelect>();

    private void Start()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();
        playerInput = gameObject.GetComponentInParent<InputHandler>();
        camPosScript = player.gameObject.GetComponentInChildren<CameraPositionChange>();
        playerState = player.gameObject.GetComponent<PlayerState>();
    }

    public void Update()
    {
        foreach (CardSelect card in cards)
        {
            if (card.hasAFingerBonus)
            {
                card.FingerBonusCardStatus(player, SpellNames.Rockthrow, PlayerFingers.LH_Index, PlayerFingers.RH_Index);
                card.FingerBonusCardStatus(player, SpellNames.Icicles, PlayerFingers.LH_Pinky, PlayerFingers.LH_Pinky);
                card.FingerBonusCardStatus(player, SpellNames.TidalWave, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb);
                card.FingerBonusCardStatus(player, SpellNames.StaticBlast, PlayerFingers.LH_Middle, PlayerFingers.RH_Middle);
                card.FingerBonusCardStatus(player, SpellNames.PointerOfDeath, PlayerFingers.LH_Index, PlayerFingers.RH_Index);
                card.FingerBonusCardStatus(player, SpellNames.ThumbsUp, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb);
                card.FingerBonusCardStatus(player, SpellNames.CursedConversion, PlayerFingers.LH_Ring, PlayerFingers.RH_Ring);
                card.FingerBonusCardStatus(player, SpellNames.GreenThumb, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb);
                card.FingerBonusCardStatus(player, SpellNames.Materialise, PlayerFingers.LH_Pinky, PlayerFingers.RH_Pinky);
            }
        }
    }

    //stat of removing card
    public void ClearSelected(PlayerManager player)
    {
        //NEED TO make sure we dont deal more than we have the size for in the randomizer dealer
        if (selectedCards.Count != 0)
        {
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                foreach (CardSelect selectedCard in selectedCards)
                {
                    if (cards[i] == selectedCard)
                    {
                        RemovingCards.Add(cards[i]);
                        if (player == GameManager.Instance.player1)
                        {
                            CardsObjectPool.Instance.cardsCurrentlyInHandP1.Remove(cards[i].gameObject);
                        }
                        else if (player == GameManager.Instance.player2)
                        {
                            CardsObjectPool.Instance.cardsCurrentlyInHandP2.Remove(cards[i].gameObject);
                        }
                        selectedCard.cardDissolve.dissolveCard = true;
                        selectedCard.cardDissolve.DissolveSpellCard();
                        cards.Remove(cards[i]);
                        emptySlots[i] = true;
                        break;
                    }
                }
            }
        }
    }

    // sebastian made this
    public void FullRemove()
    {
        foreach (CardSelect cardSelect in selectedCards)
        {
            if (cardSelect.GetComponent<SpellCard>().type == SpellType.ATTACK)
            {
                player.attackCardAmount--;
            }
            else if (cardSelect.GetComponent<SpellCard>().type == SpellType.RESTORATION)
            {
                player.restCardAmount--;
            }
            else if (cardSelect.GetComponent<SpellCard>().type == SpellType.RING)
            {
                player.ringCardAmount--;
            }
        }

        if (selectedCards.Count != 0)
        {
            for (int i = CardsObjectPool.Instance.cardsCurrentlyInHand.Count - 1; i >= 0; i--) //foreach (GameObject card in ALLCards.cardsCurrentlyInHand)
            {
                foreach (CardSelect cardSelect in selectedCards)
                {
                    GameObject justChecking = cardSelect.gameObject;
                    if (justChecking == CardsObjectPool.Instance.cardsCurrentlyInHand[i])
                    {
                        CardsObjectPool.Instance.allcardAmounts[(int)cardSelect.gameObject.GetComponent<SpellCard>().spellName] -= 1;
                        CardsObjectPool.Instance.cardsCurrentlyInHand.Remove(CardsObjectPool.Instance.cardsCurrentlyInHand[i]);
                        break;
                    }
                }
            }
            selectedCards.Clear();
        }
    }

    public void KeepCardPos()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = cardSlots[i].transform.position;
            emptySlots[i] = false;
            cards[i].isHovered = false;

            // move the cards back to the card slots
            cards[i].gameObject.transform.Rotate(115f, 0f, 0f);
            cards[i].GetComponent<CardTravelHandler>().CardTravel(0.05f, 0.7f, 0.7f, cards[i].transform, cardSlots[i].transform);
        }

        //emptySlots[4] = true;

        if (RemovingCards.Count != 0)
        {

            if (RemovingCards.Count == 1)
            {
                emptySlots[4] = true;
            }
            else if (RemovingCards.Count == 2)
            {
                emptySlots[3] = true;
                emptySlots[4] = true;
            }
            else if (RemovingCards.Count == 3)
            {
                emptySlots[2] = true;
                emptySlots[3] = true;
                emptySlots[4] = true;
            }
            RemovingCards.Clear();
        }
    }

    public void KeepCardPosInpect()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != cards[currentHoverIndex])
            {
                cards[i].transform.position = cardSlots[i].transform.position;
                emptySlots[i] = false;
                cards[i].isHovered = false;

                // move the cards back to the card slots
                cards[i].gameObject.transform.Rotate(115f, 0f, 0f);
                cards[i].GetComponent<CardTravelHandler>().CardTravel(0.00f, 0.7f, 0.7f, cards[i].transform, cardSlots[i].transform);
            }
        }
    }

    /// <summary>
    /// Sets the hovered card based on the provided index.
    /// </summary>
    /// <param name="cardIndex">Index of the card to hover.</param>
    public void SetHoveredCard(int cardIndex)
    {
        // Remove hover effect from the previously hovered card if it's not selected.
        if (currentHoverIndex >= 0 && currentHoverIndex < cards.Count && !cards[currentHoverIndex].isSelected)
        {
            cards[currentHoverIndex].OffHoverCard(player, false);
            cards[currentHoverIndex].CardGlow(false);
        }

        // Update the current hover index.
        currentHoverIndex = cardIndex;

        // Apply hover effect to the new card if it's not selected.
        if (currentHoverIndex >= 0 && currentHoverIndex < cards.Count && !cards[currentHoverIndex].isSelected)
        {
            cards[currentHoverIndex].OnHoverCard(player);
            cards[currentHoverIndex].CardGlow(true);
        }
    }

    /// <summary>
    /// Moves the hover to the next or previous card.
    /// </summary>
    /// <param name="selectDirection">Direction to move through the hand of cards (-1 for left, 1 for right).</param>
    public void MoveSelection(int selectDirection)
    {
        int newCardIndex = Mathf.Clamp(currentHoverIndex + selectDirection, 0, cards.Count - 1);

        if (newCardIndex != currentHoverIndex)
        {
            while (newCardIndex >= 0 && newCardIndex <= 4 && cards[newCardIndex].isSelected == true)
            {
                if (selectDirection == 1) // right direction
                {
                    newCardIndex++;
                }
                else // left direction
                {
                    newCardIndex--;
                }
            }
            if (newCardIndex < 0 || newCardIndex > 4)
            {
                newCardIndex = currentHoverIndex;
            }
            SetHoveredCard(newCardIndex);
        }
    }

    /// <summary>
    /// Toggles the selected state of the currently hovered card.
    /// </summary>
    public void SelectedCard()
    {
        if (currentHoverIndex < 0 || currentHoverIndex >= cards.Count)
        {
            return;
        }

        CardSelect card = cards[currentHoverIndex];

        whatCard = card.gameObject.GetComponent<SpellCard>();

        if (selectedCards.Contains(card))
        {
            card.alreadySelected = true;
        }
        else if (selectedCards.Count < maxSelectedCards)
        {
            playerState.currentSpellName = whatCard.spellName;

            // Select the card if it's not already selected and the max selection limit hasn't been reached.
            if (ActiveSpellCards.Instance.spellCards[(int)whatCard.spellName].manaCost <= player.Mana)
            {
                selectedCards.Add(card);
                card.SelectCard();
                card.OffHoverCard(player, false);
                if (whatCard.spellName != SpellNames.Icicles && whatCard.spellName != SpellNames.LefteousEnvy && whatCard.spellName != SpellNames.RighteousEnvy &&
               whatCard.spellName != SpellNames.LifeDrain && whatCard.spellName != SpellNames.ThumbsUp && whatCard.spellName != SpellNames.EchoingMana &&
               whatCard.spellName != SpellNames.CursedConversion)
                {
                    player.playerInput.SwitchCurrentActionMap("Player");
                    playerState.playerControlHandler.pickFinger.UpdateSelection();
                }
                //HoverIndexFix();
            }
        }
    }


    public void DeselectCard()
    {
        if (currentHoverIndex < 0 || currentHoverIndex >= cards.Count)
        {
            return;
        }

        CardSelect card = selectedCards[selectedCards.Count - 1];

        whatCard = card.gameObject.GetComponent<SpellCard>();

        if (selectedCards.Contains(card))
        {
            //playerState.currentSpellName = SpellNames.none;
            // Deselect the card if it's already selected.
            //selectedCards.Remove(card);
            //if (card.hasAFingerBonus)
            //{
            //    card.fingerBonus.SetActive(false);
            //}
            card.DeselectCard();
        }
    }

    public bool checkSelectedCardStatus()
    {
        CardSelect card = cards[currentHoverIndex];
        if (card.alreadySelected)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool checkDeselectedCardStatus()
    {
        CardSelect card = cards[currentHoverIndex];
        if (selectedCards.Contains(card))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void HoverIndexFix()
    {
        if ((currentHoverIndex + 1) < cards.Count && currentHoverIndex > 0)
        {
            if (!cards[currentHoverIndex + 1].isSelected)
            {
                currentHoverIndex = currentHoverIndex + 1;
                SetHoveredCard(currentHoverIndex);
            }
            else if (!cards[currentHoverIndex - 1].isSelected)
            {
                currentHoverIndex = currentHoverIndex - 1;
                SetHoveredCard(currentHoverIndex);
            }
            else
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (!cards[i].isSelected)
                    {
                        currentHoverIndex = i;
                        SetHoveredCard(i);
                        break;
                    }
                }
            }
        }
        else if (currentHoverIndex == cards.Count - 1)
        {
            if (!cards[currentHoverIndex - 1].isSelected)
            {
                currentHoverIndex = currentHoverIndex - 1;
                SetHoveredCard(currentHoverIndex);
            }
            else
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (!cards[i].isSelected)
                    {
                        currentHoverIndex = i;
                        SetHoveredCard(i);
                        break;
                    }
                }
            }
        }
        else if (currentHoverIndex == 0)
        {
            if (!cards[currentHoverIndex + 1].isSelected)
            {
                currentHoverIndex = currentHoverIndex + 1;
                SetHoveredCard(currentHoverIndex);
            }
            else
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (!cards[i].isSelected)
                    {
                        currentHoverIndex = i;
                        SetHoveredCard(i);
                        break;
                    }
                }
            }
        }

    }
}
