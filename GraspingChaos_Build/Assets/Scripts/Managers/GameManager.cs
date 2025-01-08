using UnityEngine;

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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
