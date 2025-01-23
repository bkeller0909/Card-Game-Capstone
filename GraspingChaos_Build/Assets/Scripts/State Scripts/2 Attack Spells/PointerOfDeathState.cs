public class PointerOfDeathState : FSMState
{
    PlayerState playerState;
    //Constructor
    public PointerOfDeathState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_PointerOfDeath;
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
