using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    private VideoPlayer introPlayer;

    [SerializeField] private CanvasGroup skipIntroP1;
    [SerializeField] private CanvasGroup skipIntroP2;
    [SerializeField] private float fadeDelayDuration;
    [SerializeField] private float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        introPlayer = GetComponentInChildren<VideoPlayer>();
        introPlayer.loopPointReached += IntroEnd;
        skipIntroP1.alpha = 0.0f;
        skipIntroP2.alpha = 0.0f;

        StartCoroutine(SkipIntroFade(fadeDuration, fadeDelayDuration));
    }


    // Update is called once per frame
    void Update()
    {
        SkipIntro();
    }

    private void IntroEnd(VideoPlayer source)
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
    }

    private void SkipIntro()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            //Gamepad.current
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
        }
    }

    private IEnumerator SkipIntroFade(float duration, float fadeDelay)
    {
        yield return new WaitForSeconds(fadeDelay);

        float elapsedTime = 0f;
        float startAlpha = skipIntroP1.alpha; // start everything with with same alpha

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / duration;

            skipIntroP1.alpha = Mathf.Lerp(startAlpha, 1f, normalizedTime);
            skipIntroP2.alpha = Mathf.Lerp(startAlpha, 1f, normalizedTime);

            yield return null;
        }

        skipIntroP1.alpha = 1f;
        skipIntroP2.alpha = 1f;
    }
}
