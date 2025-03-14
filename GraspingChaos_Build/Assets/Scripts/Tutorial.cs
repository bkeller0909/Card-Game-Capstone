using UnityEngine;

//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//  Title:          Tutorial
//  Date Created:   March 13, 2025
//  Instance:       No
//-----------------------------------------------------------------


/// <summary>
/// Handles the entire tutorial sequence at the start of the game.
/// 
/// This will be attached to the player.
/// </summary>
public class Tutorial : MonoBehaviour
{
    // bools for every step of the tutorial
    [HideInInspector] public bool isP1TutorialComplete;
    [HideInInspector] public bool isP2TutorialComplete;

    [HideInInspector] public bool isP1OnCards;
    [HideInInspector] public bool isP2OnCards;

    [HideInInspector] public bool isP1OnMana;
    [HideInInspector] public bool isP2OnMana;

    [HideInInspector] public bool isP1;
    [HideInInspector] public bool isP2;

    public void TutorialSequence()
    {
        // TODO - tutorial section

        // make unique action map for tutorial
        StartTutorial();


        // players load in from book    
        // they have no cards

        // dialogue will appear
        // force hands into up position while dialogue explains health

        // debugger shows the bones disappearing to show loss of health
        // force player to mana bottle view

        // they are instructed that they will be dealt 5 cards
        // performs the card dealing

        // force player to card camera view
        // dialogue explains cards

        // action map changed to card
        // card selecting and finger selected with dialogue explaining how to navigate

        // narrator says good luck when you ready up

        // QTE starts

        // goes to normal game
    }

    /// <summary>
    /// Setup everything for the start of the tutorial.
    /// </summary>
    private void StartTutorial(PlayerManager player)
    {
        player.playerInput.SwitchCurrentActionMap("Tutorial");

    }
}
