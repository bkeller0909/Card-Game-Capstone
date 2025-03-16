using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] private DialogueEvent dialogueEvent;

    // bools for every step of the tutorial
    [HideInInspector] public bool isStep1 = true;
    [HideInInspector] public bool isStep2 = false;
    [HideInInspector] public bool isStep3 = false;
    [HideInInspector] public bool isStep4 = false;
    [HideInInspector] public bool isStep5 = false;
    [HideInInspector] public bool isStep6 = false;
    [HideInInspector] public bool isStep7 = false;
    [HideInInspector] public bool isStep8 = false;
    [HideInInspector] public bool isStep9 = false;
    [HideInInspector] public bool isStep10 = false;
    [HideInInspector] public bool isStep11 = false;

    [HideInInspector] public bool isStep1Complete = false;
    [HideInInspector] public bool isStep2Complete = false;
    [HideInInspector] public bool isStep3Complete = false;
    [HideInInspector] public bool isStep4Complete = false;
    [HideInInspector] public bool isStep5Complete = false;
    [HideInInspector] public bool isStep6Complete = false;
    [HideInInspector] public bool isStep7Complete = false;
    [HideInInspector] public bool isStep8Complete = false;
    [HideInInspector] public bool isStep9Complete = false;
    [HideInInspector] public bool isStep10Complete = false;
    [HideInInspector] public bool isStep11Complete = false;

    [HideInInspector] public bool isTutorialComplete = false;

    /// <summary>
    /// Setup everything for the start of the tutorial.
    /// </summary>
    public void SetupTutorial(PlayerManager player)
    {
        player.playerInput.SwitchCurrentActionMap("Tutorial");
    }

    public void ResetAnims(PlayerManager player)
    {
        //reset animation triggers
        if(player.playerNum == PlayerType.PLAYER1)
        {
            player.skullHands.gameObject.GetComponent<Animator>().ResetTrigger("IDLE");
            player.skullHands.gameObject.GetComponent<Animator>().ResetTrigger("HandsUp");
            player.skullHands.gameObject.GetComponent<Animator>().ResetTrigger("HandsDown");
        }
        else if(player.playerNum == PlayerType.PLAYER2)
        {
            player.stagHands.gameObject.GetComponent<Animator>().ResetTrigger("IDLE");
            player.stagHands.gameObject.GetComponent<Animator>().ResetTrigger("HandsUp");
            player.stagHands.gameObject.GetComponent<Animator>().ResetTrigger("HandsDown");
        }
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
    /// <summary>
    /// Starts the dialogue for the tutorial
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private IEnumerator DoTutorialStep1(PlayerManager player)
    {
        MusicManager.Instance.GetComponent<AudioSource>().volume = 0.25f;

        isStep1 = true;
        if (isStep1)
        {
            player.playerInput.currentActionMap.Disable();
            dialogueEvent.StartDialogue(0);
            //dialogueEvent.PlayDialogueAudio();

            yield return new WaitForSeconds(7f);
            isStep1Complete = true;
            isStep1 = false;
        }
    }

    /// <summary>
    /// Moves the players hands up
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private IEnumerator DoTutorialStep2(PlayerManager player)
    {
        dialogueEvent.NextDialogue(1);
        isStep2 = true;
        if(isStep2)
        {
            ResetAnims(player);
            if(player.playerNum == PlayerType.PLAYER1)
            {
                player.skullHands.gameObject.GetComponent<Animator>().SetTrigger("HandsUp");

                yield return new WaitForSeconds(5f);
                dialogueEvent.NextDialogue(2);
                yield return new WaitForSeconds(5f);
                player.health.DamageFinger(PlayerFingers.RH_Index);
                yield return new WaitForSeconds(5f);
                player.health.DamageFinger(PlayerFingers.LH_Index);
                yield return new WaitForSeconds(5f);
                dialogueEvent.NextDialogue(3);
                yield return new WaitForSeconds(5f);

                player.health.HealFinger(PlayerFingers.RH_Index);
                player.health.HealFinger(PlayerFingers.LH_Index);
            }
            else if(player.playerNum == PlayerType.PLAYER2)
            {
                player.stagHands.gameObject.GetComponent<Animator>().SetTrigger("HandsUp");

                yield return new WaitForSeconds(5f);
                //dialogueEvent.NextDialogue(2);
                yield return new WaitForSeconds(5f);
                player.health.DamageFinger(PlayerFingers.RH_Index);
                yield return new WaitForSeconds(5f);
                player.health.DamageFinger(PlayerFingers.LH_Index);
                yield return new WaitForSeconds(5f);
                //dialogueEvent.NextDialogue(3);
                yield return new WaitForSeconds(5f);

                player.health.HealFinger(PlayerFingers.RH_Index);
                player.health.HealFinger(PlayerFingers.LH_Index);
            }
            yield return new WaitForSeconds(2f);
            isStep2Complete = true;
            isStep2 = false;
        }
    }

    /// <summary>
    /// Moves the player's hands back down to idle
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private IEnumerator DoTutorialStep3(PlayerManager player)
    {
        dialogueEvent.EndDialogue();
        isStep3 = true;
        if (isStep3)
        {
            ResetAnims(player);
            if (player.playerNum == PlayerType.PLAYER1)
            {
                player.skullHands.gameObject.GetComponent<Animator>().SetTrigger("IDLE");
            }
            else if (player.playerNum == PlayerType.PLAYER2)
            {
                player.stagHands.gameObject.GetComponent<Animator>().SetTrigger("IDLE");
            }
            yield return new WaitForSeconds(5f);
            isStep3Complete = true;
            isStep3 = false;
        }
    }

    /// <summary>
    /// Move the player camera to the mana bottle and back
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private IEnumerator DoTutorialStep4(PlayerManager player)
    {
        dialogueEvent.StartDialogue(4);
        isStep4 = true;
        if (isStep4)
        {
            player.playerCameras.NewCamPos(player.playerCameras.bottleCamPos);
            yield return new WaitForSeconds(10f);
            player.playerCameras.GetInputForced(0);
            yield return new WaitForSeconds(10f);
            isStep4Complete = true;
            isStep4 = false;
        }
    }

    /// <summary>
    /// Send the player camera view towards the cards and back.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private IEnumerator DoTutorialStep5(PlayerManager player)
    {
        isStep5 = true;
        if (isStep5)
        {
            yield return new WaitForSeconds(5f);
            player.playerCameras.GetInputForced(1);
            yield return new WaitForSeconds(5f);
            player.playerCameras.GetInputForced(0);
            yield return new WaitForSeconds(5f);
            isStep5Complete = true;
            isStep5 = false;
        }
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
