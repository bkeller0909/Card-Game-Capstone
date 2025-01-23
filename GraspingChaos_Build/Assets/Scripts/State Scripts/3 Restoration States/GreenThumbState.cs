public class GreenThumbState : FSMState
{
    PlayerState playerState;
    //Constructor
    public GreenThumbState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_GreenThumb;
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
