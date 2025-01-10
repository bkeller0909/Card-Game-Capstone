using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Game Manager
//  Date Created: 01/07/2025
//  Purpose:      This is to manage the base game fundamentals
//  Instance?     Yes
//-----------------------------------------------------------------
/// </summary>
public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            if (!instance)
            {
                Debug.LogError("No Game Manager Present !!!");
            }

            return instance;

        }
    }


    #region // Level loading: This region is all the reguired variables for loading a level
    [Tooltip("This need to be the name of the main menu scene")]
    public string ln_CurrentLevelName;

    [Tooltip("This need to be the name of the main menu scene")]
    public string ln_MainMenuName;

    [Tooltip("This need to be the name of the actual game play scene")]
    public string ln_Duelscene;

    private bool isLoading = false;

    [Tooltip("This is the visual loading screen")]
    public LoadingScreen loadingScreen;

    //how the cameras get assigned to there displayes
    private CameraAssigner cameraAssigner;

    //Everything below here are names for the scenes to showoff at milestones
    [Tooltip("This need to be the name of the prototype card selection scene")]
    public string ln_ProtoCardSelection;

    [Tooltip("This need to be the name of the prototype Object test scene")]
    public string ln_ProtoObjectTests;

    [Tooltip("This need to the name of the prototype finger select scene")]
    public string ln_ProtoFingerSelection;

    #endregion

    [Tooltip("This is the base mana value each")]
    public int startingMana;

    [Tooltip("This is the mana given to each player at the start of each turn")]
    public int manaPerTurn;

    [Tooltip("This is the max hand size of a player")]
    public int playerHandSize;

    [Tooltip("This is one of the players in the game")]
    public PlayerManager player1, player2;

    [Tooltip("This is which player is currently casting a spell")]
    public PlayerManager currentCaster;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            if (i < 2)
            {
                Display.displays[i].Activate();
            }
        }

        StartLoadingLevel(ln_MainMenuName);
    }

    // Update is called once per frame
    void Update()
    {
        if (ln_CurrentLevelName != ln_MainMenuName)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartLoadingLevel(ln_MainMenuName);
            }
        }
    }

    public void StartLoadingLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    private IEnumerator LoadLevel(string levelName)
    {
        isLoading = true;

        loadingScreen.SetLoadingScreenActive(true);

        yield return new WaitForSeconds(.25f);


        if ((!string.IsNullOrEmpty(ln_CurrentLevelName)))
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(ln_CurrentLevelName);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }

        //Testing this
        ln_CurrentLevelName = levelName;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));

        cameraAssigner = FindAnyObjectByType<CameraAssigner>();

        cameraAssigner.playerOneCamera.targetDisplay = 0;
        cameraAssigner.playerTwoCamera.targetDisplay = 1;

        ln_CurrentLevelName = levelName;

        yield return new WaitForSeconds(.5f);

        loadingScreen.SetLoadingScreenActive(false);

        isLoading = false;
    }
}
