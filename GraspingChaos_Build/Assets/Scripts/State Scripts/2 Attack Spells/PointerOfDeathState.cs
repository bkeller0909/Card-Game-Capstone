/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         PointerOfDeathState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Pointer of Death state 
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class PointerOfDeathState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public PointerOfDeathState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_PointerOfDeath;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].qteAmount;

            //This checks if the player has the finger bonus for Pointer Of Death
            if (player.AreTheseFingersAlive(PlayerFingers.LH_Index, PlayerFingers.RH_Index) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.LH_Index] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.LH_Index] != true) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.RH_Index] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.RH_Index] != true))
            {
                playerState.currentQTEAmount = playerState.currentQTEAmount - 3;
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
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
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
                    int indexVeil = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }

                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                   ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            if (indexVeil == 2)
                            {
                                if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 2);
                                }
                                break;
                            }
                        }
                        indexVeil++;
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if(indexVeil >= 3)
                    {
                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 3);
                        }
                    }

                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 3);
                    }


                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeft == true) ||
                               ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRight == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else
                        {
                            PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(rightFinger);
                            if ((((int)rightFinger >= 0 && (int)rightFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)rightFinger >= 5 && (int)rightFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(leftFinger);
                            if ((((int)leftFinger >= 0 && (int)leftFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)leftFinger >= 5 && (int)leftFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 3);
                    }


                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeft == true) ||
                               ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRight == true)
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else
                        {
                            PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(rightFinger);
                            if ((((int)rightFinger >= 0 && (int)rightFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)rightFinger >= 5 && (int)rightFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(leftFinger);
                            if ((((int)leftFinger >= 0 && (int)leftFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)leftFinger >= 5 && (int)leftFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {

                            if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else
                            {
                                PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                                enemy.health.DamageFinger(rightFinger);
                                if ((((int)rightFinger >= 0 && (int)rightFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)rightFinger >= 5 && (int)rightFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                                PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                                enemy.health.DamageFinger(leftFinger);
                                if ((((int)leftFinger >= 0 && (int)leftFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)leftFinger >= 5 && (int)leftFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

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
                    int indexVeil = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }

                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                   ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            if (indexVeil == 2)
                            {
                                if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 2);
                                }
                                break;
                            }
                        }
                        indexVeil++;
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if (indexVeil >= 3)
                    {
                        if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 3);
                        }
                    }

                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 3);
                    }


                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeft == true) ||
                               ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRight == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else
                        {
                            PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(rightFinger);
                            if ((((int)rightFinger >= 0 && (int)rightFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)rightFinger >= 5 && (int)rightFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(leftFinger);
                            if ((((int)leftFinger >= 0 && (int)leftFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)leftFinger >= 5 && (int)leftFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 3);
                    }


                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeft == true) ||
                               ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.veilOfFortitudeRight == true)
                    {
                        if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                        {
                            PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                            enemy.health.DamageFinger(newFinger);
                            if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }
                        }
                        else
                        {
                            PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(rightFinger);
                            if ((((int)rightFinger >= 0 && (int)rightFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)rightFinger >= 5 && (int)rightFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                            PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.health.DamageFinger(leftFinger);
                            if ((((int)leftFinger >= 0 && (int)leftFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)leftFinger >= 5 && (int)leftFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                            {
                                player.ringHandler.EffectVengFulMirror(player, 1);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {

                            if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                            {
                                PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                                enemy.health.DamageFinger(newFinger);
                                if ((((int)newFinger >= 0 && (int)newFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)newFinger >= 5 && (int)newFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                            }
                            else
                            {
                                PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                                enemy.health.DamageFinger(rightFinger);
                                if ((((int)rightFinger >= 0 && (int)rightFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)rightFinger >= 5 && (int)rightFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                                {
                                    player.ringHandler.EffectVengFulMirror(player, 1);
                                }

                                PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                                enemy.health.DamageFinger(leftFinger);
                                if ((((int)leftFinger >= 0 && (int)leftFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                                    ((int)leftFinger >= 5 && (int)leftFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
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

}
