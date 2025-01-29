using System.Collections.Generic;
using UnityEngine;


//----------------------------------------------------------------
//  Author:       Keller
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
    [SerializeField] private GameObject cardPrefab;

    [Tooltip("List of all the card objects for the player's Spell Hand.")]
    public List<CardSelect> cards = new List<CardSelect>();         // List to keep track of cards in the hand.
    [Tooltip("List for cards when they are selected. Keep EMPTY in editor.")]
    public List<CardSelect> selectedCards = new List<CardSelect>(); // List for selected cards.
    [Tooltip("Slot positions for the cards.")]
    public Transform[] cardSlots;                                   // Slot positions for each card.
    [Tooltip("Determines if a slot is empty. TRUE in editor.")]
    public bool[] emptySlots;                                       // Bool for the slot position being empty.

    private int currentHoverIndex = 0;                              // Index of the currently hovered card.
    private readonly int maxSelectedCards = 3;                      // Maximum number of cards that can be selected.

    private InputHandler playerInput;
    private bool cardNavPressed = false;

    private CameraPositionChange camPosScript;
    private bool finalPressed = false;

    private void Awake()
    {
        if (cards.Count <= 0)
        {
            InitializeCards();
        }
    }

    private void Start()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();
        playerInput = gameObject.GetComponentInParent<InputHandler>();
        camPosScript = player.gameObject.GetComponentInChildren<CameraPositionChange>();

        if (cards.Count > 0)
        {
            SetHoveredCard(currentHoverIndex); // Initialize hover on the first card.
        }
    }

    private void Update()
    {
        CardControls();
    }

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
        // Handle card navigation (left/right).
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

        // Handle final selection (e.g., confirm selected cards).
        if (playerInput.finishSelection && selectedCards.Count > 0 && !finalPressed)
        {
            finalPressed = true;
            player.playerInput.SwitchCurrentActionMap("QTE");
        }
        else if (!playerInput.finishSelection)
        {
            finalPressed = false;
        }

        // Handle card selection/deselection.
        if (player.playerInput.actions["Select"].triggered)
        {
            ToggleSelectedCard();
            player.playerInput.SwitchCurrentActionMap("Player");
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
        // Remove hover effect from the previously hovered card.
        if (currentHoverIndex >= 0 && currentHoverIndex < cards.Count)
        {
            cards[currentHoverIndex].OffHoverCard();
        }

        // Update the current hover index.
        currentHoverIndex = cardIndex;

        // Apply hover effect to the new card.
        if (currentHoverIndex >= 0 && currentHoverIndex < cards.Count)
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
    private void ToggleSelectedCard()
    {
        if (currentHoverIndex < 0 || currentHoverIndex >= cards.Count)
        {
            return;
        }

        CardSelect card = cards[currentHoverIndex];

        if (selectedCards.Contains(card))
        {
            // Deselect the card if it's already selected.
            selectedCards.Remove(card);
            card.DeselectCard();
            card.OnHoverCard(); // Reapply hover effect after deselecting.
        }
        else if (selectedCards.Count < maxSelectedCards)
        {
            // Select the card if it's not already selected and the max selection limit hasn't been reached.
            selectedCards.Add(card);
            card.SelectCard();
        }
    }
}
