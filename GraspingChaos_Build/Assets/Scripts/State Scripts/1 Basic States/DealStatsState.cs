//----------------------------------------------------------------
//  Author:       Wyatt
//  Title:        DealStats
//  Date Created: 01/30/2025
//  Instance:     No
//-----------------------------------------------------------------

using UnityEngine.Playables;

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

    Tutorial tutorial;

    /// <summary>
    /// A Bool to make sure they only gain mana once per a round
    /// </summary>
    bool gainedMana;

    /// <summary>
    /// A check for if each player can change there states
    /// </summary>
    bool stateChange = false;

    bool firstDealingP1 = true;
    bool firstDealingP2 = true;

    //Constructor
    public DealStatsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.DealStats;
        cardDealing = new CardDealing();
        tutorial = new Tutorial();
        cardObjPool = GameManager.Instance.cardPool;
    }

    public override void EnterStateInit()
    {
        cardObjPool = GameManager.Instance.cardPool;
        cardDealing.InitializeCardCosts();  // init the cards into lists by mana cost
        cardDealing.InitializeCardTypes();  // init the cards into lists by type
        gainedMana = false;
        stateChange = false;
        playerState.playerHand.KeepCardPos();

    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (stateChange)
        {
            player.playerCameras.GetInputForced(0);
            GameManager.Instance.isDissolveDone = false;
            stateChange = false;
            player.cardsAmountSelected = 0;
            playerState.PerformTransition(Transition.StatsGained);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        player.ringHandler.vengfulFirst = true;
        QTETSelection(player, enemy);

        ManaDealing(player, enemy);

        // If the game is scripted skip this if it isint go in
        if (!GameManager.Instance.manualCards) // the not scripted game
        {
            FireBoltFirstRound(player);
            CardDealingperTurn(player);
        }
        else // the scripted game
        {
            // ScriptedGame(player);
            ScriptedTutorial(player);
        }

        //Once both players have been delt enough cards so they each have 5, the game moves on
        if (player.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize &&
            enemy.spellHand.amtOfSpellsInHand == GameManager.Instance.maxPlayerHandSize)
        {
            stateChange = true;
            GameManager.Instance.checkAvailableCards = true;
        }
    }

    private void FireBoltFirstRound(PlayerManager player)
    {
        if (player.spellHand.amtOfSpellsInHand < 5)
        {
            if (GameManager.Instance.whatRound == 0)
            {
                if (firstDealingP1 && player == GameManager.Instance.player1)
                {
                    card = CardsObjectPool.Instance.FireBoltRound1(player);
                    player.spellHand.playerSpells.Add(card);
                    firstDealingP1 = false;
                    player.attackCardAmount++;
                    player.spellHand.amtOfSpellsInHand++;
                }

                if (firstDealingP2 && player == GameManager.Instance.player2)
                {
                    card = CardsObjectPool.Instance.FireBoltRound1(player);
                    player.spellHand.playerSpells.Add(card);
                    firstDealingP2 = false;
                    player.attackCardAmount++;
                    player.spellHand.amtOfSpellsInHand++;
                }
            }
        }
    }

    private void CardDealingperTurn(PlayerManager player)
    {
        //player.spellHand.amtOfSpellsInHand = 5 - player.cardsAmountSelected;
        if (player.spellHand.amtOfSpellsInHand < 5)  //player.cardsAmountSelected
        {
            // add a card to the player spell list
            card = cardDealing.CardDealtChance(player);
            if (CardsObjectPool.Instance.allcardAmounts[(int)card.spellName] < 3)
            {
                // done - TODO - change this value to 2 once rings are in the game
                if (player.attackCardAmount < 6 && card.type == SpellType.ATTACK)
                {
                    player.spellHand.playerSpells.Add(card);
                    CardsObjectPool.Instance.SetCardsFromPool(player, card);
                    // increase the amount of spells the player has in their hand
                    player.spellHand.amtOfSpellsInHand++;
                    player.attackCardAmount++;
                }
                else if (player.restCardAmount < 2 && card.type == SpellType.RESTORATION)
                {
                    player.spellHand.playerSpells.Add(card);
                    CardsObjectPool.Instance.SetCardsFromPool(player, card);
                    // increase the amount of spells the player has in their hand
                    player.spellHand.amtOfSpellsInHand++;
                    player.restCardAmount++;
                }
                else if (player.ringCardAmount < 2 && card.type == SpellType.RING)
                {
                    if (CheckRingInHand(player, card) && CheckRingInFingers(player))
                    {
                        player.spellHand.playerSpells.Add(card);
                        CardsObjectPool.Instance.SetCardsFromPool(player, card);
                        // increase the amount of spells the player has in their hand
                        player.spellHand.amtOfSpellsInHand++;
                        player.ringCardAmount++;
                    }
                    else
                    {
                        //loop again
                    }
                }

            }
        }
    }

    private void ManaDealing(PlayerManager player, PlayerManager enemy)
    {
        // If the player has gained mana skip this bit if not go in
        if (!gainedMana)
        {
            player.eyes.Stop();
            //player.Mana += GameManager.Instance.manaPerTurn;
            player.GetMana(GameManager.Instance.manaPerTurn);
            SoundFXManager.Instance.PlaySoundFX(SoundFXManager.Instance.manaRefill, 1);
            gainedMana = true;
            player.GetComponent<RingsHandler>().EffectManaMerchant();
            if (player.PlayedCursedConvertion)
            {
                player.PlayedCursedConvertion = false;
                player.TrackDamage();

                //This checks if the player has the finger bonus for rockthrow
                if (player.AreTheseFingersAlive(PlayerFingers.LH_Ring, PlayerFingers.RH_Ring) &&
                    (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.LH_Ring] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.LH_Ring] != true) &&
                    (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)PlayerFingers.RH_Ring] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)PlayerFingers.RH_Ring] != true))
                {
                    if (player.Mana < 11)
                    {
                        player.Mana += 2;
                    }
                }
            }
            player.DamageTrackedPerTurn = 0;
            if (player.PlayedEchoingMana)
            {
                player.PlayedEchoingMana = false;
                player.Mana += enemy.TrackMana();
                if (player.Mana >= 12)
                {
                    player.Mana = 12;
                }
            }
            player.ManaTrackedPerTurn = 0;
        }
    }

    private void QTETSelection(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.pickQTEType)
        {
            //int rand = UnityEngine.Random.Range(0, 2);
            int rand = 0;
            if (rand == 0)
            {
                player.gameObject.GetComponent<QTEHandler>().mashing = false;
                enemy.gameObject.GetComponent<QTEHandler>().mashing = false;
            }
            else if (rand == 1)
            {
                player.gameObject.GetComponent<QTEHandler>().mashing = true;
                enemy.gameObject.GetComponent<QTEHandler>().mashing = true;
            }
            GameManager.Instance.pickQTEType = false;
        }
    }

    private bool CheckRingInHand(PlayerManager currentPlayer, SpellCard card)
    {
        for (int i = 0; i < currentPlayer.gameObject.GetComponentInChildren<CardHandSlot>().cards.Count; i++)
        {
            if (currentPlayer.gameObject.GetComponentInChildren<CardHandSlot>().cards[i].gameObject.GetComponent<SpellCard>().spellName == card.spellName)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckRingInFingers(PlayerManager currentPlayer)
    {
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //if this ring is active on a finger and it is the ring that matches with the card
                if (currentPlayer.ringHandler.ringsActive[i, j] == true)
                {
                    if (i == (int)Rings.ThornsOfAgonyFail || i == (int)Rings.ThornsOfAgonyFull)
                    {
                        return false;
                    }
                    else if (i == (int)Rings.GuardiansTouchFail || i == (int)Rings.GuardiansTouchFull)
                    {
                        return false;
                    }
                    else if (i == (int)Rings.ManaMerchantFail || i == (int)Rings.ManaMerchantFull)
                    {
                        return false;
                    }
                    else if (i == (int)Rings.VengefulMirrorFail || i == (int)Rings.VengefulMirrorFull)
                    {
                        return false;
                    }
                    else if (i == (int)Rings.SpectralChainFail || i == (int)Rings.SpectralChainFull)
                    {
                        return false;
                    }
                    else if (i == (int)Rings.VampiricSurgeFail || i == (int)Rings.VampiricSurgeFull)
                    {
                        return false;
                    }
                    else if (i == (int)Rings.VeilOfFortitudeFail || i == (int)Rings.VeilOfFortitudeFull)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private void ScriptedGame(PlayerManager player)
    {
        //This is the first round of the game
        if (GameManager.Instance.whatRound == 0)
        {
            if (player.spellHand.amtOfSpellsInHand < 5)
            {
                if (player == GameManager.Instance.player1)
                {
                    // card hand
                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.FireBolt);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.FireBolt);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.ForTheCause);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.Icicles);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.Rockthrow);
                    player.spellHand.playerSpells.Add(card);
                }
                else if (player == GameManager.Instance.player2)
                {
                    // card hand
                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.Icicles);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.FireBolt);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.ForTheCause);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.Icicles);
                    player.spellHand.playerSpells.Add(card);

                    card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.Rockthrow);
                    player.spellHand.playerSpells.Add(card);
                }
            }
        }
        else
        {
            GameManager.Instance.manualCards = false;
        }
    }

    public void ScriptedTutorial(PlayerManager player)
    {
        if (GameManager.Instance.whatRound == 0)
        {
            if(player.spellHand.amtOfSpellsInHand < 5)
            {
                if(player == GameManager.Instance.player1)
                {
                    // TODO - tutorial section

                    // SETUP
                    // make unique action map for tutorial
                    //playerState.tutorialEvent.SetupTutorial(player);

                    // STEP 1
                    // players load in from book
                    // they have no cards
                    // dialogue will appear
                    if(playerState.tutorialEvent.isStep1 == true)
                    {
                        playerState.tutorialEvent.TutorialStep1(player);
                    }

                    // STEP 2
                    // force hands into up position while dialogue explains health
                    if(playerState.tutorialEvent.isStep1Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep2(player);
                        playerState.tutorialEvent.isStep1Complete = false;
                    }
                    // move the player hands back down
                    if (playerState.tutorialEvent.isStep2Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep3(player);
                        playerState.tutorialEvent.isStep2Complete = false;
                    }

                    // force player to mana bottle view
                    if(playerState.tutorialEvent.isStep3Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep4(player);
                        playerState.tutorialEvent.isStep3Complete = false;
                    }
                    // they are instructed that they will be dealt 5 cards
                
                    // performs the card dealing
                    if(playerState.tutorialEvent.isStep4Complete == true)
                    {
                        if(player.spellHand.amtOfSpellsInHand < 3)
                        {
                            card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.FireBolt);
                            player.spellHand.playerSpells.Add(card);

                            card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.QuickHeal);
                            player.spellHand.playerSpells.Add(card);

                            card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.GuardiansTouch);
                            player.spellHand.playerSpells.Add(card);
                        }
                    }

                    // force player to card camera view
                    // dialogue explains cards
                    if(playerState.tutorialEvent.isStep4Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep5(player);
                        playerState.tutorialEvent.isStep4Complete = false;
                    }

                    if (playerState.tutorialEvent.isStep5Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep6(player);
                        playerState.tutorialEvent.isStep5Complete = false;
                    }

                    if (playerState.tutorialEvent.isStep6Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep7(player);
                        playerState.tutorialEvent.isStep6Complete = false;
                    }

                    if (playerState.tutorialEvent.isStep7Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep8(player);
                        playerState.tutorialEvent.isStep7Complete = false;
                    }

                    if (playerState.tutorialEvent.isStep8Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep9(player);
                        playerState.tutorialEvent.isStep8Complete = false;
                    }

                    if (playerState.tutorialEvent.isStep9Complete == true)
                    {
                        playerState.tutorialEvent.TutorialStep10(player);
                        playerState.tutorialEvent.isStep9Complete = false;
                    }

                    // action map changed to card
                    // card selecting and finger selected with dialogue explaining how to navigate
                    if (playerState.tutorialEvent.isStep10Complete == true)
                    {
                        card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.Icicles);
                        player.spellHand.playerSpells.Add(card);

                        card = CardsObjectPool.Instance.ScriptedDealing(player, SpellNames.FireBolt);
                        player.spellHand.playerSpells.Add(card);
                    }

                    // narrator says good luck when you ready up

                    // QTE starts

                    // goes to normal game
                }
            }
        }
        else
        {
            GameManager.Instance.manualCards = false;
        }
    }
}
