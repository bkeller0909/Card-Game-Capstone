/// <summary>
//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         RockThrowState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Rock throw spell
//  Instance?      no
//-----------------------------------------------------------------
/// </summary>
public class RockThrowState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public RockThrowState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_RockThrow;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow].qteAmount;
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
                    GameManager.Instance.coroutineWaitP1 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                    PlayerFingers randomFinger = player.GetRandomFinger();
                    player.health.DamageFinger(randomFinger);

                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, player.gamepad);
                    player.cameraHandler.CameraShake(0.05f, 0.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    GameManager.Instance.coroutineWaitP1 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                    PlayerFingers randomFinger = player.GetRandomFinger();
                    player.health.DamageFinger(randomFinger);

                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, player.gamepad);
                    player.cameraHandler.CameraShake(0.05f, 0.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.5f, player.gamepad);
                }

                GameManager.Instance.particleP1Done = true;

            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    GameManager.Instance.coroutineWaitP2 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                    PlayerFingers randomFinger = player.GetRandomFinger();
                    player.health.DamageFinger(randomFinger);

                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, player.gamepad);
                    player.cameraHandler.CameraShake(0.05f, 0.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    GameManager.Instance.coroutineWaitP2 = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                    PlayerFingers randomFinger = player.GetRandomFinger();
                    player.health.DamageFinger(randomFinger);

                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, player.gamepad);
                    player.cameraHandler.CameraShake(0.05f, 0.5f);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ParticleManger.Instance.StartParticle(SpellNames.Rockthrow, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    RumbleManager.Instance.ControllerRumble(0.2f, 0.2f, 0.5f, player.gamepad);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);
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
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                    RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, enemy.gamepad);
                    enemy.cameraHandler.CameraShake(0.05f, 0.5f);
                }
            }
        }
    }

}
