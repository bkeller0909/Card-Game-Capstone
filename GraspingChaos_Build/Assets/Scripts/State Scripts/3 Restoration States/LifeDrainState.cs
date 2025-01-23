public class LifeDrainState : FSMState
{
    PlayerState playerState;
    //Constructor
    public LifeDrainState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_LifeDrain;
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
