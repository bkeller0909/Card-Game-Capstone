using UnityEngine;
using UnityEngine.InputSystem;

public class Proto_AnimationQTE_Rumble : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.controls.Rumble.RumbleAction.WasPressedThisFrame())
        {
            InputManager.instance.ControllerRumble(0.25f, 1.0f, 0.25f);
        }

        if (InputManager.instance.controls.Rumble.RumbleAction.WasReleasedThisFrame())
        {
            StartCoroutine(InputManager.instance.StopRumble(0.1f, Gamepad.current));
        }
    }
}
