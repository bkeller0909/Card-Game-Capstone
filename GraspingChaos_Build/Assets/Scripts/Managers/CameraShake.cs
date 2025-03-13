using UnityEngine;

//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//  Title:          CameraShake
//  Date Created:   March 11, 2025
//  Instance:       No
//-----------------------------------------------------------------


/// <summary>
/// Shake the to random points based on a current position.
/// 
/// Attached to the player's camera.
/// </summary>
public class CameraShake : MonoBehaviour
{
    Camera camera;

    [HideInInspector] public Vector3 initialPos;
    [HideInInspector] public float intensity;

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
