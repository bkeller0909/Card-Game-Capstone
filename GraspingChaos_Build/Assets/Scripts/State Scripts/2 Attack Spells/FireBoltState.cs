//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         FireBoltState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for FireBolt
//  Instance?      no
//-----------------------------------------------------------------
/// <summary>
/// </summary>
public class FireBoltState : FSMState
{
    PlayerState playerState;
    private int playerIndex;
    private string nextState;
    //Constructor
    public FireBoltState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_FireBolt;
        nextState = "";
    }

    public override void EnterStateInit()
    {
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].qteAmount;
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
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].qteAmount;
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
                //does nothing
            }
            else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
            {
                //enemy.fingers[(int)GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger].fingerHP -= ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt].damageValue;
                enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                RumbleManager.Instance.ControllerRumble(1f, 1f, 0.5f, enemy.gamepad);

                // TODO - figure out a way to get the audio to play from the correct spell
                // SoundManager.Instance.PlaySoundFX(/*audioclip*/, player.transform, 1);
            }

            //check if i am the second spell but the first cast

            GameManager.Instance.ChangeCurrentCaster();
            GameManager.Instance.playedSpells++;
            GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
            nextState = "Deciding";
        }
    }

}
