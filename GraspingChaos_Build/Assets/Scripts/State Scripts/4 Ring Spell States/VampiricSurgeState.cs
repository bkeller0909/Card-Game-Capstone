public class VampiricSurgeState : FSMState
{
    //refernece for player state
    PlayerState playerState;
    //reference for what player it is based in int values
    private int playerIndex;
    //string for next state
    private string nextState;
    public VampiricSurgeState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_VampiricSurge;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        //get qte amount of the speel
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.VampiricSurge].qteAmount;
        nextState = "";
        //find the player and assign it to our index
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
        //switch states if set to
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
            //create the qte sequence
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.VampiricSurge].qteAmount;
            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            bool spotTaken = false;

            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (playerState.HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
                {
                    for (int i = 0; i < 14; i++)
                    {
                        if (player.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                        {
                            spotTaken = true;
                        }
                    }

                    if (!spotTaken)
                    {
                        if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                        {
                            ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                        else
                        {
                            ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                    }
                    else
                    {
                        ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                    }
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                }

                GameManager.Instance.particleP1Done = true;

            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (playerState.HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
                {
                    for (int i = 0; i < 14; i++)
                    {
                        if (player.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                        {
                            spotTaken = true;
                        }
                    }

                    if (!spotTaken)
                    {
                        if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                        {
                            ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                        else
                        {
                            ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                    }
                    else
                    {
                        ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                    }
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
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
                if (enemy.gameObject.GetComponent<PlayerState>().HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
                {
                    for (int i = 0; i < 14; i++)
                    {
                        if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                        {
                            spotTaken = true;
                        }
                    }

                    if (!spotTaken)
                    {
                        if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                        {
                            //Turns The Ring on
                            enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            enemy.ToggleRing(true, Rings.VampiricSurgeFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFull] = GameManager.Instance.whatRound;
                        }
                        else
                        {
                            //Turns The Ring on
                            enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            enemy.ToggleRing(true, Rings.VampiricSurgeFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFail] = GameManager.Instance.whatRound;
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
                if (enemy.gameObject.GetComponent<PlayerState>().HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
                {
                    for (int i = 0; i < 14; i++)
                    {
                        if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                        {
                            spotTaken = true;
                        }
                    }

                    if (!spotTaken)
                    {
                        if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                        {
                            //Turns The Ring on
                            enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            enemy.ToggleRing(true, Rings.VampiricSurgeFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFull] = GameManager.Instance.whatRound;
                        }
                        else
                        {
                            //Turns The Ring on
                            enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            enemy.ToggleRing(true, Rings.VampiricSurgeFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFail] = GameManager.Instance.whatRound;
                        }
                    }
                }
            }
        }
    }

    private void Maintinance(PlayerManager player, PlayerManager enemy)
    {
        GameManager.Instance.spellInProgress = true;
        bool spotTaken = false;

        if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
        {
            if (enemy.gameObject.GetComponent<PlayerState>().HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
            {
                for (int i = 0; i < 14; i++)
                {
                    if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                    {
                        spotTaken = true;
                    }
                }

                if (!spotTaken)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                }
            }
            else
            {
                ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
            }
            GameManager.Instance.particleP1Done = true;
        }

        if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
        {
            if (enemy.gameObject.GetComponent<PlayerState>().HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
            {
                for (int i = 0; i < 14; i++)
                {
                    if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                    {
                        spotTaken = true;
                    }
                }

                if (!spotTaken)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                }
            }
            else
            {
                ParticleManger.Instance.StartParticle(SpellNames.VampiricSurge, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
            }
            GameManager.Instance.particleP2Done = true;
        }

        if (player == GameManager.Instance.player1 && GameManager.Instance.particleP1Done && GameManager.Instance.coroutineWaitP1)
        {
            GameManager.Instance.ChangeCurrentCaster();
            //GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
            GameManager.Instance.totalSpellsPickedP1--;
            GameManager.Instance.coroutineWaitP1 = false;
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            GameManager.Instance.particleP1Done = false;
            nextState = "Deciding";
            if (enemy.gameObject.GetComponent<PlayerState>().HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
            {
                for (int i = 0; i < 14; i++)
                {
                    if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                    {
                        spotTaken = true;
                    }
                }

                if (!spotTaken)
                {
                    if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                    {
                        //Turns The Ring on
                        enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        enemy.ToggleRing(true, Rings.VampiricSurgeFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFull] = GameManager.Instance.whatRound;
                    }
                    else
                    {
                        //Turns The Ring on
                        enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        enemy.ToggleRing(true, Rings.VampiricSurgeFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFail] = GameManager.Instance.whatRound;
                    }
                }
            }
        }

        if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
        {
            GameManager.Instance.ChangeCurrentCaster();
            //GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
            GameManager.Instance.totalSpellsPickedP2--;
            GameManager.Instance.coroutineWaitP2 = false;
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            GameManager.Instance.particleP2Done = false;
            nextState = "Deciding";
            if (enemy.gameObject.GetComponent<PlayerState>().HealthyFingerForRing(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger))
            {
                for (int i = 0; i < 14; i++)
                {
                    if (enemy.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                    {
                        spotTaken = true;
                    }
                }

                if (!spotTaken)
                {
                    if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                    {
                        //Turns The Ring on
                        enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        enemy.ToggleRing(true, Rings.VampiricSurgeFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFull] = GameManager.Instance.whatRound;
                    }
                    else
                    {
                        //Turns The Ring on
                        enemy.ringHandler.ringsActive[(int)Rings.VampiricSurgeFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        enemy.ToggleRing(true, Rings.VampiricSurgeFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.ringHandler.ringStartRound[(int)Rings.VampiricSurgeFail] = GameManager.Instance.whatRound;
                    }
                }
            }
        }
    }

}