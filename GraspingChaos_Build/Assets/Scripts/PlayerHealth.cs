using UnityEngine;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Player Health
//  Date Created: 01/07/2025
//  Purpose:      This is the player health and how it is interacted with and held
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    int[,] playerHealthStats;

    public static int MAX_AMT_FINGERS = 5;
    public static int MAX_AMT_HANDS = 2;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealthStats = new int[2, 5];
    }

    private void Start()
    {
        SetUpHealth();
    }

    /// <summary>
    ///  Assigns the amount of healh is required for each finger at the start of the game
    /// </summary>
    public void SetUpHealth()
    {
        int health;

        for (int whatHand = (int)PlayerHands.LeftHand; whatHand < MAX_AMT_HANDS; whatHand++)
        {
            for (int whatFinger = (int)PlayerFingers.Thumb; whatFinger < MAX_AMT_FINGERS; whatFinger++)
            {
                health = 3;

                if (whatFinger == (int)PlayerFingers.Thumb)
                {
                    health = 2;
                }

                playerHealthStats[whatHand, whatFinger] = health;
            }
        }
    }

    public int getFingerHealth(PlayerHands whatHand, PlayerFingers whatFinger)
    {
        return playerHealthStats[(int)whatHand, (int)whatFinger];
    }

    /// <summary>
    ///  Deals 1 Damage to a finger
    /// </summary>
    public void DamageFinger(PlayerHands whatHand, PlayerFingers whatFinger)
    {
        if (playerHealthStats[(int)whatHand, (int)whatFinger] != 0)
        {
            playerHealthStats[(int)whatHand, (int)whatFinger] -= 1;
        }
    }

    /// <summary>
    ///  Heals 1 health to a finger
    /// </summary>
    public void HealFinger(PlayerHands whatHand, PlayerFingers whatFinger)
    {
        if ((playerHealthStats[(int)whatHand, (int)whatFinger] >= 3) && (whatFinger != PlayerFingers.Thumb))
        {
            playerHealthStats[(int)whatHand, (int)whatFinger] += 1;
        }
        else if ((playerHealthStats[(int)whatHand, (int)whatFinger] >= 2) && (whatFinger == PlayerFingers.Thumb))
        {
            playerHealthStats[(int)whatHand, (int)whatFinger] += 1;
        }
    }
}
