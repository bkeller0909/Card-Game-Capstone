using UnityEngine;
using UnityEngine.XR;

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

    [SerializeField, Tooltip("These are the prefabs for each ring")]
    public GameObject[] rings;
    [SerializeField]
    public Transform[] ringPos;
    [SerializeField]
    public Transform[] ringPosVisual;

    private void Start()
    {
        ringsActive = new bool[14, 10];
    }

    public void EffectThornsOfAgony(PlayerFingers targetFinger, PlayerManager playerTarget)
    {
        PlayerFingers randoFinger;
        if (ringsActive[(int)Rings.ThornsOfAgonyFull, (int)targetFinger])
        {
            //success does 2 damage
            randoFinger = playerTarget.GetRandomFinger();
            playerTarget.health.DamageFinger(randoFinger);
            playerTarget.health.DamageFinger(randoFinger);
        }
        else if (ringsActive[(int)Rings.ThornsOfAgonyFail, (int)targetFinger])
        {
            //fail only does 1 damage
            randoFinger = playerTarget.GetRandomFinger();
            playerTarget.health.DamageFinger(randoFinger);
        }
    }
    public void EffectGuardiansTouch(PlayerFingers targetFinger, PlayerManager playerTarget)
    {
        //block damage on target finger
        if (ringsActive[(int)Rings.GuardiansTouchFull, (int)targetFinger])
        {
            //fail blocks 1 damage
            //playerTarget.health.GuardiansTouchShield = 1;
            //playerTarget.fingers[(int)]

        }
        else if(ringsActive[(int)Rings.GuardiansTouchFail, (int)targetFinger])
        {
            //success blocks 2 damage
            //playerTarget.health.GuardiansTouchShield = 2;
        }
    }
    public void EffectSpectralChain(bool success, PlayerManager playerTarget)
    {
        //removes finger bonus
        //fail lasts 1 turn
        //success lasts 3 turns
    }
    public void EffectManaMerchant()
    {
        //gain extra mana per turn
        //lasts 8 turns
        //fail gains 1 mana per turn
        //success gains 2 mana per turn
    }
    public void EffectVengefulMirror()
    {
        //reflecs first damage the hand gets per round
        //fail reflects damage 
        //success does double damage
    }
    public void EffectVampiricSurge()
    {
        //put on opponent and if that finger is damaged then heal 
        //fail you heal 1
        //success you heal 2
    }
    public void EffectVeilOfFortitude()
    {
        //reduce damage taken on hand summoned
        //fail damage taken is cut in half
        //success take no damage
    }
}
