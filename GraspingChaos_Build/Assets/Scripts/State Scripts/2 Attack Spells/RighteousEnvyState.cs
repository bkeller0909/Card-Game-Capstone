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
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.RighteousEnvy].qteAmount;
            nextState = "QTE";
        }
        else
        {
            if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Failure)
            {
                //do nothing
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Half)
            {
                //do nothing
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Success)
            {
                int totalDamage = player.GetRightHandFingerDeath();
                int partDamage = totalDamage / 2;
                PlayerFingers randomFinger = enemy.GetRandomFinger();
                for (int i = 0; i < partDamage; i++)
                {
                    enemy.health.DamageFinger(randomFinger);
                }

                partDamage = totalDamage - partDamage;
                randomFinger = enemy.GetRandomFinger();
                for (int i = 0; i < partDamage; i++)
                {
                    enemy.health.DamageFinger(randomFinger);
                }
            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;

            nextState = "Deciding";
        }
    }

}