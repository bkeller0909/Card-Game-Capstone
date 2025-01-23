public class GuardiansTouchState : FSMState
{
    PlayerState playerState;
    //Constructor
    public GuardiansTouchState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_GuardiansTouch;
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