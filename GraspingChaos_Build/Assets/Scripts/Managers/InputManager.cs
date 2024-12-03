using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public PlayerControls controls;

    private Gamepad gamepad;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void ControllerRumble(float lowFreq, float highFreq, float duration)
    {
        gamepad = Gamepad.current;

        if(gamepad != null)
        {
            gamepad.SetMotorSpeeds(lowFreq, highFreq);
            StartCoroutine(StopRumble(duration, gamepad));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad gamepad)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gamepad.SetMotorSpeeds(0f, 0f);
    }
}
