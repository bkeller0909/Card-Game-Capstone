using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButtonSelected;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject optionsButtonSelected;

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
