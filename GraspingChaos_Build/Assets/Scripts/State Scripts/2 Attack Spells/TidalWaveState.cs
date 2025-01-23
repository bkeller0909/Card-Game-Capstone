public class TidalWaveState : FSMState
{
    PlayerState playerState;
    //Constructor
    public TidalWaveState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_TidalWave;
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
