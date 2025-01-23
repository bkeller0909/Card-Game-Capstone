public class RockThrowState : FSMState
{
    PlayerState playerState;
    //Constructor
    public RockThrowState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RockThrow;
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
