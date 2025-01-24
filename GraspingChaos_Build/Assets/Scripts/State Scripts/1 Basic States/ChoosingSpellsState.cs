public class ChoosingSpellsState : FSMState
{
    PlayerState playerState;
    //Constructor
    public ChoosingSpellsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.ChoosingSpells;
    }

    public override void EnterStateInit()
    {
        GameManager.Instance.amtOfSpellsBeingCast = 0;
        GameManager.Instance.nextTestState = false;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.nextTestState)
        {
            playerState.PerformTransition(Transition.NeedDecision);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {

    }
}
