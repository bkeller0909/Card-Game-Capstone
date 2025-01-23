public class RighteousEnvyState : FSMState
{
    PlayerState playerState;
    //Constructor
    public RighteousEnvyState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RighteousEnvy;
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