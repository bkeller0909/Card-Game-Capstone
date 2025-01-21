using System.Collections;
using UnityEngine;

public class CardPlay : MonoBehaviour
{
    private PlayerManager player;
    private CardHandSlot cardHandSlot;

    public Transform playSlotPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerManager>();
        cardHandSlot = GetComponent<CardHandSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerInput.actions["PlayCards"].triggered)
        {
            PlayCards();
        }
    }

    public void PlayCards()
    {
        for (int i = 0; i < cardHandSlot.selectedCards.Count; i++)
        {
            if (cardHandSlot.cards[i].isSelected == true)
            {
                cardHandSlot.cards.Remove(cardHandSlot.cards[i]);
            }
            StartCoroutine(MoveCards(cardHandSlot.selectedCards[i].gameObject, cardHandSlot.selectedCards[i].transform.position, playSlotPos.transform.position));
        }
    }

    private IEnumerator MoveCards(GameObject go, Vector3 startPos, Vector3 endPos)
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
