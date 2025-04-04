public class DeadState : FSMState
{
    PlayerState playerState;
    private bool isDead = false;
    //Constructor
    public DeadState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Dead;
        isDead = false;
    }

    public override void EnterStateInit()
    {
        isDead = false;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {

    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (isDead == false)
        {


            isDead = true;
            if (GameManager.Instance.player1.entireHP <= 0)
            {
                GameManager.Instance.SkullWinVideo.SetActive(true);
                GameManager.Instance.EndScreenSkullVideo1.SetActive(true);
                GameManager.Instance.EndScreenSkullVideo2.SetActive(true);

            }
            else if (GameManager.Instance.player2.entireHP <= 0)
            {
                GameManager.Instance.StagWinVideo.SetActive(true);
                GameManager.Instance.EndScreenStagVideo1.SetActive(true);
                GameManager.Instance.EndScreenStagVideo2.SetActive(true);

            }
        }
    }
}
