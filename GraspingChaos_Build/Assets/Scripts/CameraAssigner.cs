using UnityEngine;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Camera Assigner
//  Date Created: 01/08/2025
//  Purpose:      This gets each camera in the scene and allows it to be assigned to each display
//  Instance?     Yes
//-----------------------------------------------------------------
/// </summary>
public class CameraAssigner : MonoBehaviour
{
    [Tooltip("This is the camera for player 1")]
    public Camera playerOneCamera;

    [Tooltip("This is the camera for player 2")]
    public Camera playerTwoCamera;
}
