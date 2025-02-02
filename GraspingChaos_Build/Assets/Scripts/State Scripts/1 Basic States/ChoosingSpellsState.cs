using UnityEngine.UI;

public class ChoosingSpellsState : FSMState
{
    PlayerState playerState;
    bool hasAddedSpells;

    //Testing
    TestStates testStates;

    //Constructor
    public ChoosingSpellsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.ChoosingSpells;
    }

    public override void EnterStateInit()
    {
        hasAddedSpells = false;
        GameManager.Instance.amtOfSpellsBeingCast = 0;
        GameManager.Instance.nextTestState = false;
        testStates = playerState.gameObject.GetComponent<TestStates>();

        foreach (Button button in testStates.choosingFingerButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (Button button in testStates.choosingSpellButtons)
        {
            WhatSpellAmI spell = button.gameObject.GetComponent<WhatSpellAmI>();
            SpellHand playerHand = playerState.player.spellHand;
            foreach (SpellCard card in playerHand.playerSpells)
            {
                if (spell.name == card.spellName)
                {
                    button.gameObject.SetActive(true);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < testStates.spellsBeingCast.Length; i++)
        {
            testStates.spellsBeingCast[i].gameObject.SetActive(true);
            testStates.spellsBeingChosenPanels[i].gameObject.SetActive(true);
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.nextTestState)
        {
            if (!hasAddedSpells)
            {
                foreach (Button button in testStates.choosingSpellButtons)
                {
                    button.gameObject.SetActive(false);
                }

                int whoAmI = -100;
                if (player == GameManager.Instance.player1)
                {
                    whoAmI = 0;
                }
                else
                {
                    whoAmI = 1;
                }

                for (int i = 0; i < testStates.spellsBeingCast.Length; i++)
                {
                    //Moving each spell that is beings casted info over to the GameManager to move to the desicion state
                    SpellsBeingCastInfo spellInfo = new SpellsBeingCastInfo();

                    if (i == 0)
                    {
                        spellInfo.numOfSpell = 1;
                    }
                    else if (i == 1)
                    {
                        spellInfo.numOfSpell = 2;
                    }
                    else if (i == 2)
                    {
                        spellInfo.numOfSpell = 3;
                    }

                    if (testStates.spellsChosen[i] != SpellNames.none)
                    {
                        spellInfo.whoIsCasting = playerState.player;
                        spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)testStates.spellsChosen[i]];
                        spellInfo.whatFinger = testStates.fingersChosen[i];

                        if (testStates.spellsChosen[i] == SpellNames.FireBolt || testStates.spellsChosen[i] == SpellNames.Rockthrow || testStates.spellsChosen[i] == SpellNames.RighteousEnvy || testStates.spellsChosen[i] == SpellNames.LefteousEnvy || testStates.spellsChosen[i] == SpellNames.Icicles || testStates.spellsChosen[i] == SpellNames.CollectorsCurse
                            || testStates.spellsChosen[i] == SpellNames.StaticBlast || testStates.spellsChosen[i] == SpellNames.Quake || testStates.spellsChosen[i] == SpellNames.RightingBolt ||
                               testStates.spellsChosen[i] == SpellNames.LeftningBolt || testStates.spellsChosen[i] == SpellNames.TidalWave || testStates.spellsChosen[i] == SpellNames.PointerOfDeath
                               || testStates.spellsChosen[i] == SpellNames.SpectralChain || testStates.spellsChosen[i] == SpellNames.SpectralChain)
                        {
                            spellInfo.whoIsBeingCastedOn = playerState.enemy;
                        }
                        else
                        {
                            spellInfo.whoIsBeingCastedOn = playerState.player;
                        }

                        GameManager.Instance.spellsBeingCast[i, whoAmI] = spellInfo;
                    }
                    else
                    {
                        spellInfo.whoIsCasting = playerState.player;
                        spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.none];
                        spellInfo.whatFinger = PlayerFingers.none;
                        spellInfo.whoIsBeingCastedOn = playerState.player;
                        GameManager.Instance.spellsBeingCast[i, whoAmI] = spellInfo;
                    }

                    testStates.spellsBeingCast[i].gameObject.SetActive(false);
                    testStates.spellsBeingChosenPanels[i].gameObject.SetActive(false);
                    testStates.fingersBeingChosenPanels[i].gameObject.SetActive(false);
                    testStates.fingersBeingChosen[i].gameObject.SetActive(false);
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///           Remove these for they are for testing if there was two players
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            GameManager.Instance.amtOfSpellsBeingCast++;
            GameManager.Instance.amtOfSpellsBeingCast++;
            GameManager.Instance.amtOfSpellsBeingCast++;
            SpellsBeingCastInfo spellInfoTEST = new SpellsBeingCastInfo();
            spellInfoTEST.whoIsCasting = playerState.enemy;
            spellInfoTEST.numOfSpell = 1;
            spellInfoTEST.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.StaticBlast];
            spellInfoTEST.whatFinger = PlayerFingers.RH_Ring;
            spellInfoTEST.whoIsBeingCastedOn = playerState.player;
            GameManager.Instance.spellsBeingCast[0, 1] = spellInfoTEST;
            spellInfoTEST.whoIsCasting = playerState.enemy;
            spellInfoTEST.numOfSpell = 2;
            spellInfoTEST.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles];
            spellInfoTEST.whatFinger = PlayerFingers.LH_Ring;
            spellInfoTEST.whoIsBeingCastedOn = playerState.player;
            GameManager.Instance.spellsBeingCast[1, 1] = spellInfoTEST;
            spellInfoTEST.whoIsCasting = playerState.enemy;
            spellInfoTEST.numOfSpell = 3;
            spellInfoTEST.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.none];
            spellInfoTEST.whatFinger = PlayerFingers.none;
            spellInfoTEST.whoIsBeingCastedOn = playerState.enemy;
            GameManager.Instance.spellsBeingCast[2, 1] = spellInfoTEST;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////

            playerState.PerformTransition(Transition.NeedDecision);

        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        for (int i = 0; i < 3; i++)
        {
            if (testStates.spellsChosen[i] == SpellNames.none)
            {
                testStates.spellsBeingCast[i].text = " ";
            }
            else if (testStates.spellsChosen[i] == SpellNames.FireBolt)
            {
                testStates.spellsBeingCast[i].text = "FireBolt";
            }
            else if (testStates.spellsChosen[i] == SpellNames.Rockthrow)
            {
                testStates.spellsBeingCast[i].text = "Rockthrow";
            }
            else if (testStates.spellsChosen[i] == SpellNames.RighteousEnvy)
            {
                testStates.spellsBeingCast[i].text = "RighteousEnvy";
            }
            else if (testStates.spellsChosen[i] == SpellNames.LefteousEnvy)
            {
                testStates.spellsBeingCast[i].text = "LefteousEnvy";
            }
            else if (testStates.spellsChosen[i] == SpellNames.Icicles)
            {
                testStates.spellsBeingCast[i].text = "Icicles";
            }
            else if (testStates.spellsChosen[i] == SpellNames.CollectorsCurse)
            {
                testStates.spellsBeingCast[i].text = "Collectors";
            }
            else if (testStates.spellsChosen[i] == SpellNames.StaticBlast)
            {
                testStates.spellsBeingCast[i].text = "StaticBlast";
            }
            else if (testStates.spellsChosen[i] == SpellNames.Quake)
            {
                testStates.spellsBeingCast[i].text = "Quake";
            }
            else if (testStates.spellsChosen[i] == SpellNames.RightingBolt)
            {
                testStates.spellsBeingCast[i].text = "RightingBolt";
            }
            else if (testStates.spellsChosen[i] == SpellNames.LeftningBolt)
            {
                testStates.spellsBeingCast[i].text = "LeftingBolt";
            }
            else if (testStates.spellsChosen[i] == SpellNames.TidalWave)
            {
                testStates.spellsBeingCast[i].text = "TidalWave";
            }
            else if (testStates.spellsChosen[i] == SpellNames.PointerOfDeath)
            {
                testStates.spellsBeingCast[i].text = "PointerOfDeath";
            }
            else if (testStates.spellsChosen[i] == SpellNames.ForTheCause)
            {
                testStates.spellsBeingCast[i].text = "ForTheCause";
            }
            else if (testStates.spellsChosen[i] == SpellNames.QuickHeal)
            {
                testStates.spellsBeingCast[i].text = "QuickHeal";
            }
            else if (testStates.spellsChosen[i] == SpellNames.ThumbsUp)
            {
                testStates.spellsBeingCast[i].text = "ThumbsUp";
            }
            else if (testStates.spellsChosen[i] == SpellNames.LifeDrain)
            {
                testStates.spellsBeingCast[i].text = "LifeDrain";
            }
            else if (testStates.spellsChosen[i] == SpellNames.CursedConversion)
            {
                testStates.spellsBeingCast[i].text = "CursedConversion";
            }
            else if (testStates.spellsChosen[i] == SpellNames.GreenThumb)
            {
                testStates.spellsBeingCast[i].text = "TheGreenThumb";
            }
            else if (testStates.spellsChosen[i] == SpellNames.Materialise)
            {
                testStates.spellsBeingCast[i].text = "Materialise";
            }
            else if (testStates.spellsChosen[i] == SpellNames.EchoingMana)
            {
                testStates.spellsBeingCast[i].text = "EchoingMana";
            }
            else if (testStates.spellsChosen[i] == SpellNames.ThornsOfAgony)
            {
                testStates.spellsBeingCast[i].text = "ThornsOfAgony";
            }
            else if (testStates.spellsChosen[i] == SpellNames.GuardiansTouch)
            {
                testStates.spellsBeingCast[i].text = "GuardiansTouch";
            }
            else if (testStates.spellsChosen[i] == SpellNames.SpectralChain)
            {
                testStates.spellsBeingCast[i].text = "SpectralChain";
            }
            else if (testStates.spellsChosen[i] == SpellNames.ManaMerchant)
            {
                testStates.spellsBeingCast[i].text = "ManaMerchant";
            }
            else if (testStates.spellsChosen[i] == SpellNames.VengefulMirror)
            {
                testStates.spellsBeingCast[i].text = "VengefulMirror";
            }
            else if (testStates.spellsChosen[i] == SpellNames.VampiricSurge)
            {
                testStates.spellsBeingCast[i].text = "VampiricSurge";
            }
            else if (testStates.spellsChosen[i] == SpellNames.VeilOfFortitude)
            {
                testStates.spellsBeingCast[i].text = "VeilOfFortitude";
            }

            if (testStates.fingersChosen[i] == PlayerFingers.none)
            {
                testStates.fingersBeingChosen[i].text = " ";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.LH_Pinky)
            {
                testStates.fingersBeingChosen[i].text = "LH Pinky";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.LH_Ring)
            {
                testStates.fingersBeingChosen[i].text = "LH Ring";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.LH_Middle)
            {
                testStates.fingersBeingChosen[i].text = "LH Middle";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.LH_Index)
            {
                testStates.fingersBeingChosen[i].text = "LH Index";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.LH_Thumb)
            {
                testStates.fingersBeingChosen[i].text = "LH Thumb";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.RH_Pinky)
            {
                testStates.fingersBeingChosen[i].text = "RH Pinky";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.RH_Ring)
            {
                testStates.fingersBeingChosen[i].text = "RH Ring";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.RH_Middle)
            {
                testStates.fingersBeingChosen[i].text = "RH Middle";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.RH_Index)
            {
                testStates.fingersBeingChosen[i].text = "RH Index";
            }
            else if (testStates.fingersChosen[i] == PlayerFingers.RH_Thumb)
            {
                testStates.fingersBeingChosen[i].text = "RH Thumb";
            }
        }
    }
}
