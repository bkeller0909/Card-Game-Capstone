using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] CanvasGroup titles, btns, arrows;
    [SerializeField] GameObject topArrow, botArrow;
    [SerializeField] float duration;


    public void ObjectArtShowCase()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoArtShowCase);
        InputManager.Instance.FindPlayers();
    }

    public void LoadDuelScene()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_Duelscene);
        InputManager.Instance.FindPlayers();
    }
    public void FSMTesting()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_StateTesting);
        GameManager.Instance.hasDuelStarted = true;
        InputManager.Instance.FindPlayers();
    }

    private void Start()
    {
        arrows.alpha = 0;
        titles.alpha = 0;
        btns.alpha = 0;
        btns.interactable = false;
        StartCoroutine(FadeInTitle(duration));
    }

    public void KillSwitch()
    {
        Application.Quit();
    }

    IEnumerator FadeInTitle(float duration)
    {
        while (titles.alpha < 1)
        {
            yield return new WaitForSeconds(duration);
            titles.alpha += 0.1f;
            btns.alpha += 0.1f;
            arrows.alpha += 0.1f;
        }
        titles.alpha = 1;
        btns.alpha = 1;
        btns.interactable = true;

        yield return null;
    }

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
