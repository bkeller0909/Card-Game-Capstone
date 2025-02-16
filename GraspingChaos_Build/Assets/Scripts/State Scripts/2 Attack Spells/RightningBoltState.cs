/// <summary>
//----------------------------------------------------------------
//  OG Author:     Sebastian
//  Title:         RightningBoltState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Right-ning Bolt spells
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class RightningBoltState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public RightningBoltState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RightningBolt;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.RightingBolt].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.RightingBolt].qteAmount;
            nextState = "QTE";
        }
        else
        {
            player.GetComponent<QTEHandler>().EvauateQTEResults();
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
            {
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.RH_Pinky || GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.LH_Thumb)
                {
                    PlayerFingers fingerToTheRight = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(fingerToTheRight);
                }
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
            {
                PlayerFingers fingerToTheRight;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.RH_Pinky || GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.LH_Thumb)
                {
                    fingerToTheRight = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(fingerToTheRight);
                    if (fingerToTheRight != PlayerFingers.RH_Pinky || fingerToTheRight != PlayerFingers.LH_Thumb)
                    {
                        fingerToTheRight = enemy.GetAdjacentFingerRight(fingerToTheRight);
                        enemy.health.DamageFinger(fingerToTheRight);
                    }
                }
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                PlayerFingers fingerToTheRight;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.RH_Pinky || GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.LH_Thumb)
                {
                    fingerToTheRight = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(fingerToTheRight);
                    if (fingerToTheRight != PlayerFingers.RH_Pinky || fingerToTheRight != PlayerFingers.LH_Thumb)
                    {
                        fingerToTheRight = enemy.GetAdjacentFingerRight(fingerToTheRight);
                        enemy.health.DamageFinger(fingerToTheRight);
                        if (fingerToTheRight != PlayerFingers.RH_Pinky || fingerToTheRight != PlayerFingers.LH_Thumb)
                        {
                            fingerToTheRight = enemy.GetAdjacentFingerRight(fingerToTheRight);
                            enemy.health.DamageFinger(fingerToTheRight);
                        }
                    }
                }
            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}
