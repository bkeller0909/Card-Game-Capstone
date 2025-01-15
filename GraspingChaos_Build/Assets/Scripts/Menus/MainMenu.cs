using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void CardSelectionBtnClicked()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoCardSelection);
        InputManager.Instance.FindPlayers();
    }

    public void ObjectTestBtnClicked()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoObjectTests);
    }

    public void ObjectArtShowCase()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoArtShowCase);
    }
}
