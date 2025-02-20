public class DefualtState : FSMState
{
    PlayerState playerState;
    CameraPositionChange posChange;
    bool animPlayed;

    //Constructor
    public DefualtState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Defualt;
        posChange = playerState.playerCam.gameObject.GetComponent<CameraPositionChange>();
        animPlayed = false;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (posChange.weHaveArrived)
        {
            playerState.PerformTransition(Transition.Start);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.hasDuelStarted && !animPlayed)
        {
            if (player = GameManager.Instance.player1)
            {
                playerState.camAnim.Play("LoadingIntoDuel");
            }
            else
            {
                playerState.camAnim.Play("LoadingIntoDuel2");
            }
            animPlayed = true;
        }

    }

}
