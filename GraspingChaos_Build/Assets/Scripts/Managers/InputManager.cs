using UnityEngine;
using UnityEngine.InputSystem;
//----------------------------------------------------------------
//  Author:       Keller
//  Co-Auther:   
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

    [HideInInspector] public PlayerManager[] players;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        playerControls = new PlayerControls();
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

        Debug.Log("We found players");
    }
}
