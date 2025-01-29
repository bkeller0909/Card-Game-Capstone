using System.Collections;
using UnityEngine;

public class CardPlay : MonoBehaviour
{
    private InputHandler playerInput;
    private CardHandSlot cardHandSlot;

    public Transform[] playSlots;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInParent<InputHandler>();
        cardHandSlot = GetComponent<CardHandSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.finishSelection)
        {
            PlayCards();
        }
    }

    /// <summary>
    /// Moves the cards from the list of cards in the player's hand. Runs the coroutine to move the cards to the play slots.
    /// </summary>
    public void PlayCards()
    {
        for (int i = 0; i < cardHandSlot.cards.Count; i++)
        {
            if (cardHandSlot.cards[i].isSelected == true)
            {
                cardHandSlot.cards.Remove(cardHandSlot.cards[i]);
                cardHandSlot.emptySlots[i] = true;
            }
            StartCoroutine(MoveCard(cardHandSlot.selectedCards[i].gameObject, cardHandSlot.selectedCards[i].transform.position, playSlots[i].position));
        }
    }

    /// <summary>
    /// Moves a card from a starting position to an end position.
    /// </summary>
    /// <param name="go">The card game object.</param>
    /// <param name="startPos">Starting position of the card.</param>
    /// <param name="endPos">Ending position of card.</param>
    /// <returns></returns>
    private IEnumerator MoveCard(GameObject go, Vector3 startPos, Vector3 endPos)
    {
        float moveTimer = 1.0f;
        float time = 0.0f;

        while (time < moveTimer)
        {
            time += Time.deltaTime;

            Vector3 lerpPos = Vector3.Lerp(startPos, endPos, (time / moveTimer));

            go.transform.position = lerpPos;

            yield return null;
        }
    }
}
