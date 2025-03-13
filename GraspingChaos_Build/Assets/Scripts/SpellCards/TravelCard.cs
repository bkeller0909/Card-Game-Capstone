using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//  Title:          TravelCard
//  Date Created:   March 13, 2025
//  Instance:       No
//-----------------------------------------------------------------


/// <summary>
/// Travels the card from a start position to an end position on a parabola.
/// 
/// Will be a component of the Card.
/// </summary>
public class TravelCard : MonoBehaviour
{

    [HideInInspector] public Transform travelStartPos;
    [HideInInspector] public Transform travelEndPos;
    [HideInInspector] public float travelTime;
    [HideInInspector] public float travelHeight;
    public float travelSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        CardTravel();
    }

    public void CardTravel()
    {
        StartCoroutine(Travel());
    }

    private IEnumerator Travel()
    {
        travelTime += Time.deltaTime;

        travelTime = travelTime % 5f;

        transform.position = MathParabola.Parabola(travelStartPos.position, travelEndPos.position, travelHeight, travelTime / travelSpeed);

        yield return null;
    }
}
