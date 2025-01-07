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
    //General
    public string spellName;
    public bool targetSelf;

    //Mana
    public int manaCost;
    public int manaGain;

    //Dmg
    public int damageValue;

    //HP
    public int healValue;

    //Duration (Rings)
    public int legnthAmount;

    //QTE (Quick Time Event)
    public int qteAmount;
    public int qteMidCheck;
    public int qteLowCheck;

    //Type of Spell (global spell type enum)


    //damage dealing function

    //health dealing function

    //QTE evaluation function


}
