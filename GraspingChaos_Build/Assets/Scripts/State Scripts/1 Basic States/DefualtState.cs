using UnityEngine;

public class DefualtState : FSMState
{
    PlayerState playerState;
    CardsObjectPool cardPool;
    CameraPositionChange posChange;
    bool animPlayed;

    //Constructor
    public DefualtState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Defualt;
        posChange = playerState.playerCam.gameObject.GetComponent<CameraPositionChange>();
        animPlayed = false;
        GameManager.Instance.moveOn = false;
        GameManager.Instance.whatRound = 0;
        GameManager.Instance.manaPerTurn = 3;

        if (playerState == GameManager.Instance.player1)
        {
            foreach (GameObject card in cardPool.objPoolCards)
            {
                card.transform.position = GameManager.Instance.player1.cardSpawnTransform.position;
            }
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.moveOn)
        {
            playerState.camAnim.enabled = false;
            playerState.PerformTransition(Transition.Start);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.hasDuelStarted && !animPlayed)
        {
            if (player = GameManager.Instance.player1)
            {
                playerState.camAnim.Play("LoadingIntoDuel");
            }
            else
            {
                playerState.camAnim.Play("LoadingIntoDuel2");
            }
            animPlayed = true;
        }
        else
        {
            if (posChange.weHaveArrived)
            {
                GameManager.Instance.moveOn = true;
            }
        }


    }

}
