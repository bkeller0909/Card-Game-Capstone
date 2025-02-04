public class RightningBoltState : FSMState
{
    PlayerState playerState;
    string nextState;
    //Constructor
    public RightningBoltState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RightningBolt;
        nextState = "";
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
