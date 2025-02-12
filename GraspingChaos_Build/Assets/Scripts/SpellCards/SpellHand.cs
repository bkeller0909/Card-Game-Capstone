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
    public List<SpellCard> playerSpells = new List<SpellCard>();
    public int amtOfSpellsInHand = 0;
    //add spells, call each time players have to draw a cad
    public void AddSpells()
    {
        for (int i = 0; i < GameManager.Instance.maxPlayerHandSize; i++)
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
        for (int w = 0; w < playerSpells.Count; w++)
        {
            if (playerSpells[w] == cardUsed)
            {
                playerSpells.Remove(cardUsed);
                //amtOfSpellsInHand--;
            }
        }
        GameManager.Instance.player1.spellHand.amtOfSpellsInHand = 5 - GameManager.Instance.player1.cardsAmountSelected;
        GameManager.Instance.player2.spellHand.amtOfSpellsInHand = 5 - GameManager.Instance.player2.cardsAmountSelected;
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
