public class ThumbsUpState : FSMState
{
    PlayerState playerState;
    //Constructor
    public ThumbsUpState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_ThumbsUp;
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
