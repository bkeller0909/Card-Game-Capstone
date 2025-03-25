using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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
    [SerializeField] private GameObject btnArrow;

    bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.playerControls.Player.Pause.WasPressedThisFrame())
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

    public IEnumerator MoveArrows(Button btn, Transform topPos)
    {
        float moveTimer = 0.3f;
        float time = 0.0f;

        while (time < moveTimer)
        {
            time += Time.deltaTime;

            Vector3 lerpPosTop = Vector3.Lerp(btnArrow.transform.position, topPos.position, (time / moveTimer));

            btnArrow.transform.position = lerpPosTop;

            yield return null;
        }

        btn.interactable = true;
        yield return null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
