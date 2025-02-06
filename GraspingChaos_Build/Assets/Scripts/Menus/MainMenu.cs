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

    public void ProtoFullCase()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoFull);
        InputManager.Instance.FindPlayers();
    }

    public void ProtoHandAnimCase()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoHandAnims);
        InputManager.Instance.FindPlayers();
    }

    public void FSMTesting()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_StateTesting);
        GameManager.Instance.hasDuelStarted = true;
        InputManager.Instance.FindPlayers();
    }
}
