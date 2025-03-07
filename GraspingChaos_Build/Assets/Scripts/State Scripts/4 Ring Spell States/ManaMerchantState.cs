public class ManaMerchantState : FSMState
{
    //refernece for player state
    PlayerState playerState;
    //reference for what player it is based in int values
    private int playerIndex;
    //string for next state
    private string nextState;
    public ManaMerchantState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_ManaMerchant;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        //get qte amount of the speel
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ManaMerchant].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ManaMerchant].qteAmount;
            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                //Turns The Ring on
                player.ringHandler.ringsActive[(int)Rings.ManaMerchantFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                player.ToggleRing(true, Rings.ManaMerchantFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFull] = GameManager.Instance.whatRound;
            }
            else
            {
                //Turns The Ring on
                player.ringHandler.ringsActive[(int)Rings.ManaMerchantFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                player.ToggleRing(true, Rings.ManaMerchantFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                player.ringHandler.ringStartRound[(int)Rings.ManaMerchantFail] = GameManager.Instance.whatRound;
            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}