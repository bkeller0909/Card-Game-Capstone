using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//  Title:          Tutorial
//  Date Created:   March 13, 2025
//  Instance:       No
//-----------------------------------------------------------------


/// <summary>
/// Handles the entire tutorial sequence at the start of the game.
/// 
/// This will be attached to the player.
/// </summary>
public class Tutorial : MonoBehaviour
{
    // bools for every step of the tutorial
    [HideInInspector] public bool isP1TutorialComplete;
    [HideInInspector] public bool isP2TutorialComplete;

    [HideInInspector] public bool isP1OnCards;
    [HideInInspector] public bool isP2OnCards;

    [HideInInspector] public bool isP1OnMana;
    [HideInInspector] public bool isP2OnMana;

    [HideInInspector] public bool isP1CompleteStep1;
    [HideInInspector] public bool isP2CompleteStep1;

    /// <summary>
    /// Setup everything for the start of the tutorial.
    /// </summary>
    public void SetupTutorial(PlayerManager player)
    {
        player.playerInput.SwitchCurrentActionMap("Tutorial");
        isP1CompleteStep1 = false;
        isP2CompleteStep1 = false;
    }

    #region TUTORIAL STEP FUNCTIONS
    public void TutorialStep1(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep1(player));
    }

    public void TutorialStep2(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep2(player));
    }

    public void TutorialStep3(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep3(player));
    }

    public void TutorialStep4(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep4(player));
    }

    public void TutorialStep5(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep5(player));
    }

    public void TutorialStep6(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep6(player));
    }

    public void TutorialStep7(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep7(player));
    }

    public void TutorialStep8(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep8(player));
    }

    public void TutorialStep9(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep9(player));
    }

    public void TutorialStep10(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep10(player));
    }

    public void TutorialStep11(PlayerManager player)
    {
        StartCoroutine(DoTutorialStep11(player));
    }
    #endregion // TUTORIAL STEP FUNCTIONS

    #region TUTORIAL STEP COROUTINES
    private IEnumerator DoTutorialStep1(PlayerManager player)
    {
        player.playerInput.currentActionMap.Disable();

        player.GetComponent<DialogueEvent>().StartDialogue(player, 0);
        // player.GetComponent<DialogueEvent>().PlayDialogueAudio()

        yield return new WaitForSeconds(10f);
        //player.GetComponent<DialogueEvent>().NextDialogueButton(player);
        yield return null;
    }

    private IEnumerator DoTutorialStep2(PlayerManager player)
    {
        //player.playerInput.currentActionMap.Disable();

        if(player.playerNum == PlayerType.PLAYER1)
        {
            player.skullHands.gameObject.GetComponent<Animator>().SetTrigger("HandsUp");
        }
        else if(player.playerNum == PlayerType.PLAYER2)
        {
            player.stagHands.gameObject.GetComponent<Animator>().SetTrigger("HandsUp");
        }
        yield return new WaitForSeconds(10f);

        //player.playerInput.currentActionMap.Enable();
        //player.GetComponent<DialogueEvent>().NextDialogueButton(player);
    }

    private IEnumerator DoTutorialStep3(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep4(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep5(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep6(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep7(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep8(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep9(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep10(PlayerManager player)
    {
        yield return null;
    }

    private IEnumerator DoTutorialStep11(PlayerManager player)
    {
        yield return null;
    }
    #endregion // TUTORIAL STEP COROUTINES
}
