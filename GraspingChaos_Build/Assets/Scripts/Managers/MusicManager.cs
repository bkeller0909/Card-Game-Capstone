using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Co-Auther:    
//  Title:        MusicManager
//  Date Created: 02/26/2025
//  Instance:     Yes
//-----------------------------------------------------------------

/// <summary>
/// Controls the music for the game.
/// </summary>
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip tutorialMusic;
    public AudioClip lowHealthMusic;

    [Header("Audio Tracks")]
    [SerializeField] private AudioSource currentTrack;
    [SerializeField] private AudioSource newTrack; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // TODO - this is pretty buggy will need to research more later. Don't got time to do it right now
    /// <summary>
    /// Fades in a new audio clip while is fades out a current audio clip being played.
    /// </summary>
    /// <param name="newMusicClip">New audio clip that will fade in.</param>
    /// <param name="currentMusicClip">Current audio clip that will fade out.</param>
    /// <returns></returns>
    private IEnumerator FadeAudioTrack(AudioClip newMusicClip, AudioClip currentMusicClip)
    {
        float fadeTime = 2f;
        float elapsedTime = 0f;

        currentTrack.clip = currentMusicClip;
        newTrack.clip = newMusicClip;

        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;

            currentTrack.volume = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            yield return new WaitForEndOfFrame();
            newTrack.Play();
            newTrack.volume = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
            yield return null;
        }

        currentTrack.Stop();
    }

    /// <summary>
    /// Just plays an audio clip cause why not
    /// </summary>
    /// <param name="scene">The scene you want it to play in</param>
    /// <param name="clip">The audio clip</param>
    public void PlayMusic(string scene, AudioClip clip)
    {
        if(GameManager.Instance.ln_CurrentLevelName == scene)
        {
            currentTrack.clip = clip;
            currentTrack.Play();
        }
    }

    // TODO - fix this
    /// <summary>
    /// Runs the coroutine to fade in between new audio.
    /// </summary>
    /// <param name="newClip">New audio clip you want to play.</param>
    /// <param name="currentClip">Current clip that will fade out.</param>
    public void SwapMusic(AudioClip newClip, AudioClip currentClip)
    {
        StartCoroutine(FadeAudioTrack(newClip, currentClip));
    }

    /// <summary>
    /// Plays intense music when a player gets to low health.
    /// </summary>
    public void MusicLowHealth()
    {
        if (GameManager.Instance.player1.entireHP == 10 || GameManager.Instance.player2.entireHP == 10)
        {
            SwapMusic(lowHealthMusic, gameMusic);
        }
    }
}
