using UnityEngine;
using UnityEngine.EventSystems;


//----------------------------------------------------------------
//  OG Author:    Keller
//  Title:        Menu Manager
//  Date Created: 01/07/2025
//  Instance?     no
//-----------------------------------------------------------------

/// <summary>
/// This scene holds each of the buttons and functions for in game menus
/// </summary>
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButtonSelected;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject optionsButtonSelected;

    bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.playerControls.Player.Pause.WasPressedThisFrame())
        {
            Pause(!isPaused);
        }
    }

    public void Pause(bool pause)
    {
        isPaused = pause;
        pauseMenu.SetActive(pause);
        if (pauseMenu.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(pauseButtonSelected);
        }
    }

    public void Options(bool options)
    {
        optionsMenu.SetActive(options);
        if (optionsMenu.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(optionsButtonSelected);
        }
    }
}
