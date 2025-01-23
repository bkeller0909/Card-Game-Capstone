public class StaticBlastState : FSMState
{
    PlayerState playerState;
    //Constructor
    public StaticBlastState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_StaticBlast;
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
