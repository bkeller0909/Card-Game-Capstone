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
            nextState = "QTE";
        }
        else
        {
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    randomFinger1 = enemy.GetRandomFinger();
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    randomFinger1 = enemy.GetRandomFinger();
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger1 = enemy.GetRandomFinger();
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    randomFinger1 = enemy.GetRandomFinger();
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    randomFinger1 = enemy.GetRandomFinger();
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    randomFinger1 = enemy.GetRandomFinger();
                    ParticleManger.Instance.StartParticle(SpellNames.Icicles, randomFinger1, player);
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
                    randomFinger2 = enemy.GetRandomFinger();
                    enemy.health.DamageFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(randomFinger1);
                    randomFinger2 = enemy.GetRandomFinger();
                    enemy.health.DamageFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        enemy.health.DamageFinger(randomFinger1);
                    }

                    randomFinger2 = player.GetRandomFinger();
                    for (int s = 0; s < 2; s++)
                    {
                        enemy.health.DamageFinger(randomFinger2);
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
                    randomFinger2 = enemy.GetRandomFinger();
                    enemy.health.DamageFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(randomFinger1);
                    randomFinger2 = enemy.GetRandomFinger();
                    enemy.health.DamageFinger(randomFinger2);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        enemy.health.DamageFinger(randomFinger1);
                    }

                    randomFinger2 = player.GetRandomFinger();
                    for (int s = 0; s < 2; s++)
                    {
                        enemy.health.DamageFinger(randomFinger2);
                    }
                }
            }
        }
    }

}
