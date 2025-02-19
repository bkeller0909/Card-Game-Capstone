/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         PointerOfDeathState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Pointer of Death state 
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath].qteAmount;
            nextState = "QTE";
        }
        else
        {
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

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
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

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
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

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
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.PointerOfDeath, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemy.health.getFingerHealth(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger) <= 0)
                        {
                            break;
                        }
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

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
                GameManager.Instance.particleP2Done = true;
            }

            if (player == GameManager.Instance.player1 && GameManager.Instance.particleP1Done && GameManager.Instance.coroutineWaitP1)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                nextState = "Deciding";
                GameManager.Instance.particleP1Done = false;
                GameManager.Instance.coroutineWaitP1 = false;
            }

            if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                nextState = "Deciding";
                GameManager.Instance.particleP2Done = false;
                GameManager.Instance.coroutineWaitP2 = false;
            }

        }
    }

}
