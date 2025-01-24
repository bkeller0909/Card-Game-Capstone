public class DealStatsState : FSMState
{
    PlayerState playerState;
    bool gainedMana;

    //Constructor
    public DealStatsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.DealStats;
    }

    public override void EnterStateInit()
    {
        gainedMana = false;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (player.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize &&
            enemy.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize)
        {
            playerState.PerformTransition(Transition.StatsGained);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!gainedMana)
        {
            player.Mana += GameManager.Instance.manaPerTurn;

            //This should only be incresed by one each time a card is given
            player.spellHand.amtOfSpellsInHand = 5;

            gainedMana = true;
        }

    }

}
