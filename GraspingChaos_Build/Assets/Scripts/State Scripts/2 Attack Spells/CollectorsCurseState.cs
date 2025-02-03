public class CollectorsCurseState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    //Constructor
    public CollectorsCurseState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_CollectorsCurse;
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse].qteAmount;
        if (playerState.player == GameManager.Instance.player1)
        {
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {

    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!playerState.finishedCurrentQTE)
        {

        }
        else
        {
            if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Failure)
            {
                //enemy.fingers[(int)enemy.GetRandomFinger()].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse].damageValue;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Half)
            {
                //enemy.fingers[(int)enemy.GetRandomFinger()].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse].damageValue;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Success)
            {
                //enemy.fingers[(int)enemy.GetRandomFinger()].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse].damageValue;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                //steal ring not yet implemented
            }
        }

    }

}
