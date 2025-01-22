public class RightningBoltState : FSMState
{
    PlayerState playerState;
    //Constructor
    public RightningBoltState(PlayerState pS)
    {
        playerState = pS;
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
