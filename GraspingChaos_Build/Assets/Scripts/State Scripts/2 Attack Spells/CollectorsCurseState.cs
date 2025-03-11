/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         CollectorsCurseState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Collectors curse spell
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class CollectorsCurseState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public CollectorsCurseState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_CollectorsCurse;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse].qteAmount;
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
                    ParticleManger.Instance.StartParticle(SpellNames.CollectorsCurse, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, enemy.gamepad);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CollectorsCurse, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, enemy.gamepad);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CollectorsCurse, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, enemy.gamepad);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, player.gamepad);
                }

                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CollectorsCurse, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, enemy.gamepad);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CollectorsCurse, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, enemy.gamepad);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CollectorsCurse, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, enemy.gamepad);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.3f, player.gamepad);
                }

                GameManager.Instance.particleP2Done = true;
            }


            if (player == GameManager.Instance.player1 && GameManager.Instance.particleP1Done && GameManager.Instance.coroutineWaitP1)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                nextState = "Deciding";
                GameManager.Instance.particleP1Done = false;
                GameManager.Instance.coroutineWaitP1 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    //steal ring
                    bool ringPlaced = false;

                    int whatRing = -6;
                    bool found = false;
                    for (int i = 0; i < 14; i++)
                    {
                        if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                        {
                            found = true;
                            whatRing = i;

                            enemy.ToggleRing(false, (Rings)i, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                            break;
                        }
                    }
                    if (found)
                    {
                        while (!ringPlaced)
                        {
                            PlayerFingers theFing = player.GetRandomFinger(PlayerFingers.none);

                            if (theFing != PlayerFingers.none)
                            {
                                bool spotTaken = false;

                                for (int i = 0; i < 14; i++)
                                {
                                    if (player.ringHandler.ringsActive[i, (int)theFing] == true)
                                    {
                                        spotTaken = true;
                                        break;
                                    }
                                }

                                if (!spotTaken)
                                {
                                    //Turns The Ring on
                                    player.ringHandler.ringsActive[whatRing, (int)theFing] = true;
                                    player.ToggleRing(true, (Rings)whatRing, theFing);
                                    player.ringHandler.ringStartRound[whatRing] = GameManager.Instance.whatRound;
                                    ringPlaced = true;
                                }
                            }
                            else
                            {
                                ringPlaced = true;
                            }
                        }
                    }
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
            }

            if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                nextState = "Deciding";
                GameManager.Instance.particleP2Done = false;
                GameManager.Instance.coroutineWaitP2 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    //steal ring
                    bool ringPlaced = false;

                    int whatRing = -6;
                    bool found = false;
                    for (int i = 0; i < 14; i++)
                    {
                        if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                        {
                            found = true;
                            whatRing = i;

                            enemy.ToggleRing(false, (Rings)i, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                            break;
                        }
                    }
                    if (found) 
                    {
                        while (!ringPlaced)
                        {
                            PlayerFingers theFing = player.GetRandomFinger(PlayerFingers.none);

                            if (theFing != PlayerFingers.none)
                            {
                                bool spotTaken = false;

                                for (int i = 0; i < 14; i++)
                                {
                                    if (player.ringHandler.ringsActive[i, (int)theFing] == true)
                                    {
                                        spotTaken = true;
                                        break;
                                    }
                                }

                                if (!spotTaken)
                                {
                                    //Turns The Ring on
                                    player.ringHandler.ringsActive[whatRing, (int)theFing] = true;
                                    player.ToggleRing(true, (Rings)whatRing, theFing);
                                    player.ringHandler.ringStartRound[whatRing] = GameManager.Instance.whatRound;
                                    ringPlaced = true;
                                }
                            }
                            else
                            {
                                ringPlaced = true;
                            }
                        }
                    }
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
            }
        }

    }

}
