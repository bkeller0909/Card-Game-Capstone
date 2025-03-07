using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    [HideInInspector] public CameraShake playerCameraShake;

    private void Awake()
    {
        playerCameraShake = GetComponentInChildren<CameraShake>();
    }

    /// <summary>
    /// Shakes the player camera.
    /// </summary>
    /// <param name="shakeIntensity">Intensity of the shake. Use a VERY LOW float value. Preferabley under 0.1f</param>
    /// <param name="shakeTimer">Duration of the camera shake.</param>
    public void CameraShake(float shakeIntensity, float shakeTimer)
    {
        StartCoroutine(DoCameraShake(shakeIntensity, shakeTimer));
    }

    private IEnumerator DoCameraShake(float shakeIntensity, float shakeTimer)
    {
        playerCameraShake.enabled = true;
        playerCameraShake.intensity = shakeIntensity;
        yield return new WaitForSeconds(shakeTimer);
        playerCameraShake.transform.position = playerCameraShake.initialPos;
        playerCameraShake.enabled = false;
    }
}
