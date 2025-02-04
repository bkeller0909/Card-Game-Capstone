public class FireBoltState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
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
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].qteAmount;
            nextState = "QTE";
        }
        else
        {
            if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Failure)
            {
                //does nothing
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Half)
            {
                //does nothing
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Success)
            {
                //enemy.fingers[(int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].damageValue;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
            }

            nextState = "Deciding";
        }
    }

}
