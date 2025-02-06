using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] questionText;

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
}
