using UnityEngine;
using UnityEngine.InputSystem;
//----------------------------------------------------------------
//  Author:       Keller
//  Title:        InputManager
//  Date Created: 01/07/2025
//  Instance:     Yes
//-----------------------------------------------------------------

/// <summary>
/// Handles the rumble function of a controller.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public PlayerControls playerControls;

    private Gamepad gamepad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerControls = new PlayerControls();
    }

    /// <summary>
    /// Enables the control scheme once a controller is connected to the device.
    /// </summary>
    private void OnEnable()
    {
        playerControls.Enable();
    }

    /// <summary>
    /// Disables the control scheme when a controller is disconnected.
    /// </summary>
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
