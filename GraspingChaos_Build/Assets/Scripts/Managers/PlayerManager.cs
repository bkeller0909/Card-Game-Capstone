using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Player Manager
//  Date Created: 01/07/2025
//  Purpose:      This is to manage everything that envolves a player and their stats
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class PlayerManager : MonoBehaviour
{
    // The players input system
    [HideInInspector] public PlayerInput playerInput;
    public Gamepad gamepad;
    public PlayerHealth health;

    //debug utilty value, just to reflect current health
    public TMP_Text healthValue;

    [Tooltip("The players hp as a whole")]
    public int entireHP;
    [Tooltip("The players mana value")]
    public int Mana;
    [Tooltip("The players Spell Hand")]
    public SpellHand spellHand;

    [Tooltip("What number of player they are")]
    [SerializeField] public PlayerType playerNum;
    [Tooltip("List of fingers which act as columns, each containing the finger joint game objects.")]
    public List<Fingers> fingers;

    [Tooltip("What number of player they are")]
    public List<VisualEffect> spellEffects;

    [Tooltip("damage tracker of each player per round")]
    public int DamageTrackedPerTurn;

    [Tooltip("check for curse convertion on dealing state")]
    public bool SucessfulCursedConversion;

    [Tooltip("Check for if the player played cursed Convertion")]
    public bool PlayedCursedConvertion = false;

    [Tooltip("mana tracker of each player per round")]
    public int ManaTrackedPerTurn;

    [Tooltip("Check for if the player played Echoing Mana")]
    public bool SucessfulEchoingMana;

    [Tooltip("Check for if the player played Echoing Mana")]
    public bool PlayedEchoingMana = false;

    public bool playerOneHands = false;

    public int cardsAmountSelected = 0;

    public int attackCardAmount = 0;
    public int restCardAmount = 0;
    public int ringCardAmount = 0;

    [Tooltip("The mana value that is shown to the players")]
    public ManaVisual manaVisual;

    public List<Fingers> healthyFingers;

    public CameraPositionChange playerCameras;

    private void Awake()
    {
        if (playerNum == PlayerType.PLAYER1)
        {
            GameManager.Instance.player1 = this;
        }
        else if (playerNum == PlayerType.PLAYER2)
        {
            GameManager.Instance.player2 = this;
        }
        healthyFingers = new List<Fingers>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gamepad = playerInput.GetDevice<Gamepad>();
        spellHand = new SpellHand();
        health = new PlayerHealth();
        health.SetUpHealth(this);

        entireHP = 28;

    }

    // Update is called once per frame
    void Update()
    {
        manaVisual.desiredManaStep = Mana;
    }

    public void GetMana(int manaAmount)
    {
        Mana += manaAmount;
        if (Mana > 12)
        {
            Mana = 12;
        }
    }

    public void LoseMana(int manaAmount)
    {
        Mana -= manaAmount;
        if (Mana < 0)
        {
            Mana = 0;
        }
    }

    public void TrackMana()
    {
        if (!PlayedEchoingMana)
        {
            //anything but sucess
            ManaTrackedPerTurn = ManaTrackedPerTurn / 2;
            Mana += ManaTrackedPerTurn;
            if (Mana > 12)
            {
                Mana = 12;
            }
        }
        else
        {
            //on success
            Mana += ManaTrackedPerTurn;
            if (Mana > 12)
            {
                Mana = 12;
            }
        }
    }

    public void TrackDamage()
    {
        if (!SucessfulCursedConversion)
        {
            //anything but success
            DamageTrackedPerTurn = DamageTrackedPerTurn / 2;
            Mana += DamageTrackedPerTurn;
            if (Mana > 12)
            {
                Mana = 12;
            }
        }
        else
        {
            //on success
            Mana += DamageTrackedPerTurn;
            if (Mana > 12)
            {
                Mana = 12;
            }
        }
    }

    public PlayerFingers GetRandomFinger()
    {
        int rand = 0;
        //rand = UnityEngine.Random.Range(0, fingers.Count);

        for (int i = 0; i < fingers.Count; i++)
        {
            if (health.playerHealthStats[i] != 0)
            {
                healthyFingers.Add(fingers[i]);
            }
        }

        if (healthyFingers.Count != 0)
        {
            rand = UnityEngine.Random.Range(0, healthyFingers.Count);
            return healthyFingers[rand].finger;
        }

        return PlayerFingers.none;
    }

    public PlayerFingers GetAdjacentFingerLeft(PlayerFingers selectedFinger)
    {
        int newFingerValue = (int)selectedFinger - 1;
        return (PlayerFingers)newFingerValue;
    }

    public PlayerFingers GetAdjacentFingerRight(PlayerFingers selectedFinger)
    {
        int newFingerValue = (int)selectedFinger + 1;
        return (PlayerFingers)newFingerValue;
    }

    public int GetLeftHandFingerDeath()
    {
        PlayerFingers currentFingers = PlayerFingers.LH_Pinky;
        int damageDealt = 0;
        for (int i = 0; i < 5; i++)
        {
            if (currentFingers == PlayerFingers.LH_Pinky)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Ring;
            }
            else if (currentFingers == PlayerFingers.LH_Ring)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Middle;
            }
            else if (currentFingers == PlayerFingers.LH_Middle)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Index;
            }
            else if (currentFingers == PlayerFingers.LH_Index)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Thumb;
            }
            else if (currentFingers == PlayerFingers.LH_Thumb)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
            }
        }
        return damageDealt;
    }

    public int GetRightHandFingerDeath()
    {
        PlayerFingers currentFingers = PlayerFingers.RH_Pinky;
        int damageDealt = 0;
        for (int i = 0; i < 5; i++)
        {
            if (currentFingers == PlayerFingers.RH_Pinky)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Ring;
            }
            else if (currentFingers == PlayerFingers.RH_Ring)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Middle;
            }
            else if (currentFingers == PlayerFingers.RH_Middle)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Index;
            }
            else if (currentFingers == PlayerFingers.RH_Index)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Thumb;
            }
            else if (currentFingers == PlayerFingers.RH_Thumb)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
            }
        }
        return damageDealt;
    }
}
