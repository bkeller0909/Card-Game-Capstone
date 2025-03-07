using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public CameraShake playerCameraShake;

    private void Awake()
    {
        playerCameraShake = GetComponentInChildren<CameraShake>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraShake(0.05f, 2f);
        }
    }

    public void CameraShake(float shakeIntensity, float shakeTimer)
    {
        StartCoroutine(DoCameraShake(shakeIntensity, shakeTimer));
    }

    public IEnumerator DoCameraShake(float shakeIntensity, float shakeTimer)
    {
        float timer = 0.0f;
        playerCameraShake.enabled = true;

        while (timer < shakeTimer)
        {
            timer += Time.deltaTime;
            playerCameraShake.intensity = shakeIntensity;
        }
        if(timer >= shakeTimer)
        {
            playerCameraShake.enabled = false;
        }
        yield return null;
    }
}
