using System.Collections;
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
    public int lengthAmount;

    //QTE (Quick Time Event)
    public int qteAmount;
    public int qteMidCheck;
    public int qteLowCheck;

    //Type of Spell (global spell type enum)
    public SpellType type;

    public KeyCode debugTester;


    //damage dealing function
    public virtual void DamageDealing()
    {
        for (int i = 0; i < damageValue; i++)
        {
            //function needed to know what hands and fingers are needed in the selecting of damage script
            // GameManager.Instance.currentCaster.health.DamageFinger(PlayerHands.LeftHand, PlayerFingers.Middle);
            GameManager.Instance.currentCaster.entireHP -= 1;
        }
    }

    //health dealing function
    public virtual void Healing()
    {
        for (int i = 0; i < healValue; i++)
        {
            //function needed to know what hands and fingers are needed in the selecting of damage script
            // GameManager.Instance.currentCaster.health.HealFinger(PlayerHands.LeftHand, PlayerFingers.Middle);
            GameManager.Instance.currentCaster.entireHP += 1;
        }
    }

    //QTE evaluation function

    public void TestSpellReleaseQTE()
    {
        if (Input.GetKeyDown(debugTester))
        {
            StartCoroutine(testing());
        }
    }

    private void Update()
    {
        TestSpellReleaseQTE();
    }

    IEnumerator testing()
    {
        yield return new WaitForSeconds(0);
        //QTEManager.Instance.CreateV2(qteAmount, GameManager.Instance.currentCaster);
        if (GameManager.Instance.testingCurrentP1)
        {
            p1.GetComponent<QTEManager>().CreateV2(qteAmount, p1.GetComponent<PlayerManager>());
        }
        else
        {
            p2.GetComponent<QTEManager>().CreateV2(qteAmount, p2.GetComponent<PlayerManager>());
        }
    }

}
