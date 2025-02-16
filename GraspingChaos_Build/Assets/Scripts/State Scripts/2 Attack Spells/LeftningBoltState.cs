/// <summary>
//----------------------------------------------------------------
//  OG Author:     Sebastian
//  Title:         LeftningBoltState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Left-ning Bolt spells
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class LeftningBoltState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public LeftningBoltState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_LeftningBolt;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LeftningBolt].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LeftningBolt].qteAmount;
            nextState = "QTE";
        }
        else
        {
            player.GetComponent<QTEHandler>().EvauateQTEResults();
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
            {
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.LH_Pinky || GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.RH_Thumb)
                {
                    PlayerFingers fingerToTheLeft = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(fingerToTheLeft);
                }
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
            {
                PlayerFingers fingerToTheLeft;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.LH_Pinky || GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.RH_Thumb)
                {
                    fingerToTheLeft = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(fingerToTheLeft);
                    if (fingerToTheLeft != PlayerFingers.LH_Pinky || fingerToTheLeft != PlayerFingers.RH_Thumb)
                    {
                        fingerToTheLeft = enemy.GetAdjacentFingerLeft(fingerToTheLeft);
                        enemy.health.DamageFinger(fingerToTheLeft);
                    }
                }
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                PlayerFingers fingerToTheLeft;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.LH_Pinky || GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger != PlayerFingers.RH_Thumb)
                {
                    fingerToTheLeft = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(fingerToTheLeft);
                    if (fingerToTheLeft != PlayerFingers.LH_Pinky || fingerToTheLeft != PlayerFingers.RH_Thumb)
                    {
                        fingerToTheLeft = enemy.GetAdjacentFingerLeft(fingerToTheLeft);
                        enemy.health.DamageFinger(fingerToTheLeft);
                        if(fingerToTheLeft != PlayerFingers.LH_Pinky || fingerToTheLeft != PlayerFingers.RH_Thumb)
                        {
                            fingerToTheLeft = enemy.GetAdjacentFingerLeft(fingerToTheLeft);
                            enemy.health.DamageFinger(fingerToTheLeft);
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
