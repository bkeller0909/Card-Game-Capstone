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
    //Private Variables
    [Tooltip("List of transforms that the camera can change to")]
    [SerializeField] private List<Transform> CamPos;
    [SerializeField] private float lerpTime;

    private int testIndex = 0;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            testIndex--;
            elapsedTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            testIndex++;
            elapsedTime = 0;
        }

        testIndex = Mathf.Clamp(testIndex, 0, 3);

        MoveCameratoNewPosition(testIndex);
    }

    /// <summary>
    /// Changes the camera's transform to match the assigned transform based on the intager provided
    /// | 1 - Default |
    /// 2 - Card View |
    /// 3 - Player Hand View |
    /// 4 - Opponent Hand View
    /// </summary>
    /// <param name="positionIndex">The index within the list of camera positions</param>
    public void MoveCameratoNewPosition(int positionIndex)
    {

        float percentCompleteForPosition = elapsedTime / lerpTime;
        float percentCompleteForRotation = (elapsedTime * 0.5f) / lerpTime;

        //Rotate and Move the camera to the desired location
        transform.position = Vector3.Lerp(transform.position, CamPos[positionIndex].position, percentCompleteForPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, CamPos[positionIndex].rotation, percentCompleteForRotation);

        percentCompleteForPosition = 0;
        percentCompleteForRotation = 0;
    }
}
