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
    public static InputManager Instance;
    public PlayerControls playerControls;

    private PlayerManager[] players;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (GameManager.Instance.ln_CurrentLevelName == GameManager.Instance.ln_ProtoCardSelection)
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

    public void FindPlayers()
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
}
