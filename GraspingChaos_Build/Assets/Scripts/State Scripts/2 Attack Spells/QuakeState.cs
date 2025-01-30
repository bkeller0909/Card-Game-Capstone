public class QuakeState : FSMState
{
    PlayerState playerState;
    //Constructor
    public QuakeState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_Quake;
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
