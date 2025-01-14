using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void CardSelectionBtnClicked()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoCardSelection);
    }

    public void ObjectTestBtnClicked()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_ProtoObjectTests);
    }

}
