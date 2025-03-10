/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         TidalWaveState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Tidal Wave spell
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class TidalWaveState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public TidalWaveState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_TidalWave;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.TidalWave].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.TidalWave].qteAmount;
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
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.TidalWave, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.3f, 0.3f, 0.7f, player.gamepad);
                    player.cameraHandler.CameraShake(0.01f, 0.7f);
                    enemy.cameraHandler.CameraShake(0.01f, 0.7f);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    for (int i = 0; i < 2; i++)
                    {
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }

                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }

                    }
                }
            }

            if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                nextState = "Deciding";
                GameManager.Instance.particleP2Done = false;
                GameManager.Instance.coroutineWaitP2 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        enemy.health.DamageFinger(currentAdjacent);
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    RumbleManager.Instance.ControllerRumble(0.5f, 1f, 0.7f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);

                    for (int i = 0; i < 2; i++)
                    {
                        enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    }

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }
                    }
                    else
                    {
                        PlayerFingers currentAdjacent = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int a = 0; a < 2; a++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }

                        currentAdjacent = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        for (int s = 0; s < 2; s++)
                        {
                            enemy.health.DamageFinger(currentAdjacent);
                        }

                    }
                }
            }
        }
    }

}
