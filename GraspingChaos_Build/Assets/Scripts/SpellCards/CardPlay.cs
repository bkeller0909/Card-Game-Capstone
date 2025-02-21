using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Title:        CardPlay
//  Date Created: 02/06/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Moves the selected cards to a specified position for display.
/// </summary>
public class CardPlay : MonoBehaviour
{
    [HideInInspector] public CardHandSlot cardHandSlot;

    public Transform cardsOutOfPlayPos;

    /// <summary>
    /// Moves a card from a starting position to an end position.
    /// </summary>
    /// <param name="go">The card game object.</param>
    /// <param name="startPos">Starting position of the card.</param>
    /// <param name="endPos">Ending position of card.</param>
    /// <returns></returns>
    public IEnumerator MoveCard(GameObject go, Vector3 startPos)
    {
        float moveTimer = 1.0f;
        float time = 0.0f;

        while (time < moveTimer)
        {
            time += Time.deltaTime;

            Vector3 lerpPos = Vector3.Lerp(startPos, cardsOutOfPlayPos.transform.position, (time / moveTimer));

            go.transform.position = lerpPos;

            yield return null;
        }
    }
}
