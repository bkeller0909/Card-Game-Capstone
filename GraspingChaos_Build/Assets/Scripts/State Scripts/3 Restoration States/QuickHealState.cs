public class QuickHealState : FSMState
{
    PlayerState playerState;
    //Constructor
    public QuickHealState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_QuickHeal;
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
