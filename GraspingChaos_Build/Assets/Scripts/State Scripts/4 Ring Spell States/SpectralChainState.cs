public class SpectralChainState : FSMState
{
    PlayerState playerState;
    //Constructor
    public SpectralChainState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_SpectralChain;
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