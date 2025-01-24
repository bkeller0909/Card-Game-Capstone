using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestStates : MonoBehaviour
{

    [SerializeField]
    public TMP_Text aOSC_text;

    [SerializeField]
    public TMP_Text[] spellsBeingCast;
    public GameObject[] spellsBeingChosenPanels;

    [SerializeField]
    public Button[] choosingSpellButtons;

    public SpellNames[] spellsChosen;
    public int amtOfSpells;

    public void Start()
    {
        foreach (Button button in choosingSpellButtons)
        {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < spellsBeingCast.Length; i++)
        {
            spellsBeingCast[i].gameObject.SetActive(false);
            spellsBeingChosenPanels[i].gameObject.SetActive(false);
        }

        spellsChosen = new SpellNames[3];

        for (int i = 0; i < 3; i++)
        {
            spellsChosen[i] = SpellNames.none;
        }
    }

    public void choosingSpell(WhatSpellAmI whatSpell)
    {
        bool spellHasBeenChosen = false;
        int whichChosenSpell = -3;

        for (int i = 0; i < 3; i++)
        {
            if (spellsChosen[i] == whatSpell.name)
            {
                spellHasBeenChosen = true;
                whichChosenSpell = i;
            }
        }

        if (spellHasBeenChosen)
        {
            amtOfSpells--;
            GameManager.Instance.amtOfSpellsBeingCast--;
            aOSC_text.text = "Amt of spells Chosen: " + GameManager.Instance.amtOfSpellsBeingCast.ToString();
            spellsChosen[whichChosenSpell] = SpellNames.none;

            if (whichChosenSpell == 0)
            {
                if (spellsChosen[2] != SpellNames.none)
                {
                    spellsChosen[0] = spellsChosen[1];
                    spellsChosen[1] = spellsChosen[2];
                    spellsChosen[2] = SpellNames.none;
                }
                else if (spellsChosen[1] != SpellNames.none)
                {
                    spellsChosen[0] = spellsChosen[1];
                    spellsChosen[1] = SpellNames.none;
                }
            }
            else if (whichChosenSpell == 1)
            {
                if (spellsChosen[2] != SpellNames.none)
                {
                    spellsChosen[1] = spellsChosen[2];
                    spellsChosen[2] = SpellNames.none;
                }
            }
        }
        else if (amtOfSpells < 3)
        {
            amtOfSpells++;
            GameManager.Instance.amtOfSpellsBeingCast++;
            aOSC_text.text = "Amt of spells Chosen: " + GameManager.Instance.amtOfSpellsBeingCast.ToString();
            for (int i = 0; i < 3; i++)
            {
                if (spellsChosen[i] == SpellNames.none)
                {
                    spellsChosen[i] = whatSpell.name;
                    break;
                }
            }
        }
    }

    public void addSpell()
    {
        if (GameManager.Instance.amtOfSpellsBeingCast < 3)
        {
            GameManager.Instance.amtOfSpellsBeingCast++;
            aOSC_text.text = "Amt of spells Chosen: " + GameManager.Instance.amtOfSpellsBeingCast.ToString();
        }
    }

    public void minSpell()
    {
        if (GameManager.Instance.amtOfSpellsBeingCast > 0)
        {
            GameManager.Instance.amtOfSpellsBeingCast--;
            aOSC_text.text = "Amt of spells Chosen: " + GameManager.Instance.amtOfSpellsBeingCast.ToString();
        }
    }

    public void startCasting()
    {
        if (GameManager.Instance.amtOfSpellsBeingCast != 0)
        {
            GameManager.Instance.nextTestState = true;
        }
    }
}
