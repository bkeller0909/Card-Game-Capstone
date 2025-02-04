public class QTEState : FSMState
{
    PlayerState playerState;
    private bool makeQTESequence = true;
    private bool changeState = false;

    //QTE amount and Player to which it is tied to

    //Constructor
    public QTEState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.QTE;
    }

    public override void EnterStateInit()
    {
        makeQTESequence = true;
        changeState = false;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (changeState)
        {
            playerState.PerformTransition(Transition.NeedDecision);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (makeQTESequence)
        {
            makeQTESequence = false;
            player.gameObject.GetComponent<QTEHandler>().Create(playerState.currentQTEAmount, player);
        }

        if (player.gameObject.GetComponent<QTEHandler>().timeisDone == true && enemy.gameObject.GetComponent<QTEHandler>().timeisDone == true)
        {
            if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.Tie)
            {
                if (GameManager.Instance.P1QTESpeed > GameManager.Instance.P2QTESpeed)
                {
                    GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerOneIsFaster;
                    GameManager.Instance.currentCaster = GameManager.Instance.player1;
                }
                else if (GameManager.Instance.P1QTESpeed < GameManager.Instance.P2QTESpeed)
                {
                    GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerTwoIsFaster;
                    GameManager.Instance.currentCaster = GameManager.Instance.player2;
                }
                else
                {
                    int rand = UnityEngine.Random.Range(0, 2);
                    if (rand == 0)
                    {
                        GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerOneIsFaster;
                        GameManager.Instance.currentCaster = GameManager.Instance.player1;
                    }
                    else if (rand == 1)
                    {
                        GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerTwoIsFaster;
                        GameManager.Instance.currentCaster = GameManager.Instance.player2;
                    }
                }

            }
            playerState.finishedCurrentQTE = true;
            changeState = true;
        }
    }

}
