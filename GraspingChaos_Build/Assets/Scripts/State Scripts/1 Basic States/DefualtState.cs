public class DefualtState : FSMState
{
    PlayerState playerState;

    //Constructor
    public DefualtState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Defualt;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.hasDuelStarted)
        {
            playerState.PerformTransition(Transition.Start);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        //do nothing
    }

}
