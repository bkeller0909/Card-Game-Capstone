/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         TidalWaveState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Tidal Wave spell
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class TidalWaveState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public TidalWaveState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_TidalWave;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.TidalWave].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.TidalWave].qteAmount;

            //This checks if the player has the finger bonus for Tidalwave
            if (player.AreTheseFingersAlive(PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.LH_Thumb] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.LH_Thumb] != true) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.RH_Thumb] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.RH_Thumb] != true))
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
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //do no damage becuase of veil
                    }
                    else
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //do no damage becuase of veil
                    }
                    else
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    for (int i = 0; i < 2; i++)
                    {

                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                            break;
                        }

                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }

                    }

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }

                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }

                    }
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //do no damage becuase of veil
                    }
                    else
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //do no damage becuase of veil
                    }
                    else
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else
                        {
                            PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    for (int i = 0; i < 2; i++)
                    {

                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                            break;
                        }

                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }

                    }

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }

                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                            {
                                enemy.health.DamageFinger(currentAdjacent);
                                if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }
                                break;
                            }

                            enemy.health.DamageFinger(currentAdjacent);
                            if ((((int)currentAdjacent >= 0 && (int)currentAdjacent <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                            ((int)currentAdjacent >= 5 && (int)currentAdjacent <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }

                    }
                }
            }
        }
    }

}
