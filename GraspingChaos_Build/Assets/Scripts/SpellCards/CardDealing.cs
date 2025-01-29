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
    /// <summary>
    /// Will take the current mana of the player and the mana cost of a card.
    /// Baed on the player's mana, they will be dealt a card that is not higher than their current mana.
    /// However, there are different chances of being dealt cards of mana values lower than the player's current mana.
    /// </summary>
    /// <param name="player">The player that will be dealt the card.</param>
    /// <param name="card">Chance to return this card based on the mana of the player.</param>
    /// <returns></returns>
    public SpellCard CardDealtChance(PlayerManager player, SpellCard card)
    {
        int cardChance = Random.Range(0, 100);

        // Player mana is 1 or 2 (can only get manaCost 1 cards)
        if (player.Mana == 1 || player.Mana == 2)
        {
            if (card.manaCost == 1)
            {
                return card;
            }
        }

        // Player mana is 3
        else if (player.Mana == 3)
        {
            if (card.manaCost == 1 && cardChance >= 1 && cardChance <= 30)
            {
                return card;
            }
            if (card.manaCost == 2 && cardChance >= 31 && cardChance <= 75)
            {
                return card;
            }
            if (card.manaCost == 3 && cardChance >= 76 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 4
        else if (player.Mana == 4)
        {
            if (card.manaCost == 1 && cardChance >= 1 && cardChance <= 15)
            {
                return card;
            }
            if (card.manaCost == 2 && cardChance >= 16 && cardChance <= 50)
            {
                return card;
            }
            if (card.manaCost == 3 && cardChance >= 51 && cardChance <= 80)
            {
                return card;
            }
            if (card.manaCost == 4 && cardChance >= 81 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 5
        else if (player.Mana == 5)
        {
            if (card.manaCost == 1 && cardChance >= 1 && cardChance <= 5)
            {
                return card;
            }
            if (card.manaCost == 2 && cardChance >= 6 && cardChance <= 25)
            {
                return card;
            }
            if (card.manaCost == 3 && cardChance >= 26 && cardChance <= 60)
            {
                return card;
            }
            if (card.manaCost == 4 && cardChance >= 61 && cardChance <= 70)
            {
                return card;
            }
            if (card.manaCost == 5 && cardChance >= 71 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 6
        else if (player.Mana == 6)
        {
            if (card.manaCost == 2 && cardChance >= 1 && cardChance <= 20)
            {
                return card;
            }
            if (card.manaCost == 3 && cardChance >= 21 && cardChance <= 35)
            {
                return card;
            }
            if (card.manaCost == 4 && cardChance >= 36 && cardChance <= 45)
            {
                return card;
            }
            if (card.manaCost == 5 && cardChance >= 46 && cardChance <= 75)
            {
                return card;
            }
            if (card.manaCost == 6 && cardChance >= 76 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 7
        else if (player.Mana == 7)
        {
            if (card.manaCost == 2 && cardChance >= 1 && cardChance <= 15)
            {
                return card;
            }
            if (card.manaCost == 3 && cardChance >= 16 && cardChance <= 25)
            {
                return card;
            }
            if (card.manaCost == 4 && cardChance >= 26 && cardChance <= 35)
            {
                return card;
            }
            if (card.manaCost == 5 && cardChance >= 36 && cardChance <= 60)
            {
                return card;
            }
            if (card.manaCost == 6 && cardChance >= 61 && cardChance <= 90)
            {
                return card;
            }
            if (card.manaCost == 7 && cardChance >= 91 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 8
        else if (player.Mana == 8)
        {
            if (card.manaCost == 2 && cardChance >= 1 && cardChance <= 8)
            {
                return card;
            }
            if (card.manaCost == 3 && cardChance >= 9 && cardChance <= 12)
            {
                return card;
            }
            if (card.manaCost == 4 && cardChance >= 13 && cardChance <= 14)
            {
                return card;
            }
            if (card.manaCost == 5 && cardChance >= 15 && cardChance <= 42)
            {
                return card;
            }
            if (card.manaCost == 6 && cardChance >= 43 && cardChance <= 70)
            {
                return card;
            }
            if (card.manaCost == 7 && cardChance >= 71 && cardChance <= 90)
            {
                return card;
            }
            if (card.manaCost == 8 && cardChance >= 91 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 9
        else if (player.Mana == 9)
        {
            if (card.manaCost == 3 && cardChance >= 1 && cardChance <= 8)
            {
                return card;
            }
            if (card.manaCost == 4 && cardChance >= 9 && cardChance <= 10)
            {
                return card;
            }
            if (card.manaCost == 5 && cardChance >= 11 && cardChance <= 32)
            {
                return card;
            }    
            if (card.manaCost == 6 && cardChance >= 33 && cardChance <= 54)
            {
                return card;
            }
            if (card.manaCost == 7 && cardChance >= 55 && cardChance <= 75)
            {
               return card;
            }
            if (card.manaCost == 8 && cardChance >= 76 && cardChance <= 90)
            {
                return card;
            }
            if (card.manaCost == 9 && cardChance >= 91 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 10
        else if (player.Mana == 10)
        {
            if (card.manaCost == 3 && cardChance >= 1 && cardChance <= 8)
            {
                return card;
            }
            if (card.manaCost == 4 && cardChance >= 9 && cardChance <= 10)
            {
                return card;
            }
            if (card.manaCost == 5 && cardChance >= 11 && cardChance <= 30)
            {
                return card;
            }
            if (card.manaCost == 6 && cardChance >= 31 && cardChance <= 50)
            {
                return card;
            }
            if (card.manaCost == 7 && cardChance >= 51 && cardChance <= 70)
            {
                return card;
            }
            if (card.manaCost == 8 && cardChance >= 71 && cardChance <= 90)
            {
                return card;
            }
            if (card.manaCost == 9 && cardChance >= 91 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 11
        else if (player.Mana == 11)
        {
            if (card.manaCost == 5 && cardChance >= 1 && cardChance <= 5)
            {
                return card;
            }
            if (card.manaCost == 6 && cardChance >= 6 && cardChance <= 25)
            {
                return card;
            }
            if (card.manaCost == 7 && cardChance >= 26 && cardChance <= 50)
            {
                return card;
            }
            if (card.manaCost == 8 && cardChance >= 51 && cardChance <= 75)
            {
                return card;
            }
            if (card.manaCost == 9 && cardChance >= 76 && cardChance <= 100)
            {
                return card;
            }
        }

        // Player mana is 12
        else if (player.Mana == 12)
        {
            if (card.manaCost == 5 && cardChance >= 1 && cardChance <= 5)
            {
                return card;
            }
            if (card.manaCost == 6 && cardChance >= 6 && cardChance <= 15)
            {
                return card;
            }
            if (card.manaCost == 7 && cardChance >= 16 && cardChance <= 40)
            {
                return card;
            }
            if (card.manaCost == 8 && cardChance >= 41 && cardChance <= 70)
            {
                return card;
            }
            if (card.manaCost == 9 && cardChance >= 71 && cardChance <= 100)
            {
                return card;
            }
        }

        return null; // no card selected
    }
}
