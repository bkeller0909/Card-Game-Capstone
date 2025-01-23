public class VampiricSurgeState : FSMState
{
    PlayerState playerState;
    //Constructor
    public VampiricSurgeState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_VampiricSurge;
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