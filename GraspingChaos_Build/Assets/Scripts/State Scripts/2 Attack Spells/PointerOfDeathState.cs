public class PointerOfDeathState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public PointerOfDeathState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_PointerOfDeath;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].qteAmount;
        nextState = "";
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].qteAmount;
            nextState = "QTE";
        }
        else
        {
            if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Failure)
            {
                //enemy.fingers[(int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].damageValue;
                do
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                } while (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) != 0);
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Half)
            {
                //enemy.fingers[(int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].damageValue;
                do
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                } while (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) != 0);

                if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                {
                    PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                    enemy.health.DamageFinger(newFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                {
                    PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                    enemy.health.DamageFinger(newFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                {
                    PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                    enemy.health.DamageFinger(newFinger);
                }
                else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                {
                    PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                    enemy.health.DamageFinger(newFinger);
                }
                else
                {
                    PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(rightFinger);
                    PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    enemy.health.DamageFinger(leftFinger);

                }
            }
            else if (player.GetComponent<QTEHandler>().EvauateQTEResults() == QTEOUTCOMES.Success)
            {
                //enemy.fingers[(int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].damageValue;
                do
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                } while (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) != 0);

                for (int i = 0; i < 2; i++)
                {

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.LH_Pinky);
                        enemy.health.DamageFinger(newFinger);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.LH_Thumb);
                        enemy.health.DamageFinger(newFinger);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers newFinger = enemy.GetAdjacentFingerLeft(PlayerFingers.RH_Pinky);
                        enemy.health.DamageFinger(newFinger);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers newFinger = enemy.GetAdjacentFingerRight(PlayerFingers.RH_Thumb);
                        enemy.health.DamageFinger(newFinger);
                    }
                    else
                    {
                        PlayerFingers rightFinger = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(rightFinger);
                        PlayerFingers leftFinger = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(leftFinger);

                    }
                }
            }

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;

            nextState = "Deciding";
        }
    }

}
