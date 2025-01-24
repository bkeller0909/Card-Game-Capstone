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

    private bool canInput = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the player input
        GetInput();
    }

    private void GetInput()
    {
        //Checks if the player can input
        if (canInput)
        {
            //Get the player input for Up and Down and check if there is a camera position to go to
            if (player.playerInput.actions["NavDown"].triggered && cameraIndex > 0)
            {
                cameraIndex--;
                StartCoroutine(MoveCameratoNewPosition(cameraIndex));

                //Lock player input till move is complete
                canInput = false;
            }
            else if (player.playerInput.actions["NavUp"].triggered && cameraIndex < 2)
            {
                cameraIndex++;
                StartCoroutine(MoveCameratoNewPosition(cameraIndex));

                //Lock player input till move is complete
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

        //Unlock player input allowing the next move
        canInput = true;
    }
}
