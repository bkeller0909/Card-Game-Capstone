public class VengefulMirrorState : FSMState
{
    //refernece for player state
    PlayerState playerState;
    //reference for what player it is based in int values
    private int playerIndex;
    //string for next state
    private string nextState;
    public VengefulMirrorState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Ri_VengefulMirror;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        //get qte amount of the speel
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.VengefulMirror].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.VengefulMirror].qteAmount;
            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            bool spotTaken = false;

            for (int i = 0; i < 14; i++)
            {
                if (player.ringHandler.ringsActive[i, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] == true)
                {
                    spotTaken = true;
                }
            }

            if (!spotTaken)
            {
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    //Turns The Ring on
                    player.ringHandler.ringsActive[(int)Rings.VengefulMirrorFull, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                    player.ToggleRing(true, Rings.VengefulMirrorFull, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    player.ringHandler.ringStartRound[(int)Rings.VengefulMirrorFull] = GameManager.Instance.whatRound;
                }
                else
                {
                    //Turns The Ring on
                    player.ringHandler.ringsActive[(int)Rings.VengefulMirrorFail, (int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger] = true;
                    player.ToggleRing(true, Rings.VengefulMirrorFail, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    player.ringHandler.ringStartRound[(int)Rings.VengefulMirrorFail] = GameManager.Instance.whatRound;
                }
            }
            else // ring dosent get put on the finger
            {

            }

            //temp just for it working
            if (player == GameManager.Instance.player1)
            {
                GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
            }
            else if (player == GameManager.Instance.player2)
            {
                GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}