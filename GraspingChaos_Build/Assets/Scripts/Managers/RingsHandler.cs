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

    public bool vengfulMirrorLeft;
    public bool vengfulMirrorRight;

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
            for(int i = 0; i < 2; i++)
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
        if(manaMerchantSuccess)
        {
            gameObject.GetComponent<PlayerManager>().GetMana(2);
        }
        else if(manaMerchantFailure)
        {
            gameObject.GetComponent<PlayerManager>().GetMana(1);
        }
    }
    public void EffectVengefulMirror(PlayerFingers targetFinger, PlayerManager playerTarget)
    {
        // Game manager or instance variable that tracks first damage 
        // on the attack spells track the total damage based on the card value (will have to implement)
        //on the ring state dupicate the value if its a success
        //this function is over all not really needed
        if ((int)targetFinger >= 0 && (int)targetFinger < 4)
        {
            //left hand placement
            vengfulMirrorLeft = true;
        }
        else if ((int)targetFinger >= 5 && (int)targetFinger < 9)
        {
            //left hand placement
            vengfulMirrorRight = true;
        }


        if (ringsActive[(int)Rings.VengefulMirrorFull, (int)targetFinger])
        {
            //easy - just revert the player that takes the damage and insted of target based make it random based
        }
        else if (ringsActive[(int)Rings.VengefulMirrorFail, (int)targetFinger])
        {
            //kinda easy kinda hard - just revert the player that takes the damage, make it duplicate and insted of target based make it random based
        }


    }
    public void EffectVampiricSurge(PlayerFingers targetFinger, PlayerManager playerTarget)
    {
        PlayerFingers randoFinger;
        //I need to make a function that finds only fingers that are not full HP
        if (ringsActive[(int)Rings.VampiricSurgeFull, (int)targetFinger])
        {
            //success gains 2 hp
            randoFinger = playerTarget.GetRandomFinger(PlayerFingers.none);
            playerTarget.health.HealFinger(randoFinger);
            playerTarget.health.HealFinger(randoFinger);
        }
        else if (ringsActive[(int)Rings.VampiricSurgeFail, (int)targetFinger])
        {
            //fail only gains 1 HP
            randoFinger = playerTarget.GetRandomFinger(PlayerFingers.none);
            playerTarget.health.HealFinger(randoFinger);
        }
    }
    public void EffectVeilOfFortitude(PlayerFingers targetFinger, PlayerManager playerTarget)
    {
        //reduce damage taken on hand summoned
        //fail damage taken is cut in half
        //success take no damage

        if ((int)targetFinger >= 0 && (int)targetFinger < 4)
        {
            //left hand placement
            veilOfFortitudeLeft = true;
        }
        else if ((int)targetFinger >= 5 && (int)targetFinger < 9)
        {
            //right hand placement
            veilOfFortitudeRight = true;
        }


        if (ringsActive[(int)Rings.VeilOfFortitudeFull, (int)targetFinger])
        {
            //easy - simply block damage with previous bool
        }
        else if (ringsActive[(int)Rings.VeilOfFortitudeFail, (int)targetFinger])
        {
            //hard - figure out a way to half the damage (with some spells and the way the damage function is set I might have to re-write some functionality)
        }
    }
}
