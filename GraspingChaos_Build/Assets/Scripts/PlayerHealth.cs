using UnityEngine;



//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Player Health
//  Date Created: 01/07/2025
//  Purpose:      This is the player health and how it is interacted with and held
//  Instance?     no
//-----------------------------------------------------------------

/// <summary>
/// This is the player health and how it is interacted with and held
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    PlayerManager player;

    // This is the health value of each digit a player has
    public int[] playerHealthStats;

    // The max amount of fingers a player can have
    public static int MAX_AMT_FINGERS = 10;


    // Start is called before the first frame update
    void Awake()
    {
    }

    /// <summary>
    ///  Assigns the amount of healh is required for each finger at the start of the game
    /// </summary>
    public void SetUpHealth(PlayerManager whoAmI)
    {
        //Getting which player they are
        player = whoAmI;

        playerHealthStats = new int[MAX_AMT_FINGERS];

        int health;

        for (int whatFinger = (int)PlayerFingers.LH_Pinky; whatFinger < MAX_AMT_FINGERS; whatFinger++)
        {
            health = 3;

            if (whatFinger == (int)PlayerFingers.LH_Thumb || whatFinger == (int)PlayerFingers.RH_Thumb)
            {
                health = 2;
            }

            playerHealthStats[whatFinger] = health;
        }
    }

    public int getFingerHealth(PlayerFingers whatFinger)
    {
        return playerHealthStats[(int)whatFinger];
    }

    /// <summary>
    ///  Deals 1 Damage to a finger
    /// </summary>
    public void DamageFinger(PlayerFingers whatFinger)
    {
        if (playerHealthStats[(int)whatFinger] > 0)
        {
            playerHealthStats[(int)whatFinger] -= 1;
            player.fingers[(int)whatFinger].removeCurrentSegment();
            player.entireHP--;
        }
        else
        {
            playerHealthStats[(int)whatFinger] = 0;
        }

    }

    /// <summary>
    ///  Heals 1 health to a finger
    /// </summary>
    public void HealFinger(PlayerFingers whatFinger)
    {
        if ((playerHealthStats[(int)whatFinger] >= 3) && (whatFinger != PlayerFingers.LH_Thumb) && (whatFinger != PlayerFingers.RH_Thumb))
        {
            playerHealthStats[(int)whatFinger] += 1;
            player.fingers[(int)whatFinger].addSegment();
            player.entireHP++;
        }
        else if ((playerHealthStats[(int)whatFinger] >= 2) && (whatFinger == PlayerFingers.LH_Thumb) && (whatFinger == PlayerFingers.RH_Thumb))
        {
            playerHealthStats[(int)whatFinger] += 1;
            player.fingers[(int)whatFinger].addSegment();
            player.entireHP++;
        }
    }
}
