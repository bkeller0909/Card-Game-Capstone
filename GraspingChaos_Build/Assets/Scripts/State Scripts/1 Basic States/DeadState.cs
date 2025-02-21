public class DeadState : FSMState
{
    PlayerState playerState;

    //Constructor
    public DeadState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Dead;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {

    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.player1.entireHP <= 0)
        {
            GameManager.Instance.tempEndScreenStagWins1.SetActive(true);
            GameManager.Instance.tempEndScreenStagWins2.SetActive(true);
        }
        else if (GameManager.Instance.player2.entireHP <= 0)
        {
            GameManager.Instance.tempEndScreenSkullWins1.SetActive(true);
            GameManager.Instance.tempEndScreenSkullWins2.SetActive(true);
        }
    }
}
