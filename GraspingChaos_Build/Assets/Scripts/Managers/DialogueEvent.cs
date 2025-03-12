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
    private PlayerManager playerManager;                            // the player

    [Header("Dialogue")]
    [SerializeField, TextArea] private string[] dialogueText;       // dialogue text that will be displayed
    [SerializeField] private int numOfMessages = 1;                 // how much dialogue messages are there to read

    [Header("UI GameObjects")]
    [SerializeField] private GameObject dialogueBox;                // the text box that will display the dialogue text
    [SerializeField] private GameObject controlsBox;                // for the input controls icons

    private int currentMessage;                                     // the index of the current message
    private bool isTalking;                                         // is dialogue current happening

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();

        currentMessage = 0;
        isTalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Activates the dialogue and enables all the message boxes.
    /// </summary>
    /// <param name="dialogueIndex">Index of the dialogue to use</param>
    public void StartDialogue(int dialogueIndex)
    {
        playerManager.playerInput.SwitchCurrentActionMap("Dialogue");
        isTalking = true;
        currentMessage = dialogueIndex;

        dialogueBox.GetComponent<TextMeshProUGUI>().enabled = isTalking;
        dialogueBox.GetComponent<TextMeshProUGUI>().text = dialogueText[currentMessage];

        if (playerManager.playerInput.actions["Next"].WasPressedThisFrame())
        {
            if(numOfMessages > 1)
            {
                NextDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    /// <summary>
    /// Increments the dialogue index and changes the text to the next message.
    /// </summary>
    public void NextDialogue()
    {
        currentMessage++;

        dialogueBox.GetComponent<TextMeshProUGUI>().text = dialogueText[currentMessage];
    }

    /// <summary>
    /// Deactivates the message boxes and ends dialogue.
    /// </summary>
    public void EndDialogue()
    {
        playerManager.playerInput.SwitchCurrentActionMap("Card");
        isTalking = false;

        dialogueBox.GetComponent<TextMeshProUGUI>().enabled = isTalking;
        dialogueBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}
