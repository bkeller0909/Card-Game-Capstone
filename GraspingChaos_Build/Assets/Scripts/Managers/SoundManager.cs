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
    private static SoundManager Instance;
    [SerializeField] private AudioSource soundObject;

    // [SerializeField] private SoundList[] soundsList;        // takes in an array of the struct SoundList

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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

    /*/// <summary>
    /// Plays the sound clip
    /// </summary>
    /// <param name="spellSound">Name of spell that will be associated with the sound.</param>
    /// <param name="volume">Volume of the sound clip.</param>
    public void PlaySound(SpellNames spellSound, float volume = 1)
    {
        for(int i = 0; i < soundsList.Length; i++)
        {
            if (soundsList[i].SpellName == spellSound)
            {
                if (soundsList[i].Sounds.Length > 0)
                {
                    AudioClip clip = soundsList[i].Sounds[UnityEngine.Random.Range(0, soundsList[i].Sounds.Length)];
                    audioSource.PlayOneShot(clip, volume);
                }
            }
        }
    }*/
}

/*/// <summary>
/// Struct of audio clips
/// </summary>
[Serializable]
public struct SoundList
{
    [SerializeField] private SpellNames spellName;      // sets the spell name of the card that will have the sound clip
    [SerializeField] private AudioClip[] sounds;        // array of sound clips

    public SpellNames SpellName => spellName;
    public AudioClip[] Sounds => sounds;
}*/
