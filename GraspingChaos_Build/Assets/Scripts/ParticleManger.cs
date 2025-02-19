using System.Collections;

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
    private static ParticleManger instance;
    public static ParticleManger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ParticleManger>();
            }

            if (!instance)
            {
                Debug.LogError("No Game Manager Present !!!");
            }

            return instance;

        }
    }

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

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    /// <summary>
    /// Call this function with the "Spell Name" and the "Target Finger" to play the desired particle
    /// </summary>
    public void StartParticle(SpellNames spellToCast, PlayerFingers targetFinger, PlayerManager playerCasting)
    {
        spellToCastIndex = -1;

        switch (spellToCast)
        {
            //Attack Spells |=========================================
            case SpellNames.FireBolt:
                FullEffects[0].gameObject.SetActive(true);
                FullEffects[0].Play();
                StartCoroutine(DisableSpell(8, 0, playerCasting));
                break;

            case SpellNames.Rockthrow:
                FullEffects[1].gameObject.SetActive(true);
                FullEffects[1].Play();
                StartCoroutine(DisableSpell(8, 1, playerCasting));
                break;

            case SpellNames.RighteousEnvy:
                FullEffects[2].gameObject.SetActive(true);
                FullEffects[2].Play();
                StartCoroutine(DisableSpell(8, 2, playerCasting));
                break;

            case SpellNames.LefteousEnvy:
                FullEffects[2].gameObject.SetActive(true);
                FullEffects[2].Play();
                StartCoroutine(DisableSpell(8, 2, playerCasting));
                break;

            case SpellNames.Icicles:
                FullEffects[3].gameObject.SetActive(true);
                FullEffects[3].Play();
                StartCoroutine(DisableSpell(8, 3, playerCasting));
                break;

            case SpellNames.CollectorsCurse:
                FullEffects[4].gameObject.SetActive(true);
                FullEffects[4].Play();
                StartCoroutine(DisableSpell(8, 4, playerCasting));
                break;

            case SpellNames.StaticBlast:
                FullEffects[5].gameObject.SetActive(true);
                FullEffects[5].Play();
                StartCoroutine(DisableSpell(8, 5, playerCasting));
                break;

            case SpellNames.Quake:
                FullEffects[6].gameObject.SetActive(true);
                FullEffects[6].Play();
                StartCoroutine(DisableSpell(8, 6, playerCasting));
                break;

            case SpellNames.RightingBolt:
                FullEffects[7].gameObject.SetActive(true);
                FullEffects[7].Play();
                StartCoroutine(DisableSpell(8, 7, playerCasting));
                break;

            case SpellNames.LeftningBolt:
                FullEffects[7].gameObject.SetActive(true);
                FullEffects[7].Play();
                StartCoroutine(DisableSpell(8, 7, playerCasting));
                break;

            case SpellNames.TidalWave:
                FullEffects[8].gameObject.SetActive(true);
                FullEffects[8].Play();
                StartCoroutine(DisableSpell(8, 8, playerCasting));
                break;

            case SpellNames.PointerOfDeath:
                FullEffects[9].gameObject.SetActive(true);
                FullEffects[9].Play();
                StartCoroutine(DisableSpell(8, 9, playerCasting));
                break;

            //Restoration Spells |=========================================

            case SpellNames.ForTheCause:
                FullEffects[10].gameObject.SetActive(true);
                FullEffects[10].Play();
                StartCoroutine(DisableSpell(8, 10, playerCasting));
                break;

            case SpellNames.QuickHeal:
                FullEffects[11].gameObject.SetActive(true);
                FullEffects[11].Play();
                StartCoroutine(DisableSpell(8, 11, playerCasting));
                break;

            case SpellNames.ThumbsUp:
                FullEffects[12].gameObject.SetActive(true);
                FullEffects[12].Play();
                StartCoroutine(DisableSpell(8, 12, playerCasting));
                break;

            case SpellNames.LifeDrain:
                FullEffects[13].gameObject.SetActive(true);
                FullEffects[13].Play();
                StartCoroutine(DisableSpell(8, 13, playerCasting));
                break;

            case SpellNames.CursedConversion:
                FullEffects[14].gameObject.SetActive(true);
                FullEffects[14].Play();
                StartCoroutine(DisableSpell(8, 14, playerCasting));
                break;

            case SpellNames.GreenThumb:
                FullEffects[15].gameObject.SetActive(true);
                FullEffects[15].Play();
                StartCoroutine(DisableSpell(8, 15, playerCasting));
                break;

            case SpellNames.Materialise:
                FullEffects[16].gameObject.SetActive(true);
                FullEffects[16].Play();
                StartCoroutine(DisableSpell(8, 16, playerCasting));
                break;

            case SpellNames.EchoingMana:
                FullEffects[17].gameObject.SetActive(true);
                FullEffects[17].Play();
                StartCoroutine(DisableSpell(8, 17, playerCasting));
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

    IEnumerator DisableSpell(int secondsToWait, int spellIndex, PlayerManager currentPlayer)
    {
        yield return new WaitForSeconds(secondsToWait);

        FullEffects[spellIndex].gameObject.SetActive(false);

        if (currentPlayer == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex])
        {
            GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
            GameManager.Instance.coroutineWaitP1 = true;
        }
        else if (currentPlayer == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex])
        {
            GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
            GameManager.Instance.coroutineWaitP2 = true;
        }
    }
}
