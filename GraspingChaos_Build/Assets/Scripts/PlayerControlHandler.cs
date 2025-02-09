using UnityEngine;

public class PlayerControlHandler : MonoBehaviour
{
    PlayerManager player;
    SelectableFinger pickFinger;
    PlayerState stateHandler;
    private InputHandler playerInput;
    [SerializeField] CardHandSlot pickCards;
    [SerializeField] CameraPositionChange changeCameras;

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
                if (player.playerInput.actions["FingerLeft"].triggered)
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                }
                else if (player.playerInput.actions["FingerRight"].triggered)
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                }
            }
            else if (!player.playerOneHands && player == GameManager.Instance.player1)
            {
                if (player.playerInput.actions["FingerLeft"].triggered)
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                }
                else if (player.playerInput.actions["FingerRight"].triggered)
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                }
            }
            else if (player.playerOneHands && player == GameManager.Instance.player2)
            {
                if (player.playerInput.actions["FingerLeft"].triggered)
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                }
                else if (player.playerInput.actions["FingerRight"].triggered)
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                }
            }
            else if (!player.playerOneHands && player == GameManager.Instance.player2)
            {
                if (player.playerInput.actions["FingerLeft"].triggered)
                {
                    pickFinger.MoveSelection(-1, 0, -1, 0);
                }
                else if (player.playerInput.actions["FingerRight"].triggered)
                {
                    pickFinger.MoveSelection(1, 0, 1, 0);
                }
            }

            if (player.playerInput.actions["SelectFinger"].triggered)
            {
                //need function for setting or storing finger selected
                player.playerInput.SwitchCurrentActionMap("Card");
            }

            if (player.playerInput.actions["Cancel"].triggered)
            {
                //deselect card
                player.playerInput.SwitchCurrentActionMap("Card");
            }

            //Selecting Card (Card action Map)
            if (player.playerInput.actions["NavCardLeft"].triggered)
            {
                pickCards.MoveSelection(1);
            }
            else if (player.playerInput.actions["NavCardRight"].triggered)
            {
                pickCards.MoveSelection(-1);
            }

            if (player.playerInput.actions["Select"].triggered)
            {
                pickCards.ToggleSelectedCard();
            }

            if (player.playerInput.actions["Deselect"].triggered)
            {
                pickCards.ToggleSelectedCard();
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
