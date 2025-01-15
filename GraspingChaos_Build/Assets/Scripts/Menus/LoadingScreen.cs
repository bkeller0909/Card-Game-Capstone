using UnityEngine;

//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Loading Screen
//  Date Created: 01/07/2025
//  Purpose:      This is to manage the the two loading screen canvases
//  Instance?     no
//-----------------------------------------------------------------

/// <summary>
/// This is to manage the the two loading screen canvases
/// </summary>
public class LoadingScreen : MonoBehaviour
{
    [Tooltip("This is the loading screen gameobject that displays on display 1")]
    public GameObject loadingScreen1;
    [Tooltip("This is the loading screen gameobject that displays on display 2")]
    public GameObject loadingScreen2;

    void Awake()
    {
        loadingScreen1.SetActive(false);
        loadingScreen2.SetActive(false);
    }

    public void SetLoadingScreenActive(bool isActive)
    {
        loadingScreen1.SetActive(isActive);
        loadingScreen2.SetActive(isActive);
    }
}
