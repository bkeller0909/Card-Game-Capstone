//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         QuakeState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Quake Spell
//  Instance?      no
//-----------------------------------------------------------------
using System;
using Unity.Properties;
using UnityEngine;

/// <summary>
/// </summary>
public class QuakeState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public QuakeState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_Quake;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Quake].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Quake].qteAmount;
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
                    ParticleManger.Instance.StartParticle(SpellNames.Quake, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.PulseRumble(0.2f, 0.2f, 0.5f, 1.5f, player.gamepad);
                    RumbleManager.Instance.PulseRumble(1f, 0.5f, 0.5f, 1.5f, enemy.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 1.5f);
                    enemy.cameraHandler.CameraShake(0.05f, 1.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Quake, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.PulseRumble(0.2f, 0.2f, 0.5f, 1.5f, player.gamepad);
                    RumbleManager.Instance.PulseRumble(1f, 0.5f, 0.5f, 1.5f, enemy.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 1.5f);
                    enemy.cameraHandler.CameraShake(0.05f, 1.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Quake, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.PulseRumble(0.2f, 0.2f, 0.5f, 1.5f, player.gamepad);
                    RumbleManager.Instance.PulseRumble(1f, 0.5f, 0.5f, 1.5f, enemy.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 1.5f);
                    enemy.cameraHandler.CameraShake(0.05f, 1.5f);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Quake, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.PulseRumble(0.2f, 0.2f, 0.5f, 1.5f, player.gamepad);
                    RumbleManager.Instance.PulseRumble(1f, 0.5f, 0.5f, 1.5f, enemy.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 1.5f);
                    enemy.cameraHandler.CameraShake(0.05f, 1.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Quake, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.PulseRumble(0.2f, 0.2f, 0.5f, 1.5f, player.gamepad);
                    RumbleManager.Instance.PulseRumble(1f, 0.5f, 0.5f, 1.5f, enemy.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 1.5f);
                    enemy.cameraHandler.CameraShake(0.05f, 1.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Quake, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.PulseRumble(0.2f, 0.2f, 0.5f, 1.5f, player.gamepad);
                    RumbleManager.Instance.PulseRumble(1f, 0.5f, 0.5f, 1.5f, enemy.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 1.5f);
                    enemy.cameraHandler.CameraShake(0.05f, 1.5f);
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
                    //randomize fingers tht are not selected
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    PlayerFingers randomFinger = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, PlayerFingers.none);
                    if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger);
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }

                    }

                    PlayerFingers randomFinger2 = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, randomFinger);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger2);
                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }
                    }

                    enemy.ResetHandAnimations();
                    if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4 && (int)randomFinger >= 0 && (int)randomFinger <= 4
                        && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9 && (int)randomFinger >= 5 && (int)randomFinger <= 9
                        && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
                    {
                        enemy.PlayerHands.SetTrigger("RightHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    else
                    {
                        enemy.PlayerHands.SetTrigger("BothHandsDamaged");
                        enemy.PlayerFakeHands.SetTrigger("BothHandsDamaged");
                    }
                    enemy.BackToIDLE();

                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    //randomize fingers tht are not selected
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    PlayerFingers randomFinger = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, PlayerFingers.none);
                    if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger);
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }

                    }

                    PlayerFingers randomFinger2 = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, randomFinger);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger2);
                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }
                    }


                    enemy.ResetHandAnimations();
                    if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4 && (int)randomFinger >= 0 && (int)randomFinger <= 4
                        && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9 && (int)randomFinger >= 5 && (int)randomFinger <= 9
                        && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
                    {
                        enemy.PlayerHands.SetTrigger("RightHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    else
                    {
                        enemy.PlayerHands.SetTrigger("BothHandsDamaged");
                        enemy.PlayerFakeHands.SetTrigger("BothHandsDamaged");
                    }
                    enemy.BackToIDLE();

                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
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
                    //randomize fingers tht are not selected
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    PlayerFingers randomFinger = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, PlayerFingers.none);
                    if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger);
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }

                    }

                    PlayerFingers randomFinger2 = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, randomFinger);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger2);
                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }
                    }

                    enemy.ResetHandAnimations();
                    if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4 && (int)randomFinger >= 0 && (int)randomFinger <= 4
                        && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9 && (int)randomFinger >= 5 && (int)randomFinger <= 9
                        && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
                    {
                        enemy.PlayerHands.SetTrigger("RightHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    else
                    {
                        enemy.PlayerHands.SetTrigger("BothHandsDamaged");
                        enemy.PlayerFakeHands.SetTrigger("BothHandsDamaged");
                    }
                    enemy.BackToIDLE();


                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    //randomize fingers tht are not selected
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    PlayerFingers randomFinger = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, PlayerFingers.none);
                    if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger);
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }

                    }

                    PlayerFingers randomFinger2 = enemy.GetRandomFingersForQuake(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, randomFinger);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                    {
                        //no damage becuase of veil
                    }
                    else
                    {
                        enemy.health.DamageFinger(randomFinger2);
                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }
                    }


                    enemy.ResetHandAnimations();
                    if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4 && (int)randomFinger >= 0 && (int)randomFinger <= 4
                        && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9 && (int)randomFinger >= 5 && (int)randomFinger <= 9
                        && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
                    {
                        enemy.PlayerHands.SetTrigger("RightHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("RightHandDamaged");
                    }
                    else
                    {
                        enemy.PlayerHands.SetTrigger("BothHandsDamaged");
                        enemy.PlayerFakeHands.SetTrigger("BothHandsDamaged");
                    }
                    enemy.BackToIDLE();

                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
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
            }

        }
    }

}
