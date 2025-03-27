//----------------------------------------------------------------
//  OG Author:     Sebastian
//  other Authors: Wyatt
//  Title:         QTEState
//  Date Created:  02/10/2025
//  Purpose:       QTE State check
//  Instance?      no
//-----------------------------------------------------------------
using UnityEngine;

/// <summary>
/// </summary>
public class QTEState : FSMState
{
    PlayerState playerState;
    private bool makeQTESequence = true;
    private bool changeState = false;

    //QTE amount and Player to which it is tied to

    //Constructor
    public QTEState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.QTE;
    }

    public override void EnterStateInit()
    {
        //set the trigger for sequence to be true
        makeQTESequence = true;
        //make sure you dont instanly leave the state
        changeState = false;
        //make sure both players values fro their qte sequence is reset
        GameManager.Instance.player2FinishedQTE = false;
        GameManager.Instance.player1FinishedQTE = false;

        foreach (Fingers playerFingers in playerState.player.GetComponentInChildren<HandsHandler>().fingers)
        {
            foreach (SkinnedMeshRenderer fingerRender in playerFingers.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                fingerRender.material.SetFloat("_BonusColourOn", 0.0f);
            }
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        //if the state is still on QTE
        if (changeState)
        {
            //if the player1 and Player2 finished their QTE 
            if (GameManager.Instance.player1FinishedQTE && GameManager.Instance.player2FinishedQTE)
            {
                //reset variables
                player.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                enemy.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                //go back to the desired state
                playerState.PerformTransition(Transition.NeedDecision);
            }
            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName != SpellNames.none)
            {
                //this happens if you didn't chose a spell
                player.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                enemy.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                //GameManager.Instance.currentCaster = GameManager.Instance.player2;
                playerState.PerformTransition(Transition.NeedDecision);
            }
            else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName != SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
            {
                //this happens if you didn't choose a spell
                player.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                enemy.gameObject.GetComponent<QTEHandler>().timeisDone = false;
                //GameManager.Instance.currentCaster = GameManager.Instance.player1;
                playerState.PerformTransition(Transition.NeedDecision);
            }
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (makeQTESequence)
        {
            //create the QTE SEquence if it still has not been created
            makeQTESequence = false;
            playerState.StartQTECreation(playerState.currentQTEAmount, player);
        }

        if (player.gameObject.GetComponent<QTEHandler>().timeisDone == true && enemy.gameObject.GetComponent<QTEHandler>().timeisDone == true)
        {
            //check if its a tie and see the time of qte ending of each playerto determine who will cast first
            if (GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] == Decider.Tie || GameManager.Instance.racedBasedQTE)
            {
                //player 1 finished their QTE before player 2 then they cast first
                if (GameManager.Instance.P1QTESpeed > GameManager.Instance.P2QTESpeed)
                {
                    GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerOneIsFaster;
                    GameManager.Instance.currentCaster = GameManager.Instance.player1;
                    GameManager.Instance.player1FinishedQTE = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                    changeState = true;
                } //player 2 finished their QTE before player 1 then they cast first
                else if (GameManager.Instance.P1QTESpeed < GameManager.Instance.P2QTESpeed)
                {
                    GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerTwoIsFaster;
                    GameManager.Instance.currentCaster = GameManager.Instance.player2;
                    GameManager.Instance.player2FinishedQTE = true;
                    GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                    changeState = true;
                }
                else
                {
                    //if both ended at the same time then randomizer the player that goes first
                    int rand = UnityEngine.Random.Range(0, 2);
                    if (rand == 0)
                    {
                        GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerOneIsFaster;
                        GameManager.Instance.currentCaster = GameManager.Instance.player1;
                        GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = true;
                        changeState = true;
                    }
                    else if (rand == 1)
                    {
                        GameManager.Instance.whoesOnFirst[GameManager.Instance.spellIndex] = Decider.PlayerTwoIsFaster;
                        GameManager.Instance.currentCaster = GameManager.Instance.player2;
                        GameManager.Instance.particleWait[GameManager.Instance.spellIndex] = false;
                        changeState = true;
                    }
                }

            }
            if (player == GameManager.Instance.player1)
            {
                //set the player to finsih their QTE
                GameManager.Instance.player1FinishedQTE = true;
                //GameManager.Instance.particleWait = true;
            }
            else
            {
                //set the player to finsih their QTE
                GameManager.Instance.player2FinishedQTE = true;
                //GameManager.Instance.particleWait = false;
            }
            //set all QTE variables to qhat they should be asfter evaluation of speed
            playerState.finishedCurrentQTE = true;
            changeState = true;
        }//if player 2 is done and they picked a spell then go in here
        else if (player == GameManager.Instance.player2 && player.gameObject.GetComponent<QTEHandler>().timeisDone == true
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName == SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName != SpellNames.none)
        {
            GameManager.Instance.player2FinishedQTE = true;
            playerState.finishedCurrentQTE = true;
            changeState = true;
        }//if player 1 is done and they picked a spell then go in here
        else if (player == GameManager.Instance.player1 && player.gameObject.GetComponent<QTEHandler>().timeisDone == true
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER1].whatSpell.spellName != SpellNames.none
            && GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, (int)PlayerType.PLAYER2].whatSpell.spellName == SpellNames.none)
        {
            GameManager.Instance.player1FinishedQTE = true;
            playerState.finishedCurrentQTE = true;
            changeState = true;
        }
    }

}
