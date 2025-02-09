using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestStates : MonoBehaviour
{

    [SerializeField]
    public TMP_Text aOSC_text;

    [SerializeField]
    PlayerManager player;

    [SerializeField]
    public TMP_Text[] spellsBeingCast;
    public GameObject[] spellsBeingChosenPanels;

    [SerializeField]
    public TMP_Text[] fingersBeingChosen;
    public GameObject[] fingersBeingChosenPanels;

    [SerializeField]
    public Button[] choosingSpellButtons;
    public Button[] choosingFingerButtons;
    public Button castBtn;

    public SpellNames[] spellsChosen;
    public PlayerFingers[] fingersChosen;
    public int amtOfSpells;

    public void Start()
    {
        castBtn.gameObject.SetActive(false);

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

    private void Update()
    {
        aOSC_text.text = "Amt of spells Chosen: " + GameManager.Instance.amtOfSpellsBeingCast.ToString();
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
            spellsChosen[whichChosenSpell] = SpellNames.none;
            fingersChosen[whichChosenSpell] = PlayerFingers.none;

            if (whichChosenSpell == 0)
            {
                if (spellsChosen[2] != SpellNames.none)
                {
                    spellsChosen[0] = spellsChosen[1];
                    spellsChosen[1] = spellsChosen[2];
                    spellsChosen[2] = SpellNames.none;

                    fingersChosen[0] = fingersChosen[1];
                    fingersChosen[1] = fingersChosen[2];
                    fingersChosen[2] = PlayerFingers.none;
                }
                else if (spellsChosen[1] != SpellNames.none)
                {
                    spellsChosen[0] = spellsChosen[1];
                    spellsChosen[1] = SpellNames.none;

                    fingersChosen[0] = fingersChosen[1];
                    fingersChosen[1] = PlayerFingers.none;
                }
            }
            else if (whichChosenSpell == 1)
            {
                if (spellsChosen[2] != SpellNames.none)
                {
                    spellsChosen[1] = spellsChosen[2];
                    spellsChosen[2] = SpellNames.none;

                    fingersChosen[1] = fingersChosen[2];
                    fingersChosen[2] = PlayerFingers.none;
                }
            }
        }
        else if (amtOfSpells < 3)
        {
            amtOfSpells++;
            GameManager.Instance.amtOfSpellsBeingCast++;
            for (int i = 0; i < 3; i++)
            {
                if (spellsChosen[i] == SpellNames.none)
                {
                    spellsChosen[i] = whatSpell.name;
                    break;
                }
            }

            //Turns off Spell btns
            castBtn.gameObject.SetActive(false);

            foreach (Button button in choosingSpellButtons)
            {
                button.gameObject.SetActive(false);
            }

            bool firstFingerBtnCreated = false;

            foreach (Button button in choosingFingerButtons)
            {
                button.gameObject.SetActive(true);

                if (!firstFingerBtnCreated)
                {
                    firstFingerBtnCreated = true;
                    button.Select();
                }
            }
        }
    }

    public void choosingFinger(WhatFingerAmI whatFinger)
    {
        if (spellsChosen[1] == SpellNames.none && spellsChosen[2] == SpellNames.none)
        {
            fingersChosen[0] = whatFinger.fingerName;
        }
        else if (spellsChosen[2] == SpellNames.none)
        {
            fingersChosen[1] = whatFinger.fingerName;
        }
        else
        {
            fingersChosen[2] = whatFinger.fingerName;
        }

        //Turns off finger btns
        foreach (Button button in choosingFingerButtons)
        {
            button.gameObject.SetActive(false);
        }

        bool firstSpellBtnCreated = false;

        castBtn.gameObject.SetActive(true);

        foreach (Button button in choosingSpellButtons)
        {
            button.gameObject.SetActive(true);

            if (!firstSpellBtnCreated)
            {
                firstSpellBtnCreated = true;
                button.Select();
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
            if (player == GameManager.Instance.player1)
            {
                GameManager.Instance.nextStateP1 = true;
            }
            else
            {
                GameManager.Instance.nextStateP2 = true;
            }
        }
    }
}
