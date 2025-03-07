public class VeilOfFortitudeState : FSMState
{
    //refernece for player state
    PlayerState playerState;
    //reference for what player it is based in int values
    private int playerIndex;
    //string for next state
    private string nextState;

    public VeilOfFortitudeState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_VeilOfFortitude;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        //get qte amount of the speel
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.VeilOfFortitude].qteAmount;
        nextState = "";
        //find the player and assign it to our index
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
        //switch states if set to
        if (nextState == "Deciding")
        {
            GameManager.Instance.spellInProgress = false;
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
            //create the qte sequence
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.VeilOfFortitude].qteAmount;
            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                //Turns The Ring on
                player.ringHandler.ringsActive[(int)Rings.VeilOfFortitudeFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                player.ToggleRing(true, Rings.VeilOfFortitudeFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                player.ringHandler.ringStartRound[(int)Rings.VeilOfFortitudeFull] = GameManager.Instance.whatRound;
            }
            else
            {
                //Turns The Ring on
                player.ringHandler.ringsActive[(int)Rings.VeilOfFortitudeFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                player.ToggleRing(true, Rings.VeilOfFortitudeFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                player.ringHandler.ringStartRound[(int)Rings.VeilOfFortitudeFail] = GameManager.Instance.whatRound;
            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}