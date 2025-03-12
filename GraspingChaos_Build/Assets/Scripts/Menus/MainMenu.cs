using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//----------------------------------------------------------------
//  Author:       Wyatt
//  Title:        MainMenu
//  Date Created: 01/09/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Holds the functions that operate the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    Button[] buttons;

    [SerializeField]
    CanvasGroup titles, btns, arrows;

    [SerializeField]
    GameObject topArrow, botArrow;

    [SerializeField]
    float duration;

    [SerializeField]
    bool setFirstButton;

    bool aButtonIsActive;

    /// <summary>
    /// Loads the game into the duel Scene
    /// </summary>
    public void LoadDuelScene()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_Duelscene);
        InputManager.Instance.FindPlayers();
    }

    private void Start()
    {
        setFirstButton = false;
        aButtonIsActive = false;
        arrows.alpha = 0;
        titles.alpha = 0;
        btns.alpha = 0;
        btns.interactable = false;
        StartCoroutine(FadeInTitle(duration));
    }

    private void Update()
    {
        if (setFirstButton)
        {
            bool buttonFound = false;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].GetComponent<BtnIsSelected>().isSelected)
                {
                    aButtonIsActive = true;
                    buttonFound = true;
                }

                if (i == buttons.Length - 1 && buttonFound == false)
                {
                    aButtonIsActive = false;
                }
            }

            if (!aButtonIsActive)
            {
                buttons[0].Select();
            }
        }
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void KillSwitch()
    {
        Application.Quit();
    }

    /// <summary>
    /// Fades in the Main Menu Ui
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator FadeInTitle(float duration)
    {
        while (titles.alpha < 1)
        {
            yield return new WaitForSeconds(duration);
            titles.alpha += 0.05f;
            btns.alpha += 0.05f;
            arrows.alpha += 0.05f;
        }
        titles.alpha = 1;
        btns.alpha = 1;
        btns.interactable = true;

        yield return null;
    }

    /// <summary>
    /// Moves the arrows from location to location
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="topPos"></param>
    /// <param name="botPos"></param>
    /// <returns></returns>
    public IEnumerator MoveArrows(Button btn, Transform topPos, Transform botPos)
    {
        float moveTimer = 0.3f;
        float time = 0.0f;

        while (time < moveTimer)
        {
            time += Time.deltaTime;

            Vector3 lerpPosTop = Vector3.Lerp(topArrow.transform.position, topPos.position, (time / moveTimer));
            Vector3 lerpPosBot = Vector3.Lerp(botArrow.transform.position, botPos.position, (time / moveTimer));

            topArrow.transform.position = lerpPosTop;
            botArrow.transform.position = lerpPosBot;

            yield return null;
        }

        btn.interactable = true;
        yield return null;
    }
}
