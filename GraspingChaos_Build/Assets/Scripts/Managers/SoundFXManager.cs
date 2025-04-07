using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

//----------------------------------------------------------------
//  Author:       Keller
//  Co-Auther:    
//  Title:        SoundFXManager
//  Date Created: 02/26/2025
//  Instance:     Yes
//-----------------------------------------------------------------

/// <summary>
/// Handles sound effects and other things related.
/// </summary>
public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    public AudioMixer soundFXMixer;

    [SerializeField] private AudioSource soundObject;
    [SerializeField] private AudioSource dialogueAudioObject;

    [Header("Sound Effects")]
    public AudioClip[] boneDamage;
    public AudioClip manaRefill;
    public AudioClip roundFinished;
    public AudioClip cardSelect;
    public AudioClip cardSelectComplete;
    public AudioClip uISelect;
    public AudioClip uIMove;

    [Header("Dialogue Audio")]
    [Tooltip("List of all the dialogue audio for the Narrator")]
    public AudioClip[] narratorAudioClips;

    [Tooltip("List of all the intro dialogue audio for the Stag")]
    public AudioClip[] stagIntroAudioClips;

    [Tooltip("List of all the intro dialogue audio for the Skull")]
    public AudioClip[] skullIntroAudioClips;

    [Tooltip("List of all the intro dialogue audio for the Stag")]
    public AudioClip[] stagTauntAudioClips;

    [Tooltip("List of all the intro dialogue audio for the Skull")]
    public AudioClip[] skullTauntAudioClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Plays a sound effect.
    /// </summary>
    /// <param name="soundClip">Sound clip that will be played.</param>
    /// <param name="volume">Volume of the sound clip.</param>
    /// <param name="audioListIndex">Index position of the list of audio.</param>
    public void PlayAudioFromList(AudioClip[] soundClip, int audioListIndex, float volume)
    {
        // spawn in the gameobject
        AudioSource audioSource = Instantiate(dialogueAudioObject, gameObject.transform.position, Quaternion.identity);

        // sound clip
        audioSource.clip = soundClip[audioListIndex];

        // sound volume
        audioSource.volume = volume;

        audioSource.Play();

        float soundClipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, soundClipLength);
    }

    /// <summary>
    /// Plays a sound effect.
    /// </summary>
    /// <param name="soundClip">Sound clip that will be played.</param>
    /// <param name="volume">Volume of the sound clip</param>
    public void PlaySoundFX(AudioClip soundClip, float volume)
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
    /// Plays a random sound effect.
    /// </summary>
    /// <param name="soundClip">The sound clip that will be played from an array.</param>
    /// <param name="volume">Volume of the sound clip</param>
    public void PlayRandomSFX(AudioClip[] soundClip, float volume)
    {
        int random = Random.Range(0, soundClip.Length);

        // spawn in the gameobject
        AudioSource audioSource = Instantiate(soundObject, gameObject.transform.position, Quaternion.identity);

        // sound clip
        audioSource.clip = soundClip[random];

        // sound volume
        audioSource.volume = volume;

        audioSource.Play();

        float soundClipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, soundClipLength);
    }

    public void startIntroSkullFirst(int whatIntro, float waitTime)
    {
        StartCoroutine(playIntroSkullFirst(whatIntro, waitTime));
    }

    IEnumerator playIntroSkullFirst(int whatIntro, float waitTime)
    {
        SoundFXManager.Instance.PlayAudioFromList(SoundFXManager.Instance.skullIntroAudioClips, whatIntro, 1.3f);
        yield return new WaitForSeconds(waitTime);
        SoundFXManager.Instance.PlayAudioFromList(SoundFXManager.Instance.stagIntroAudioClips, whatIntro, 1.8f);
    }

    public void startIntroStagFirst(int whatIntro, float waitTime)
    {
        StartCoroutine(playIntroStagFirst(whatIntro, waitTime));
    }

    IEnumerator playIntroStagFirst(int whatIntro, float waitTime)
    {
        SoundFXManager.Instance.PlayAudioFromList(SoundFXManager.Instance.stagIntroAudioClips, whatIntro, 1.8f);
        yield return new WaitForSeconds(waitTime);
        SoundFXManager.Instance.PlayAudioFromList(SoundFXManager.Instance.skullIntroAudioClips, whatIntro, 1.3f);
    }
}
