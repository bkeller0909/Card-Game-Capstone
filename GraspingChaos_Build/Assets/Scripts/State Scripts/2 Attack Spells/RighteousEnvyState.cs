/// <summary>
//----------------------------------------------------------------
//  OG Author:     Sebastian
//  Title:         RighteousEnvyState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Right-eous envy spells
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class RighteousEnvyState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public RighteousEnvyState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RighteousEnvy;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.RighteousEnvy].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.RighteousEnvy].qteAmount;
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
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player, 3);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player, 3);
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
                    int totalDamage = player.health.GetHowManyBonesMissing(false) / 2;
                    if (totalDamage == 1)
                    {
                        PlayerFingers randomFinger = enemy.GetRandomFinger(PlayerFingers.none);
                        enemy.health.DamageFinger(randomFinger);
                        enemy.ResetHandAnimations();
                        if ((int)randomFinger >= 0 && (int)randomFinger <= 4)
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
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }
                    }
                    else if (totalDamage == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        int partDamage = totalDamage / 2;
                        int savedPartDamage = partDamage;
                        PlayerFingers randomFinger = enemy.GetRandomFinger(PlayerFingers.none);
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            if (savedPartDamage > 1)
                            {
                                partDamage = savedPartDamage / 2;
                            }
                        }

                        for (int i = 0; i < partDamage; i++)
                        {
                            enemy.health.DamageFinger(randomFinger);
                        }

                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, partDamage);
                        }

                        partDamage = totalDamage - savedPartDamage;
                        savedPartDamage = partDamage;
                        PlayerFingers randomFinger2 = enemy.GetRandomFinger(randomFinger);

                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            if (savedPartDamage > 1)
                            {
                                partDamage = savedPartDamage / 2;
                            }
                        }

                        for (int i = 0; i < partDamage; i++)
                        {
                            enemy.health.DamageFinger(randomFinger2);
                        }

                        enemy.ResetHandAnimations();
                        if ((int)randomFinger >= 0 && (int)randomFinger <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                        {
                            enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                            enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                        }
                        else if ((int)randomFinger >= 5 && (int)randomFinger <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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
                            player.ringHandler.EffectVengFulMirror(player, partDamage);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    int totalDamage = player.health.GetHowManyBonesMissing(false) / 2;
                    if (totalDamage == 1)
                    {
                        PlayerFingers randomFinger = enemy.GetRandomFinger(PlayerFingers.none);
                        enemy.health.DamageFinger(randomFinger);
                        enemy.ResetHandAnimations();
                        if ((int)randomFinger >= 0 && (int)randomFinger <= 4)
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
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, 1);
                        }
                    }
                    else if (totalDamage == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        int partDamage = totalDamage / 2;
                        int savedPartDamage = partDamage;
                        PlayerFingers randomFinger = enemy.GetRandomFinger(PlayerFingers.none);
                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            if (savedPartDamage > 1)
                            {
                                partDamage = savedPartDamage / 2;
                            }
                        }

                        for (int i = 0; i < partDamage; i++)
                        {
                            enemy.health.DamageFinger(randomFinger);
                        }

                        if ((((int)randomFinger >= 0 && (int)randomFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)randomFinger >= 5 && (int)randomFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                        {
                            player.ringHandler.EffectVengFulMirror(player, partDamage);
                        }

                        partDamage = totalDamage - savedPartDamage;
                        savedPartDamage = partDamage;
                        PlayerFingers randomFinger2 = enemy.GetRandomFinger(randomFinger);

                        if ((((int)randomFinger2 >= 0 && (int)randomFinger2 <= 4) && enemy.ringHandler.veilOfFortitudeLeftFail == true) ||
                                ((int)randomFinger2 >= 5 && (int)randomFinger2 <= 9) && enemy.ringHandler.veilOfFortitudeRightFail == true)
                        {
                            if (savedPartDamage > 1)
                            {
                                partDamage = savedPartDamage / 2;
                            }
                        }

                        for (int i = 0; i < partDamage; i++)
                        {
                            enemy.health.DamageFinger(randomFinger2);
                        }

                        enemy.ResetHandAnimations();
                        if ((int)randomFinger >= 0 && (int)randomFinger <= 4 && (int)randomFinger2 >= 0 && (int)randomFinger2 <= 4)
                        {
                            enemy.PlayerHands.SetTrigger("LeftHandDamaged");
                            enemy.PlayerFakeHands.SetTrigger("LeftHandDamaged");
                        }
                        else if ((int)randomFinger >= 5 && (int)randomFinger <= 9 && (int)randomFinger2 >= 5 && (int)randomFinger2 <= 9)
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
                            player.ringHandler.EffectVengFulMirror(player, partDamage);
                        }
                    }
                }
            }
        }
    }

}