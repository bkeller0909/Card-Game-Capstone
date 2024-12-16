using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
