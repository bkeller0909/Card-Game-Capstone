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
                    //do nothing
                    GameManager.Instance.coroutineWaitP1 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    //do nothing
                    GameManager.Instance.coroutineWaitP1 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    //do nothing
                    GameManager.Instance.coroutineWaitP2 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    //do nothing
                    GameManager.Instance.coroutineWaitP2 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.RighteousEnvy, PlayerFingers.RH_Middle, player);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    int totalDamage = player.health.GetHowManyBonesMissing(false) / 2;
                    int partDamage = totalDamage / 2;
                    PlayerFingers randomFinger = enemy.GetRandomFinger(PlayerFingers.none);
                    for (int i = 0; i < partDamage; i++)
                    {
                        enemy.health.DamageFinger(randomFinger);
                    }

                    partDamage = totalDamage - partDamage;
                    randomFinger = enemy.GetRandomFinger(randomFinger);
                    for (int i = 0; i < partDamage; i++)
                    {
                        enemy.health.DamageFinger(randomFinger);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    int totalDamage = player.health.GetHowManyBonesMissing(false) / 2;
                    int partDamage = totalDamage / 2;
                    PlayerFingers randomFinger = enemy.GetRandomFinger(PlayerFingers.none);
                    for (int i = 0; i < partDamage; i++)
                    {
                        enemy.health.DamageFinger(randomFinger);
                    }

                    partDamage = totalDamage - partDamage;
                    randomFinger = enemy.GetRandomFinger(randomFinger);
                    for (int i = 0; i < partDamage; i++)
                    {
                        enemy.health.DamageFinger(randomFinger);
                    }
                }
            }
        }
    }

}