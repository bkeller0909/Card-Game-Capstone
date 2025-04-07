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

    public CanvasGroup titles, btns, arrows;

    [SerializeField]
    GameObject topArrow, botArrow;

    [SerializeField]
    float duration;

    [SerializeField]
    bool setFirstButton;

    bool aButtonIsActive;

    [SerializeField]
    Image P1Tutorial;

    [SerializeField]
    Image P2Tutorial;

    /// <summary>
    /// Loads the game into the duel Scene
    /// </summary>
    public void LoadDuelScene()
    {
        SoundFXManager.Instance.PlaySoundFX(SoundFXManager.Instance.uISelect, 1);
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
        if (GameManager.Instance.manualCards)
        {
            P1Tutorial.enabled = true;
            P2Tutorial.enabled = true;
        }
        else
        {
            P1Tutorial.enabled = false;
            P2Tutorial.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.BackQuote) && (GameManager.Instance.ln_CurrentLevelName == GameManager.Instance.ln_MainMenuName))
        {
            if (GameManager.Instance.manualCards == false)
            {
                GameManager.Instance.manualCards = true;
            }
            else
            {
                GameManager.Instance.manualCards = false;
            }
        }

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
        SoundFXManager.Instance.PlaySoundFX(SoundFXManager.Instance.uISelect, 1);
        Application.Quit();
    }

    /// <summary>
    /// Fades in the Main Menu Ui
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator FadeInTitle(float duration)
    {
        yield return new WaitForSeconds(3f);

        float elapsedTime = 0f;
        float startAlpha = titles.alpha; // start everything with with same alpha

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / duration;

            titles.alpha = Mathf.Lerp(startAlpha, 1f, normalizedTime);
            btns.alpha = Mathf.Lerp(startAlpha, 1f, normalizedTime);
            arrows.alpha = Mathf.Lerp(startAlpha, 1f, normalizedTime);

            yield return null;
        }

        titles.alpha = 1;
        btns.alpha = 1;
        btns.interactable = true;
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
