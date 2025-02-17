/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        CursedConvertionState
//  Date Created: 02/5/2025
//  Purpose:      Spell state for the Cursed Convertion spell 
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class CursedConversionState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public CursedConversionState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_CursedConversion;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.CursedConversion].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ForTheCause].qteAmount;
            nextState = "QTE";
        }
        else
        {
            player.GetComponent<QTEHandler>().EvauateQTEResults();
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
            {
                //track damage taken of each turn
                //make function for mana gain and ensure that it cant go beyond 12
                player.SucessfulCursedConversion = false;
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
            {
                //track damage taken of each turn
                //make function for mana gain and ensure that it cant go beyond 12
                player.SucessfulCursedConversion = false;
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                //track damage taken of each turn
                //make function for mana gain and ensure that it cant go beyond 12
                player.SucessfulCursedConversion = true;
            }

            //check if i am the second spell but the first cast
            player.PlayedCursedConvertion = true;
            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}
