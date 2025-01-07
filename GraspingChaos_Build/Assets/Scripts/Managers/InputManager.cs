using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Manages the different control schemes within the input system and handles the rumble function of a controller.
/// </summary>
/// Brandon Keller
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

    /// <summary>
    /// Rumble actions for any controller connected that allows rumble functionality.
    /// 
    /// IT IS EXTREMELY IMPORTANT that the frequeny parameters do not go over 1.0f!!!!!
    /// Exceeding 1.0f will wear out the motors of the controller over time.
    /// </summary>
    /// <param name="lowFreq">Intensity for the low frequencey motors are within the controller.</param>
    /// <param name="highFreq">Intensity for the high frequencey motors are within the controller.</param>
    /// <param name="duration">How long, in seconds, the controller will rumble.</param>
    public void ControllerRumble(float lowFreq, float highFreq, float duration)
    {
        gamepad = Gamepad.current; // current gamepad connected to the device.

        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(lowFreq, highFreq);
            StartCoroutine(StopRumble(duration, gamepad));
        }
    }

    /// <summary>
    /// Stops the rumble of the controller so it does not go on infinitely.
    /// </summary>
    /// <param name="duration">The applied duration from the ControllerRumble function.</param>
    /// <param name="gamepad">Takes the gamepad connected to the device.</param>
    /// <returns></returns>
    private IEnumerator StopRumble(float duration, Gamepad gamepad)
    {
        float time = 0f;
        // stops the rumble once the timer reaches the set duration amount.
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        // resets the motors.
        gamepad.SetMotorSpeeds(0f, 0f);
    }
}
