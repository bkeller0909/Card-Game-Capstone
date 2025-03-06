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

    private void Start()
    {
        ringsActive = new bool[7, 10];
    }

    public void EffectThornsOfAgony()
    {

    }
    public void EffectGuardiansTouch()
    {

    }
    public void EffectSpectralChain()
    {

    }
    public void EffectManaMerchant()
    {

    }
    public void EffectVengefulMirror()
    {

    }
    public void EffectVampiricSurge()
    {

    }
    public void EffectVeilOfFortitude()
    {

    }
}
