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

    private PlayerManager[] players;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        players = FindObjectsOfType<PlayerManager>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].gamepad is Gamepad)
            {
                players[i].gamepad = (Gamepad)players[i].playerInput.devices[i];
            }
        }
    }

    private void Update()
    {
        if (players[0].playerInput.actions["Select"].WasPressedThisFrame())
        {
            RumbleManager.instance.ControllerRumble(0.25f, 0.5f, 0.25f, players[0].gamepad);
        }
        if (players[1].playerInput.actions["Select"].WasPressedThisFrame())
        {
            RumbleManager.instance.ControllerRumble(0.25f, 0.5f, 0.25f, players[1].gamepad);
        }
    }
}
