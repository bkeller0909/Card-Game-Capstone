using UnityEngine;


//----------------------------------------------------------------
//  Author:       Keller
//  Title:        CardSelectManager
//  Date Created: 01/07/2025
//  Instance:     Yes
//-----------------------------------------------------------------

/// <summary>
/// Keep track of the index of selected card to smooth navigation of the controller.
/// </summary>
public class CardSelectManager : MonoBehaviour
{
    public static CardSelectManager instance;

    [Tooltip("List of all the card objects for the player's Spell Hand.")]
    public GameObject[] cards;  // array for the cards to keep track of what is selected
    public CardSelect LastSelectedCard { get; set; }
    public int LastSelectedCardIndex { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cards.Length > 0)
        {
            SetSelectedCard(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.playerControls.Player.NavigateFingerRight.WasPressedThisFrame())
        {
            MoveSelection(1);
        }
        else if (InputManager.instance.playerControls.Player.NavigateFingerLeft.WasPressedThisFrame())
        {
            MoveSelection(-1);
        }
    }

    public void SetSelectedCard(int cardIndex)
    {
        if (LastSelectedCard != null)
        {
            LastSelectedCard.DeselectCard();
        }

        LastSelectedCardIndex = cardIndex;
        LastSelectedCard = cards[cardIndex].GetComponent<CardSelect>();
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

    /*private void OnEnable()
    {
        StartCoroutine(SetSelectedAfterFrame());
    }

    /// <summary>
    /// Waits one frame then sets the selected game object in the hand of cards.
    /// Weird glitch that Unity UI system isn't setting correct transform position unless 1 frame has passed. Idk.
    /// </summary>
    private IEnumerator SetSelectedAfterFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(cards[0]);
    }*/
}
