public class ForTheCauseState : FSMState
{
    PlayerState playerState;
    //Constructor
    public ForTheCauseState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_ForTheCause;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {

    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!playerState.finishedCurrentQTE)
        {

        }
        else
        {

        }
    }

}
