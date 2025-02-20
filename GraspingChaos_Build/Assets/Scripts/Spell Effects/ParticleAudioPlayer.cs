using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[ExecuteAlways]
[RequireComponent(typeof(VisualEffect))]
public class ParticleAudioPlayer : VFXOutputEventAbstractHandler
{
    public override bool canExecuteInEditor => true;
    public AudioSource audioSource;

    public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
