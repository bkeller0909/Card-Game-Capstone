/// <summary>
//----------------------------------------------------------------
//  OG Author:    Cooper
//  Title:        ParticleManager
//  Date Created: 02/05/2025
//  Purpose:      This is to Initialize and Cast spell particles according to the spell provided
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// This is to Initialize and Cast spell particles according to the spell provided
/// </summary>
public class ParticleManger : MonoBehaviour
{
    [SerializeField] private List<VisualEffect> FullEffects;
    [SerializeField] private List<VisualEffect> lowEffects;

    [Header("End Position for the particles")]
    [Tooltip("The Position for the particle to end at")]
    [SerializeField] Transform EndPosition;

    [Header("The Adjasent positions for particles to branch")]
    [SerializeField] Transform adjasentPosition1;
    [SerializeField] Transform adjasentPosition2;
    [SerializeField] Transform adjasentPosition3;

    int spellToCastIndex;

    /// <summary>
    /// Call this function with the "Spell Name" and the "Target Finger" to play the desired particle
    /// </summary>
    public void StartParticle(SpellNames spellToCast, PlayerFingers targetFinger, PlayerType playerCasting)
    {
        spellToCastIndex = -1;

        switch (spellToCast)
        {
            //Attack Spells |=========================================
            case SpellNames.FireBolt:
                FullEffects[0].gameObject.SetActive(true);
                FullEffects[0].Play();
                break;

            case SpellNames.Rockthrow:

                break;

            case SpellNames.RighteousEnvy:

                break;

            case SpellNames.LefteousEnvy:

                break;

            case SpellNames.Icicles:

                break;

            case SpellNames.CollectorsCurse:

                break;

            case SpellNames.StaticBlast:

                break;

            case SpellNames.Quake:

                break;

            case SpellNames.RightingBolt:

                break;

            case SpellNames.LeftningBolt:

                break;

            case SpellNames.TidalWave:

                break;

            case SpellNames.PointerOfDeath:

                break;

            //Restoration Spells |=========================================

            case SpellNames.ForTheCause:

                break;

            case SpellNames.QuickHeal:

                break;

            case SpellNames.ThumbsUp:

                break;

            case SpellNames.LifeDrain:

                break;

            case SpellNames.CursedConversion:

                break;

            case SpellNames.GreenThumb:

                break;

            case SpellNames.Materialise:

                break;

            case SpellNames.EchoingMana:

                break;

            //Ring Spells |=========================================

            case SpellNames.ThornsOfAgony:
                break;

            case SpellNames.GuardiansTouch:
                break;

            case SpellNames.SpectralChain:
                break;

            case SpellNames.ManaMerchant:
                break;

            case SpellNames.VengefulMirror:
                break;

            case SpellNames.VampiricSurge:
                break;

            case SpellNames.VeilOfFortitude:
                break;
        }
    }
}
