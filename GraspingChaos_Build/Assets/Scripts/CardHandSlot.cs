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
    public GameObject[] cards;  // array for the cards to keep track of what is selected
    public CardSelect LastSelectedCard { get; set; }
    public int LastSelectedCardIndex { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerManager>();

        if (cards.Length > 0)
        {
            SetSelectedCard(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerInput.actions["NavCardRight"].WasPressedThisFrame())
        {
            MoveSelection(1);
        }
        else if (player.playerInput.actions["NavCardLeft"].WasPressedThisFrame())
        {
            MoveSelection(-1);
        }
        if (player.playerInput.actions["Select"].WasPressedThisFrame())
        {
            RumbleManager.instance.ControllerRumble(0.25f, 0.5f, 0.25f, player.gamepad);
        }
        if (player.playerInput.actions["Pause"].WasPressedThisFrame())
        {
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
        }
    }

    public void SetSelectedCard(int cardIndex)
    {
        if (LastSelectedCard != null)
        {
            LastSelectedCard.DeselectCard();
        }

        LastSelectedCardIndex = cardIndex;
        LastSelectedCard = cards[cardIndex].GetComponentInChildren<CardSelect>();
        LastSelectedCard.SelectCard();
    }

    public void MoveSelection(int selectDirection)
    {
        int newCardIndex = Mathf.Clamp(LastSelectedCardIndex + selectDirection, 0, cards.Length - 1);

        if (newCardIndex != LastSelectedCardIndex)
        {
            SetSelectedCard(newCardIndex);
        }
    }
}
