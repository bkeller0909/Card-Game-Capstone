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

    public void EffectThornsOfAgony()
    {

    }
    public void EffectGuardiansTouch()
    {
        //block damage on target finger
        //fail blocks 1 damage
        //success blocks 2 damage
    }
    public void EffectSpectralChain()
    {
        //
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

    }
}
