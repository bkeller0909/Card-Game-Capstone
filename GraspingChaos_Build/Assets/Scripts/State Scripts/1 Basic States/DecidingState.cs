
public class DecidingState : FSMState
{
    PlayerState playerState;
    bool castASpell, performAct;
    bool roundEnd;
    //Constructor
    public DecidingState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Deciding;
        roundEnd = false;
    }

    public override void EnterStateInit()
    {
        GameManager.Instance.spellInProgress = false;
        roundEnd = false;

        if (!GameManager.Instance.roundCheck)
        {
            if (playerState.player == GameManager.Instance.player1)
            {
                GameManager.Instance.player1IsHere = true;
            }
            else
            {
                GameManager.Instance.player2IsHere = true;
            }

            if (GameManager.Instance.player1IsHere && GameManager.Instance.player2IsHere)
            {
                for (int i = 0; i < 3; i++)
                {
                    // If player 1's first card and player 2's first card have the same mana cost
                    if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none &&
                        GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
                    {
                        GameManager.Instance.whoesOnFirst[i] = Decider.NoSpellsChosen;
                    }
                    else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none)
                    {
                        GameManager.Instance.whoesOnFirst[i] = Decider.PlayerTwoIsFaster;
                        GameManager.Instance.particleWait[i] = false;

                        if (i == 0)
                        {
                            GameManager.Instance.currentCaster = GameManager.Instance.player2;
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
                    {
                        GameManager.Instance.whoesOnFirst[i] = Decider.PlayerOneIsFaster;
                        GameManager.Instance.particleWait[i] = true;

                        if (i == 0)
                        {
                            GameManager.Instance.currentCaster = GameManager.Instance.player1;
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.manaCost == GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.manaCost)
                    {
                        GameManager.Instance.whoesOnFirst[i] = Decider.Tie;
                    }
                    else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.manaCost < GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.manaCost)
                    {
                        GameManager.Instance.whoesOnFirst[i] = Decider.PlayerOneIsFaster;
                        GameManager.Instance.particleWait[i] = true;

                        if (i == 0)
                        {
                            GameManager.Instance.currentCaster = GameManager.Instance.player1;
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.manaCost > GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.manaCost)
                    {
                        GameManager.Instance.whoesOnFirst[i] = Decider.PlayerTwoIsFaster;
                        GameManager.Instance.particleWait[i] = false;

                        if (i == 0)
                        {
                            GameManager.Instance.currentCaster = GameManager.Instance.player2;
                        }
                    }
                }
                GameManager.Instance.roundCheck = true;
                GameManager.Instance.player1IsHere = false;
                GameManager.Instance.player2IsHere = false;
            }
            performAct = false;
            castASpell = true;
            GameManager.Instance.spellIndex = 0;
            GameManager.Instance.playedSpells = 0;
            GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER1] = false;
            GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER2] = false;
        }
        else
        {
            castASpell = false;
            performAct = true;

            if (GameManager.Instance.playedSpells == 0)
            {
                if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.PlayerOneIsFaster)
                {
                    GameManager.Instance.currentCaster = GameManager.Instance.player1;
                }
                else if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.PlayerTwoIsFaster)
                {
                    GameManager.Instance.currentCaster = GameManager.Instance.player2;
                }
            }
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (player.entireHP <= 0 || enemy.entireHP <= 0)
        {
            playerState.PerformTransition(Transition.died);
        }
        if (roundEnd || GameManager.Instance.spellIndex >= 3)
        {
            playerState.PerformTransition(Transition.EndOfRound);
        }

        if (GameManager.Instance.roundCheck && castASpell)
        {
            if (player == GameManager.Instance.player1)
            {
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.FireBolt)
                {
                    playerState.PerformTransition(Transition.TA_FireBolt);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.Rockthrow)
                {
                    playerState.PerformTransition(Transition.TA_RockThrow);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.RighteousEnvy)
                {
                    playerState.PerformTransition(Transition.TA_RighteousEnvy);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.LefteousEnvy)
                {
                    playerState.PerformTransition(Transition.TA_LefteousEnvy);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.Icicles)
                {
                    playerState.PerformTransition(Transition.TA_Icicles);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.CollectorsCurse)
                {
                    playerState.PerformTransition(Transition.TA_CollectorsCurse);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.StaticBlast)
                {
                    playerState.PerformTransition(Transition.TA_StaticBlast);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.Quake)
                {
                    playerState.PerformTransition(Transition.TA_Quake);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.RightingBolt)
                {
                    playerState.PerformTransition(Transition.TA_RightningBolt);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.LeftningBolt)
                {
                    playerState.PerformTransition(Transition.TA_LeftningBolt);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.TidalWave)
                {
                    playerState.PerformTransition(Transition.TA_TidalWave);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.PointerOfDeath)
                {
                    playerState.PerformTransition(Transition.TA_PointerOfDeath);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.ForTheCause)
                {
                    playerState.PerformTransition(Transition.TRe_ForTheCause);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.QuickHeal)
                {
                    playerState.PerformTransition(Transition.TRe_QuickHeal);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.ThumbsUp)
                {
                    playerState.PerformTransition(Transition.TRe_ThumbsUp);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.LifeDrain)
                {
                    playerState.PerformTransition(Transition.TRe_LifeDrain);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.CursedConversion)
                {
                    playerState.PerformTransition(Transition.TRe_CursedConversion);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.GreenThumb)
                {
                    playerState.PerformTransition(Transition.TRe_GreenThumb);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.Materialise)
                {
                    playerState.PerformTransition(Transition.TRe_Materialise);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.EchoingMana)
                {
                    playerState.PerformTransition(Transition.TRe_EchoingMana);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.ThornsOfAgony)
                {
                    playerState.PerformTransition(Transition.TRi_ThornsOfAgony);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.GuardiansTouch)
                {
                    playerState.PerformTransition(Transition.TRi_GuardiansTouch);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.SpectralChain)
                {
                    playerState.PerformTransition(Transition.TRi_SpectralChain);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.ManaMerchant)
                {
                    playerState.PerformTransition(Transition.TRi_ManaMerchant);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.VengefulMirror)
                {
                    playerState.PerformTransition(Transition.TRi_VengefulMirror);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.VampiricSurge)
                {
                    playerState.PerformTransition(Transition.TRi_VampiricSurge);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.VeilOfFortitude)
                {
                    playerState.PerformTransition(Transition.TRi_VeilOfFortitude);
                }
            }
            // Player 2
            else
            {
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.FireBolt)
                {
                    playerState.PerformTransition(Transition.TA_FireBolt);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.Rockthrow)
                {
                    playerState.PerformTransition(Transition.TA_RockThrow);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.RighteousEnvy)
                {
                    playerState.PerformTransition(Transition.TA_RighteousEnvy);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.LefteousEnvy)
                {
                    playerState.PerformTransition(Transition.TA_LefteousEnvy);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.Icicles)
                {
                    playerState.PerformTransition(Transition.TA_Icicles);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.CollectorsCurse)
                {
                    playerState.PerformTransition(Transition.TA_CollectorsCurse);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.StaticBlast)
                {
                    playerState.PerformTransition(Transition.TA_StaticBlast);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.Quake)
                {
                    playerState.PerformTransition(Transition.TA_Quake);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.RightingBolt)
                {
                    playerState.PerformTransition(Transition.TA_RightningBolt);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.LeftningBolt)
                {
                    playerState.PerformTransition(Transition.TA_LeftningBolt);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.TidalWave)
                {
                    playerState.PerformTransition(Transition.TA_TidalWave);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.PointerOfDeath)
                {
                    playerState.PerformTransition(Transition.TA_PointerOfDeath);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.ForTheCause)
                {
                    playerState.PerformTransition(Transition.TRe_ForTheCause);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.QuickHeal)
                {
                    playerState.PerformTransition(Transition.TRe_QuickHeal);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.ThumbsUp)
                {
                    playerState.PerformTransition(Transition.TRe_ThumbsUp);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.LifeDrain)
                {
                    playerState.PerformTransition(Transition.TRe_LifeDrain);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.CursedConversion)
                {
                    playerState.PerformTransition(Transition.TRe_CursedConversion);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.GreenThumb)
                {
                    playerState.PerformTransition(Transition.TRe_GreenThumb);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.Materialise)
                {
                    playerState.PerformTransition(Transition.TRe_Materialise);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.EchoingMana)
                {
                    playerState.PerformTransition(Transition.TRe_EchoingMana);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.ThornsOfAgony)
                {
                    playerState.PerformTransition(Transition.TRi_ThornsOfAgony);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.GuardiansTouch)
                {
                    playerState.PerformTransition(Transition.TRi_GuardiansTouch);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.SpectralChain)
                {
                    playerState.PerformTransition(Transition.TRi_SpectralChain);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.ManaMerchant)
                {
                    playerState.PerformTransition(Transition.TRi_ManaMerchant);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.VengefulMirror)
                {
                    playerState.PerformTransition(Transition.TRi_VengefulMirror);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.VampiricSurge)
                {
                    playerState.PerformTransition(Transition.TRi_VampiricSurge);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.VeilOfFortitude)
                {
                    playerState.PerformTransition(Transition.TRi_VeilOfFortitude);
                }
            }
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!GameManager.Instance.spellInProgress)
        {
            if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.NoSpellsChosen)
            {
                roundEnd = true;
            }
            else if (!castASpell && performAct)
            {
                if (GameManager.Instance.playedSpells == 2)
                {
                    int whoAmI = -1;
                    if (player == GameManager.Instance.player1)
                    {
                        whoAmI = (int)PlayerType.PLAYER1;
                    }
                    else
                    {
                        whoAmI = (int)PlayerType.PLAYER2;
                    }

                    if (GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER1] == true && GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER2] == true)
                    {
                        GameManager.Instance.spellsThatHaveBeenCast[whoAmI] = false;
                        castASpell = true;
                        performAct = false;
                        playerState.finishedCurrentQTE = false;
                    }
                    else if (GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER1] == false && GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER2] == true)
                    {
                        castASpell = true;
                        performAct = false;
                        GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER2] = false;
                        GameManager.Instance.playedSpells = 0;
                        GameManager.Instance.spellIndex++;
                        playerState.finishedCurrentQTE = false;
                    }
                    else if (GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER2] == false && GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER1] == true)
                    {
                        castASpell = true;
                        performAct = false;
                        GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER1] = false;
                        GameManager.Instance.playedSpells = 0;
                        GameManager.Instance.spellIndex++;
                        playerState.finishedCurrentQTE = false;
                    }
                }
                else if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.PlayerOneIsFaster &&
                GameManager.Instance.currentCaster == GameManager.Instance.player1 && player == GameManager.Instance.player1)
                {
                    castASpell = true;
                }
                else if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.PlayerTwoIsFaster &&
                    GameManager.Instance.currentCaster == GameManager.Instance.player2 && player == GameManager.Instance.player2)
                {
                    castASpell = true;
                }
                else if (GameManager.Instance.currentCaster == GameManager.Instance.player1 && player == GameManager.Instance.player1)
                {
                    castASpell = true;
                }
                else if (GameManager.Instance.currentCaster == GameManager.Instance.player2 && player == GameManager.Instance.player2)
                {
                    castASpell = true;
                }
                else if (GameManager.Instance.playedSpells == 1)
                {
                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none
                        && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName != SpellNames.none)
                    {
                        castASpell = true;
                        performAct = false;
                        GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER2] = false;
                        GameManager.Instance.playedSpells = 0;
                        GameManager.Instance.spellIndex++;
                        playerState.finishedCurrentQTE = false;
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName != SpellNames.none
                        && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
                    {
                        castASpell = true;
                        performAct = false;
                        GameManager.Instance.spellsThatHaveBeenCast[(int)PlayerType.PLAYER1] = false;
                        GameManager.Instance.playedSpells = 0;
                        GameManager.Instance.spellIndex++;
                        playerState.finishedCurrentQTE = false;
                    }
                }
            }
        }
    }

}
