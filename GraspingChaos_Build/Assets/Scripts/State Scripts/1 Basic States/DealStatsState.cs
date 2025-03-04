//----------------------------------------------------------------
//  Author:       Wyatt
//  Title:        DealStats
//  Date Created: 01/30/2025
//  Instance:     No
//-----------------------------------------------------------------

using System;
/// <summary>
/// The State in which all the stats are assigned to the player at the start of each round
/// </summary>
public class DealStatsState : FSMState
{
    /// <summary>
    /// The Randomizer card dealing script
    /// </summary>
    CardDealing cardDealing;

    /// <summary>
    /// The card object pool script
    /// </summary>
    CardsObjectPool cardObjPool;

    /// <summary>
    /// The state handler script
    /// </summary>
    PlayerState playerState;

    /// <summary>
    /// The base spell card script
    /// </summary>
    SpellCard card;

    /// <summary>
    /// A Bool to make sure they only gain mana once per a round
    /// </summary>
    bool gainedMana;

    /// <summary>
    /// A check for if each player can change there states
    /// </summary>
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
        if(GameManager.Instance.pickQTEType)
        {
            //int rand = UnityEngine.Random.Range(0, 2);
            int rand = 0;
            if(rand == 0)
            {
                player.gameObject.GetComponent<QTEHandler>().mashing = false;
                enemy.gameObject.GetComponent<QTEHandler>().mashing = false;
            }
            else if(rand == 1)
            {
                player.gameObject.GetComponent<QTEHandler>().mashing = true;
                enemy.gameObject.GetComponent<QTEHandler>().mashing = true;
            }
            GameManager.Instance.pickQTEType = false;
        }

        // If the player has gained mana skip this bit if not go in
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

        // If the game is scripted skip this if it isint go in
        if (!GameManager.Instance.manualCards) // the not scripted game
        {
            if (!GameManager.Instance.firstRoundCheck)
            {
                //player.spellHand.amtOfSpellsInHand = 5 - player.cardsAmountSelected;
                if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                {
                    // add a card to the player spell list
                    card = cardDealing.CardDealtChance(player);
                    if (CardsObjectPool.Instance.allcardAmounts[(int)card.spellName] < 3)
                    {
                        // TODO - change this value to 2 once rings are in the game
                        if (player.attackCardAmount < 3 && card.type == SpellType.ATTACK)
                        {
                            player.spellHand.playerSpells.Add(card);
                            CardsObjectPool.Instance.SetCardsFromPool(player, card);
                            // increase the amount of spells the player has in their hand
                            player.spellHand.amtOfSpellsInHand++;
                            player.attackCardAmount++;
                        }
                        else if (player.restCardAmount < 3 && card.type == SpellType.RESTORATION)
                        {
                            player.spellHand.playerSpells.Add(card);
                            CardsObjectPool.Instance.SetCardsFromPool(player, card);
                            // increase the amount of spells the player has in their hand
                            player.spellHand.amtOfSpellsInHand++;
                            player.restCardAmount++;
                        }
                        else if (player.ringCardAmount < 2 && card.type == SpellType.RING)
                        {
                            player.spellHand.playerSpells.Add(card);
                            CardsObjectPool.Instance.SetCardsFromPool(player, card);
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
                    if (CardsObjectPool.Instance.allcardAmounts[(int)card.spellName] < 3) // convert enum name to int
                    {
                        // TODO - value 2
                        if (player.attackCardAmount < 3 && card.type == SpellType.ATTACK)
                        {
                            player.spellHand.playerSpells.Add(card);
                            CardsObjectPool.Instance.SetCardsFromPool(player, card);
                            // increase the amount of spells the player has in their hand
                            player.spellHand.amtOfSpellsInHand++;
                            player.attackCardAmount++;
                        }
                        else if (player.restCardAmount < 3 && card.type == SpellType.RESTORATION)
                        {
                            player.spellHand.playerSpells.Add(card);
                            CardsObjectPool.Instance.SetCardsFromPool(player, card);
                            // increase the amount of spells the player has in their hand
                            player.spellHand.amtOfSpellsInHand++;
                            player.restCardAmount++;
                        }
                        else if (player.ringCardAmount < 2 && card.type == SpellType.RING)
                        {
                            player.spellHand.playerSpells.Add(card);
                            CardsObjectPool.Instance.SetCardsFromPool(player, card);
                            // increase the amount of spells the player has in their hand
                            player.spellHand.amtOfSpellsInHand++;
                            player.ringCardAmount++;
                        }
                    }
                }
            }
        }
        else // the scripted game
        {
            //This is the first round of the game
            if (GameManager.Instance.whatRound == 0)
            {
                if (player == GameManager.Instance.player1)
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ForTheCause];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                else
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.QuickHeal];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.ThumbsUp];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
            }
            else if (GameManager.Instance.whatRound == 1)
            {
                if (player == GameManager.Instance.player1)
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.TidalWave];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Quake];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                else
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.StaticBlast];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.FireBolt];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
            }
            else if (GameManager.Instance.whatRound == 2)
            {
                if (player == GameManager.Instance.player1)
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.QuickHeal];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.TidalWave];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                else
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LefteousEnvy];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
            }
            else if (GameManager.Instance.whatRound == 3)
            {
                if (player == GameManager.Instance.player1)
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.QuickHeal];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                else
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.CollectorsCurse];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Icicles];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
            }
            else if (GameManager.Instance.whatRound == 4)
            {
                if (player == GameManager.Instance.player1)
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Materialise];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                else
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Materialise];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.StaticBlast];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
            }
            else if (GameManager.Instance.whatRound == 5)
            {
                if (player == GameManager.Instance.player1)
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.CursedConversion];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                else
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.EchoingMana];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.restCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
            }
            else if (GameManager.Instance.whatRound == 6)
            {
                if (player == GameManager.Instance.player1)
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.Rockthrow];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.RightingBolt];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                else
                {
                    if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
                    {
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.PointerOfDeath];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                        card = ActiveSpellCards.Instance.spellCards[(int)SpellNames.LeftningBolt];
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        player.attackCardAmount++;
                        player.spellHand.amtOfSpellsInHand++;
                    }
                }
                GameManager.Instance.manualCards = false;
            }
        }

        //Once both players have been delt enough cards so they each have 5, the game moves on
        if (player.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize &&
            enemy.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize)
        {
            stateChange = true;
            GameManager.Instance.checkAvailableCards = true;
        }
    }

}
