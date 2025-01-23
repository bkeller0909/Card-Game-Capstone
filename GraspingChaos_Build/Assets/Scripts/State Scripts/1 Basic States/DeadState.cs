public class DeadState : FSMState
{
    PlayerState playerState;

    //Constructor
    public DeadState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Dead;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {

    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {

    }
}
