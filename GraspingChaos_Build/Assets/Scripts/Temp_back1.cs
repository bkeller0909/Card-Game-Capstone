using UnityEngine;

/// <summary>
/// delete after presentation
/// </summary>
public class Temp_back1 : MonoBehaviour
{
    public void BackButton()
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
    }
}
