using UnityEngine;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
    public bool changeScene = false;
    private void Update()
    {
        if (changeScene)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
            }
        }
    }
}
