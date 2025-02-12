/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         StaticBastState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Static Blast spell
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class StaticBlastState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public StaticBlastState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_StaticBlast;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.StaticBlast].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.StaticBlast].qteAmount;
            nextState = "QTE";
        }
        else
        {
            if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
            {
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
            {
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                PlayerFingers adjacentFinger;
                int randomSide = UnityEngine.Random.Range(0, 2);

                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                {
                    adjacentFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                {
                    adjacentFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                {
                    adjacentFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                {
                    adjacentFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else
                {
                    //need to check if both adjacent fingers are alive to use the rando, if only one is then use the one that can take damage (STILL NEED TO DO)
                    if (randomSide == 0)
                    {
                        adjacentFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(adjacentFinger);
                    }
                    else
                    {
                        adjacentFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(adjacentFinger);
                    }
                }
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                PlayerFingers adjacentFinger;

                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                {
                    adjacentFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                {
                    adjacentFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                {
                    adjacentFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                {
                    adjacentFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }
                else
                {
                    adjacentFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                    adjacentFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(adjacentFinger);
                }

            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;

            nextState = "Deciding";
        }
    }

}
