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
    private PlayerManager playerManager;

    [Header("Dialogue")]
    [SerializeField, TextArea] private string[] dialogueText;
    [SerializeField] private int numOfMessages = 1;

    [Header("UI GameObjects")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject controlsBox;

    private int currentMessage;
    private bool isTalking;

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

    public void NextDialogue()
    {
        currentMessage++;

        dialogueBox.GetComponent<TextMeshProUGUI>().text = dialogueText[currentMessage];
    }

    public void EndDialogue()
    {
        playerManager.playerInput.SwitchCurrentActionMap("Card");
        isTalking = false;

        dialogueBox.GetComponent<TextMeshProUGUI>().enabled = isTalking;
        dialogueBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}
