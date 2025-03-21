using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject dialoguePrefabP1;                                // the text box that will display the dialogue text for player 1
    public GameObject qteIconP1;
    public GameObject manaIconP1;
    public GameObject dialoguePrefabP2;                                // the text box that will display the dialogue text for player 2
    public GameObject qteIconP2;
    public GameObject manaIconP2;

    private bool isTalking;                                         // is dialogue current happening

    private void Awake()
    {
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
    public void StartDialogue(int dialogueIndex)
    {
        isTalking = true;

        dialoguePrefabP1.SetActive(isTalking);
        dialoguePrefabP1.GetComponentInChildren<TMP_Text>().text = dialogueText[dialogueIndex];

        dialoguePrefabP2.SetActive(isTalking);
        dialoguePrefabP2.GetComponentInChildren<TMP_Text>().text = dialogueText[dialogueIndex];
    }

    /// <summary>
    /// Increments the dialogue index and changes the text to the next message.
    /// </summary>
    public void NextDialogue(int dialogueIndex)
    {
        dialoguePrefabP1.GetComponentInChildren<TMP_Text>().text = dialogueText[dialogueIndex];
        dialoguePrefabP2.GetComponentInChildren<TMP_Text>().text = dialogueText[dialogueIndex];
    }

    /// <summary>
    /// Deactivates the message boxes and ends dialogue.
    /// </summary>
    public void EndDialogue()
    {
        isTalking = false;

        dialoguePrefabP1.SetActive(isTalking);
        dialoguePrefabP1.GetComponentInChildren<TMP_Text>().text = "";

        dialoguePrefabP2.SetActive(isTalking);
        dialoguePrefabP2.GetComponentInChildren<TMP_Text>().text = "";
    }
}
