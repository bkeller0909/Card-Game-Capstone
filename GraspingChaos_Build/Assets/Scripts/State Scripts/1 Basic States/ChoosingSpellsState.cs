public class ChoosingSpellsState : FSMState
{
    PlayerState playerState;
    bool hasAddedSpells;
    bool firstEnter;

    //Constructor
    public ChoosingSpellsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.ChoosingSpells;
    }

    public override void EnterStateInit()
    {
        //Remove past spells
        firstEnter = false;

        SpellsBeingCastInfo spellInfo = new SpellsBeingCastInfo();

        for (int w = 0; w < 3; w++)
        {
            for (int y = 0; y < 2; y++)
            {
                spellInfo.whoIsCasting = playerState.player;
                spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.none];
                spellInfo.whatFinger = PlayerFingers.none;
                spellInfo.whoIsBeingCastedOn = playerState.player;
                GameManager.Instance.spellsBeingCast[w, y] = spellInfo;
            }
        }

        GameManager.Instance.nextStateP1 = false;
        GameManager.Instance.nextStateP2 = false;

        hasAddedSpells = false;
        GameManager.Instance.amtOfSpellsBeingCast = 0;

        playerState.player.playerInput.SwitchCurrentActionMap("Cards");
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.nextStateP1 && GameManager.Instance.nextStateP2)
        {
            if (!hasAddedSpells)
            {
                int whoAmI = -100;
                if (player == GameManager.Instance.player1)
                {
                    whoAmI = (int)PlayerType.PLAYER1;
                }
                else
                {
                    whoAmI = (int)PlayerType.PLAYER2;
                }

                //for (int i = 0; i < testStates.spellsBeingCast.Length; i++)
                //{
                //    //Moving each spell that is beings casted info over to the GameManager to move to the desicion state
                //    SpellsBeingCastInfo spellInfo = new SpellsBeingCastInfo();

                //    if (i == 0)
                //    {
                //        spellInfo.numOfSpell = 1;
                //    }
                //    else if (i == 1)
                //    {
                //        spellInfo.numOfSpell = 2;
                //    }
                //    else if (i == 2)
                //    {
                //        spellInfo.numOfSpell = 3;
                //    }

                //    if (testStates.spellsChosen[i] != SpellNames.none)
                //    {
                //        spellInfo.whoIsCasting = playerState.player;
                //        spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)testStates.spellsChosen[i]];
                //        spellInfo.whatFinger = testStates.fingersChosen[i];

                //        if (testStates.spellsChosen[i] == SpellNames.FireBolt || testStates.spellsChosen[i] == SpellNames.Rockthrow || testStates.spellsChosen[i] == SpellNames.RighteousEnvy || testStates.spellsChosen[i] == SpellNames.LefteousEnvy || testStates.spellsChosen[i] == SpellNames.Icicles || testStates.spellsChosen[i] == SpellNames.CollectorsCurse
                //            || testStates.spellsChosen[i] == SpellNames.StaticBlast || testStates.spellsChosen[i] == SpellNames.Quake || testStates.spellsChosen[i] == SpellNames.RightingBolt ||
                //               testStates.spellsChosen[i] == SpellNames.LeftningBolt || testStates.spellsChosen[i] == SpellNames.TidalWave || testStates.spellsChosen[i] == SpellNames.PointerOfDeath
                //               || testStates.spellsChosen[i] == SpellNames.SpectralChain || testStates.spellsChosen[i] == SpellNames.SpectralChain)
                //        {
                //            spellInfo.whoIsBeingCastedOn = playerState.enemy;
                //        }
                //        else
                //        {
                //            spellInfo.whoIsBeingCastedOn = playerState.player;
                //        }

                //        GameManager.Instance.spellsBeingCast[i, whoAmI] = spellInfo;
                //    }
                //    else
                //    {
                //        spellInfo.whoIsCasting = playerState.player;
                //        spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.none];
                //        spellInfo.whatFinger = PlayerFingers.none;
                //        spellInfo.whoIsBeingCastedOn = playerState.player;
                //        GameManager.Instance.spellsBeingCast[i, whoAmI] = spellInfo;
                //    }

                //    // Remove the spell from the players hand
                //    if (spellInfo.whatSpell.spellName != SpellNames.none)
                //    {
                //        player.spellHand.RemoveSpells(spellInfo.whatSpell);
                //    }
                //}
            }

            playerState.PerformTransition(Transition.NeedDecision);

        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {

    }
}
