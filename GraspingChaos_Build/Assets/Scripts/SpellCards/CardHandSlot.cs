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
            ToggleSelectedCard();
            RumbleManager.instance.ControllerRumble(0.25f, 0.5f, 0.25f, player.gamepad);
        }

        // Handle pause input.
        if (player.playerInput.actions["Pause"].triggered)
        {
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
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
    public void ToggleSelectedCard()
    {
        if (currentHoverIndex < 0 || currentHoverIndex >= cards.Count)
        {
            return;
        }

        CardSelect card = cards[currentHoverIndex];

        whatCard = card.gameObject.GetComponent<SpellCard>();

        if (selectedCards.Contains(card))
        {
            playerState.currentSpellName = SpellNames.none;

            // Deselect the card if it's already selected.
            selectedCards.Remove(card);
            card.DeselectCard();
            card.OnHoverCard(); // Reapply hover effect after deselecting.
        }
        else if (selectedCards.Count < maxSelectedCards)
        {
            playerState.currentSpellName = whatCard.spellName;

            // Select the card if it's not already selected and the max selection limit hasn't been reached.
            selectedCards.Add(card);
            card.SelectCard();
            playerInput.gameObject.GetComponentInChildren<CameraPositionChange>().GetInputForced(2);
        }
    }
}
