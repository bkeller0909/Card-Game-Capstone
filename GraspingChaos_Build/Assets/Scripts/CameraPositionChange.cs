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
    InputHandler playerInput;

    //Private Variables
    [Tooltip("List of transforms that the camera can change to")]
    [SerializeField] private List<Transform> CamPos;
    [SerializeField] private float lerpTime;
    [SerializeField] public bool weHaveArrived = false;

    public Transform QTECamPos;
    public Transform bottleCamPos;

    private int cameraIndex = 0;
    private float elapsedTime;

    private bool canInput = true;

    private bool cameraInputPressed = false;

    public bool noButtonUsage = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerManager>();
        playerInput = GetComponentInParent<InputHandler>();
    }

    /// <summary>
    /// Gets the player controls to change the camera position through the array
    /// </summary>
    public void GetInput()
    {
        //Checks if the player can input
        if (canInput)
        {
            //Get the player input for Up and Down and check if there is a camera position to go to
            if (playerInput.cameraDown && cameraIndex > 0 && !cameraInputPressed)
            {
                cameraInputPressed = true;
                cameraIndex--;
                StartCoroutine(MoveCameratoNewPosition(cameraIndex));

                //Lock player input till move is complete
                canInput = false;
            }
            else if (playerInput.cameraUp && cameraIndex < 1 && !cameraInputPressed)
            {
                cameraInputPressed = true;
                cameraIndex++;
                StartCoroutine(MoveCameratoNewPosition(cameraIndex));

                //Lock player input till move is complete
                canInput = false;
            }
            else if (!playerInput.cameraDown && !playerInput.cameraUp)
            {
                cameraInputPressed = false;
            }
        }
    }

    /// <summary>
    /// Forces the camera position to be changed
    /// </summary>
    /// <param name="currentCamera">Index of the array for the current camera position</param>
    public void GetInputForced(int currentCamera)
    {
        if (canInput)
        {
            if (currentCamera == 0)
            {
                StartCoroutine(MoveCameratoNewPosition(0));
                cameraIndex = 0;
                canInput = false;
            }
            else if (currentCamera == 1)
            {
                StartCoroutine(MoveCameratoNewPosition(1));
                cameraIndex = 1;
                canInput = false;
            }
            else if (currentCamera == 2)
            {
                StartCoroutine(MoveCameratoNewPosition(2));
                cameraIndex = 2;
                canInput = false;
            }
            else if(currentCamera == 3)
            {
                StartCoroutine(MoveCameratoNewPosition(3));
                cameraIndex = 3;
                canInput = false;
            }
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
        //Sets the cameras move timer to 0 (starting the move)
        elapsedTime = 0.0f;

        //Loop and move the camera until the elapsed time reaches the desired time
        while (elapsedTime < lerpTime)
        {
            noButtonUsage = true;
            elapsedTime += Time.deltaTime;

            //Set 2 different progressions for the position and rotation
            float percentCompleteForPosition = elapsedTime / lerpTime;
            float percentCompleteForRotation = (elapsedTime * 0.5f) / lerpTime;

            //Rotate and Move the camera to the desired location
            transform.position = Vector3.Lerp(transform.position, CamPos[positionIndex].position, percentCompleteForPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, CamPos[positionIndex].rotation, percentCompleteForRotation);

            //Reset the complete percent for next move
            percentCompleteForPosition = 0;
            percentCompleteForRotation = 0;

            //wait till the next frame to move again
            yield return new WaitForEndOfFrame();
        }

        noButtonUsage = false;
        //Unlock player input allowing the next move
        canInput = true;
    }

    public IEnumerator ForceCameraNewPos(Transform newCamPos)
    {
        Transform currentCamPos = transform;

        //Sets the cameras move timer to 0 (starting the move)
        elapsedTime = 0.0f;

        //Loop and move the camera until the elapsed time reaches the desired time
        while (elapsedTime < lerpTime)
        {
            elapsedTime += Time.deltaTime;

            //Set 2 different progressions for the position and rotation
            float percentCompleteForPosition = elapsedTime / lerpTime;
            float percentCompleteForRotation = (elapsedTime * 0.5f) / lerpTime;

            //Rotate and Move the camera to the desired location
            transform.position = Vector3.Lerp(currentCamPos.position, newCamPos.position, percentCompleteForPosition);
            transform.rotation = Quaternion.Lerp(currentCamPos.rotation, newCamPos.rotation, percentCompleteForRotation);

            //Reset the complete percent for next move
            percentCompleteForPosition = 0;
            percentCompleteForRotation = 0;

            //wait till the next frame to move again
            yield return new WaitForEndOfFrame();
        }

        //Unlock player input allowing the next move
        canInput = true;
    }
}
