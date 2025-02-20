using UnityEngine;

public class PlayerControlHandler : MonoBehaviour
{
    PlayerManager player;
    SelectableFinger pickFinger;
    PlayerState stateHandler;
    private InputHandler playerInput;
    [SerializeField] CardHandSlot pickCards;
    [SerializeField] CameraPositionChange changeCameras;
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
        if (stateHandler.CurrentState.ID == FSMStateID.ChoosingSpells)
        {
            //Selecting Finger (Player action Map)
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
                //need function for setting or storing finger selected
                pickFinger.SelectFinger();
                stateHandler.FingerHasBeenSelected();
                player.playerInput.SwitchCurrentActionMap("Card");
                changeCameras.GetInputForced(1);
                playerInput.selectFinger = false;
                playerInput.Abtn = false;
                pickFinger.DeselectOnPick();
            }

            if (player.playerInput.actions["Cancel"].WasPressedThisFrame())
            {
                //deselect card
                player.playerInput.SwitchCurrentActionMap("Card");
                playerInput.cancelFingerSelect = false;
                playerInput.Bbtn = false;
            }

            //Selecting Card (Card action Map)
            if (player.playerInput.actions["NavCardLeft"].WasPressedThisFrame())
            {
                pickCards.MoveSelection(-1);
                playerInput.cardMoveLeft = false;
            }
            else if (player.playerInput.actions["NavCardRight"].WasPressedThisFrame())
            {
                pickCards.MoveSelection(1);
                playerInput.cardMoveRight = false;
            }

            if (player.playerInput.actions["Select"].WasPressedThisFrame())
            {
                pickCards.SelectedCard();
                if (ActiveSpellCards.Instance.spellCards[(int)pickCards.whatCard.spellName].manaCost <= player.Mana)
                {
                    if (pickCards.checkSelectedCardStatus())
                    {
                        stateHandler.CardHasBeenSelected();
                        //player.playerInput.SwitchCurrentActionMap("Player");
                        //changeCameras.GetInputForced(2);
                        playerInput.selectCard = false;
                        playerInput.Abtn = false;
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }

            if (player.playerInput.actions["Deselect"].WasPressedThisFrame())
            {
                pickCards.DeselectCard();
                if (pickCards.checkDeselectedCardStatus())
                {
                    stateHandler.CardHasBeenDeselected();
                    playerInput.deselectCard = false;
                    playerInput.Bbtn = false;
                }
                else
                {
                    //do nothing
                }
            }

            //if (playerInput.finishSelection)
            //{
            //    stateHandler.ReadyToCast();
            //    playerInput.finishSelection = false;
            //    index++;
            //}

            if (player.playerInput.actions["SetFinal"].WasPressedThisFrame())
            {
                stateHandler.ReadyToCast();
                playerInput.finishSelection = false;
                playerInput.Xbtn = false;
                foreach (CardSelect card in pickCards.selectedCards)
                {
                    player.cardsAmountSelected++;
                }
                index++;
                GameManager.Instance.firstRoundCheck = false;
            }


            //Camera Movement
            changeCameras.GetInput();

        }

        if (player.playerInput.actions["Pause"].triggered)
        {
            GameManager.Instance.StartLoadingLevel(GameManager.Instance.ln_MainMenuName);
        }
    }
}
