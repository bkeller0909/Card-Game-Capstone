public class ChoosingSpellsState : FSMState
{
    PlayerState playerState;
    //Constructor
    public ChoosingSpellsState(PlayerState pS)
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
