/// <summary>
//----------------------------------------------------------------
//  OG Author:     Sebastian
//  Title:         IciclesState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Icicles spells
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class IciclesState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    PlayerFingers randomFinger1;
    PlayerFingers randomFinger2;
    //Constructor
    public IciclesState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_Icicles;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles].qteAmount;

            //This checks if the player has the finger bonus for icicles
            if (player.AreTheseFingersAlive(PlayerFingers.LH_Pinky, PlayerFingers.RH_Pinky) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.LH_Pinky] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.LH_Pinky] != true) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.RH_Pinky] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.RH_Pinky] != true))
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
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player, 1);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player, 1);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player, 3);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player, 1);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player, 1);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player, 3);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
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
                    enemy.health.DamageFinger(randomFinger1);
                    if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    randomFinger2 = enemy.GetRandomFinger(randomFinger1);
                    enemy.health.DamageFinger(randomFinger2);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    enemy.ResetHandAnimations();
                    if ((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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

                    RumbleManager.Instance.ControllerRumble(0.75f, 0.75f, 0.8f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.02f, 0.8f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(randomFinger1);
                    if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    randomFinger2 = enemy.GetRandomFinger(randomFinger1);
                    enemy.health.DamageFinger(randomFinger2);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    enemy.ResetHandAnimations();
                    if ((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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

                    RumbleManager.Instance.ControllerRumble(0.75f, 0.75f, 0.8f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.02f, 0.8f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    RumbleManager.Instance.ControllerRumble(0.75f, 0.75f, 0.8f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.02f, 0.8f);

                    for (int i = 0; i < 2; i++)
                    {
                        if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            enemy.health.DamageFinger(randomFinger1);
                            break;
                        }
                        else
                        {
                            enemy.health.DamageFinger(randomFinger1);
                        }
                    }

                    if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 2);
                    }


                    randomFinger2 = player.GetRandomFinger(randomFinger1);
                    for (int s = 0; s < 2; s++)
                    {
                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            enemy.health.DamageFinger(randomFinger2);
                            break;
                        }
                        else
                        {
                            enemy.health.DamageFinger(randomFinger2);
                        }
                    }

                    enemy.ResetHandAnimations();
                    if ((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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

                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 2);
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
                    enemy.health.DamageFinger(randomFinger1);
                    if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    randomFinger2 = enemy.GetRandomFinger(randomFinger1);
                    enemy.health.DamageFinger(randomFinger2);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    enemy.ResetHandAnimations();
                    if ((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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

                    RumbleManager.Instance.ControllerRumble(0.75f, 0.75f, 0.8f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.02f, 0.8f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(randomFinger1);
                    if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    randomFinger2 = enemy.GetRandomFinger(randomFinger1);
                    enemy.health.DamageFinger(randomFinger2);
                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }

                    enemy.ResetHandAnimations();
                    if ((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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

                    RumbleManager.Instance.ControllerRumble(0.75f, 0.75f, 0.8f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.02f, 0.8f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    RumbleManager.Instance.ControllerRumble(0.75f, 0.75f, 0.8f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.02f, 0.8f);

                    for (int i = 0; i < 2; i++)
                    {
                        if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            enemy.health.DamageFinger(randomFinger1);
                            break;
                        }
                        else
                        {
                            enemy.health.DamageFinger(randomFinger1);
                        }
                    }

                    if ((((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 2);
                    }


                    randomFinger2 = player.GetRandomFinger(randomFinger1);
                    for (int s = 0; s < 2; s++)
                    {
                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            enemy.health.DamageFinger(randomFinger2);
                            break;
                        }
                        else
                        {
                            enemy.health.DamageFinger(randomFinger2);
                        }
                    }

                    enemy.ResetHandAnimations();
                    if ((int)randomFinger1 >= 0 && (int)randomFinger1 <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                    {
                        enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                        enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                    }
                    else if ((int)randomFinger1 >= 5 && (int)randomFinger1 <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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

                    if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 2);
                    }

                }
            }
        }
    }

}
