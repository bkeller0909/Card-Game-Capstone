/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        LifeDrainState
//  Date Created: 02/5/2025
//  Purpose:      Spell state for the Life drain spell
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class LifeDrainState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public LifeDrainState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.Re_LifeDrain;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LifeDrain].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LifeDrain].qteAmount;
            nextState = "QTE";
        }
        else
        {
            player.GetComponent<QTEHandler>().EvauateQTEResults();
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
            {
                //does nothing
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
            {
                PlayerFingers randomFinger = enemy.GetRandomFinger();
                enemy.health.DamageFinger(randomFinger);
                randomFinger = player.GetRandomFinger();
                player.health.HealFinger(randomFinger);
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                PlayerFingers randomFinger = enemy.GetRandomFinger();
                PlayerFingers randomFinger2 = enemy.GetRandomFinger();
                for (int i = 0; i < 2; i++)
                {
                    enemy.health.DamageFinger(randomFinger);
                    enemy.health.DamageFinger(randomFinger2);
                }

                randomFinger = player.GetRandomFinger();
                randomFinger2 = player.GetRandomFinger();
                for (int s = 0; s < 2; s++)
                {
                    player.health.HealFinger(randomFinger);
                    player.health.HealFinger(randomFinger2);
                }
            }

            //check if i am the second spell but the first cast

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}
