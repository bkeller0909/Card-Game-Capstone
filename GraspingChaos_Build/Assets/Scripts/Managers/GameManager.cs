using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Game Manager
//  Date Created: 01/07/2025
//  Purpose:      This is to manage the base game fundamentals
//  Instance?     Yes
//-----------------------------------------------------------------

/// <summary>
/// This is to manage the base game fundamentals
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
    [Tooltip("This needs to be the name of the prototype Art Showcase")]
    public string ln_ProtoArtShowCase;

    [Tooltip("This is the testing scene for the FSM")]
    public string ln_StateTesting;
    #endregion

    [Tooltip("This is whether the actual duel has started")]
    public bool hasDuelStarted;

    [Tooltip("This is the base mana value each")]
    public int startingMana;

    [Tooltip("This is the mana given to each player at the start of each turn")]
    public int manaPerTurn;

    [Tooltip("This is the max hand size of a player")]
    public int maxPlayerHandSize;

    [Tooltip("This is one of the players in the game")]
    public PlayerManager player1, player2;

    [Tooltip("This is which player is currently casting a spell")]
    public PlayerManager currentCaster;

    [Tooltip("This is which player is currently casting a spell")]
    public int timerQTE = 5;

    [Tooltip("This is which player is currently casting a spell")]
    public int amtOfSpellsBeingCast = 0;

    [Tooltip("What Slected spells are being played")]
    public int spellIndex = 0;

    [Tooltip("keep track of all spells played per round")]
    public int playedSpells = 0;

    [Tooltip("This sets up which spell goes first")]
    public bool roundCheck = false;

    [Tooltip("This is a test bool for moving to the next state")]
    public bool nextStateP1 = false;

    [Tooltip("This is a test bool for moving to the next state")]
    public bool nextStateP2 = false;

    [Tooltip("This is checking if player one has entered the state")]
    public bool player1IsHere = false;

    [Tooltip("This is checking if player two has entered the state")]
    public bool player2IsHere = false;

    [Tooltip("This is checking if player two has entered the state")]
    public bool[] spellsThatHaveBeenCast;

    public SpellsBeingCastInfo[,] spellsBeingCast;
    public Decider[] whoesOnFirst;

    public bool testingCurrentP1 = true;

    public float P1QTESpeed;
    public float P2QTESpeed;

    public CardsObjectPool cardPool;

    public bool player1FinishedQTE, player2FinishedQTE = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        hasDuelStarted = false;
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

        cardPool = new CardsObjectPool();
        spellsBeingCast = new SpellsBeingCastInfo[3, 2];
        spellsThatHaveBeenCast = new bool[2];
        whoesOnFirst = new Decider[3];
        roundCheck = false;
        currentCaster = null;

        player1IsHere = false;
        player2IsHere = false;

        StartLoadingLevel(ln_MainMenuName);
    }

    // Update is called once per frame
    void Update()
    {

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

        if (levelName == ln_Duelscene)
        {
            GameObject tempObj = GameObject.FindGameObjectWithTag("CardObjectPool");
            cardPool = tempObj.GetComponent<CardsObjectPool>();
            hasDuelStarted = true;
        }

        cameraAssigner.playerOneCamera.targetDisplay = 0;
        cameraAssigner.playerTwoCamera.targetDisplay = 1;

        ln_CurrentLevelName = levelName;

        yield return new WaitForSeconds(.5f);

        loadingScreen.SetLoadingScreenActive(false);

        isLoading = false;
    }

    public void ChangeCurrentCaster()
    {
        if (currentCaster == player1)
        {
            currentCaster = player2;
        }
        else if (currentCaster == player2)
        {
            currentCaster = player1;
        }
    }

}
