using System.Collections.Generic;
using UnityEngine;
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        SpellHand
//  Date Created: 01/07/2025
//  Purpose:      Class for spells on the players hand
//  Instance?     No
//-----------------------------------------------------------------
public class SpellHand : MonoBehaviour
{
    List<SpellCard> playerSpells = new List<SpellCard>();
    public int amtOfSpellsInHand = 0;
    //add spells, call each time players have to draw a cad
    public void AddSpells()
    {
        for (int i = 0; i < GameManager.Instance.playerHandSize; i++)
        {
            //will reach for card algorithm script fro drawing cards
            //SpellCard newCard = call algorithm to copy card into value then add value to list
            //playerSpells.Add(newCard)
        }
    }

    //remove spells, call each time a spell is used
    public void RemoveSpells(SpellCard cardUsed)
    {
        //might need more functionality based on how the trigger events are handled in the game loop
        playerSpells.Remove(cardUsed);
    }

    //Filter by mana amount and spell type
    public void GrouopByType()
    {
        //add bubble sort
    }

    public void GroupByMana()
    {
        //add bubble sort
    }

}
