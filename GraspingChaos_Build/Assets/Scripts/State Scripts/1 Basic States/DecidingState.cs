
public class DecidingState : FSMState
{
    PlayerState playerState;
    bool castASpell, performAct;
    //Constructor
    public DecidingState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Deciding;
    }

    public override void EnterStateInit()
    {
        if (!GameManager.Instance.roundCheck)
        {
            for (int i = 0; i < 3; i++)
            {
                // If player 1's first card and player 2's first card have the same mana cost
                if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none)
                {
                    GameManager.Instance.whoesOnFirst[i] = Decider.PlayerTwoIsFaster;
                }
                else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
                {
                    GameManager.Instance.whoesOnFirst[i] = Decider.PlayerOneIsFaster;
                }
                else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.manaCost == GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.manaCost)
                {
                    GameManager.Instance.whoesOnFirst[i] = Decider.Tie;
                }
                else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.manaCost < GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.manaCost)
                {
                    GameManager.Instance.whoesOnFirst[i] = Decider.PlayerOneIsFaster;
                }
                else if (GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER1].whatSpell.manaCost > GameManager.Instance.spellsBeingCast[i, (int)PlayerType.PLAYER2].whatSpell.manaCost)
                {
                    GameManager.Instance.whoesOnFirst[i] = Decider.PlayerTwoIsFaster;
                }
            }
            performAct = false;
            GameManager.Instance.roundCheck = true;
            castASpell = true;
        }
        else
        {
            castASpell = false;
            performAct = true;
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
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
        if (player.entireHP <= 0 || enemy.entireHP <= 0)
        {
            playerState.PerformTransition(Transition.died);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!castASpell && performAct)
        {
            if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.PlayerOneIsFaster && player == GameManager.Instance.player1)
            {
                castASpell = true;
            }
            else if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.PlayerTwoIsFaster && player == GameManager.Instance.player2)
            {
                castASpell = true;
            }
        }
    }

}
