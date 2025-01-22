public class EchoingManaState : FSMState
{
    PlayerState playerState;
    //Constructor
    public EchoingManaState(PlayerState pS)
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
