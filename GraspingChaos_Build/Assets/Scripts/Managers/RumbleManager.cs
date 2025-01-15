using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//----------------------------------------------------------------
//  Author:       Keller
//  Title:        RumbleManager
//  Date Created: 01/14/2025
//  Instance:     Yes
//-----------------------------------------------------------------

/// <summary>
/// Handles the rumble function of a controller.
/// </summary>
public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
    /// <param name="gamepad">The gamepad that will be affected by rumble.</param>
    public void ControllerRumble(float lowFreq, float highFreq, float duration, Gamepad gamepad)
    {
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
    /// <param name="gamepad">The affected gamepad.</param>
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
