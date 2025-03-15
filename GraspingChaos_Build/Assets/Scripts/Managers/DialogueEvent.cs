using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//  Title:          DialogueEvent
//  Date Created:   March 12, 2025
//  Instance:       No
//-----------------------------------------------------------------

/// <summary>
/// Plays a dialogue event that can appear for a player.
/// 
/// This will be attached to dialogue events that will be on a canvas that only the player can see.
/// That way when another player is given a similar dialogue event they can read the text at their own pace.
/// </summary>
public class DialogueEvent : MonoBehaviour
{
    [SerializeField] private AudioSource soundObject;

    [Header("Dialogue")]
    [SerializeField, TextArea] private string[] dialogueText;       // dialogue text that will be displayed

    [Header("UI GameObjects")]
    [SerializeField] private GameObject dialogueBoxP1;                // the text box that will display the dialogue text for player 1
    [SerializeField] private GameObject dialogueBoxP2;                // the text box that will display the dialogue text for player 2
    [SerializeField] private GameObject controlsBox;                  // for the input controls icons

    private int numOfMessages;                 // how much dialogue messages are there to read
    private int currentMessage;                                     // the index of the current message
    private bool isTalking;                                         // is dialogue current happening

    private void Awake()
    {
        numOfMessages = dialogueText.Length;
        currentMessage = 0;
        isTalking = false;
    }

    /// <summary>
    /// Plays a sound effect.
    /// </summary>
    /// <param name="soundClip">Sound clip that will be played.</param>
    /// <param name="volume">Volume of the sound clip</param>
    public void PlayDialogueAudio(AudioClip soundClip, float volume)
    {
        // spawn in the gameobject
        AudioSource audioSource = Instantiate(soundObject, gameObject.transform.position, Quaternion.identity);

        // sound clip
        audioSource.clip = soundClip;

        // sound volume
        audioSource.volume = volume;

        audioSource.Play();

        float soundClipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, soundClipLength);
    }

    /// <summary>
    /// Activates the dialogue and enables all the message boxes.
    /// </summary>
    /// <param name="dialogueIndex">Index of the dialogue to use</param>
    public void StartDialogue(PlayerManager player, int dialogueIndex)
    {
        player.playerInput.SwitchCurrentActionMap("Dialogue");
        isTalking = true;
        currentMessage = dialogueIndex;

        dialogueBoxP1.GetComponent<TextMeshProUGUI>().enabled = isTalking;
        dialogueBoxP1.GetComponent<TextMeshProUGUI>().text = dialogueText[currentMessage];

        dialogueBoxP2.GetComponent<TextMeshProUGUI>().enabled = isTalking;
        dialogueBoxP2.GetComponent<TextMeshProUGUI>().text = dialogueText[currentMessage];
    }

    public void NextDialogueButton(PlayerManager player)
    {
        if (player.playerInput.actions["Next"].WasPressedThisFrame())
        {
            if (numOfMessages > 1)
            {
                NextDialogue();
            }
            else
            {
                EndDialogue(player);
            }
        }
    }

    /// <summary>
    /// Increments the dialogue index and changes the text to the next message.
    /// </summary>
    public void NextDialogue()
    {
        currentMessage++;

        dialogueBoxP1.GetComponent<TextMeshProUGUI>().text = dialogueText[currentMessage];
        dialogueBoxP2.GetComponent<TextMeshProUGUI>().text = dialogueText[currentMessage];
    }

    /// <summary>
    /// Deactivates the message boxes and ends dialogue.
    /// </summary>
    public void EndDialogue(PlayerManager player)
    {
        player.playerInput.SwitchCurrentActionMap("Card");
        isTalking = false;

        dialogueBoxP1.GetComponent<TextMeshProUGUI>().enabled = isTalking;
        dialogueBoxP1.GetComponent<TextMeshProUGUI>().text = "";

        dialogueBoxP2.GetComponent<TextMeshProUGUI>().enabled = isTalking;
        dialogueBoxP2.GetComponent<TextMeshProUGUI>().text = "";
    }
}
