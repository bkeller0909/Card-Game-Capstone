/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         RockThrowState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Rock throw spell
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class RockThrowState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public RockThrowState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RockThrow;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].qteAmount;
        nextState = "";
        if (playerState.player == GameManager.Instance.player1)
        {
            playerIndex = (int)PlayerType.PLAYER1;
        }
        else
        {
            playerIndex = (int)PlayerType.PLAYER2;
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (nextState == "Deciding")
        {
            GameManager.Instance.spellInProgress = false;
            playerState.PerformTransition(Transition.NeedDecision);
        }
        else if (nextState == "QTE")
        {
            playerState.PerformTransition(Transition.Challenge);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!playerState.finishedCurrentQTE)
        {
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].qteAmount;

            //This checks if the player has the finger bonus for rockthrow
            if (player.AreTheseFingersAlive(PlayerFingers.LH_Index, PlayerFingers.RH_Index) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.LH_Index] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.LH_Index] != true) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.RH_Index] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.RH_Index] != true))
            {
                playerState.currentQTEAmount = playerState.currentQTEAmount - 2;
            }

            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.5f, player.gamepad);
                }

                GameManager.Instance.particleP1Done = true;

            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.5f, player.gamepad);
                }
                GameManager.Instance.particleP2Done = true;
            }

            if (player == GameManager.Instance.player1 && GameManager.Instance.particleP1Done && GameManager.Instance.coroutineWaitP1)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                GameManager.Instance.totalSpellsPickedP1--;
                nextState = "Deciding";
                GameManager.Instance.particleP1Done = false;
                GameManager.Instance.coroutineWaitP1 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    enemy.ResetHandAnimations();
                    if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else
                    {
                        enemy.PlayerHands.SetTrigger("RightHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    enemy.BackToIDLE();

                }
                else
                {
                    PlayerFingers randomFinger = player.GetRandomFinger(PlayerFingers.none);
                    player.health.DamageFinger(randomFinger);
                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, player.gamepad);
                    player.cameraHandler.CameraShake(0.05f, 0.5f);

                    player.ResetHandAnimations();
                    if ((int)randomFinger >= 0 && (int)randomFinger <= 4)
                    {
                        player.PlayerHands.SetTrigger("LeftHandDamaged");
                        player.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else
                    {
                        player.PlayerHands.SetTrigger("RightHandDamaged");
                        player.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    player.BackToIDLE();

                }
            }

            if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                GameManager.Instance.totalSpellsPickedP2--;
                nextState = "Deciding";
                GameManager.Instance.particleP2Done = false;
                GameManager.Instance.coroutineWaitP2 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    enemy.ResetHandAnimations();
                    if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else
                    {
                        enemy.PlayerHands.SetTrigger("RightHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    enemy.BackToIDLE();

                }
                else
                {
                    PlayerFingers randomFinger = player.GetRandomFinger(PlayerFingers.none);
                    player.health.DamageFinger(randomFinger);
                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, player.gamepad);
                    player.cameraHandler.CameraShake(0.05f, 0.5f);

                    player.ResetHandAnimations();
                    if ((int)randomFinger >= 0 && (int)randomFinger <= 4)
                    {
                        player.PlayerHands.SetTrigger("LeftHandDamaged");
                        player.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else
                    {
                        player.PlayerHands.SetTrigger("RightHandDamaged");
                        player.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    player.BackToIDLE();

                }
            }
        }
    }

}
