public class DealStatsState : FSMState
{
    CardDealing cardDealing;
    PlayerState playerState;
    bool gainedMana;

    //Constructor
    public DealStatsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.DealStats;
        cardDealing = new CardDealing();
    }

    public override void EnterStateInit()
    {
        cardDealing.InitializeCardCosts();
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
            if (player.spellHand.amtOfSpellsInHand < 5)
            {
                // add a card to the player spell list
                player.spellHand.playerSpells.Add(cardDealing.CardDealtChance(player));

                // increase the amount of spells the player has in their hand
                player.spellHand.amtOfSpellsInHand++;
            }

            //This should only be incresed by one each time a card is given
            //player.spellHand.amtOfSpellsInHand = 5;

            gainedMana = true;
        }

    }

}
