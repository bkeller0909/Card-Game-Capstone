using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
    public List<GameObject> cardHistory = new List<GameObject>();       // list of card gameobjects 
    public Transform[] historySlots;                                    // transform slot positions for the card in the history

    /// <summary>
    /// Add a card to the history.
    /// </summary>
    public void AddCardHistroy(PlayerManager player)
    {
        // take the cards that have been played in the previous round
        // put them in the cardHistory list
    }

    /// <summary>
    /// Remove a card from the history.
    /// </summary>
    public void RemoveFromHistory()
    {
        // when a round is finished use this to reset the cardHistory list back to nothing
    }

    /// <summary>
    /// Display all of the cards from the card history list. Instantiate the card prefabs to the
    /// correct transform slots so that when the player performs a button input they can be shown in UI.
    /// </summary>
    public void ShowCardHistory()
    { 
        // this is called when you want to display all the cards from the card history
        // instantiate each card to the appropriate slot 
    }
}
