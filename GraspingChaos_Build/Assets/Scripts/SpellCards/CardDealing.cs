using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Title:        CardDealing
//  Date Created: 01/28/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Manages dealing cards to both player's currently in the game.
/// </summary>
public class CardDealing : MonoBehaviour
{
    private List<SpellCard> oneManaCards = new List<SpellCard>();
    private List<SpellCard> twoManaCards = new List<SpellCard>();
    private List<SpellCard> threeManaCards = new List<SpellCard>();
    private List<SpellCard> fourManaCards = new List<SpellCard>();
    private List<SpellCard> fiveManaCards = new List<SpellCard>();
    private List<SpellCard> sixManaCards = new List<SpellCard>();
    private List<SpellCard> sevenManaCards = new List<SpellCard>();
    private List<SpellCard> eightManaCards = new List<SpellCard>();
    private List<SpellCard> nineManaCards = new List<SpellCard>();
    private List<SpellCard> tenManaCards = new List<SpellCard>();
    private List<SpellCard> elevenManaCards = new List<SpellCard>();
    private List<SpellCard> twelveManaCards = new List<SpellCard>();

    public void InitializeCardCosts()
    {
        for (int i = 0; i < ActiveSpellCards.Instance.spellCards.Length; i++)
        {
            if (ActiveSpellCards.Instance.spellCards[i].manaCost == 1)
            {
                oneManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 2)
            {
                twoManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 3)
            {
                threeManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 4)
            {
                fourManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 5)
            {
                fiveManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 6)
            {
                sixManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 7)
            {
                sevenManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 8)
            {
                eightManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 9)
            {
                nineManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 10)
            {
                tenManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 11)
            {
                elevenManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
            else if (ActiveSpellCards.Instance.spellCards[i].manaCost == 12)
            {
                twelveManaCards.Add(ActiveSpellCards.Instance.spellCards[i]);
            }
        }
    }


    /// <summary>
    /// Will take the current mana of the player and the mana cost of a card.
    /// Baed on the player's mana, they will be dealt a card that is not higher than their current mana.
    /// However, there are different chances of being dealt cards of mana values lower than the player's current mana.
    /// </summary>
    /// <param name="player">The player that will be dealt the card.</param>
    /// <returns>A card after doing the calculations.</returns>
    public SpellCard CardDealtChance(PlayerManager player)
    {
        int cardChance = Random.Range(0, 100);

        // Player mana is 1 or 2 (can only get manaCost 1 cards)
        if (player.Mana == 1 || player.Mana == 2)
        {
            int oneManaChance = Random.Range(0, oneManaCards.Count);
            return oneManaCards[cardChance];
        }

        // Player mana is 3
        else if (player.Mana == 3)
        {
            if (cardChance >= 1 && cardChance <= 30)    // 1 mana
            {
                int oneManaChance = Random.Range(0, oneManaCards.Count);
                return oneManaCards[oneManaChance];
            }
            if (cardChance >= 31 && cardChance <= 75)     // 2 mana
            {
                int twoManaChance = Random.Range(0, twoManaCards.Count);
                return twoManaCards[twoManaChance];
            }
            if (cardChance >= 76 && cardChance <= 100)    // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
        }

        // Player mana is 4
        else if (player.Mana == 4)
        {
            if (cardChance >= 1 && cardChance <= 15)  // 1 mana
            {
                int oneManaChance = Random.Range(0, oneManaCards.Count);
                return oneManaCards[oneManaChance];
            }
            if (cardChance >= 16 && cardChance <= 50) // 2 mana
            {
                int twoManaChance = Random.Range(0, twoManaCards.Count);
                return twoManaCards[twoManaChance];
            }
            if (cardChance >= 51 && cardChance <= 80) // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
            if (cardChance >= 81 && cardChance <= 100)    // 4 mana
            {
                int fourManaChance = Random.Range(0, fourManaCards.Count);
                return fourManaCards[fourManaChance];
            }
        }

        // Player mana is 5
        else if (player.Mana == 5)
        {
            if (cardChance >= 1 && cardChance <= 5)   // 1 mana
            {
                int oneManaChance = Random.Range(0, oneManaCards.Count);
                return oneManaCards[oneManaChance];
            }
            if (cardChance >= 6 && cardChance <= 25)  // 2 mana
            {
                int twoManaChance = Random.Range(0, twoManaCards.Count);
                return twoManaCards[twoManaChance];
            }
            if (cardChance >= 26 && cardChance <= 60) // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
            if (cardChance >= 61 && cardChance <= 70) // 4 mana           
            {
                int fourManaChance = Random.Range(0, fourManaCards.Count);
                return fourManaCards[fourManaChance];
            }
            if (cardChance >= 71 && cardChance <= 100)    // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
        }

        // Player mana is 6
        else if (player.Mana == 6)
        {
            if (cardChance >= 1 && cardChance <= 20)  // 2 mana
            {
                int twoManaChance = Random.Range(0, twoManaCards.Count);
                return twoManaCards[twoManaChance];
            }
            if (cardChance >= 21 && cardChance <= 35) // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
            if (cardChance >= 36 && cardChance <= 45) // 4 mana
            {
                int fourManaChance = Random.Range(0, fourManaCards.Count);
                return fourManaCards[fourManaChance];
            }
            if (cardChance >= 46 && cardChance <= 75) // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
            if (cardChance >= 76 && cardChance <= 100)    // 6 mana
            {
                int sixManaChance = Random.Range(0, sixManaCards.Count);
                return sixManaCards[sixManaChance];
            }
        }

        // Player mana is 7
        else if (player.Mana == 7)
        {
            if (cardChance >= 1 && cardChance <= 15)  // 2 mana
            {
                int twoManaChance = Random.Range(0, twoManaCards.Count);
                return twoManaCards[twoManaChance];
            }
            if (cardChance >= 16 && cardChance <= 25) // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
            if (cardChance >= 26 && cardChance <= 35) // 4 mana
            {
                int fourManaChance = Random.Range(0, fourManaCards.Count);
                return fourManaCards[fourManaChance];
            }
            if (cardChance >= 36 && cardChance <= 60) // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
            if (cardChance >= 61 && cardChance <= 90) // 6 mana
            {
                int sixManaChance = Random.Range(0, sixManaCards.Count);
                return sixManaCards[sixManaChance];
            }
            if (cardChance >= 91 && cardChance <= 100)    // 7 mana
            {
                int sevenManaChance = Random.Range(0, sevenManaCards.Count);
                return sevenManaCards[sevenManaChance];
            }
        }

        // Player mana is 8
        else if (player.Mana == 8)
        {
            if (cardChance >= 1 && cardChance <= 8)   // 2 mana
            {
                int twoManaChance = Random.Range(0, twoManaCards.Count);
                return twoManaCards[twoManaChance];
            }
            if (cardChance >= 9 && cardChance <= 12)  // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
            if (cardChance >= 13 && cardChance <= 14) // 4 mana
            {
                int fourManaChance = Random.Range(0, fourManaCards.Count);
                return fourManaCards[fourManaChance];
            }
            if (cardChance >= 15 && cardChance <= 42) // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
            if (cardChance >= 43 && cardChance <= 70) // 6 mana
            {
                int sixManaChance = Random.Range(0, sixManaCards.Count);
                return sixManaCards[sixManaChance];
            }
            if (cardChance >= 71 && cardChance <= 90) // 7 mana
            {
                int sevenManaChance = Random.Range(0, sevenManaCards.Count);
                return sevenManaCards[sevenManaChance];
            }
            if (cardChance >= 91 && cardChance <= 100)    // 8 mana
            {
                int eightManaChance = Random.Range(0, eightManaCards.Count);
                return eightManaCards[eightManaChance];
            }
        }

        // Player mana is 9
        else if (player.Mana == 9)
        {
            if (cardChance >= 1 && cardChance <= 8)   // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
            if (cardChance >= 9 && cardChance <= 10)  // 4 mana
            {
                int fourManaChance = Random.Range(0, fourManaCards.Count);
                return fourManaCards[fourManaChance];
            }
            if (cardChance >= 11 && cardChance <= 32) // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
            if (cardChance >= 33 && cardChance <= 54) // 6 mana
            {
                int sixManaChance = Random.Range(0, sixManaCards.Count);
                return sixManaCards[sixManaChance];
            }
            if (cardChance >= 55 && cardChance <= 75) // 7 mana   
            {
                int sevenManaChance = Random.Range(0, sevenManaCards.Count);
                return sevenManaCards[sevenManaChance];
            }
            if (cardChance >= 76 && cardChance <= 90) // 8 mana
            {
                int eightManaChance = Random.Range(0, eightManaCards.Count);
                return eightManaCards[eightManaChance];
            }
            if (cardChance >= 91 && cardChance <= 100)    // 9 mana
            {
                int nineManaChance = Random.Range(0, nineManaCards.Count);
                return nineManaCards[nineManaChance];
            }
        }

        // Player mana is 10
        else if (player.Mana == 10)
        {
            if (cardChance >= 1 && cardChance <= 8)     // 3 mana
            {
                int threeManaChance = Random.Range(0, threeManaCards.Count);
                return threeManaCards[threeManaChance];
            }
            if (cardChance >= 9 && cardChance <= 10)    // 4 mana
            {
                int fourManaChance = Random.Range(0, fourManaCards.Count);
                return fourManaCards[fourManaChance];
            }
            if (cardChance >= 11 && cardChance <= 30)   // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
            if (cardChance >= 31 && cardChance <= 50)   // 6 mana
            {
                int sixManaChance = Random.Range(0, sixManaCards.Count);
                return sixManaCards[sixManaChance];
            }
            if (cardChance >= 51 && cardChance <= 70)   // 7 mana
            {
                int sevenManaChance = Random.Range(0, sevenManaCards.Count);
                return sevenManaCards[sevenManaChance];
            }
            if (cardChance >= 71 && cardChance <= 90)   // 8 mana
            {
                int eightManaChance = Random.Range(0, eightManaCards.Count);
                return eightManaCards[eightManaChance];
            }
            if (cardChance >= 91 && cardChance <= 100)  // 9 mana
            {
                int nineManaChance = Random.Range(0, nineManaCards.Count);
                return nineManaCards[nineManaChance];
            }
        }

        // Player mana is 11
        else if (player.Mana == 11)
        {
            if (cardChance >= 1 && cardChance <= 5)       // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
            if (cardChance >= 6 && cardChance <= 25)      // 6 mana
            {
                int sixManaChance = Random.Range(0, sixManaCards.Count);
                return sixManaCards[sixManaChance];
            }
            if (cardChance >= 26 && cardChance <= 50)     // 7 mana
            {
                int sevenManaChance = Random.Range(0, sevenManaCards.Count);
                return sevenManaCards[sevenManaChance];
            }
            if (cardChance >= 51 && cardChance <= 75)     // 8 mana
            {
                int eightManaChance = Random.Range(0, eightManaCards.Count);
                return eightManaCards[eightManaChance];
            }
            if (cardChance >= 76 && cardChance <= 100)    // 9 mana
            {
                int nineManaChance = Random.Range(0, nineManaCards.Count);
                return nineManaCards[nineManaChance];
            }
        }

        // Player mana is 12
        else if (player.Mana == 12)
        {
            if (cardChance >= 1 && cardChance <= 5)       // 5 mana
            {
                int fiveManaChance = Random.Range(0, fiveManaCards.Count);
                return fiveManaCards[fiveManaChance];
            }
            if (cardChance >= 6 && cardChance <= 15)      // 6 mana
            {
                int sixManaChance = Random.Range(0, sixManaCards.Count);
                return sixManaCards[sixManaChance];
            }
            if (cardChance >= 16 && cardChance <= 40)     // 7 mana
            {
                int sevenManaChance = Random.Range(0, sevenManaCards.Count);
                return sevenManaCards[sevenManaChance];
            }
            if (cardChance >= 41 && cardChance <= 70)     // 8 mana
            {
                int eightManaChance = Random.Range(0, eightManaCards.Count);
                return eightManaCards[eightManaChance];
            }
            if (cardChance >= 71 && cardChance <= 100)    // 9 mana
            {
                int nineManaChance = Random.Range(0, nineManaCards.Count);
                return nineManaCards[nineManaChance];
            }
        }

        return null; // no card selected
    }
}
