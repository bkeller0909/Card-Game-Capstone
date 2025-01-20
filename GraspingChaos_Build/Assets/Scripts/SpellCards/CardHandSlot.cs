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

    [Tooltip("List of all the card objects for the player's Spell Hand.")]
    public GameObject[] cards;                                          // array for the cards to keep track of what is being hovered and selected.

    private List<CardSelect> selectedCards = new List<CardSelect>();    // for the cards that are selected.
    public CardSelect LastHoveredCard { get; set; }
    public int LastHoveredCardIndex { get; set; }

    private int maxSelectedCards = 3;                                   // can select up to three cards.

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();

        if (cards.Length > 0)
        {
            SetHoveredCard(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerInput.actions["NavCardRight"].triggered)
        {
            MoveSelection(1);
        }
        else if (player.playerInput.actions["NavCardLeft"].triggered)
        {
            MoveSelection(-1);
        }
        if (player.playerInput.actions["Select"].triggered)
        {
            SetSelectedCard();
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
        LastHoveredCard = cards[cardIndex].GetComponentInChildren<CardSelect>();

        // use hover effect if the card is not selected
        if (!selectedCards.Contains(LastHoveredCard))
        {
            LastHoveredCard.OnHoverCard();
        }

    }

    /// <summary>
    /// Moves the hover to the next or previous card.
    /// </summary>
    /// <param name="selectDirection">Direction to move through the hand of cards (-1 for left, 1 for right).</param>
    public void MoveSelection(int selectDirection)
    {
        int newCardIndex = Mathf.Clamp(LastHoveredCardIndex + selectDirection, 0, cards.Length - 1);

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
