using System.Collections.Generic;
using UnityEngine;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        GameDebugger
//  Date Created: 02/15/2025
//  Purpose:      Multi purpose Script to set "Scriptable" hand scenarios
//                but also to track and debug all stats from the game
//  Instance?     No
//-----------------------------------------------------------------
/// </summary>

public class GameDebugger : MonoBehaviour
{
    #region References needed for script to work
    [Header("Needed info for script to work")]
    //[SerializeField] private CardsObjectPool debugPool;
    [SerializeField] private PlayerManager player1;
    [SerializeField] private PlayerManager player2;
    [SerializeField] private CardHandSlot cardHandP1;
    [SerializeField] private CardHandSlot cardHandP2;
    #endregion

    #region All Custom Player's Hands
    [Header("Attack Spells Hand Customizer")]
    [SerializeField] private List<SpellNames> attackSpellsDebugP1 = new List<SpellNames>();
    [SerializeField] private List<SpellNames> attackSpellsDebugP2 = new List<SpellNames>();

    [Header("Restoration Spells Hand Customizer")]
    [SerializeField] private List<SpellNames> restorationSpellsDebugP1 = new List<SpellNames>();
    [SerializeField] private List<SpellNames> restorationSpellsDebugP2 = new List<SpellNames>();

    [Header("Small Spells Hand Customizer")]
    [SerializeField] private List<SpellNames> smallSpellsDebugP1 = new List<SpellNames>();
    [SerializeField] private List<SpellNames> smallSpellsDebugP2 = new List<SpellNames>();

    [Header("Medium Spells Hand Customizer")]
    [SerializeField] private List<SpellNames> mediumSpellsDebugP1 = new List<SpellNames>();
    [SerializeField] private List<SpellNames> mediumSpellsDebugP2 = new List<SpellNames>();

    [Header("High Spells Hand Customizer")]
    [SerializeField] private List<SpellNames> highSpellsDebugP1 = new List<SpellNames>();
    [SerializeField] private List<SpellNames> highSpellsDebugP2 = new List<SpellNames>();
    #endregion

    #region Mana Managment
    [Header("Mana Amount P1")]
    public int manaP1 = 3;
    public int newManaAmountP1 = 0;

    [Header("Mana Amount P2")]
    public int manaP2 = 3;
    public int newManaAmountP2 = 0;
    #endregion

    #region Health Managment
    [Header("Total Health P1")]
    public int p1TotalHealth = 28;

    [Header("Total Health P2")]
    public int p2TotalHealth = 28;

    //finger health tracking P1
    [Header("Left Hand Finger Health P1")]
    public bool player1ActiveforDebugFingerInteraction = true;
    public bool player1AtiveDamageFinger = true;
    public bool player1AtiveHealingFinger = false;
    public int LeftPinkyP1 = 3;
    public int LeftRingP1 = 3;
    public int LeftMiddleP1 = 3;
    public int LeftIndexP1 = 3;
    public int LeftThumbP1 = 2;

    [Header("Right Hand Finger Health P1")]
    public int RightPinkyP1 = 3;
    public int RightRingP1 = 3;
    public int RightMiddleP1 = 3;
    public int RightIndexP1 = 3;
    public int RightThumbP1 = 2;

    //finger health tracking P2
    [Header("Left Hand Finger Health P2")]
    public bool player2ActiveforDebugFingerInteraction = false;
    public bool player2AtiveDamageFinger = true;
    public bool player2AtiveHealingFinger = false;
    public int LeftPinkyP2 = 3;
    public int LeftRingP2 = 3;
    public int LeftMiddleP2 = 3;
    public int LeftIndexP2 = 3;
    public int LeftThumbP2 = 2;

    [Header("Right Hand Finger Health P2")]
    public int RightPinkyP2 = 3;
    public int RightRingP2 = 3;
    public int RightMiddleP2 = 3;
    public int RightIndexP2 = 3;
    public int RightThumbP2 = 2;
    #endregion

    #region Card Object Pool Tracking
    //track objectPool
    [Header("All Attack Spells being used at the moment")]
    public int fireboltAmount = 0;
    public int rockThrowAmount = 0;
    public int collectorsCurseAmount = 0;
    public int iciclesAmount = 0;
    public int lefteousEnvyAmount = 0;
    public int righteousEnvyAmount = 0;
    public int leftningBoltAmount = 0;
    public int rightningBoltAmount = 0;
    public int pointerOfDeathAmount = 0;
    public int quakeAmount = 0;
    public int staticBlastAmount = 0;
    public int tidalWaveAmount = 0;

    [Header("All Restoration Spells being used at the moment")]
    public int cursedConvertionAmount = 0;
    public int echoingManaAmount = 0;
    public int forTheCauseAmount = 0;
    public int greenThumbAmount = 0;
    public int lifeDrainAmount = 0;
    public int materializeAmount = 0;
    public int quickHealAmount = 0;
    public int thumbsUpAmount = 0;
    #endregion

    void Update()
    {
        if (GameManager.Instance.checkAvailableCards)
        {
            TrackFingerHealth();
            TrackSpellsAvailable();
            TrackManaOfPlayers();
            GameManager.Instance.checkAvailableCards = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player1ActiveforDebugFingerInteraction)
            {
                player1ActiveforDebugFingerInteraction = false;
                player2ActiveforDebugFingerInteraction = true;
            }
            else
            {
                player2ActiveforDebugFingerInteraction = false;
                player1ActiveforDebugFingerInteraction = true;
            }
        }


        if (player1ActiveforDebugFingerInteraction)
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                player1AtiveDamageFinger = true;
                player1AtiveHealingFinger = false;
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                player1AtiveDamageFinger = false;
                player1AtiveHealingFinger = true;
            }
        }

        if (player2ActiveforDebugFingerInteraction)
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                player2AtiveDamageFinger = true;
                player2AtiveHealingFinger = false;
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                player2AtiveDamageFinger = false;
                player2AtiveHealingFinger = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            ClearALLSpellsP1();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            ClearALLSpellsP2();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            ClearALLSpellsP1();
            DealSpellsP1(attackSpellsDebugP1);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            ClearALLSpellsP2();
            DealSpellsP2(attackSpellsDebugP2);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            ClearALLSpellsP1();
            DealSpellsP1(restorationSpellsDebugP1);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            ClearALLSpellsP2();
            DealSpellsP2(restorationSpellsDebugP2);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            ClearALLSpellsP1();
            DealSpellsP1(smallSpellsDebugP1);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            ClearALLSpellsP2();
            DealSpellsP2(smallSpellsDebugP2);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            ClearALLSpellsP1();
            DealSpellsP1(mediumSpellsDebugP1);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            ClearALLSpellsP2();
            DealSpellsP2(mediumSpellsDebugP2);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            ClearALLSpellsP1();
            DealSpellsP1(highSpellsDebugP1);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            ClearALLSpellsP2();
            DealSpellsP2(highSpellsDebugP2);
            TrackSpellsAvailable();
        }

        if (Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            GiveManaToPlayer(player1, newManaAmountP1);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            GiveManaToPlayer(player2, newManaAmountP2);
        }

        DebugDamageAndHealingFingers();
    }


    //track the health of each player
    private void TrackFingerHealth()
    {
        //track total health of P1
        p1TotalHealth = player1.entireHP;
        //player 1
        LeftPinkyP1 = player1.health.playerHealthStats[(int)PlayerFingers.LH_Pinky];
        LeftRingP1 = player1.health.playerHealthStats[(int)PlayerFingers.LH_Ring];
        LeftMiddleP1 = player1.health.playerHealthStats[(int)PlayerFingers.LH_Middle];
        LeftIndexP1 = player1.health.playerHealthStats[(int)PlayerFingers.LH_Index];
        LeftThumbP1 = player1.health.playerHealthStats[(int)PlayerFingers.LH_Thumb];
        RightPinkyP1 = player1.health.playerHealthStats[(int)PlayerFingers.RH_Pinky];
        RightRingP1 = player1.health.playerHealthStats[(int)PlayerFingers.RH_Ring];
        RightMiddleP1 = player1.health.playerHealthStats[(int)PlayerFingers.RH_Middle];
        RightIndexP1 = player1.health.playerHealthStats[(int)PlayerFingers.RH_Index];
        RightThumbP1 = player1.health.playerHealthStats[(int)PlayerFingers.RH_Thumb];
        //track total health of P1
        p2TotalHealth = player2.entireHP;
        //player 2
        LeftPinkyP2 = player2.health.playerHealthStats[(int)PlayerFingers.LH_Pinky];
        LeftRingP2 = player2.health.playerHealthStats[(int)PlayerFingers.LH_Ring];
        LeftMiddleP2 = player2.health.playerHealthStats[(int)PlayerFingers.LH_Middle];
        LeftIndexP2 = player2.health.playerHealthStats[(int)PlayerFingers.LH_Index];
        LeftThumbP2 = player2.health.playerHealthStats[(int)PlayerFingers.LH_Thumb];
        RightPinkyP2 = player2.health.playerHealthStats[(int)PlayerFingers.RH_Pinky];
        RightRingP2 = player2.health.playerHealthStats[(int)PlayerFingers.RH_Ring];
        RightMiddleP2 = player2.health.playerHealthStats[(int)PlayerFingers.RH_Middle];
        RightIndexP2 = player2.health.playerHealthStats[(int)PlayerFingers.RH_Index];
        RightThumbP2 = player2.health.playerHealthStats[(int)PlayerFingers.RH_Thumb];
    }

    //track the spells available in the pool
    private void TrackSpellsAvailable()
    {
        foreach (GameObject cardsInUse in CardsObjectPool.Instance.objPoolCards)
        {
            if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.FireBolt)
            {
                fireboltAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.Rockthrow)
            {
                rockThrowAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.CollectorsCurse)
            {
                collectorsCurseAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.Icicles)
            {
                iciclesAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.LefteousEnvy)
            {
                lefteousEnvyAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.RighteousEnvy)
            {
                righteousEnvyAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.LeftningBolt)
            {
                leftningBoltAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.RightingBolt)
            {
                rightningBoltAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.PointerOfDeath)
            {
                pointerOfDeathAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.Quake)
            {
                quakeAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.StaticBlast)
            {
                staticBlastAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.TidalWave)
            {
                tidalWaveAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.CursedConversion)
            {
                cursedConvertionAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.EchoingMana)
            {
                echoingManaAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.ForTheCause)
            {
                forTheCauseAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.GreenThumb)
            {
                greenThumbAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.LifeDrain)
            {
                lifeDrainAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.Materialise)
            {
                materializeAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.QuickHeal)
            {
                quickHealAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
            else if (cardsInUse.GetComponent<SpellCard>().spellName == SpellNames.ThumbsUp)
            {
                thumbsUpAmount = CardsObjectPool.Instance.allcardAmounts[(int)cardsInUse.GetComponent<SpellCard>().spellName];
            }
        }
    }

    //clear the hands and objectpool
    private void ClearALLSpellsP1()
    {
        //clear hand slots
        for (int i = 0; i < cardHandP1.emptySlots.Length; i++)
        {
            cardHandP1.emptySlots[i] = true;
        }

        //clear object pool of the cards P2 is holding 
        foreach (GameObject cardInHand in CardsObjectPool.Instance.cardsCurrentlyInHandP1)
        {
            for (int i = CardsObjectPool.Instance.cardsCurrentlyInHand.Count - 1; i >= 0; i--)
            {
                if (cardInHand == CardsObjectPool.Instance.cardsCurrentlyInHand[i])
                {
                    CardsObjectPool.Instance.cardsCurrentlyInHand.Remove(CardsObjectPool.Instance.cardsCurrentlyInHand[i]);
                    break;
                }
            }
            cardInHand.SetActive(false);
            CardsObjectPool.Instance.allcardAmounts[(int)cardInHand.GetComponent<SpellCard>().spellName] -= 1;
        }
        CardsObjectPool.Instance.cardsCurrentlyInHandP1.Clear();
        cardHandP1.cards.Clear();
        cardHandP1.selectedCards.Clear();

    }

    //clear the hands and objectpool
    private void ClearALLSpellsP2()
    {
        //clear hand slots
        for (int i = 0; i < cardHandP2.emptySlots.Length; i++)
        {
            cardHandP2.emptySlots[i] = true;
        }

        //clear object pool of the cards P2 is holding 
        foreach (GameObject cardInHand in CardsObjectPool.Instance.cardsCurrentlyInHandP2)
        {
            for (int i = CardsObjectPool.Instance.cardsCurrentlyInHand.Count - 1; i >= 0; i--)
            {
                if (cardInHand == CardsObjectPool.Instance.cardsCurrentlyInHand[i])
                {
                    CardsObjectPool.Instance.cardsCurrentlyInHand.Remove(CardsObjectPool.Instance.cardsCurrentlyInHand[i]);
                    break;
                }
            }
            cardInHand.SetActive(false);
            CardsObjectPool.Instance.allcardAmounts[(int)cardInHand.GetComponent<SpellCard>().spellName] -= 1;
        }
        CardsObjectPool.Instance.cardsCurrentlyInHandP2.Clear();
        cardHandP2.cards.Clear();
        cardHandP2.selectedCards.Clear();
    }

    //deal any list you want from the above debug lists for P1
    private void DealSpellsP1(List<SpellNames> listOfPreference)
    {
        foreach (SpellNames attackSpell in listOfPreference)
        {
            foreach (GameObject card in CardsObjectPool.Instance.objPoolCards)
            {
                if (card.GetComponent<SpellCard>().spellName == attackSpell && !card.activeSelf)
                {
                    CardsObjectPool.Instance.allcardAmounts[(int)card.GetComponent<SpellCard>().spellName] += 1;
                    card.transform.eulerAngles = new Vector3(24.291f, -180f, transform.eulerAngles.z);
                    for (int i = 0; i < cardHandP1.emptySlots.Length; i++)
                    {
                        if (cardHandP1.emptySlots[i])
                        {
                            card.SetActive(true);
                            card.transform.position = cardHandP1.cardSlots[i].transform.position;
                            cardHandP1.cards.Add(card.GetComponent<CardSelect>());
                            cardHandP1.emptySlots[i] = false;
                            CardsObjectPool.Instance.cardsCurrentlyInHand.Add(card);
                            CardsObjectPool.Instance.cardsCurrentlyInHandP1.Add(card);
                            break;
                        }
                    }
                    //Debug.Log(card.GetComponent<SpellCard>().spellName);
                    break;
                }

            }
        }
    }

    //deal any list you want from the above debug lists for P2
    private void DealSpellsP2(List<SpellNames> listOfPreference)
    {
        foreach (SpellNames attackSpell in listOfPreference)
        {
            foreach (GameObject card in CardsObjectPool.Instance.objPoolCards)
            {
                if (card.GetComponent<SpellCard>().spellName == attackSpell && !card.activeSelf)
                {
                    CardsObjectPool.Instance.allcardAmounts[(int)card.GetComponent<SpellCard>().spellName] += 1;
                    card.transform.eulerAngles = new Vector3(24.291f, 0f, transform.eulerAngles.z);
                    for (int i = 0; i < cardHandP2.emptySlots.Length; i++)
                    {
                        if (cardHandP2.emptySlots[i])
                        {
                            card.SetActive(true);
                            card.transform.position = cardHandP2.cardSlots[i].transform.position;
                            cardHandP2.cards.Add(card.GetComponent<CardSelect>());
                            cardHandP2.emptySlots[i] = false;
                            CardsObjectPool.Instance.cardsCurrentlyInHand.Add(card);
                            CardsObjectPool.Instance.cardsCurrentlyInHandP2.Add(card);
                            break;
                        }
                    }
                    //Debug.Log(card.GetComponent<SpellCard>().spellName);
                    break;
                }

            }
        }
    }

    //track mana amount for both players
    private void TrackManaOfPlayers()
    {
        manaP1 = player1.Mana;
        manaP2 = player2.Mana;
    }

    //give any amount of mana you decide for Player 1 and Player 2
    private void GiveManaToPlayer(PlayerManager player, int manaToGive)
    {
        if (player == player1)
        {
            if (manaToGive > 12)
            {
                manaToGive = 12;
            }
            else if (manaToGive < 0)
            {
                manaToGive = 0;
            }
            player1.Mana = manaToGive;
            manaP1 = player1.Mana;
        }
        else if (player == player2)
        {
            if (manaToGive > 12)
            {
                manaToGive = 12;
            }
            else if (manaToGive < 0)
            {
                manaToGive = 0;
            }
            player2.Mana = manaToGive;
            manaP2 = player2.Mana;
        }
    }

    //remove the health of any fingers for Player 1 and Player 2
    private void DebugDamageAndHealingFingers()
    {
        if (player1ActiveforDebugFingerInteraction)
        {
            if (player1AtiveDamageFinger)
            {
                //left side
                //pinky numpad 0
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    player1.health.DamageFinger(PlayerFingers.LH_Pinky);
                }
                //ring numpad 1
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    player1.health.DamageFinger(PlayerFingers.LH_Ring);
                }
                //middle numpad 2
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    player1.health.DamageFinger(PlayerFingers.LH_Middle);
                }
                //index numpad 3
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    player1.health.DamageFinger(PlayerFingers.LH_Index);
                }
                //thumb numpad 4
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    player1.health.DamageFinger(PlayerFingers.LH_Thumb);
                }
                //right side
                //pinky numpad 5
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    player1.health.DamageFinger(PlayerFingers.RH_Pinky);
                }
                //ring numpad 6
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    player1.health.DamageFinger(PlayerFingers.RH_Ring);
                }
                //middle numpad 7
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    player1.health.DamageFinger(PlayerFingers.RH_Middle);
                }
                //index numpad 8
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    player1.health.DamageFinger(PlayerFingers.RH_Index);
                }
                //thumb numpad 9
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    player1.health.DamageFinger(PlayerFingers.RH_Thumb);
                }
                TrackFingerHealth();
            }
            else if (player1AtiveHealingFinger)
            {
                //left side
                //pinky numpad 0
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    player1.health.HealFinger(PlayerFingers.LH_Pinky);
                }
                //ring numpad 1
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    player1.health.HealFinger(PlayerFingers.LH_Ring);
                }
                //middle numpad 2
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    player1.health.HealFinger(PlayerFingers.LH_Middle);
                }
                //index numpad 3
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    player1.health.HealFinger(PlayerFingers.LH_Index);
                }
                //thumb numpad 4
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    player1.health.HealFinger(PlayerFingers.LH_Thumb);
                }
                //right side
                //pinky numpad 5
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    player1.health.HealFinger(PlayerFingers.RH_Pinky);
                }
                //ring numpad 6
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    player1.health.HealFinger(PlayerFingers.RH_Ring);
                }
                //middle numpad 7
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    player1.health.HealFinger(PlayerFingers.RH_Middle);
                }
                //index numpad 8
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    player1.health.HealFinger(PlayerFingers.RH_Index);
                }
                //thumb numpad 9
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    player1.health.HealFinger(PlayerFingers.RH_Thumb);
                }
                TrackFingerHealth();
            }
        }

        if (player2ActiveforDebugFingerInteraction)
        {
            if (player2AtiveDamageFinger)
            {
                //left side
                //pinky numpad 0
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    player2.health.DamageFinger(PlayerFingers.LH_Pinky);
                }
                //ring numpad 1
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    player2.health.DamageFinger(PlayerFingers.LH_Ring);
                }
                //middle numpad 2
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    player2.health.DamageFinger(PlayerFingers.LH_Middle);
                }
                //index numpad 3
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    player2.health.DamageFinger(PlayerFingers.LH_Index);
                }
                //thumb numpad 4
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    player2.health.DamageFinger(PlayerFingers.LH_Thumb);
                }
                //right side
                //pinky numpad 5
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    player2.health.DamageFinger(PlayerFingers.RH_Pinky);
                }
                //ring numpad 6
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    player2.health.DamageFinger(PlayerFingers.RH_Ring);
                }
                //middle numpad 7
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    player2.health.DamageFinger(PlayerFingers.RH_Middle);
                }
                //index numpad 8
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    player2.health.DamageFinger(PlayerFingers.RH_Index);
                }
                //thumb numpad 9
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    player2.health.DamageFinger(PlayerFingers.RH_Thumb);
                }
                TrackFingerHealth();
            }
            else if (player2AtiveHealingFinger)
            {
                //left side
                //pinky numpad 0
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    player2.health.HealFinger(PlayerFingers.LH_Pinky);
                }
                //ring numpad 1
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    player2.health.HealFinger(PlayerFingers.LH_Ring);
                }
                //middle numpad 2
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    player2.health.HealFinger(PlayerFingers.LH_Middle);
                }
                //index numpad 3
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    player2.health.HealFinger(PlayerFingers.LH_Index);
                }
                //thumb numpad 4
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    player2.health.HealFinger(PlayerFingers.LH_Thumb);
                }
                //right side
                //pinky numpad 5
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    player2.health.HealFinger(PlayerFingers.RH_Pinky);
                }
                //ring numpad 6
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    player2.health.HealFinger(PlayerFingers.RH_Ring);
                }
                //middle numpad 7
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    player2.health.HealFinger(PlayerFingers.RH_Middle);
                }
                //index numpad 8
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    player2.health.HealFinger(PlayerFingers.RH_Index);
                }
                //thumb numpad 9
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    player2.health.HealFinger(PlayerFingers.RH_Thumb);
                }
                TrackFingerHealth();
            }
        }
    }

}
