//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         FireBoltState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for FireBolt
//  Instance?      no
//-----------------------------------------------------------------
/// <summary>
/// </summary>
public class FireBoltState : FSMState
{
    //refernece for player state
    PlayerState playerState;
    //reference for what player it is based in int values
    private int playerIndex;
    //string for next state
    private string nextState;
    //Constructor
    public FireBoltState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_FireBolt;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        //get qte amount of the speel
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].qteAmount;
            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            if (!player.castAnimDone)
            {
                player.ResetHandAnimations();
                player.PlayerHands.SetTrigger("HandsCast1");
                player.PlayerFakeHands.SetTrigger("HandsCast1");
                player.WaitforCastingAnim();
            }
            //check if the player is player 1, if the particle has not been played and if the particle for player 1 is done
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done && player.castAnimDone)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.FireBolt, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    RumbleManager.Instance.ControllerRumble(0.1f, 0.1f, 0.2f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.FireBolt, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    RumbleManager.Instance.ControllerRumble(0.1f, 0.1f, 0.2f, player.gamepad);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.FireBolt, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
                }
                player.BackToIDLE();
                //check if i am the second spell but the first cast
                GameManager.Instance.particleP1Done = true;
                //nextState = "Deciding";
            }//check if the player is player 2, if the particle has not been played and if the particle for player 2 is done
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done && player.castAnimDone)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.FireBolt, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.FireBolt, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.FireBolt, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.2f, player.gamepad);
                }
                player.BackToIDLE();
                //check if i am the second spell but the first cast
                GameManager.Instance.particleP2Done = true;
                //nextState = "Deciding";
            }

            if (player == GameManager.Instance.player1 && GameManager.Instance.particleP1Done && GameManager.Instance.coroutineWaitP1)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                GameManager.Instance.totalSpellsPickedP1--;
                GameManager.Instance.coroutineWaitP1 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
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
                    player.ImpactAndLeave();
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }
                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);
                    //enemy.BackToIDLE();
                }
            }

            if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                GameManager.Instance.totalSpellsPickedP2--;
                GameManager.Instance.coroutineWaitP2 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
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
                    player.ImpactAndLeave();
                    if ((((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 0 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 4) && enemy.ringHandler.vengfulMirrorLeft == true) ||
                       ((int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger >= 5 && (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger <= 9) && enemy.ringHandler.vengfulMirrorRight == true)
                    {
                        player.ringHandler.EffectVengFulMirror(player, 1);
                    }
                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);
                    //enemy.BackToIDLE();
                }
            }

            if(player.endStall)
            {
                nextState = "Deciding";
                GameManager.Instance.playedSpells++;
                player.castAnimDone = false;
                GameManager.Instance.particleP1Done = false;
                GameManager.Instance.particleP2Done = false;
                player.endStall = false;
            }
        }
    }

}
