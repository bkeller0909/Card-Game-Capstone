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
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (playerNum == PlayerType.PLAYER2)
        //{
        //    GameManager.Instance.currentCaster = this;
        //}
        playerInput = GetComponent<PlayerInput>();

        spellHand = new SpellHand();

        entireHP = 28;
        //for (int i = 0; i < playerInput.devices.Count; i++)
        //{
        //    if (playerInput.devices[i] is Gamepad)
        //    {
        //        gamepad = (Gamepad)playerInput.devices[i];
        //        break;
        //    }
        //}
    }

    public void fillFingers()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //healthValue.text = entireHP.ToString();
    }

    public PlayerFingers GetRandomFinger()
    {
        int rand = 0;

        do
        {
            rand = UnityEngine.Random.Range(0, fingers.Count);
        } while (fingers[rand].fingerHP == 0);

        return fingers[rand].finger;
    }

    public PlayerFingers GetAdjacentFingerLeft(PlayerFingers selectedFinger)
    {
        int newFingerValue = (int)selectedFinger - 1;
        if (health.getFingerHealth((PlayerFingers)newFingerValue) != 0)
        {
            return (PlayerFingers)newFingerValue;
        }
        else
        {
            return PlayerFingers.none;
        }
    }

    public PlayerFingers GetAdjacentFingerRight(PlayerFingers selectedFinger)
    {
        int newFingerValue = (int)selectedFinger + 1;
        if (health.getFingerHealth((PlayerFingers)newFingerValue) != 0)
        {
            return (PlayerFingers)newFingerValue;
        }
        else
        {
            return PlayerFingers.none;
        }
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
