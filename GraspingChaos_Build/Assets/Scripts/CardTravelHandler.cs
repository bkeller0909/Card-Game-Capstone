using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//  Title:          CardTravelHandler
//  Date Created:   March 13, 2025
//  Instance:       No
//-----------------------------------------------------------------


/// <summary>
/// Handles the travel speed and height of the card coming from the book.
/// </summary>
public class CardTravelHandler : MonoBehaviour
{
    [HideInInspector] public TravelCard travelCard;

    // Start is called before the first frame update
    void Awake()
    {
        travelCard = GetComponent<TravelCard>();
    }

    private IEnumerator DoCardTravel(float travelHeight, float travelTimer, float travelSpeed, Transform startPos, Transform endPos)
    {
        travelCard.travelHeight = travelHeight;
        travelCard.travelSpeed = travelSpeed;
        travelCard.travelTime = travelTimer;
        travelCard.travelStartPos = startPos;
        travelCard.travelEndPos = endPos;
        travelCard.enabled = true;
        yield return new WaitForSeconds(travelTimer);
        travelCard.gameObject.transform.position = endPos.position;
        travelCard.enabled = false;
    }

    public void CardTravel(float travelHeight, float travelTimer, float travelSpeed, Transform startPos, Transform endPos)
    {
        StartCoroutine(DoCardTravel(travelHeight, travelTimer, travelSpeed, startPos, endPos));
    }
}
