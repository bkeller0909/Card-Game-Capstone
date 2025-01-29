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
    public List<CardSelect> cards = new List<CardSelect>();         // array for the cards to keep track of what is being hovered and selected.
    [Tooltip("List for cards when they are selected. Keep EMPTY in editor.")]
    public List<CardSelect> selectedCards = new List<CardSelect>(); // for the cards that are selected.
    [Tooltip("Slot positions for the cards.")]
    public Transform[] cardSlots;                                   // slot positions for each card
    [Tooltip("Determines if a slot is empty. TRUE in editor.")]
    public bool[] emptySlots;                                       // bool for the slot position being empty

    public CardSelect LastHoveredCard { get; set; }
    public int LastHoveredCardIndex { get; set; }

    private readonly int maxSelectedCards = 3;                      // can select up to three cards.

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

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();
        playerInput = gameObject.GetComponentInParent<InputHandler>();
        camPosScript = player.gameObject.GetComponentInChildren<CameraPositionChange>();

        if (cards.Count > 0)
        {
            SetHoveredCard(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CardControls();
    }

    public void InitializeCards()
    {
        GameObject card = cardPrefab;

        for (int i = 0; i < emptySlots.Length; i++)
        {
            if (emptySlots[i] == true)
            {
                Instantiate(card);
                card.transform.position = cardSlots[i].position;
                cards.Add(card.GetComponent<CardSelect>());
                emptySlots[i] = false;
            }
        }
    }

    private void CardControls()
    {
        if (playerInput.cardMoveRight)
        {
            if (!cardNavPressed)
            {
                cardNavPressed = true;
                MoveSelection(1);
            }
        }
        else if (playerInput.cardMoveLeft)
        {
            if (!cardNavPressed)
            {
                cardNavPressed = true;
                MoveSelection(-1);
            }
        }
        else
        {
            cardNavPressed = false;
        }

        if (playerInput.finishSelection)
        {
            if (selectedCards.Count > 0)
            {
                if (!finalPressed)
                {
                    finalPressed = true;
                    player.playerInput.SwitchCurrentActionMap("QTE");
                }
            }
        }
        else
        {
            finalPressed = false;
        }


        if (player.playerInput.actions["Select"].triggered)
        {
            SetSelectedCard();
            player.playerInput.SwitchCurrentActionMap("Player");
            //camPosScript.callRoutine();
            //StartCoroutine(camPosScript.MoveCameratoNewPosition(4));
            RumbleManager.instance.ControllerRumble(0.25f, 0.5f, 0.25f, player.gamepad);
        }
        if (player.playerInput.actions["Pause"].triggered)
        {
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
        }
    }

    /// <summary>
    /// Adds the effect to indicate the card is hovered.
    /// </summary>
    /// <param name="cardIndex">Index of the card that is hovered.</param>
    public void SetHoveredCard(int cardIndex)
    {
        // remove hover effect from the hovered card if it's not selected
        if (LastHoveredCard != null && !selectedCards.Contains(LastHoveredCard))
        {
            LastHoveredCard.OffHoverCard();
        }

        // update index to the new card that is hovered
        LastHoveredCardIndex = cardIndex;
        LastHoveredCard = cards[cardIndex];

        // use hover effect if the card is not selected
        if (!selectedCards.Contains(LastHoveredCard))
        {
            LastHoveredCard.OnHoverCard();
        }

        Debug.Log(LastHoveredCard + " " + LastHoveredCardIndex);
    }

    /// <summary>
    /// Moves the hover to the next or previous card.
    /// </summary>
    /// <param name="selectDirection">Direction to move through the hand of cards (-1 for left, 1 for right).</param>
    public void MoveSelection(int selectDirection)
    {
        int newCardIndex = Mathf.Clamp(LastHoveredCardIndex + selectDirection, 0, cards.Count - 1);

        if (newCardIndex != LastHoveredCardIndex)
        {
            SetHoveredCard(newCardIndex);
        }
    }

    /// <summary>
    /// Toggles the state of the card.
    /// </summary>
    private void SetSelectedCard()
    {
        if (LastHoveredCard == null)
        {
            return;
        }

        if (selectedCards.Contains(LastHoveredCard))
        {
            // deselect the card if it's already selected
            // add the hover effect back
            selectedCards.Remove(LastHoveredCard);
            LastHoveredCard.DeselectCard();
            LastHoveredCard.OnHoverCard();
        }
        else
        {
            // select the card if it's not already selected
            // can only have total of 3 cards selected at a time
            if (selectedCards.Count < maxSelectedCards)
            {
                selectedCards.Add(LastHoveredCard);
                LastHoveredCard.SelectCard();
            }
        }
    }
}
