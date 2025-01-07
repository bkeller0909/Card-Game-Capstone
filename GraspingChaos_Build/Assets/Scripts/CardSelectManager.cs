using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Keep track of the index of selected card to smooth navigation of the controller.
/// 
/// Instance: Yes
/// </summary>
public class CardSelectManager : MonoBehaviour
{
    public static CardSelectManager instance;

    [Tooltip("List of all the card objects for the player's Spell Hand.")]
    public GameObject[] cards;  // array for the cards to keep track of what is selected
    public GameObject lastSelectedCard { get; set; }
    public int lastSelectedCardIndex { get; set; }

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
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
    }
}
