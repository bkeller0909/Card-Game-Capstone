using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Co-Auther:   
//  Title:        SoundManager
//  Date Created: 02/13/2025
//  Instance:     Yes
//-----------------------------------------------------------------

/// <summary>
/// Handle all sound output for the game. 
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource soundObject;

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
    /// <param name="spawnTransform">Transform position where the sound will be played.</param>
    /// <param name="volume">Volume of the sound clip</param>
    public void PlaySoundFX(AudioClip soundClip, Transform spawnTransform, float volume)
    {
        // spawn in the gameobject
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

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
    /// <param name="spawnTransform">Transform position where the sound will be played.</param>
    /// <param name="volume">Volume of the sound clip</param>
    public void PlayRandomSFX(AudioClip[] soundClip, Transform spawnTransform, float volume)
    {
        int random = Random.Range(0, soundClip.Length);

        // spawn in the gameobject
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        // sound clip
        audioSource.clip = soundClip[random];

        // sound volume
        audioSource.volume = volume;

        audioSource.Play();

        float soundClipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, soundClipLength);
    }
}
