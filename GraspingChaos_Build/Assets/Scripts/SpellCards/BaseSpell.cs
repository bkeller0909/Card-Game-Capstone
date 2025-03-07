using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BaseSpell", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class BaseSpell : ScriptableObject
{
    public SpellNames spellName;
    public SpellType type;
    public bool targetSelf;
    public bool TargetRandomFinger;
    public int amtOfFingers;
    public int manaCost;
    public int manaGain;
    public int damageValue;
    public int healvALUE;
    public int lengthAmount;
    public int amtQTEOutcomes;
    public int qteAmount;
}
