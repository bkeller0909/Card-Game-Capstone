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
        GameManager.Instance.player2FinishedQTE = false;
        GameManager.Instance.player1FinishedQTE = false;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (changeState)
        {
            if (GameManager.Instance.player1FinishedQTE && GameManager.Instance.player2FinishedQTE)
            {
                player.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                enemy.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                playerState.PerformTransition(Transition.NeedDecision);
            }
            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName != SpellNames.none)
            {
                player.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                enemy.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                //GameManager.Instance.currentCaster = GameManager.Instance.player2;
                playerState.PerformTransition(Transition.NeedDecision);
            }
            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName != SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
            {
                player.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                enemy.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                //GameManager.Instance.currentCaster = GameManager.Instance.player1;
                playerState.PerformTransition(Transition.NeedDecision);
            }
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (makeQTESequence)
        {
            makeQTESequence = false;
            playerState.StartQTECreation(playerState.currentQTEAmount, player);
            //player.gameObject.GetComponent<QTEHandler>().Create(playerState.currentQTEAmount, player);
            //playerState.player.playerInput.SwitchCurrentActionMap("QTE");
        }

        if (player.gameObject.GetComponent<QTEHandler>().timeisDone == true && enemy.gameObject.GetComponent<QTEHandler>().timeisDone == true)
        {
            if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.Tie)
            {
                if (GameManager.Instance.P1QTESpeed > GameManager.Instance.P2QTESpeed)
                {
                    GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerOneIsFaster;
                    GameManager.Instance.currentCaster = GameManager.Instance.player1;
                    GameManager.Instance.player1FinishedQTE = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                    changeState = true;
                }
                else if (GameManager.Instance.P1QTESpeed < GameManager.Instance.P2QTESpeed)
                {
                    GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerTwoIsFaster;
                    GameManager.Instance.currentCaster = GameManager.Instance.player2;
                    GameManager.Instance.player2FinishedQTE = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                    changeState = true;
                }
                else
                {
                    int rand = UnityEngine.Random.Range(0, 2);
                    if (rand == 0)
                    {
                        GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerOneIsFaster;
                        GameManager.Instance.currentCaster = GameManager.Instance.player1;
                        GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                        changeState = true;
                    }
                    else if (rand == 1)
                    {
                        GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerTwoIsFaster;
                        GameManager.Instance.currentCaster = GameManager.Instance.player2;
                        GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                        changeState = true;
                    }
                }

            }
            if (player == GameManager.Instance.player1)
            {
                GameManager.Instance.player1FinishedQTE = true;
                //GameManager.Instance.particleWait = true;
            }
            else
            {
                GameManager.Instance.player2FinishedQTE = true;
                //GameManager.Instance.particleWait = false;
            }
            playerState.finishedCurrentQTE = true;
            changeState = true;
        }
        else if (player == GameManager.Instance.player2 && player.gameObject.GetComponent<QTEHandler>().timeisDone == true
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName != SpellNames.none)
        {
            GameManager.Instance.player2FinishedQTE = true;
            playerState.finishedCurrentQTE = true;
            changeState = true;
        }
        else if (player == GameManager.Instance.player1 && player.gameObject.GetComponent<QTEHandler>().timeisDone == true
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName != SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
        {
            GameManager.Instance.player1FinishedQTE = true;
            playerState.finishedCurrentQTE = true;
            changeState = true;
        }
    }

}
