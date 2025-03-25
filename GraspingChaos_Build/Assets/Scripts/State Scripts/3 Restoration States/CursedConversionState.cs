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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ForTheCause].qteAmount;
            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CursedConversion, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    //track damage taken of each turn
                    //make function for mana gain and ensure that it cant go beyond 12
                    player.SucessfulCursedConversion = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CursedConversion, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 2);
                    //track damage taken of each turn
                    //make function for mana gain and ensure that it cant go beyond 12
                    player.SucessfulCursedConversion = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CursedConversion, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
                    //track damage taken of each turn
                    //make function for mana gain and ensure that it cant go beyond 12
                    player.SucessfulCursedConversion = true;
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CursedConversion, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 1);
                    //track damage taken of each turn
                    //make function for mana gain and ensure that it cant go beyond 12
                    player.SucessfulCursedConversion = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CursedConversion, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 2);
                    //track damage taken of each turn
                    //make function for mana gain and ensure that it cant go beyond 12
                    player.SucessfulCursedConversion = false;
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.CursedConversion, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player, 3);
                    //track damage taken of each turn
                    //make function for mana gain and ensure that it cant go beyond 12
                    player.SucessfulCursedConversion = true;
                }
                GameManager.Instance.particleP2Done = true;
            }

            if (player == GameManager.Instance.player1 && GameManager.Instance.particleP1Done && GameManager.Instance.coroutineWaitP1)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                player.PlayedCursedConvertion = true;
                GameManager.Instance.totalSpellsPickedP1--;
                nextState = "Deciding";
                GameManager.Instance.particleP1Done = false;
                GameManager.Instance.coroutineWaitP1 = false;
            }

            if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                player.PlayedCursedConvertion = true;
                GameManager.Instance.totalSpellsPickedP2--;
                nextState = "Deciding";
                GameManager.Instance.particleP2Done = false;
                GameManager.Instance.coroutineWaitP2 = false;
            }

        }
    }

}
