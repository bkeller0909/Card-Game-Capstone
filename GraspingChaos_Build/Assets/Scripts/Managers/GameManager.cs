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

    [Tooltip("This need to be the name of the actual game play scene")]
    public string ln_IntroScene;

    //Tracks wheather the game is in the loading procces
    private bool isLoading = false;

    [Tooltip("This is the visual loading screen")]
    public LoadingScreen loadingScreen;

    //how the cameras get assigned to there displayes
    private CameraAssigner cameraAssigner;
    #endregion // This ends the level loading setup

    [Tooltip("Are the QTE's race based")]
    public bool racedBasedQTE = false;

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

    [Tooltip("This is to show case the game")]
    public bool manualCards = false;

    [Tooltip("This is what round the game is on")]
    public int whatRound = 0;

    [HideInInspector, Tooltip("These are the current spells chosen to cast each round")]
    public SpellsBeingCastInfo[,] spellsBeingCast;
    [HideInInspector, Tooltip("Tracks whose spell is being played first")]
    public Decider[] whoesOnFirst;
    [HideInInspector, Tooltip("Timer for the particle")]
    public bool[] particleWait;

    [Tooltip("The speed that player 1 finished there QTE")]
    public float P1QTESpeed;
    [Tooltip("The speed that player 2 finished there QTE")]
    public float P2QTESpeed;

    [HideInInspector, Tooltip("The Object pool for each of the cards")]
    public CardsObjectPool cardPool;

    [HideInInspector, Tooltip("Checks if player 1 or 2 has finished the QTE")]
    public bool player1FinishedQTE, player2FinishedQTE = false;

    [HideInInspector, Tooltip("Checks if it is the time both players have entered the deciding state")]
    public bool firstRoundCheck = true;

    [HideInInspector, Tooltip("Makes sure both players are ready to move on from the loading into duel animation")]
    public bool moveOn = false;

    [HideInInspector, Tooltip("Check for if player 1's particle has finished casting")]
    public bool particleP1Done;
    [HideInInspector, Tooltip("Check for if player 2's particle has finished casting")]
    public bool particleP2Done;

    [HideInInspector, Tooltip("Check for stopping the player 1 long enough to play there particle")]
    public bool coroutineWaitP1;
    [HideInInspector, Tooltip("Check for stopping the player 2 long enough to play there particle")]
    public bool coroutineWaitP2;

    [HideInInspector, Tooltip("How many QTES did player 1 get correct")]
    public int raceTieP1;
    [HideInInspector, Tooltip("How many QTES did player 2 get correct")]
    public int raceTieP2;

    [HideInInspector, Tooltip("Temp Canvases for telling who one")]
    public GameObject tempEndScreenSkullWins1, tempEndScreenSkullWins2, tempEndScreenStagWins1, tempEndScreenStagWins2;

    [HideInInspector, Tooltip("Player 1's aura color")]
    public Color defaultColorP1;
    [HideInInspector, Tooltip("Player 1's aura color")]
    public Color defaultColorP2;

    //GameDebugger variable, testing only but important it is not removed yet
    public bool checkAvailableCards = false;
    [Tooltip("Safeguard for index timing")]
    public bool spellInProgress;

    public bool pickQTEType;

    public bool isDissolveDone;

    public int totalSpellsPickedP1;

    public int totalSpellsPickedP2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        hasDuelStarted = false;

        tempEndScreenSkullWins1.SetActive(false);
        tempEndScreenSkullWins2.SetActive(false);
        tempEndScreenStagWins1.SetActive(false);
        tempEndScreenStagWins2.SetActive(false);
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

        isDissolveDone = false;
        cardPool = new CardsObjectPool();
        spellsBeingCast = new SpellsBeingCastInfo[3, 2];
        spellsThatHaveBeenCast = new bool[2];
        whoesOnFirst = new Decider[3];
        roundCheck = false;
        currentCaster = null;
        moveOn = false;
        player1IsHere = false;
        player2IsHere = false;
        // currentDeselectedCard = 4;
        particleWait = new bool[3];
        particleP1Done = false;
        particleP2Done = false;
        coroutineWaitP1 = false;
        coroutineWaitP2 = false;
        StartLoadingLevel(ln_IntroScene);
        pickQTEType = true;
        spellInProgress = false;
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
            MusicManager.Instance.PlayMusic("GC_Game", MusicManager.Instance.gameMusic); // TODO - change this later
            GameObject tempObj = GameObject.FindGameObjectWithTag("CardObjectPool");
            cardPool = tempObj.GetComponent<CardsObjectPool>();
            hasDuelStarted = true;
        }
        else if (levelName == ln_MainMenuName)
        {
            MusicManager.Instance.PlayMusic("GC_MainMenu", MusicManager.Instance.mainMenuMusic);    // TODO - change this later
            tempEndScreenSkullWins1.SetActive(false);
            tempEndScreenSkullWins2.SetActive(false);
            tempEndScreenStagWins1.SetActive(false);
            tempEndScreenStagWins2.SetActive(false);
        }

        cameraAssigner.playerOneCamera.targetDisplay = 0;
        cameraAssigner.playerTwoCamera.targetDisplay = 1;

        ln_CurrentLevelName = levelName;

        yield return new WaitForSeconds(.5f);

        loadingScreen.SetLoadingScreenActive(false);

        isLoading = false;
    }

    /// <summary>
    /// Changes current caster to the opposite one that it is set to at the momment the function is called
    /// </summary>
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
