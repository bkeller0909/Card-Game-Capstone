using UnityEngine;

//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Active Spell Cards
//  Date Created: 01/28/2025
//  Purpose:      This holds every spellcard, with each of the base stats assigned
//  Instance?     Yes
//-----------------------------------------------------------------

/// <summary>
/// This holds every spellcard, with each of the base stats assigned
/// </summary>
public class ActiveSpellCards : MonoBehaviour
{
    private static ActiveSpellCards instance;
    public static ActiveSpellCards Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ActiveSpellCards>();
            }

            if (!instance)
            {
                Debug.LogError("No Game Manager Present !!!");
            }

            return instance;

        }
    }

    public int AmtOfCards;

    public SpellCard[] spellCards;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        spellCards = new SpellCard[AmtOfCards];

        // Equals -100 if that state is not used on that card
        for (int i = 0; i < AmtOfCards; i++)
        {
            SpellCard spell = new SpellCard();
            switch (i)
            {
                case 0:
                    // spell.name = "FireBolt";
                    spell.spellName = SpellNames.FireBolt;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 1;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 1:
                    spell.spellName = SpellNames.Rockthrow;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 1;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 2:
                    spell.spellName = SpellNames.RighteousEnvy;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 0;
                    spell.manaCost = 2;
                    spell.manaGain = -100;
                    spell.damageValue = 0;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 6;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 3:
                    spell.spellName = SpellNames.LefteousEnvy;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 0;
                    spell.manaCost = 2;
                    spell.manaGain = -100;
                    spell.damageValue = 0;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 6;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 4:
                    spell.spellName = SpellNames.Icicles;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = true;
                    spell.amtOfFingers = 2;
                    spell.manaCost = 3;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 7;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 5:
                    spell.spellName = SpellNames.CollectorsCurse;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 3;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 7;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 6:
                    spell.spellName = SpellNames.StaticBlast;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 5;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 8;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 7:
                    spell.spellName = SpellNames.Quake;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 3;
                    spell.manaCost = 6;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 8;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 8:
                    spell.spellName = SpellNames.RightingBolt;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 2;
                    spell.manaCost = 7;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 9;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 9:
                    spell.spellName = SpellNames.LeftingBolt;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 2;
                    spell.manaCost = 7;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 9;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 10:
                    spell.spellName = SpellNames.TidalWave;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 3;
                    spell.manaCost = 8;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 11;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 11:
                    spell.spellName = SpellNames.PointerOfDeath;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 9;
                    spell.manaGain = -100;
                    spell.damageValue = 3;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 14;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 12:
                    spell.spellName = SpellNames.ForTheCause;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 1;
                    spell.manaGain = 3;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 12;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 13:
                    spell.spellName = SpellNames.QuickHeal;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 2;
                    spell.manaGain = -100;
                    spell.damageValue = -100;
                    spell.healValue = 1;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 4;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 14:
                    spell.spellName = SpellNames.ThumbsUp;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 2;
                    spell.manaCost = 3;
                    spell.manaGain = -100;
                    spell.damageValue = -100;
                    spell.healValue = 1;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 4;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 15:
                    spell.spellName = SpellNames.LifeDrain;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 1;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = 1;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 10;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 16:
                    spell.spellName = SpellNames.CursedConversion;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 0;
                    spell.manaCost = 4;
                    spell.manaGain = 1;
                    spell.damageValue = -100;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 17:
                    spell.spellName = SpellNames.GreenThumb;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 5;
                    spell.manaGain = -100;
                    spell.damageValue = -100;
                    spell.healValue = 1;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 18:
                    spell.spellName = SpellNames.Materialise;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 6;
                    spell.manaGain = -100;
                    spell.damageValue = 0;
                    spell.healValue = 1;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 3;
                    spell.qteAmount = 10;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 19:
                    spell.spellName = SpellNames.EchoingMana;
                    spell.type = SpellType.RESTORATION;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 0;
                    spell.manaCost = 6;
                    spell.manaGain = 1;
                    spell.damageValue = -100;
                    spell.healValue = -100;
                    spell.lengthAmount = -100;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 8;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                ////////////////////////////////////////////////////////////////////Start here
                case 20:
                    spell.spellName = SpellNames.ThornsOfAgony;
                    spell.type = SpellType.RING;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = true;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 2;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = 5;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 21:
                    spell.spellName = SpellNames.GuardiansTouch;
                    spell.type = SpellType.RING;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 2;
                    spell.manaGain = -100;
                    spell.damageValue = -100;
                    spell.healValue = -100;
                    spell.lengthAmount = 3;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 22:
                    spell.spellName = SpellNames.SpectralChain;
                    spell.type = SpellType.RING;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 2;
                    spell.manaGain = -100;
                    spell.damageValue = -100;
                    spell.healValue = -100;
                    spell.lengthAmount = 3;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 4;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 23:
                    spell.spellName = SpellNames.ManaMerchant;
                    spell.type = SpellType.ATTACK;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 3;
                    spell.manaGain = 1;
                    spell.damageValue = -100;
                    spell.healValue = -100;
                    spell.lengthAmount = 8;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 24:
                    spell.spellName = SpellNames.VengefulMirror;
                    spell.type = SpellType.RING;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 3;
                    spell.manaGain = -100;
                    spell.damageValue = 1;
                    spell.healValue = -100;
                    spell.lengthAmount = 3;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 8;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 25:
                    spell.spellName = SpellNames.VampiricSurge;
                    spell.type = SpellType.RING;
                    spell.targetSelf = false;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 5;
                    spell.manaGain = -100;
                    spell.damageValue = -100;
                    spell.healValue = 1;
                    spell.lengthAmount = 3;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 5;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
                case 26:
                    spell.spellName = SpellNames.VeilOfFortitude;
                    spell.type = SpellType.RING;
                    spell.targetSelf = true;
                    spell.TargetRandomFinger = false;
                    spell.amtOfFingers = 1;
                    spell.manaCost = 5;
                    spell.manaGain = -100;
                    spell.damageValue = -100;
                    spell.healValue = -100;
                    spell.lengthAmount = 3;
                    spell.amtQTEOutcomes = 2;
                    spell.qteAmount = 8;
                    spell.qteMidCheck = spell.qteAmount / 2;

                    spellCards[i] = spell;
                    break;
            }
        }
    }
}
