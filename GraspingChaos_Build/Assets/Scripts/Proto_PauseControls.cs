using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Proto_Menus : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButtonSelected;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject optionsButtonSelected;

    PlayerControls playerControls;

    bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.playerControls.Player.Pause.WasPressedThisFrame())
        {
            Pause(!isPaused);
        }
        Options();
    }

    public void Pause(bool paused)
    {
        isPaused = paused;
        pauseMenu.SetActive(paused);
        if(pauseMenu.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(pauseButtonSelected);
        }
    }

    public void Options()
    {
        if (optionsMenu.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(optionsButtonSelected);
        }
    }
}
