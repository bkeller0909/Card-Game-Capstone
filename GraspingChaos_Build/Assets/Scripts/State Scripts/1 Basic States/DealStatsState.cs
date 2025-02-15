public class DealStatsState : FSMState
{
    CardDealing cardDealing;
    CardsObjectPool cardObjPool;
    PlayerState playerState;
    SpellCard card;
    bool gainedMana;
    bool stateChange = false;

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
        cardDealing.InitializeCardCosts();  // init the cards into lists by mana cost
        cardDealing.InitializeCardTypes();  // init the cards into lists by type
        gainedMana = false;
        stateChange = false;
        if (!GameManager.Instance.firstRoundCheck)
        {
            playerState.playerHand.KeepCardPos();
        }

    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (stateChange)
        {
            stateChange = false;
            player.cardsAmountSelected = 0;
            playerState.PerformTransition(Transition.StatsGained);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (!gainedMana)
        {
            //player.Mana += GameManager.Instance.manaPerTurn;
            player.GetMana(GameManager.Instance.manaPerTurn);
            gainedMana = true;
            if (player.PlayedCursedConvertion)
            {
                player.PlayedCursedConvertion = false;
                player.TrackDamage();
            }
        }

        if (!GameManager.Instance.firstRoundCheck)
        {
            //player.spellHand.amtOfSpellsInHand = 5 - player.cardsAmountSelected;
            if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
            {
                // add a card to the player spell list
                card = cardDealing.CardDealtChance(player);
                if (cardObjPool.allcardAmounts[(int)card.spellName] < 3)
                {
                    // TODO - change this value to 2 once rings are in the game
                    if (player.attackCardAmount < 3 && card.type == SpellType.ATTACK)
                    {
                        player.spellHand.playerSpells.Add(card);
                        cardObjPool.SetCardsFromPool(player, card);
                        // increase the amount of spells the player has in their hand
                        player.spellHand.amtOfSpellsInHand++;
                        player.attackCardAmount++;
                    }
                    else if (player.restCardAmount < 3 && card.type == SpellType.RESTORATION)
                    {
                        player.spellHand.playerSpells.Add(card);
                        cardObjPool.SetCardsFromPool(player, card);
                        // increase the amount of spells the player has in their hand
                        player.spellHand.amtOfSpellsInHand++;
                        player.restCardAmount++;
                    }
                    else if (player.ringCardAmount < 2 && card.type == SpellType.RING)
                    {
                        player.spellHand.playerSpells.Add(card);
                        cardObjPool.SetCardsFromPool(player, card);
                        // increase the amount of spells the player has in their hand
                        player.spellHand.amtOfSpellsInHand++;
                        player.ringCardAmount++;

                    }

                }
            }
        }

        // for some reason this needs to be here a second time or else game breaks!!! :P
        if (GameManager.Instance.firstRoundCheck)
        {
            if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
            {
                // add a card to the player spell list
                card = cardDealing.CardDealtChance(player);
                if (cardObjPool.allcardAmounts[(int)card.spellName] < 3) // convert enum name to int
                {
                    // TODO - value 2
                    if (player.attackCardAmount < 3 && card.type == SpellType.ATTACK)
                    {
                        player.spellHand.playerSpells.Add(card);
                        cardObjPool.SetCardsFromPool(player, card);
                        // increase the amount of spells the player has in their hand
                        player.spellHand.amtOfSpellsInHand++;
                        player.attackCardAmount++;
                    }
                    else if (player.restCardAmount < 3 && card.type == SpellType.RESTORATION)
                    {
                        player.spellHand.playerSpells.Add(card);
                        cardObjPool.SetCardsFromPool(player, card);
                        // increase the amount of spells the player has in their hand
                        player.spellHand.amtOfSpellsInHand++;
                        player.restCardAmount++;
                    }
                    else if (player.ringCardAmount < 2 && card.type == SpellType.RING)
                    {
                        player.spellHand.playerSpells.Add(card);
                        cardObjPool.SetCardsFromPool(player, card);
                        // increase the amount of spells the player has in their hand
                        player.spellHand.amtOfSpellsInHand++;
                        player.ringCardAmount++;
                    }
                }
            }
        }

        if (player.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize &&
            enemy.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize)
        {
            stateChange = true;
            GameManager.Instance.checkAvailableCards = true;
        }
    }

}
