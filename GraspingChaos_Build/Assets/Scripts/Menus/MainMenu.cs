using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] questionText;

    public void CardSelectionBtnClicked()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoCardSelection);
        InputManager.Instance.FindPlayers();
    }

    public void ObjectTestBtnClicked()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoObjectTests);
        InputManager.Instance.FindPlayers();
    }

    public void ObjectArtShowCase()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoArtShowCase);
        InputManager.Instance.FindPlayers();
    }

    public void ObjectParticleCase()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoParticleShowCase);
        InputManager.Instance.FindPlayers();
    }

    public void QTECase()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoQTECase);
        InputManager.Instance.FindPlayers();
    }

    public void FSMTesting()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_StateTesting);
        GameManager.Instance.hasDuelStarted = true;
        InputManager.Instance.FindPlayers();
    }
}
