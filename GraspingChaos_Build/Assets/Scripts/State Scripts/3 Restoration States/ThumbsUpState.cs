/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        ThumbsUpState
//  Date Created: 02/5/2025
//  Purpose:      spell state for Thumbs up spell
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class ThumbsUpState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public ThumbsUpState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_ThumbsUp;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ThumbsUp].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ThumbsUp].qteAmount;
            nextState = "QTE";
        }
        else
        {
            bool hasBonus = false;

            //This checks if the player has the finger bonus for Green Thumb
            if (player.AreTheseFingersAlive(PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.LH_Thumb] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.LH_Thumb] != true) &&
                (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.RH_Thumb] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.RH_Thumb] != true))
            {
                hasBonus = true;
            }

            GameManager.Instance.spellInProgress = true;
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ThumbsUp, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    GameManager.Instance.coroutineWaitP1 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ThumbsUp, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    GameManager.Instance.coroutineWaitP1 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ThumbsUp, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ThumbsUp, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    GameManager.Instance.coroutineWaitP2 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ThumbsUp, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    GameManager.Instance.coroutineWaitP2 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.ThumbsUp, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
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
                    player.health.HealFinger(PlayerFingers.LH_Thumb);
                    player.health.HealFinger(PlayerFingers.RH_Thumb);

                    if (hasBonus)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            PlayerFingers randomHeal = player.GetRandomHurtFingers(PlayerFingers.none);

                            if (randomHeal != PlayerFingers.none)
                            {
                                player.health.HealFinger(randomHeal);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    player.health.HealFinger(PlayerFingers.LH_Thumb);
                    player.health.HealFinger(PlayerFingers.RH_Thumb);
                }
            }

        }
    }

}
