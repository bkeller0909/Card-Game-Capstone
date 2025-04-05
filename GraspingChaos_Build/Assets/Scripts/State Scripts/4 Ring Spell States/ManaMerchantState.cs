public class ManaMerchantState : FSMState
{
    //refernece for player state
    PlayerState playerState;
    //reference for what player it is based in int values
    private int playerIndex;
    //string for next state
    private string nextState;
    public ManaMerchantState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_ManaMerchant;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        //get qte amount of the speel
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ManaMerchant].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ManaMerchant].qteAmount;
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
                            ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                        else
                        {
                            ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                    }
                    else
                    {
                        ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                    }
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
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
                            ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                        else
                        {
                            ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                        }
                    }
                    else
                    {
                        ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                    }
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
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
                            //Turns The Ring on
                            player.ringHandler.ringsActive[(int)Rings.ManaMerchantFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            player.ToggleRing(true, Rings.ManaMerchantFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFull] = GameManager.Instance.whatRound;
                            //player.GetComponent<RingsHandler>().manaMerchantSuccess = true;
                        }
                        else
                        {
                            //Turns The Ring on
                            player.ringHandler.ringsActive[(int)Rings.ManaMerchantFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            player.ToggleRing(true, Rings.ManaMerchantFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFail] = GameManager.Instance.whatRound;
                            //player.GetComponent<RingsHandler>().manaMerchantFailure = true;
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
                            //Turns The Ring on
                            player.ringHandler.ringsActive[(int)Rings.ManaMerchantFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            player.ToggleRing(true, Rings.ManaMerchantFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFull] = GameManager.Instance.whatRound;
                            //player.GetComponent<RingsHandler>().manaMerchantSuccess = true;
                        }
                        else
                        {
                            //Turns The Ring on
                            player.ringHandler.ringsActive[(int)Rings.ManaMerchantFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                            player.ToggleRing(true, Rings.ManaMerchantFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFail] = GameManager.Instance.whatRound;
                            //player.GetComponent<RingsHandler>().manaMerchantFailure = true;
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
                    ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                }
            }
            else
            {
                ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
            }
            GameManager.Instance.particleP1Done = true;
        }

        if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
        {
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
                    ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 4);
                }
                else
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
                }
            }
            else
            {
                ParticleManger.Instance.StartParticle(SpellNames.ManaMerchant, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 5);
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
                        //Turns The Ring on
                        player.ringHandler.ringsActive[(int)Rings.ManaMerchantFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        player.ToggleRing(true, Rings.ManaMerchantFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFull] = GameManager.Instance.whatRound;
                        //player.GetComponent<RingsHandler>().manaMerchantSuccess = true;
                    }
                    else
                    {
                        //Turns The Ring on
                        player.ringHandler.ringsActive[(int)Rings.ManaMerchantFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        player.ToggleRing(true, Rings.ManaMerchantFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFail] = GameManager.Instance.whatRound;
                        //player.GetComponent<RingsHandler>().manaMerchantFailure = true;
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
                        //Turns The Ring on
                        player.ringHandler.ringsActive[(int)Rings.ManaMerchantFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        player.ToggleRing(true, Rings.ManaMerchantFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFull] = GameManager.Instance.whatRound;
                        //player.GetComponent<RingsHandler>().manaMerchantSuccess = true;
                    }
                    else
                    {
                        //Turns The Ring on
                        player.ringHandler.ringsActive[(int)Rings.ManaMerchantFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                        player.ToggleRing(true, Rings.ManaMerchantFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFail] = GameManager.Instance.whatRound;
                        //player.GetComponent<RingsHandler>().manaMerchantFailure = true;
                    }
                }
            }
        }
    }

}