public class FireBoltState : FSMState
{
    PlayerState playerState;
    //Constructor
    public FireBoltState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_FireBolt;
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
