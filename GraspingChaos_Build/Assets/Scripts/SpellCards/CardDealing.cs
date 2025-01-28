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

        // Player mana is 1 or 2
        if (player.Mana == 1 || player.Mana == 2)
        {
            if (card.manaCost == 1) // Card cost is 1
            {
                return card;
            }
        }

        // Player mana is 3
        if (player.Mana == 3)
        {
            if (card.manaCost == 1) // Card cost is 1
            {
                switch (cardChance)
                {
                    case 1 - 30:
                        return card;
                }
            }
            else if (card.manaCost == 2) // Card cost is 2
            {
                switch (cardChance)
                {
                    case 31 - 75:
                        return card;
                }
            }
            else if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 76 - 100:
                        return card;
                }
            }
        }

        // Player mana is 4
        if (player.Mana == 4)
        {
            if (card.manaCost == 1) // Card cost is 1
            {
                switch (cardChance)
                {
                    case 1 - 15:
                        return card;
                }
            }
            else if (card.manaCost == 2) // Card cost is 2
            {
                switch (cardChance)
                {
                    case 16 - 50:
                        return card;
                }
            }
            else if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 51 - 80:
                        return card;
                }
            }
            else if (card.manaCost == 4) // Card cost is 4
            {
                switch (cardChance)
                {
                    case 81 - 100:
                        return card;
                }
            }
        }

        // Player Mana is 5
        if (player.Mana == 5)
        {
            if (card.manaCost == 1) // Card cost is 1
            {
                switch (cardChance)
                {
                    case 1 - 5:
                        return card;
                }
            }
            else if (card.manaCost == 2) // Card cost is 2
            {
                switch (cardChance)
                {
                    case 6 - 25:
                        return card;
                }
            }
            else if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 26 - 60:
                        return card;
                }
            }
            else if (card.manaCost == 4) // Card cost is 4
            {
                switch (cardChance)
                {
                    case 61 - 70:
                        return card;
                }
            }
            else if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 71 - 100:
                        return card;
                }
            }
        }

        // Player mana is 6
        if (player.Mana == 6)
        {
            if (card.manaCost == 2) // Card cost is 2
            {
                switch (cardChance)
                {
                    case 1 - 20:
                        return card;
                }
            }
            else if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 21 - 35:
                        return card;
                }
            }
            else if (card.manaCost == 4) // Card cost is 4
            {
                switch (cardChance)
                {
                    case 36 - 45:
                        return card;
                }
            }
            else if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 46 - 75:
                        return card;
                }
            }
            else if (card.manaCost == 6) // Card cost is 6
            {
                switch (cardChance)
                {
                    case 76 - 100:
                        return card;
                }
            }
        }

        // Player mana is 7
        if (player.Mana == 7)
        {
            if (card.manaCost == 2) // Card cost is 2
            {
                switch (cardChance)
                {
                    case 1 - 15:
                        return card;
                }
            }
            else if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 16 - 25:
                        return card;
                }
            }
            else if (card.manaCost == 4) // Card cost is 4
            {
                switch (cardChance)
                {
                    case 26 - 35:
                        return card;
                }
            }
            else if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 36 - 60:
                        return card;
                }
            }
            else if (card.manaCost == 6) // Card cost is 6
            {
                switch (cardChance)
                {
                    case 61 - 90:
                        return card;
                }
            }
            else if (card.manaCost == 7) // Card cost is 7
            {
                switch (cardChance)
                {
                    case 91 - 100:
                        return card;
                }
            }
        }

        // Player mana is 8
        if (player.Mana == 8)
        {
            if (card.manaCost == 2) // Card cost is 2
            {
                switch (cardChance)
                {
                    case 1 - 8:
                        return card;
                }
            }
            else if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 9 - 12:
                        return card;
                }
            }
            else if (card.manaCost == 4) // Card cost is 4
            {
                switch (cardChance)
                {
                    case 13 - 14:
                        return card;
                }
            }
            else if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 15 - 42:
                        return card;
                }
            }
            else if (card.manaCost == 6) // Card cost is 6
            {
                switch (cardChance)
                {
                    case 42 - 70:
                        return card;
                }
            }
            else if (card.manaCost == 7) // Card cost is 7
            {
                switch (cardChance)
                {
                    case 71 - 90:
                        return card;
                }
            }
            else if (card.manaCost == 8) // Card cost is 8
            {
                switch (cardChance)
                {
                    case 91 - 100:
                        return card;
                }
            }
        }


        // Player mana is 9
        if (player.Mana == 9)
        {
            if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 1 - 8:
                        return card;
                }
            }
            else if (card.manaCost == 4) // Card cost is 4
            {
                switch (cardChance)
                {
                    case 9 - 10:
                        return card;
                }
            }
            else if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 11 - 32:
                        return card;
                }
            }
            else if (card.manaCost == 6) // Card cost is 6
            {
                switch (cardChance)
                {
                    case 33 - 54:
                        return card;
                }
            }
            else if (card.manaCost == 7) // Card cost is 7
            {
                switch (cardChance)
                {
                    case 55 - 75:
                        return card;
                }
            }
            else if (card.manaCost == 8) // Card cost is 8
            {
                switch (cardChance)
                {
                    case 76 - 90:
                        return card;
                }
            }
            else if (card.manaCost == 9) // Card cost is 9
            {
                switch (cardChance)
                {
                    case 91 - 100:
                        return card;
                }
            }
        }

        // Player mana is 10
        if (player.Mana == 10)
        {
            if (card.manaCost == 3) // Card cost is 3
            {
                switch (cardChance)
                {
                    case 1 - 8:
                        return card;
                }
            }
            else if (card.manaCost == 4) // Card cost is 4
            {
                switch (cardChance)
                {
                    case 9 - 10:
                        return card;
                }
            }
            else if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 11 - 30:
                        return card;
                }
            }
            else if (card.manaCost == 6) // Card cost is 6
            {
                switch (cardChance)
                {
                    case 31 - 50:
                        return card;
                }
            }
            else if (card.manaCost == 7) // Card cost is 7
            {
                switch (cardChance)
                {
                    case 51 - 70:
                        return card;
                }
            }
            else if (card.manaCost == 8) // Card cost is 8
            {
                switch (cardChance)
                {
                    case 71 - 90:
                        return card;
                }
            }
            else if (card.manaCost == 9) // Card cost is 9
            {
                switch (cardChance)
                {
                    case 91 - 100:
                        return card;
                }
            }
        }

        // Player mana is 11
        if (player.Mana == 11)
        {
            if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 1 - 5:
                        return card;
                }
            }
            else if (card.manaCost == 6) // Card cost is 6
            {
                switch (cardChance)
                {
                    case 6 - 25:
                        return card;
                }
            }
            else if (card.manaCost == 7) // Card cost is 7
            {
                switch (cardChance)
                {
                    case 26 - 50:
                        return card;
                }
            }
            else if (card.manaCost == 8) // Card cost is 8
            {
                switch (cardChance)
                {
                    case 51 - 75:
                        return card;
                }
            }
            else if (card.manaCost == 9) // Card cost is 9
            {
                switch (cardChance)
                {
                    case 76 - 100:
                        return card;
                }
            }
        }

        // Player mana is 12
        if (player.Mana == 12)
        {
            if (card.manaCost == 5) // Card cost is 5
            {
                switch (cardChance)
                {
                    case 1 - 5:
                        return card;
                }
            }
            else if (card.manaCost == 6) // Card cost is 6
            {
                switch (cardChance)
                {
                    case 6 - 15:
                        return card;
                }
            }
            else if (card.manaCost == 7) // Card cost is 7
            {
                switch (cardChance)
                {
                    case 16 - 40:
                        return card;
                }
            }
            else if (card.manaCost == 8) // Card cost is 8
            {
                switch (cardChance)
                {
                    case 41 - 70:
                        return card;
                }
            }
            else if (card.manaCost == 9) // Card cost is 9
            {
                switch (cardChance)
                {
                    case 71 - 100:
                        return card;
                }
            }
        }
        return card;
    }
}
