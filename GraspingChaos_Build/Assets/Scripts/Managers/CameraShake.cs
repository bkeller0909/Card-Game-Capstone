using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera camera;
    public float intensity;
    Vector3 initialPos;

    private void Awake()
    {
        camera = GetComponent<Camera>();

        initialPos = camera.transform.position;
    }

    private void Update()
    {
        ShakeCamera(intensity);
    }

    public void ShakeCamera(float shakeIntensity)
    {
        camera.transform.position = initialPos + Random.insideUnitSphere * shakeIntensity;
    }
}
