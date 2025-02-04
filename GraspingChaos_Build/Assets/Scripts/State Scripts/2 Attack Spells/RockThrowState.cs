public class RockThrowState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public RockThrowState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RockThrow;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].qteAmount;
            nextState = "QTE";
        }
        else
        {
            if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Failure)
            {
                //player.fingers[(int)player.GetRandomFinger()].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].damageValue;
                PlayerFingers randomFinger = player.GetRandomFinger();
                enemy.health.DamageFinger(randomFinger);
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Half)
            {
                //player.fingers[(int)player.GetRandomFinger()].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].damageValue;
                PlayerFingers randomFinger = player.GetRandomFinger();
                enemy.health.DamageFinger(randomFinger);
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Success)
            {
                //player.fingers[(int)player.GetRandomFinger()].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].damageValue;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
            }

            nextState = "Deciding";
        }
    }

}
