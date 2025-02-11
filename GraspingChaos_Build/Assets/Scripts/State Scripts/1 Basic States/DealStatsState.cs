public class DealStatsState : FSMState
{
    CardDealing cardDealing;
    CardsObjectPool cardObjPool;
    PlayerState playerState;
    SpellCard card;
    bool gainedMana;

    //Constructor
    public DealStatsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.DealStats;
        cardDealing = new CardDealing();
        cardObjPool = GameManager.Instance.cardPool;
    }

    public override void EnterStateInit()
    {
        cardObjPool = GameManager.Instance.cardPool;
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
            gainedMana = true;
            if (player.PlayedCursedConvertion)
            {
                player.PlayedCursedConvertion = false;
                player.TrackDamage();
            }
        }
        if (player.spellHand.amtOfSpellsInHand < 5) //player.cardsAmountSelected
        {
            // add a card to the player spell list
            card = cardDealing.CardDealtChance(player);
            if (cardObjPool.allcardAmounts[(int)card.spellName] < 3)
            {
                player.spellHand.playerSpells.Add(card);
                cardObjPool.SetCardsFromPool(player, card);
                // increase the amount of spells the player has in their hand
                player.spellHand.amtOfSpellsInHand++;

            }
        }
    }

}
