using UnityEngine;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        SpellCard
//  Date Created: 01/07/2025
//  Purpose:      Base class for all Spells / cards
//  Instance?     No
//-----------------------------------------------------------------
/// </summary>
public class SpellCard : MonoBehaviour
{

    //temp
    public GameObject p1;
    public GameObject p2;

    //General
    public SpellNames spellName;
    public bool targetSelf;
    public bool TargetRandomFinger;
    public int amtOfFingers;

    //Mana
    public int manaCost;
    public int manaGain;

    //Dmg
    public int damageValue;

    //HP
    public int healValue;

    //Duration (Rings)
    public int lengthAmount;

    //QTE (Quick Time Event)
    public int amtQTEOutcomes;
    public int qteAmount;
    public int qteMidCheck;

    //Type of Spell (global spell type enum)
    public SpellType type;

    public KeyCode debugTester;

    public int currentOrderValue = 4;

    public Rings playerRings;
}
