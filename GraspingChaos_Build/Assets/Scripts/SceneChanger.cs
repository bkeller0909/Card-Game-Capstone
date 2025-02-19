using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public bool changeScene = false;
    private void Update()
    {
        if (changeScene == true || Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);

            // uncomment below if you want to test without persistant
            //SceneManager.LoadScene("GC_MainMenu");
        }
    }
}
