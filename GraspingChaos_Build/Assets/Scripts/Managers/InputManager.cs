using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public PlayerControls controls;
    public PlayerInput playerInput;

    private Gamepad gamepad;

    private void Start()
    {
        playerInput.onControlsChanged += SwitchControls;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        controls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
        playerInput.onControlsChanged -= SwitchControls;
    }

    public void ControllerRumble(float lowFreq, float highFreq, float duration)
    {
        gamepad = Gamepad.current;

        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(lowFreq, highFreq);
        }
    }

    public IEnumerator StopRumble(float duration, Gamepad gamepad)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gamepad.SetMotorSpeeds(0f, 0f);
    }

    private void SwitchControls(PlayerInput input)
    {
        Debug.Log("Device is now: " + input.currentControlScheme);
    }
}
