using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author: Sebastian
//  Co-Author: Keller (comments)
//  Title: PlayerControlHandler
//  Date Created: 02/11/2025
//  Instance: No
//-----------------------------------------------------------------

/// <summary>
/// Attached to the Player prefab. This reads all of the control inputs and sets all of the actions they perform.
/// </summary>
public class PlayerControlHandler : MonoBehaviour
{
    PlayerManager player;
    SelectableFinger pickFinger;
    PlayerState stateHandler;
    private InputHandler playerInput;
    [SerializeField] public CardHandSlot pickCards;
    [SerializeField] CameraPositionChange changeCameras;
    public bool deconfirm = false;
    public int index = 0;

    private void Start()
    {
        player = GetComponent<PlayerManager>();
        playerInput = GetComponent<InputHandler>();
        pickFinger = GetComponent<SelectableFinger>();
        stateHandler = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check that we are in the choosing spell state.
        // This is the state of the game where we are using all 
        // of the PLAYER and CARD controls
        if (stateHandler.CurrentState.ID == FSMStateID.ChoosingSpells)
        {
            #region Selecing Finger Controls
            if (player.playerOneHands && player == GameManager.Instance.player1)
            {
                if (player.playerInput.actions["FingerLeft"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                    playerInput.moveLeft = false;
                }
                else if (player.playerInput.actions["FingerRight"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                    playerInput.moveRight = false;
                }
            }
            else if (!player.playerOneHands && player == GameManager.Instance.player1)
            {
                if (player.playerInput.actions["FingerLeft"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                    playerInput.moveLeft = false;
                }
                else if (player.playerInput.actions["FingerRight"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                    playerInput.moveRight = false;
                }
            }
            else if (player.playerOneHands && player == GameManager.Instance.player2)
            {
                if (player.playerInput.actions["FingerLeft"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                    playerInput.moveLeft = false;
                }
                else if (player.playerInput.actions["FingerRight"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                    playerInput.moveRight = false;
                }
            }
            else if (!player.playerOneHands && player == GameManager.Instance.player2)
            {
                if (player.playerInput.actions["FingerLeft"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                    playerInput.moveLeft = false;
                }
                else if (player.playerInput.actions["FingerRight"].WasPressedThisFrame())
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                    playerInput.moveRight = false;
                }
            }

            if (player.playerInput.actions["SelectFinger"].WasPressedThisFrame())
            {
                // CONTROLS
                pickFinger.SelectFinger();                          // selects the finger
                stateHandler.FingerHasBeenSelected();               // changes state to the finger is selected
                player.playerInput.SwitchCurrentActionMap("Card");  // switches controls to card controls
                changeCameras.GetInputForced(0);                    // changes camera back to default
                playerInput.selectFinger = false;                   // no longer selecting a finger
                playerInput.Abtn = false;                           // idk
                pickFinger.DeselectOnPick();                        // highlight of the finger is removed once selected
            }

            if (player.playerInput.actions["Cancel"].WasPressedThisFrame())
            {

            }
            #endregion // Selecting Finger Controls

            #region Navigating Cards Controls
            // Navigate through your hand of cards to the left
            if (player.playerInput.actions["NavCardLeft"].WasPressedThisFrame())
            {
                pickCards.MoveSelection(-1);
                playerInput.cardMoveLeft = false;
            }
            // navigate to the right
            else if (player.playerInput.actions["NavCardRight"].WasPressedThisFrame())
            {
                pickCards.MoveSelection(1);
                playerInput.cardMoveRight = false;
            }

            // Selecting a card
            if (player.playerInput.actions["Select"].WasPressedThisFrame())
            {
                pickCards.SelectedCard();   // the card that is hovered is now selected
                if (ActiveSpellCards.Instance.spellCards[(int)pickCards.whatCard.spellName].manaCost <= player.Mana)
                {
                    if (pickCards.checkSelectedCardStatus())
                    {
                        stateHandler.CardHasBeenSelected();
                        playerInput.selectCard = false;
                        playerInput.Abtn = false;
                    }
                }
            }

            // Deselect the selected card
            if (player.playerInput.actions["Deselect"].WasPressedThisFrame())
            {
                if (pickCards.selectedCards.Count > 0)
                {
                    pickCards.DeselectCard();                   // sets the card from selected to hovered
                                                                //if (pickCards.checkDeselectedCardStatus())
                                                                //{
                    stateHandler.CardHasBeenDeselected();   // changes the state of the card to deselected
                    playerInput.deselectCard = false;       // deselect card button is now false after being pressed
                    playerInput.Bbtn = false;               // the button used is now false after being pressed
                }
            }
            #endregion // Card Select Controls


            //this if check makes sure that we can only "confirm" our cards if we have at least 1 card selected
            if (player.gameObject.GetComponentInChildren<CardHandSlot>().selectedCards.Count > 0)
            {
                // set your selection of cards
                if (player.playerInput.actions["SetFinal"].WasPressedThisFrame() && !deconfirm)
                {
                    stateHandler.ReadyToCast();             // changes the player state to Ready To Cast
                    changeCameras.GetInputForced(0);        // sets the camera position back to default
                    playerInput.finishSelection = false;    // finish selection input is now false after being pressed
                    playerInput.Xbtn = false;
                    foreach (CardSelect card in pickCards.selectedCards)
                    {
                        player.cardsAmountSelected++;
                    }
                    index++;
                    //QTEWait will make sure you wont move any cards or cameras after pressing confirm
                    deconfirm = true;
                    player.playerInput.SwitchCurrentActionMap("QTEWait");
                }
            }

            //Camera Movement
            changeCameras.GetInput();
        }

        // Once the pause button is pressed just load us back to the main menu
        if (player.playerInput.actions["Pause"].triggered)
        {
            CardsObjectPool.Instance.ResetPoolDistributionValues();
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
        }


        //lock Controls
        if (player.playerInput.actions["UnConfirm"].WasPressedThisFrame())
        {
            player.GetComponent<PlayerState>().readyToCast = false;
            playerInput.finishSelection = false;
            changeCameras.GetInputForced(1);
            player.cardsAmountSelected = 0;
            player.playerInput.SwitchCurrentActionMap("Card");
            Debug.Log(player.playerInput.currentActionMap.ToString());
            if (player == GameManager.Instance.player1)
            {
                {
                    GameManager.Instance.nextStateP1 = false;
                    player.GetComponent<PlayerState>().finishedCastingImage.SetActive(false);
                }
            }
            else if (player == GameManager.Instance.player2)
            {
                {
                    GameManager.Instance.nextStateP2 = false;
                    player.GetComponent<PlayerState>().finishedCastingImage.SetActive(false);
                }
            }

            StartCoroutine(ButtonCheckUnConfirm());
        }

        // Kill Switch for killing player 2
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameManager.Instance.player2.entireHP = 0;
        }
    }


    IEnumerator ButtonCheckUnConfirm()
    {
        yield return new WaitForSeconds(1);
        deconfirm = false;
    }
}
