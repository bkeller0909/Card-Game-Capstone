using UnityEngine;

//----------------------------------------------------------------
//  OG Author:    Wyatt
// Contributors:  Sebastian
//  Title:        Rings Handler
//  Date Created: 03/06/2025
//  Purpose:      This holds the functions for every ring and wheather they are active or not 
//  Instance?     Technicly Yes
//-----------------------------------------------------------------


/// <summary>
/// This holds the functions for every ring and wheather they are active or not 
/// </summary>
public class RingsHandler : MonoBehaviour
{
    public bool[,] ringsActive;

    public int[] ringStartRound;

    public bool veilOfFortitudeLeft;
    public bool veilOfFortitudeRight;
    public bool veilOfFortitudeLeftFail;
    public bool veilOfFortitudeRightFail;

    public bool vengfulMirrorLeft;
    public bool vengfulMirrorRight;
    public bool vengfulFirst;

    public bool manaMerchantSuccess;
    public bool manaMerchantFailure;

    [SerializeField, Tooltip("This is the amount of rounds the rings lasts")]
    public int[] defualtRingLength;

    private void Start()
    {
        ringsActive = new bool[14, 10];
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ringsActive[i, j] = false;
            }
        }

        ringStartRound = new int[14];
        for (int i = 0; i < 14; i++)
        {
            ringStartRound[i] = -1;
        }
        manaMerchantFailure = false;
        manaMerchantSuccess = false;
        vengfulFirst = false;
    }

    public void EffectThornsOfAgony(PlayerFingers targetFinger, PlayerManager targetPlayer)
    {
        PlayerFingers randoFinger;
        if (ringsActive[(int)Rings.ThornsOfAgonyFull, (int)targetFinger])
        {
            //success does 2 damage
            randoFinger = targetPlayer.GetRandomFinger(PlayerFingers.none);
            //targetPlayer.health.DamageFinger(randoFinger);
            //targetPlayer.health.DamageFinger(randoFinger);
            for (int i = 0; i < 2; i++)
            {
                targetPlayer.health.playerHealthStats[(int)randoFinger] -= 1;
                targetPlayer.fingers[(int)randoFinger].removeCurrentSegment();
                targetPlayer.visualFingers[(int)randoFinger].removeCurrentSegment();
                targetPlayer.entireHP--;
            }
        }
        else if (ringsActive[(int)Rings.ThornsOfAgonyFail, (int)targetFinger])
        {
            //fail only does 1 damage
            randoFinger = targetPlayer.GetRandomFinger(PlayerFingers.none);
            targetPlayer.health.playerHealthStats[(int)randoFinger] -= 1;
            targetPlayer.fingers[(int)randoFinger].removeCurrentSegment();
            targetPlayer.visualFingers[(int)randoFinger].removeCurrentSegment();
            targetPlayer.entireHP--;
        }
    }
    public void EffectGuardiansTouch(PlayerFingers targetFinger)
    {
        //block damage on target finger
        if (ringsActive[(int)Rings.GuardiansTouchFull, (int)targetFinger])
        {
            //blocks 2 damage
            gameObject.GetComponent<PlayerManager>().fingers[(int)targetFinger].fingerShield = 2;

        }
        else if (ringsActive[(int)Rings.GuardiansTouchFail, (int)targetFinger])
        {
            //blocks 1 damage
            gameObject.GetComponent<PlayerManager>().fingers[(int)targetFinger].fingerShield = 1;
        }
    }

    public void GuardiansTouchStolenOrOver(PlayerFingers targetFinger)
    {
        gameObject.GetComponent<PlayerManager>().fingers[(int)targetFinger].fingerShield = 0;
    }

    public void EffectSpectralChain(PlayerManager playerTarget)
    {
        //removes finger bonus
        //fail lasts 1 turn
        //success lasts 3 turns
    }
    //call this on deal stats, if the ring is active the function will know, and deal mana acordingly
    public void EffectManaMerchant()
    {
        if (manaMerchantSuccess)
        {
            gameObject.GetComponent<PlayerManager>().GetMana(2);
        }
        else if (manaMerchantFailure)
        {
            gameObject.GetComponent<PlayerManager>().GetMana(1);
        }
    }
    
    //TODO
    public void ApplyVengFulMirror(PlayerFingers targetFinger)
    {
        vengfulFirst = true;
        if ((int)targetFinger >= 0 && (int)targetFinger <= 4)
        {
            //left hand placement
            vengfulMirrorLeft = true;
        }
        else if ((int)targetFinger >= 5 && (int)targetFinger <= 9)
        {
            //left hand placement
            vengfulMirrorRight = true;
        }
    }

    public void EffectVengFulMirror(PlayerManager targetPlayer, int DamageAmount)
    {
        if (vengfulFirst)
        {
            PlayerFingers targetFinger = PlayerFingers.none;
            for (int i = 0; i < DamageAmount; i++)
            {
                targetFinger = targetPlayer.GetRandomFinger(PlayerFingers.none);
                targetPlayer.health.DamageFinger(targetFinger);
                targetPlayer.ResetHandAnimations();
                if ((int)targetFinger >= 0 && (int)targetFinger <= 4)
                {
                    targetPlayer.PlayerHands.SetTrigger("LeftHandDamaged");
                    targetPlayer.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                }
                else
                {
                    targetPlayer.PlayerHands.SetTrigger("RightHandDamaged");
                    targetPlayer.PlayerFakeHands.SetTrigger("RightHandDamaged");
                }
                targetPlayer.BackToIDLE();
            }
        }
        vengfulFirst = false;
    }

    public void EffectVampiricSurge(PlayerFingers targetFinger, PlayerManager playerTarget)
    {
        PlayerFingers randoFinger;
        //I need to make a function that finds only fingers that are not full HP
        if (ringsActive[(int)Rings.VampiricSurgeFull, (int)targetFinger])
        {
            //success gains 2 hp
            randoFinger = playerTarget.GetRandomHurtFingers(PlayerFingers.none);
            playerTarget.health.HealFinger(randoFinger);
            playerTarget.health.HealFinger(randoFinger);
        }
        else if (ringsActive[(int)Rings.VampiricSurgeFail, (int)targetFinger])
        {
            //fail only gains 1 HP
            randoFinger = playerTarget.GetRandomHurtFingers(PlayerFingers.none);
            playerTarget.health.HealFinger(randoFinger);
        }
    }
    public void EffectVeilOfFortitude(PlayerFingers targetFinger)
    {

        if (ringsActive[(int)Rings.VeilOfFortitudeFull, (int)targetFinger])
        {
            if ((int)targetFinger >= 0 && (int)targetFinger <= 4)
            {
                //left hand placement
                veilOfFortitudeLeft = true;
            }
            else if ((int)targetFinger >= 5 && (int)targetFinger <= 9)
            {
                //right hand placement
                veilOfFortitudeRight = true;
            }
        }
        if (ringsActive[(int)Rings.VeilOfFortitudeFail, (int)targetFinger])
        {
            //hard - figure out a way to half the damage (with some spells and the way the damage function is set I might have to re-write some functionality)
            if ((int)targetFinger >= 0 && (int)targetFinger <= 4)
            {
                //left hand placement
                veilOfFortitudeLeftFail = true;
            }
            else if ((int)targetFinger >= 5 && (int)targetFinger <= 9)
            {
                //right hand placement
                veilOfFortitudeRightFail = true;
            }

        }
    }
}
