using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
//  Author: Cooper Hill
//  Title: Camera Position Change
//  Date: Created: 01/21/2025
//  Purpose: Changes the position of the assigned camera based on Inputs
//  Instance: No
//-----------------------------------------------------------------

/// <summary>
/// This script can only be put on a camera and will move the camera to set points in the "CamPos" list through functions called on player inputs
/// </summary>

[RequireComponent(typeof(Camera))]
public class CameraPositionChange : MonoBehaviour
{
    PlayerManager player;

    //Private Variables
    [Tooltip("List of transforms that the camera can change to")]
    [SerializeField] private List<Transform> CamPos;
    [SerializeField] private float lerpTime;

    private int cameraIndex = 0;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerManager>();
        cameraIndex = Mathf.Clamp(cameraIndex, 0, CamPos.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerInput.actions["NavDown"].triggered)
        {
            cameraIndex--;
            StartCoroutine(MoveCameratoNewPosition(cameraIndex));
        }
        if (player.playerInput.actions["NavUp"].triggered)
        {
            cameraIndex++;
            StartCoroutine(MoveCameratoNewPosition(cameraIndex));
        }
    }

    /// <summary>
    /// Changes the camera's transform to match the assigned transform based on the intager provided
    /// | 1 - Default |
    /// 2 - Card View |
    /// 3 - Player Hand View |
    /// 4 - Opponent Hand View
    /// </summary>
    /// <param name="positionIndex">The index within the list of camera positions</param>
    public IEnumerator MoveCameratoNewPosition(int positionIndex)
    {
        elapsedTime = 0.0f;
        while(elapsedTime < lerpTime)
        {
            elapsedTime += Time.deltaTime;

            float percentCompleteForPosition = elapsedTime / lerpTime;
            float percentCompleteForRotation = (elapsedTime * 0.5f) / lerpTime;

            //Rotate and Move the camera to the desired location
            transform.position = Vector3.Lerp(transform.position, CamPos[positionIndex].position, percentCompleteForPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, CamPos[positionIndex].rotation, percentCompleteForRotation);

            percentCompleteForPosition = 0;
            percentCompleteForRotation = 0;

            yield return null;
        }
    }
}
