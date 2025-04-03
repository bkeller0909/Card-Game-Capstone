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
        bool veilOfFortitudeFullActive = false;
        if ((((int)whatFinger >= 0 && (int)whatFinger <= 4) && player.ringHandler.veilOfFortitudeLeft == true) ||
            ((int)whatFinger >= 5 && (int)whatFinger <= 9) && player.ringHandler.veilOfFortitudeRight == true)
        {
            veilOfFortitudeFullActive = true;
        }

        if (!veilOfFortitudeFullActive)
        {
            if (playerHealthStats[(int)whatFinger] > 0)
            {
                if (player.fingers[(int)whatFinger].fingerShield <= 0)
                {
                    if (player == GameManager.Instance.player1)
                    {
                        player.GetComponent<RingsHandler>().EffectThornsOfAgony(whatFinger, GameManager.Instance.player2);
                        player.GetComponent<RingsHandler>().EffectVampiricSurge(whatFinger, GameManager.Instance.player2);
                    }
                    else if (player == GameManager.Instance.player2)
                    {
                        player.GetComponent<RingsHandler>().EffectThornsOfAgony(whatFinger, GameManager.Instance.player1);
                        player.GetComponent<RingsHandler>().EffectVampiricSurge(whatFinger, GameManager.Instance.player2);
                    }
                    playerHealthStats[(int)whatFinger] -= 1;
                    player.fingers[(int)whatFinger].removeCurrentSegment();
                    player.visualFingers[(int)whatFinger].removeCurrentSegment();
                    player.entireHP--;
                    player.stagHands.boneVisualEffects[(int)whatFinger].Play();
                    player.skullHands.boneVisualEffects[(int)whatFinger].Play();
                    SoundFXManager.Instance.PlayRandomSFX(SoundFXManager.Instance.boneDamage, 1);
                    player.DamageTrackedPerTurn++;
                }
                else
                {
                    player.fingers[(int)whatFinger].fingerShield--;
                    if (player.fingers[(int)whatFinger].fingerShield <= 0)
                    {
                        player.ringHandler.ringsActive[(int)Rings.GuardiansTouchFull, (int)whatFinger] = false;
                        player.ToggleRing(false, Rings.GuardiansTouchFull, whatFinger);
                        player.ringHandler.ringsActive[(int)Rings.GuardiansTouchFail, (int)whatFinger] = false;
                        player.ToggleRing(false, Rings.GuardiansTouchFail, whatFinger);
                    }
                }
            }
            else
            {
                playerHealthStats[(int)whatFinger] = 0;
            }

            if (playerHealthStats[(int)whatFinger] == 0)
            {
                for (int i = 0; i < 14; i++)
                {
                    if (player.ringHandler.ringsActive[i, (int)whatFinger] == true)
                    {
                        player.ringHandler.ringsActive[i, (int)whatFinger] = false;
                        player.ToggleRing(false, (Rings)i, whatFinger);
                        if (i == (int)Rings.ManaMerchantFail)
                        {
                            player.GetComponent<RingsHandler>().manaMerchantFailure = false;
                        }
                        else if (i == (int)Rings.ManaMerchantFull)
                        {
                            player.GetComponent<RingsHandler>().manaMerchantSuccess = false;
                        }
                    }
                }
            }
        }
    }

    public void DamageFingerTwice(PlayerFingers whatFinger)
    {
        bool veilOfFortitudeActive = false;
        if ((((int)whatFinger >= 0 && (int)whatFinger < 4) && player.ringHandler.veilOfFortitudeLeft == true) ||
            ((int)whatFinger >= 5 && (int)whatFinger < 9) && player.ringHandler.veilOfFortitudeRight == true)
        {
            veilOfFortitudeActive = true;
        }

        if (!veilOfFortitudeActive)
        {
            if (playerHealthStats[(int)whatFinger] > 0)
            {
                if (player.fingers[(int)whatFinger].fingerShield <= 0)
                {
                    if (player == GameManager.Instance.player1)
                    {
                        player.GetComponent<RingsHandler>().EffectThornsOfAgony(whatFinger, GameManager.Instance.player2);
                        player.GetComponent<RingsHandler>().EffectVampiricSurge(whatFinger, GameManager.Instance.player2);
                    }
                    else if (player == GameManager.Instance.player2)
                    {
                        player.GetComponent<RingsHandler>().EffectThornsOfAgony(whatFinger, GameManager.Instance.player1);
                        player.GetComponent<RingsHandler>().EffectVampiricSurge(whatFinger, GameManager.Instance.player1);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if (playerHealthStats[(int)whatFinger] > 0)
                        {
                            playerHealthStats[(int)whatFinger] -= 1;
                            player.fingers[(int)whatFinger].removeCurrentSegment();
                            player.visualFingers[(int)whatFinger].removeCurrentSegment();
                            player.entireHP--;
                            player.stagHands.boneVisualEffects[(int)whatFinger].Play();
                            player.skullHands.boneVisualEffects[(int)whatFinger].Play();
                            SoundFXManager.Instance.PlayRandomSFX(SoundFXManager.Instance.boneDamage, 1);
                            player.DamageTrackedPerTurn++;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        player.fingers[(int)whatFinger].fingerShield--;
                    }
                    if (player.fingers[(int)whatFinger].fingerShield <= 0)
                    {
                        player.ringHandler.ringsActive[(int)Rings.GuardiansTouchFull, (int)whatFinger] = false;
                        player.ToggleRing(false, Rings.GuardiansTouchFull, whatFinger);
                        player.ringHandler.ringsActive[(int)Rings.GuardiansTouchFail, (int)whatFinger] = false;
                        player.ToggleRing(false, Rings.GuardiansTouchFail, whatFinger);
                    }
                }
            }
            else
            {
                playerHealthStats[(int)whatFinger] = 0;
            }

            if (playerHealthStats[(int)whatFinger] == 0)
            {
                for (int i = 0; i < 14; i++)
                {
                    if (player.ringHandler.ringsActive[i, (int)whatFinger] == true)
                    {
                        player.ringHandler.ringsActive[i, (int)whatFinger] = false;
                        player.ToggleRing(false, (Rings)i, whatFinger);
                        if (i == (int)Rings.ManaMerchantFail)
                        {
                            player.GetComponent<RingsHandler>().manaMerchantFailure = false;
                        }
                        else if (i == (int)Rings.ManaMerchantFull)
                        {
                            player.GetComponent<RingsHandler>().manaMerchantSuccess = false;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    ///  Heals 1 health to a finger
    /// </summary>
    public void HealFinger(PlayerFingers whatFinger)
    {
        if ((whatFinger != PlayerFingers.LH_Thumb) && (whatFinger != PlayerFingers.RH_Thumb))
        {
            //asumming here that most healing cant revive a finger unless you are materialize
            if (playerHealthStats[(int)whatFinger] == 1 || playerHealthStats[(int)whatFinger] == 2 || (playerHealthStats[(int)whatFinger] == 0))
            {
                playerHealthStats[(int)whatFinger] += 1;
                player.fingers[(int)whatFinger].addSegment();
                player.visualFingers[(int)whatFinger].addSegment();
                player.entireHP++;
            }
            else if ((playerHealthStats[(int)whatFinger] >= 3))
            {
                playerHealthStats[(int)whatFinger] = 3;
            }
        }
        else
        {
            //asumming here that most healing cant revive a finger unless you are materialize
            if (playerHealthStats[(int)whatFinger] == 1 || (playerHealthStats[(int)whatFinger] == 0))
            {
                playerHealthStats[(int)whatFinger] += 1;
                player.fingers[(int)whatFinger].addSegment();
                player.visualFingers[(int)whatFinger].addSegment();
                player.entireHP++;
            }
            else if ((playerHealthStats[(int)whatFinger] >= 2))
            {
                playerHealthStats[(int)whatFinger] = 2;
            }
        }

    }

    /// <summary>
    /// This gets howmany segments from a hand, TRUE is left hand, FALSE is Right hand
    /// </summary>
    /// <returns></returns>
    public int GetHowManyBonesMissing(bool isLeft)
    {
        int amtOfBones = 0;

        if (isLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                if ((PlayerFingers)i == PlayerFingers.LH_Thumb)
                {
                    amtOfBones = amtOfBones + (2 - playerHealthStats[i]);
                }
                else
                {
                    amtOfBones = amtOfBones + (3 - playerHealthStats[i]);
                }
            }
        }
        else
        {
            for (int i = 5; i < 10; i++)
            {
                if ((PlayerFingers)i == PlayerFingers.RH_Thumb)
                {
                    amtOfBones = amtOfBones + (2 - playerHealthStats[i]);
                }
                else
                {
                    amtOfBones = amtOfBones + (3 - playerHealthStats[i]);
                }
            }
        }
        return amtOfBones;
    }
}
