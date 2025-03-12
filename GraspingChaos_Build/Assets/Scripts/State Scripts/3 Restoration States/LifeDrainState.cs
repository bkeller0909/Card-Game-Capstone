/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        LifeDrainState
//  Date Created: 02/5/2025
//  Purpose:      Spell state for the Life drain spell
//  Instance?     no
//-----------------------------------------------------------------
/// <summary>
/// </summary>
public class LifeDrainState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    PlayerFingers randomFinger1;
    PlayerFingers randomFinger2;
    //Constructor
    public LifeDrainState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_LifeDrain;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LifeDrain].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LifeDrain].qteAmount;
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
                    //does nothing
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    //ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, randomFinger1, player);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    //ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger1 = enemy.GetRandomFinger(PlayerFingers.none);
                    ParticleManger.Instance.StartParticle(SpellNames.LifeDrain, randomFinger1, player);
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
                    enemy.health.DamageFinger(randomFinger1);
                    randomFinger2 = player.GetRandomHurtFingers(PlayerFingers.none);
                    player.health.HealFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(randomFinger1);
                    randomFinger2 = player.GetRandomHurtFingers(PlayerFingers.none);
                    player.health.HealFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger2 = enemy.GetRandomFinger(randomFinger1);
                    for (int i = 0; i < 2; i++)
                    {
                        enemy.health.DamageFinger(randomFinger1);
                        enemy.health.DamageFinger(randomFinger2);
                    }

                    randomFinger1 = player.GetRandomHurtFingers(PlayerFingers.none);
                    randomFinger2 = player.GetRandomHurtFingers(randomFinger1);
                    for (int s = 0; s < 2; s++)
                    {
                        player.health.HealFinger(randomFinger1);
                        player.health.HealFinger(randomFinger2);
                    }
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
                    enemy.health.DamageFinger(randomFinger1);
                    randomFinger2 = player.GetRandomFinger(PlayerFingers.none);
                    player.health.HealFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(randomFinger1);
                    randomFinger2 = player.GetRandomFinger(PlayerFingers.none);
                    player.health.HealFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger2 = enemy.GetRandomFinger(randomFinger1);
                    for (int i = 0; i < 2; i++)
                    {
                        enemy.health.DamageFinger(randomFinger1);
                        enemy.health.DamageFinger(randomFinger2);
                    }

                    randomFinger1 = player.GetRandomFinger(PlayerFingers.none);
                    randomFinger2 = player.GetRandomFinger(randomFinger1);
                    for (int s = 0; s < 2; s++)
                    {
                        player.health.HealFinger(randomFinger1);
                        player.health.HealFinger(randomFinger2);
                    }
                }
            }
        }
    }

}
