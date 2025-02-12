/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        QuickHealState
//  Date Created: 02/5/2025
//  Purpose:      spell state for quick heal spell
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class QuickHealState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public QuickHealState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_QuickHeal;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.QuickHeal].qteAmount;
        nextState = "";
        if (playerState.player == GameManager.Instance.player1)
        {
            playerIndex = (int)PlayerType.PLAYER1;
        }
        else
        {
            playerIndex = (int)PlayerType.PLAYER2;
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (nextState == "Deciding")
        {
            playerState.PerformTransition(Transition.NeedDecision);
        }
        else if (nextState == "QTE")
        {
            playerState.PerformTransition(Transition.Challenge);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!playerState.finishedCurrentQTE)
        {
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.QuickHeal].qteAmount;
            nextState = "QTE";
        }
        else
        {
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
            {
                //does nothing
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
            {
                //does nothing
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                player.health.HealFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
            }

            //check if i am the second spell but the first cast

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}
