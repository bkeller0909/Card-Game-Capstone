public class DealStatsState : FSMState
{
    PlayerState playerState;
    //Constructor
    public DealStatsState(PlayerState pS)
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

    }

}
