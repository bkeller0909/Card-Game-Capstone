using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // The players health system
    [HideInInspector] public PlayerHealth health;

    //debug utilty value, just to reflect current health
    public TMP_Text healthValue;

    [Tooltip("The players hp as a whole")]
    public int entireHP;
    [Tooltip("The players mana value")]
    public int Mana;

    [Tooltip("What number of player they are")]
    [SerializeField] public PlayerType playerNum;
    [Tooltip("List of fingers which act as columns, each containing the finger joint game objects.")]
    public List<Fingers> fingers;

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
        playerInput = GetComponent<PlayerInput>();

        entireHP = 28;
        //fillFingers();
    }

    public void fillFingers()
    {
        //int index = 0;
        //foreach (GameObject finger in leftHand.fingersInHand)
        //{
        //    arrayOfFingers[index] = finger;
        //    index++;
        //}

        //foreach (GameObject finger in rightHand.fingersInHand)
        //{
        //    arrayOfFingers[index] = finger;
        //    index++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        healthValue.text = entireHP.ToString();
    }
}
