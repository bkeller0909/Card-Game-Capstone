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

    //Private Variables
    //==============================================================================================================

    //List of different spell varients
    //==============================================================================================================

    [SerializeField] private List<VisualEffect> FullEffects;
    [SerializeField] private List<VisualEffect> medEffects;
    [SerializeField] private List<VisualEffect> lowEffects;
    [SerializeField] private VisualEffect ringPlacementParticle;
    [SerializeField] private VisualEffect ringPlacementParticleFail;
    [SerializeField] private VisualEffect vengfulInpact;
    [SerializeField] private VisualEffect thronsInpact;
    [SerializeField] private VisualEffect vampiricInpact;
    [SerializeField] private VisualEffect P1QTEHalf;
    [SerializeField] private VisualEffect P1QTEFull;
    [SerializeField] private VisualEffect P2QTEHalf;
    [SerializeField] private VisualEffect P2QTEFull;

    //Custom colors and gradients for select spells
    //==============================================================================================================

    [SerializeField] private Gradient cursedConversionP1Gradient;
    [SerializeField] private Gradient cursedConversionExplosionP1Gradient;
    [SerializeField] private Color cursedConversionP1Color;

    [SerializeField] private Gradient cursedConversionP2Gradient;
    [SerializeField] private Gradient cursedConversionExplosionP2Gradient;
    [SerializeField] private Color cursedConversionP2Color;

    //Particle Positioning variables
    //==============================================================================================================

    [SerializeField] private List<Transform> player1FingerPositions;
    [SerializeField] private List<Transform> player2FingerPositions;

    [Header("End Position for the particles")]
    [SerializeField] Transform EnemyPosition;
    [SerializeField] Transform PlayerPosition;
    [SerializeField] Transform ManaBottle;

    [SerializeField] Transform P1ForcedPosition;
    [SerializeField] Transform P2ForcedPosition;

    [SerializeField] Transform P1LeftHand;
    [SerializeField] Transform P1RightHand;

    [SerializeField] Transform P2LeftHand;
    [SerializeField] Transform P2RightHand;




    [Header("Extra Positions for particles")]
    [SerializeField] Transform adjasentPosition1;
    [SerializeField] Transform adjasentPosition2;
    [SerializeField] Transform adjasentPosition3;
    [SerializeField] Transform halfWay1;
    [SerializeField] Transform halfWay2;
    [SerializeField] Transform Extra;
    [SerializeField] Transform TableCenter;

    [SerializeField] private Transform halfway1Cache;
    [SerializeField] private Transform halfway2Cache;

    [SerializeField] private Transform p1ManaBottleCache;
    [SerializeField] private Transform p2ManaBottleCache;

    //Public Variables
    //==============================================================================================================

    public bool spawnExtra1;
    public bool spawnExtra2;
    public bool spawnExtra3;

    public PlayerFingers particleAdjacent1;
    public PlayerFingers particleAdjacent2;
    public PlayerFingers particleAdjacent3;

    int spellToCastIndex;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        FullEffects[5].SetBool("Extra 1", spawnExtra1);
        FullEffects[5].SetBool("Extra 2", spawnExtra2);
    }


    /// <summary>
    /// Call this function with the "Spell Name" and the "Target Finger" to play the desired particle
    /// </summary>
    public void StartParticle(SpellNames spellToCast, PlayerFingers targetFinger, PlayerManager playerCasting, int effectLevel)
    {
        spellToCastIndex = -1;
        switch (effectLevel)
        {
            //================================================ SPELL FAIL ===========================================================
            case 1:
                switch (spellToCast)
                {
                    //Attack Spells |=========================================
                    case SpellNames.FireBolt:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[0].Play();
                        StartCoroutine(DisableSpell(2, 0, playerCasting));
                        break;

                    case SpellNames.Rockthrow:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[1].Play();
                        StartCoroutine(DisableSpell(2, 1, playerCasting));
                        break;

                    case SpellNames.RighteousEnvy:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            Extra.position = player1FingerPositions[Random.Range(5, 3)].position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            Extra.position = player2FingerPositions[Random.Range(5, 3)].position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[2].Play();
                        StartCoroutine(DisableSpell(3, 3, playerCasting));
                        break;

                    case SpellNames.LefteousEnvy:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            Extra.position = player1FingerPositions[Random.Range(0, 4)].position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            Extra.position = player2FingerPositions[Random.Range(0, 4)].position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[2].Play();
                        StartCoroutine(DisableSpell(3, 3, playerCasting));
                        break;

                    case SpellNames.Icicles:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[3].Play();
                        StartCoroutine(DisableSpell(2.2f, 3, playerCasting));
                        break;

                    case SpellNames.CollectorsCurse:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        FullEffects[4].Play();
                        StartCoroutine(DisableSpell(3, 4, playerCasting));
                        break;

                    case SpellNames.StaticBlast:
                        FullEffects[5].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            spawnExtra1 = false;
                            spawnExtra2 = false;
                            PlayerPosition.position = P1RightHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            adjasentPosition1.position = player2FingerPositions[(int)particleAdjacent1].position;
                            adjasentPosition2.position = player2FingerPositions[(int)particleAdjacent2].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            spawnExtra1 = false;
                            spawnExtra2 = false;
                            PlayerPosition.position = P2RightHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            adjasentPosition1.position = player1FingerPositions[(int)particleAdjacent1].position;
                            adjasentPosition2.position = player1FingerPositions[(int)particleAdjacent2].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        FullEffects[5].Play();
                        StartCoroutine(DisableSpell(1.5f, 5, playerCasting));
                        break;

                    case SpellNames.Quake:
                        lowEffects[6].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            FullEffects[6].gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                        else
                        {
                            FullEffects[6].gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        lowEffects[6].Play();
                        StartCoroutine(DisableSpell(1.6f, 6, playerCasting));
                        break;

                    case SpellNames.RightingBolt:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1RightHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2RightHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        lowEffects[7].Play();
                        StartCoroutine(DisableSpell(1.6f, 7, playerCasting));
                        break;

                    case SpellNames.LeftningBolt:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1LeftHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2LeftHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        lowEffects[7].Play();
                        StartCoroutine(DisableSpell(1.6f, 7, playerCasting));
                        break;

                    case SpellNames.TidalWave:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            lowEffects[8].gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            lowEffects[8].gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        lowEffects[8].Play();
                        StartCoroutine(DisableSpell(1.6f, 8, playerCasting));
                        break;

                    case SpellNames.PointerOfDeath:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[9].Play();
                        StartCoroutine(DisableSpell(4, 9, playerCasting));
                        break;

                    //Restoration Spells |=========================================

                    case SpellNames.ForTheCause:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            lowEffects[10].SetGradient("Particle Color Gradient", cursedConversionP1Gradient);
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                            ManaBottle.position = p1ManaBottleCache.position;
                        }
                        else
                        {
                            lowEffects[10].SetGradient("Particle Color Gradient", cursedConversionP2Gradient);
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                            ManaBottle.position = p2ManaBottleCache.position;
                        }
                        lowEffects[10].Play();
                        StartCoroutine(DisableSpell(1.8f, 10, playerCasting));
                        break;

                    case SpellNames.QuickHeal:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[11].Play();
                        StartCoroutine(DisableSpell(3.5f, 11, playerCasting));
                        break;

                    case SpellNames.ThumbsUp:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[4].position;
                            Extra.position = player1FingerPositions[5].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[4].position;
                            Extra.position = player2FingerPositions[5].position;
                        }
                        lowEffects[12].Play();
                        StartCoroutine(DisableSpell(5, 12, playerCasting));
                        break;

                    case SpellNames.LifeDrain:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[13].Play();
                        StartCoroutine(DisableSpell(4, 13, playerCasting));
                        break;

                    case SpellNames.CursedConversion:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            lowEffects[14].SetGradient("Spell Color Gradient", cursedConversionP1Gradient);
                            lowEffects[14].SetGradient("Spell Color Gradient", cursedConversionExplosionP1Gradient);
                            lowEffects[14].SetVector4("Orb Color", cursedConversionP1Color);
                            ManaBottle.position = p1ManaBottleCache.position;
                        }
                        else
                        {
                            lowEffects[14].SetGradient("Spell Color Gradient", cursedConversionP2Gradient);
                            lowEffects[14].SetGradient("Spell Color Gradient", cursedConversionExplosionP2Gradient);
                            lowEffects[14].SetVector4("Orb Color", cursedConversionP2Color);
                            ManaBottle.position = p2ManaBottleCache.position;
                        }
                        lowEffects[14].Play();
                        StartCoroutine(DisableSpell(6.5f, 14, playerCasting));
                        break;

                    case SpellNames.GreenThumb:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[15].Play();
                        StartCoroutine(DisableSpell(5.5f, 15, playerCasting));
                        break;

                    case SpellNames.Materialise:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        lowEffects[16].Play();
                        StartCoroutine(DisableSpell(4.5f, 16, playerCasting));
                        break;

                    case SpellNames.EchoingMana:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            EnemyPosition.position = P2ForcedPosition.position;
                            ManaBottle.position = p1ManaBottleCache.position;
                        }
                        else
                        {
                            EnemyPosition.position = P1ForcedPosition.position;
                            ManaBottle.position = p2ManaBottleCache.position;
                        }
                        lowEffects[17].Play();
                        StartCoroutine(DisableSpell(4f, 17, playerCasting));
                        break;
                }
                break;
            //================================================ SPELL medium ===========================================================
            case 2:
                switch (spellToCast)
                {
                    //Attack Spells |=========================================

                    case SpellNames.RightingBolt:
                        medEffects[0].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1RightHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2RightHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        medEffects[0].Play();
                        StartCoroutine(DisableSpell(2, 7, playerCasting));
                        break;

                    case SpellNames.LeftningBolt:
                        medEffects[0].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1LeftHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2LeftHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        medEffects[0].Play();
                        StartCoroutine(DisableSpell(2, 7, playerCasting));
                        break;

                    case SpellNames.PointerOfDeath:
                        medEffects[1].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        medEffects[1].Play();
                        StartCoroutine(DisableSpell(4.5f, 9, playerCasting));
                        break;

                    //Restoration Spells |=========================================

                    case SpellNames.ForTheCause:
                        medEffects[2].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            medEffects[2].SetGradient("Particle Color Gradient", cursedConversionP1Gradient);
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                            ManaBottle.position = p1ManaBottleCache.position;
                        }
                        else
                        {
                            medEffects[2].SetGradient("Particle Color Gradient", cursedConversionP2Gradient);
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                            ManaBottle.position = p2ManaBottleCache.position;
                        }
                        medEffects[2].Play();
                        StartCoroutine(DisableSpell(1.8f, 10, playerCasting));
                        break;

                    case SpellNames.Materialise:
                        medEffects[3].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        medEffects[3].Play();
                        StartCoroutine(DisableSpell(4.5f, 16, playerCasting));
                        break;

                }
                break;
            //================================================ SPELL SUCCESS ===========================================================
            case 3:
                switch (spellToCast)
                {
                    //Attack Spells |=========================================
                    case SpellNames.FireBolt:
                        FullEffects[0].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[0].Play();
                        StartCoroutine(DisableSpell(2, 0, playerCasting));
                        break;

                    case SpellNames.Rockthrow:
                        FullEffects[1].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[1].Play();
                        StartCoroutine(DisableSpell(2, 1, playerCasting));
                        break;

                    case SpellNames.RighteousEnvy:
                        FullEffects[2].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            Extra.position = player1FingerPositions[Random.Range(5, 9)].position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            Extra.position = player2FingerPositions[Random.Range(5, 9)].position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[2].Play();
                        StartCoroutine(DisableSpell(6f, 2, playerCasting));
                        break;

                    case SpellNames.LefteousEnvy:
                        FullEffects[2].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            Extra.position = player1FingerPositions[Random.Range(0, 4)].position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            Extra.position = player2FingerPositions[Random.Range(0, 4)].position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[2].Play();
                        StartCoroutine(DisableSpell(6f, 2, playerCasting));
                        break;

                    case SpellNames.Icicles:
                        FullEffects[3].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[3].Play();
                        StartCoroutine(DisableSpell(2.2f, 3, playerCasting));
                        break;

                    case SpellNames.CollectorsCurse:
                        FullEffects[4].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        FullEffects[4].Play();
                        StartCoroutine(DisableSpell(3, 4, playerCasting));
                        break;

                    case SpellNames.StaticBlast:
                        FullEffects[5].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1RightHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            adjasentPosition1.position = player2FingerPositions[(int)particleAdjacent1].position;
                            adjasentPosition2.position = player2FingerPositions[(int)particleAdjacent2].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2RightHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            adjasentPosition1.position = player1FingerPositions[(int)particleAdjacent1].position;
                            adjasentPosition2.position = player1FingerPositions[(int)particleAdjacent2].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        FullEffects[5].Play();
                        StartCoroutine(DisableSpell(1.5f, 5, playerCasting));
                        break;

                    case SpellNames.Quake:
                        FullEffects[6].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            FullEffects[6].gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                        else
                        {
                            FullEffects[6].gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        FullEffects[6].Play();
                        StartCoroutine(DisableSpell(1.6f, 6, playerCasting));
                        break;

                    case SpellNames.RightingBolt:
                        FullEffects[7].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1RightHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2RightHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        FullEffects[7].Play();
                        StartCoroutine(DisableSpell(1.6f, 7, playerCasting));
                        break;

                    case SpellNames.LeftningBolt:
                        FullEffects[7].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1LeftHand.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                        }
                        else
                        {
                            PlayerPosition.position = P2LeftHand.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                        }
                        FullEffects[7].Play();
                        StartCoroutine(DisableSpell(1.6f, 7, playerCasting));
                        break;

                    case SpellNames.TidalWave:
                        FullEffects[8].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            FullEffects[8].gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            FullEffects[8].gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        FullEffects[8].Play();
                        StartCoroutine(DisableSpell(1.6f, 8, playerCasting));
                        break;

                    case SpellNames.PointerOfDeath:
                        FullEffects[9].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[9].Play();
                        StartCoroutine(DisableSpell(4.5f, 9, playerCasting));
                        break;

                    //Restoration Spells |=========================================

                    case SpellNames.ForTheCause:
                        FullEffects[10].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            FullEffects[10].SetGradient("Particle Color Gradient", cursedConversionP1Gradient);
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                            ManaBottle.position = p1ManaBottleCache.position;
                        }
                        else
                        {
                            FullEffects[10].SetGradient("Particle Color Gradient", cursedConversionP2Gradient);
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                            ManaBottle.position = p2ManaBottleCache.position;
                        }
                        FullEffects[10].Play();
                        StartCoroutine(DisableSpell(1.8f, 10, playerCasting));
                        break;

                    case SpellNames.QuickHeal:
                        FullEffects[11].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[11].Play();
                        StartCoroutine(DisableSpell(3.5f, 11, playerCasting));
                        break;

                    case SpellNames.ThumbsUp:
                        FullEffects[12].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[4].position;
                            Extra.position = player1FingerPositions[5].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[4].position;
                            Extra.position = player2FingerPositions[5].position;
                        }
                        FullEffects[12].Play();
                        StartCoroutine(DisableSpell(5, 12, playerCasting));
                        break;

                    case SpellNames.LifeDrain:
                        FullEffects[13].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = P1ForcedPosition.position;
                            EnemyPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = P2ForcedPosition.position;
                            EnemyPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[13].Play();
                        StartCoroutine(DisableSpell(4, 13, playerCasting));
                        break;

                    case SpellNames.CursedConversion:
                        FullEffects[14].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {

                            ManaBottle.position = p1ManaBottleCache.position;
                            FullEffects[14].SetGradient("Spell Color Gradient", cursedConversionP1Gradient);
                            FullEffects[14].SetGradient("Spell Color Gradient", cursedConversionExplosionP1Gradient);
                            FullEffects[14].SetVector4("Orb Color", cursedConversionP1Color);
                        }
                        else
                        {
                            ManaBottle.position = p2ManaBottleCache.position;
                            FullEffects[14].SetGradient("Spell Color Gradient", cursedConversionP2Gradient);
                            FullEffects[14].SetGradient("Spell Color Gradient", cursedConversionExplosionP2Gradient);
                            FullEffects[14].SetVector4("Orb Color", cursedConversionP2Color);
                        }
                        FullEffects[14].Play();
                        StartCoroutine(DisableSpell(6.5f, 14, playerCasting));
                        break;

                    case SpellNames.GreenThumb:
                        FullEffects[15].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            halfWay1.position = halfway1Cache.position;
                            halfWay2.position = halfway2Cache.position;
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            halfWay1.position = halfway2Cache.position;
                            halfWay2.position = halfway1Cache.position;
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[15].Play();
                        StartCoroutine(DisableSpell(5.5f, 15, playerCasting));
                        break;

                    case SpellNames.Materialise:
                        FullEffects[16].gameObject.SetActive(true);
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        FullEffects[16].Play();
                        StartCoroutine(DisableSpell(4.5f, 16, playerCasting));
                        break;

                    case SpellNames.EchoingMana:

                        if (playerCasting == GameManager.Instance.player1)
                        {
                            EnemyPosition.position = P2ForcedPosition.position;
                            ManaBottle.position = p1ManaBottleCache.position;
                        }
                        else
                        {
                            EnemyPosition.position = P1ForcedPosition.position;
                            ManaBottle.position = p2ManaBottleCache.position;
                        }
                        FullEffects[17].Play();
                        StartCoroutine(DisableSpell(4f, 17, playerCasting));
                        break;
                }
                break;
            //================================================ RING ===========================================================
            case 4:
                switch (spellToCast)
                {
                    case SpellNames.ThornsOfAgony:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticle.Play();
                        StartCoroutine(DisableSpell(1.5f, 18, playerCasting));
                        break;
                    case SpellNames.GuardiansTouch:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticle.Play();
                        StartCoroutine(DisableSpell(1.5f, 19, playerCasting));
                        break;
                    case SpellNames.ManaMerchant:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticle.Play();
                        StartCoroutine(DisableSpell(1.5f, 20, playerCasting));
                        break;
                    case SpellNames.SpectralChain:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticle.Play();
                        StartCoroutine(DisableSpell(1.5f, 21, playerCasting));
                        break;
                    case SpellNames.VampiricSurge:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticle.Play();
                        StartCoroutine(DisableSpell(1.5f, 22, playerCasting));
                        break;
                    case SpellNames.VengefulMirror:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticle.Play();
                        StartCoroutine(DisableSpell(1.5f, 23, playerCasting));
                        break;
                    case SpellNames.VeilOfFortitude:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticle.Play();
                        StartCoroutine(DisableSpell(1.5f, 24, playerCasting));
                        break;
                }
                break;
            //================================================ RING FAIL ===========================================================
            case 5:
                switch (spellToCast)
                {
                    case SpellNames.ThornsOfAgony:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticleFail.Play();
                        StartCoroutine(DisableSpell(1.5f, 18, playerCasting));
                        break;
                    case SpellNames.GuardiansTouch:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticleFail.Play();
                        StartCoroutine(DisableSpell(1.5f, 19, playerCasting));
                        break;
                    case SpellNames.ManaMerchant:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticleFail.Play();
                        StartCoroutine(DisableSpell(1.5f, 20, playerCasting));
                        break;
                    case SpellNames.SpectralChain:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticleFail.Play();
                        StartCoroutine(DisableSpell(1.5f, 21, playerCasting));
                        break;
                    case SpellNames.VampiricSurge:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticleFail.Play();
                        StartCoroutine(DisableSpell(1.5f, 22, playerCasting));
                        break;
                    case SpellNames.VengefulMirror:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticleFail.Play();
                        StartCoroutine(DisableSpell(1.5f, 23, playerCasting));
                        break;
                    case SpellNames.VeilOfFortitude:
                        if (playerCasting == GameManager.Instance.player1)
                        {
                            PlayerPosition.position = player1FingerPositions[(int)targetFinger].position;
                        }
                        else
                        {
                            PlayerPosition.position = player2FingerPositions[(int)targetFinger].position;
                        }
                        ringPlacementParticleFail.Play();
                        StartCoroutine(DisableSpell(1.5f, 24, playerCasting));
                        break;
                }
                break;

        }

    }

    IEnumerator DisableSpell(float secondsToWait, int spellIndex, PlayerManager currentPlayer)
    {
        yield return new WaitForSeconds(secondsToWait);

        //FullEffects[spellIndex].gameObject.SetActive(false);

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

    public void QTEParticle(bool failOrSuccess, PlayerManager player)
    {
        StartCoroutine(RunParticle(failOrSuccess, player));
    }

    IEnumerator RunParticle(bool failOrSuccess, PlayerManager player)
    {
        if (failOrSuccess && player == GameManager.Instance.player1)
        {
            P1QTEFull.Play();
            yield return new WaitForSeconds(1);
            P1QTEFull.Stop();
        }
        else if (failOrSuccess && player == GameManager.Instance.player2)
        {
            P2QTEFull.Play();
            yield return new WaitForSeconds(1);
            P2QTEFull.Stop();
        }
        else if (!failOrSuccess && player == GameManager.Instance.player1)
        {
            P1QTEHalf.Play();
            yield return new WaitForSeconds(1);
            P1QTEHalf.Stop();
        }
        else if (!failOrSuccess && player == GameManager.Instance.player2)
        {
            P2QTEHalf.Play();
            yield return new WaitForSeconds(1);
            P2QTEHalf.Stop();
        }

    }
}