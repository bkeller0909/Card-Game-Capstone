using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumble : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.playerControls.Rumble.RumbleAction.WasPressedThisFrame())
        {
            InputManager.instance.ControllerRumble(0.25f, 1.0f, 0.15f);
        }

        if (InputManager.instance.playerControls.Rumble.RumbleSelect.WasPressedThisFrame())
        {
            InputManager.instance.ControllerRumble(0.15f, 0.5f, 0.10f);
        }
    }
}
