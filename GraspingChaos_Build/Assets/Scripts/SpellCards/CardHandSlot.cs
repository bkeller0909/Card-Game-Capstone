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

    [Tooltip("List of all the card objects for the player's Spell Hand.")]
    public List<CardSelect> cards = new List<CardSelect>();         // List to keep track of cards in the hand.
    [Tooltip("List for cards when they are selected. Keep EMPTY in editor.")]
    public List<CardSelect> selectedCards = new List<CardSelect>(); // List for selected cards.
    [Tooltip("Slot positions for the cards.")]
    public Transform[] cardSlots;                                   // Slot positions for each card.
    [Tooltip("Determines if a slot is empty. TRUE in editor.")]
    public bool[] emptySlots;                                       // Bool for the slot position being empty.

    SpellCard whatCard;

    private int currentHoverIndex = 0;                              // Index of the currently hovered card.
    private readonly int maxSelectedCards = 3;                      // Maximum number of cards that can be selected.

    private InputHandler playerInput;
    private bool cardNavPressed = false;

    private CameraPositionChange camPosScript;
    private bool finalPressed = false;

    public CardsObjectPool ALLCards;

    public List<CardSelect> RemovingCards = new List<CardSelect>();

    private void Awake()
    {
        if (cards.Count <= 0)
        {
            //InitializeCards();
        }
    }

    private void Start()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();
        playerInput = gameObject.GetComponentInParent<InputHandler>();
        camPosScript = player.gameObject.GetComponentInChildren<CameraPositionChange>();
        playerState = player.gameObject.GetComponent<PlayerState>();

        if (cards.Count > 0)
        {
            //SetHoveredCard(currentHoverIndex); // Initialize hover on the first card.

        }
    }

    private void Update()
    {
        //CardControls();
    }

    /*IEnumerator QTECountOne()
    {
        player.playerInput.SwitchCurrentActionMap("QTE");
        playerInput.Xbtn = false;
        yield return new WaitForSeconds(1f);
        playerInput.gameObject.GetComponent<QTEHandler>().Create(8, player);
        yield return new WaitForSeconds(GameManager.Instance.timerQTE + 0.5f);
        playerInput.gameObject.GetComponent<QTEHandler>().resetLoop = true;
    }

    IEnumerator QTECountTwo()
    {
        player.playerInput.SwitchCurrentActionMap("QTE");
        playerInput.Xbtn = false;
        yield return new WaitForSeconds(1f);
        playerInput.gameObject.GetComponent<QTEHandler>().Create(12, player);
        yield return new WaitForSeconds(GameManager.Instance.timerQTE + 1);
        playerInput.gameObject.GetComponent<QTEHandler>().Create(5, player);
        yield return new WaitForSeconds(GameManager.Instance.timerQTE + 0.5f);
        playerInput.gameObject.GetComponent<QTEHandler>().resetLoop = true;
    }

    IEnumerator QTECountThree()
    {
        player.playerInput.SwitchCurrentActionMap("QTE");
        playerInput.Xbtn = false;
        yield return new WaitForSeconds(1f);
        playerInput.gameObject.GetComponent<QTEHandler>().Create(4, player);
        yield return new WaitForSeconds(GameManager.Instance.timerQTE + 1);
        playerInput.gameObject.GetComponent<QTEHandler>().Create(8, player);
        yield return new WaitForSeconds(GameManager.Instance.timerQTE + 1);
        playerInput.gameObject.GetComponent<QTEHandler>().Create(12, player);
        yield return new WaitForSeconds(GameManager.Instance.timerQTE + 0.5f);
        playerInput.gameObject.GetComponent<QTEHandler>().resetLoop = true;
    }*/




    public void InitializeCards()
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
    }

    private void CardControls()
    {
        //Handle card navigation(left / right).
        if (playerInput.cardMoveRight && !cardNavPressed)
        {
            cardNavPressed = true;
            MoveSelection(1);
        }
        else if (playerInput.cardMoveLeft && !cardNavPressed)
        {
            cardNavPressed = true;
            MoveSelection(-1);
        }
        else if (!playerInput.cardMoveRight && !playerInput.cardMoveLeft)
        {
            cardNavPressed = false;
        }

        //Handle final selection(e.g., confirm selected cards).
        if (playerInput.finishSelection && !finalPressed)
        {
            //playerInput.gameObject.GetComponentInChildren<CameraPositionChange>().GetInputForced(0);
            //finalPressed = true;
            //if (selectedCards.Count == 1)
            //{
            //    playerInput.finishSelection = false;
            //    playerInput.selectCard = false;
            //    //StartCoroutine(QTECountOne());
            //}
            //else if (selectedCards.Count == 2)
            //{
            //    playerInput.finishSelection = false;
            //    playerInput.selectCard = false;
            //    //StartCoroutine(QTECountTwo());
            //}
            //else if (selectedCards.Count == 3)
            //{
            //    playerInput.finishSelection = false;
            //    playerInput.selectCard = false;
            //    //StartCoroutine(QTECountThree());
            //}
            finalPressed = true;
        }
        else if (!playerInput.finishSelection)
        {
            finalPressed = false;
        }

        //Handle card selection / deselection.
        if (player.playerInput.actions["Select"].triggered)
        {
            SelectedCard();
            RumbleManager.Instance.ControllerRumble(0.25f, 0.5f, 0.25f, player.gamepad);
        }

        // Handle pause input.
        if (player.playerInput.actions["Pause"].triggered)
        {
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
        }
    }


    //stat of removing card
    public void ClearSelected()
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
                        cards.Remove(cards[i]);
                        emptySlots[i] = true;
                        break;
                    }
                }
            }

            //foreach(CardSelect selectedCard in selectedCards)
            //{
            //    foreach(CardSelect card in cards)
            //    {
            //        if(card == selectedCard)
            //        {
            //            cards.Remove(selectedCard);
            //        }
            //    }
            //}

        }
    }

    // sebastian made this
    public void FullRemove()
    {
        if (selectedCards.Count != 0)
        {
            for (int i = CardsObjectPool.Instance.cardsCurrentlyInHand.Count - 1; i >= 0; i--) //foreach (GameObject card in ALLCards.cardsCurrentlyInHand)
            {
                foreach (CardSelect cardSelect in selectedCards)
                {
                    // remove the card from your hand once you have used that card of the certain type
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
                    GameObject justChecking = cardSelect.gameObject;
                    if (justChecking == CardsObjectPool.Instance.cardsCurrentlyInHand[i])
                    {
                        CardsObjectPool.Instance.cardsCurrentlyInHand[i].SetActive(false);
                        CardsObjectPool.Instance.allcardAmounts[(int)cardSelect.gameObject.GetComponent<SpellCard>().spellName] -= 1;
                        CardsObjectPool.Instance.cardsCurrentlyInHand[i].GetComponent<CardSelect>().isSelected = false;
                        CardsObjectPool.Instance.cardsCurrentlyInHand[i].GetComponent<CardSelect>().isHovered = false;
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

    /// <summary>
    /// Sets the hovered card based on the provided index.
    /// </summary>
    /// <param name="cardIndex">Index of the card to hover.</param>
    public void SetHoveredCard(int cardIndex)
    {
        // Remove hover effect from the previously hovered card if it's not selected.
        if (currentHoverIndex >= 0 && currentHoverIndex < cards.Count && !cards[currentHoverIndex].isSelected)
        {
            cards[currentHoverIndex].OffHoverCard();
        }

        // Update the current hover index.
        currentHoverIndex = cardIndex;

        // Apply hover effect to the new card if it's not selected.
        if (currentHoverIndex >= 0 && currentHoverIndex < cards.Count && !cards[currentHoverIndex].isSelected)
        {
            cards[currentHoverIndex].OnHoverCard();
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
            selectedCards.Add(card);
            card.SelectCard();
            player.playerInput.SwitchCurrentActionMap("Player");
        }
    }

    public void DeselectCard()
    {
        if (currentHoverIndex < 0 || currentHoverIndex >= cards.Count)
        {
            return;
        }

        CardSelect card = cards[currentHoverIndex];

        whatCard = card.gameObject.GetComponent<SpellCard>();

        if (selectedCards.Contains(card))
        {
            SetCurrentDeselectedCard();
            card.alreadySelected = false;
            playerState.currentSpellName = SpellNames.none;

            // Deselect the card if it's already selected.
            selectedCards.Remove(card);
            card.DeselectCard();
            card.OnHoverCard(); // Reapply hover effect after deselecting.
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

    public void SetCurrentDeselectedCard()
    {
        SpellCard card = cards[currentHoverIndex].gameObject.GetComponent<SpellCard>();
        foreach (CardSelect cardsSelected in selectedCards)
        {
            cardsSelected.gameObject.GetComponent<SpellCard>().currentOrderValue = 0;
        }

        foreach (CardSelect cardsSelected in selectedCards)
        {
            cardsSelected.gameObject.GetComponent<SpellCard>().currentOrderValue++;
        }

        card.currentOrderValue = GameManager.Instance.currentDeselectedCard;
    }
}
