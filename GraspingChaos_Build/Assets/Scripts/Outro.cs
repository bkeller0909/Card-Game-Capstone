using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Outro : MonoBehaviour
{
    private VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.loopPointReached += OutroEnd; 
    }

    private void OutroEnd(VideoPlayer source)
    {
        GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
    }
}
