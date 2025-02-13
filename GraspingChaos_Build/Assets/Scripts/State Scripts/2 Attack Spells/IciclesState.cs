/// <summary>
//----------------------------------------------------------------
//  OG Author:     Sebastian
//  Title:         IciclesState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Icicles spells
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class IciclesState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public IciclesState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_Icicles;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles].qteAmount;
            nextState = "QTE";
        }
        else
        {
            player.GetComponent<QTEHandler>().EvauateQTEResults();
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
            {
                PlayerFingers randomFinger = player.GetRandomFinger();
                enemy.health.DamageFinger(randomFinger);
                randomFinger = player.GetRandomFinger();
                enemy.health.DamageFinger(randomFinger);
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
            {
                PlayerFingers randomFinger = player.GetRandomFinger();
                enemy.health.DamageFinger(randomFinger);
                randomFinger = player.GetRandomFinger();
                enemy.health.DamageFinger(randomFinger);
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                PlayerFingers randomFinger = player.GetRandomFinger();
                for (int i = 0; i < 2; i++)
                {
                    enemy.health.DamageFinger(randomFinger);
                }

                randomFinger = player.GetRandomFinger();
                for (int s = 0; s < 2; s++)
                {
                    enemy.health.DamageFinger(randomFinger);
                }
            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}
