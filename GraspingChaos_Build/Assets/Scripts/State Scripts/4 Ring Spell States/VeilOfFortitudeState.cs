public class VeilOfFortitudeState : FSMState
{
    PlayerState playerState;
    //Constructor
    public VeilOfFortitudeState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_VeilOfFortitude;
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