using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Co-Auther:    
//  Title:        CardHistory
//  Date Created: 02/13/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Handle all sound output for the game. 
/// </summary>
public class CardHistory : MonoBehaviour
{
    private List<GameObject> cardHistoryList = new List<GameObject>();       // list of card gameobjects 
    public Transform[] historySlots;                                                          // transform slot positions for the card in the history
    public List<GameObject> cardHistoryObjPool = new List<GameObject>();

    /// <summary>
    /// Add a card to the history.
    /// </summary>
    public void AddCardHistroy(List<CardSelect> cardHistory)
    {
        // take the cards that have been played in the previous round
        // put them in the cardHistory list
        for (int i = 0; i < cardHistory.Count; i++)
        {
            cardHistoryList.Add(cardHistory[i].gameObject);
        }
    }

    /// <summary>
    /// Remove a card from the history.
    /// </summary>
    public void RemoveFromHistory()
    {
        // when a round is finished use this to reset the cardHistory list back to nothing
        for (int i = 0; i < cardHistoryList.Count; i++)
        {
            cardHistoryList.Remove(cardHistoryList[i]);
        }
    }

    /// <summary>
    /// Display all of the cards from the card history list. Instantiate the card prefabs to the
    /// correct transform slots so that when the player performs a button input they can be shown in UI.
    /// </summary>
    public void ShowCardHistoryFromPool()
    {
        // this is called when you want to display all the cards from the card history
        // instantiate each card to the appropriate slot 
        for (int i = 0; i < cardHistoryObjPool.Count; i++)
        {

        }
    }
}
