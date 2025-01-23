public class LeftningBoltState : FSMState
{
    PlayerState playerState;
    //Constructor
    public LeftningBoltState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_LeftningBolt;
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
