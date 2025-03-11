//----------------------------------------------------------------
//  Author:       Wyatt
//  Title:        ChoosingSpellState
//  Date Created: 01/30/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// The State is where the player chooses what type spells they have chosen
/// </summary>
public class ChoosingSpellsState : FSMState
{
    /// <summary>
    /// The state handler
    /// </summary>
    PlayerState playerState;

    /// <summary>
    /// Checks if spells have been added to the casting spell list, so they don't get added for more than once
    /// </summary>
    bool hasAddedSpells;

    /// <summary>
    /// What spells have been chosen during this state
    /// </summary>
    public SpellNames[] spellsChosen;

    /// <summary>
    /// What Fingers have been chosen during this state
    /// </summary>
    public PlayerFingers[] fingersChosen;

    /// <summary>
    /// The amount of spells chose for this player
    /// </summary>
    int amtOfSpells;

    //Constructor
    public ChoosingSpellsState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.ChoosingSpells;
        spellsChosen = new SpellNames[3];
        fingersChosen = new PlayerFingers[3];
    }

    public override void EnterStateInit()
    {
        //If player one has entered this state then increase the amount of mana gained per a turn and what round it is
        if (playerState.player == GameManager.Instance.player1)
        {
            // TODO - change this later when audio stuff is fixed
            // MusicManager.Instance.MusicLowHealth();     // if any player's health is 10 or lower play intense music
            GameManager.Instance.whatRound++;
            GameManager.Instance.manaPerTurn++;
        }

        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (playerState.player.ringHandler.ringsActive[i, j] == true)
                {
                    if (playerState.player.ringHandler.ringStartRound[i] + playerState.player.ringHandler.defualtRingLength[i] == GameManager.Instance.whatRound)
                    {
                        playerState.player.ringHandler.ringsActive[i, j] = false;
                        playerState.player.ToggleRing(false, (Rings)i, (PlayerFingers)j);
                        break;
                    }
                }
            }
        }

        //resets the last selected card and sets the currently sellected card to the first one in the players hand
        playerState.player.gameObject.GetComponent<PlayerControlHandler>().pickCards.SetHoveredCard(0);

        amtOfSpells = 0;

        // Resets the last chosen spells to nothing
        for (int i = 0; i < 3; i++)
        {
            spellsChosen[i] = SpellNames.none;
            fingersChosen[i] = PlayerFingers.none;
        }

        //The spell info for storage location tracks everything needed to know about a chosen spell
        SpellsBeingCastInfo spellInfo = new SpellsBeingCastInfo();

        for (int w = 0; w < 3; w++)
        {
            for (int y = 0; y < 2; y++)
            {
                spellInfo.whoIsCasting = playerState.player;
                spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.none];
                spellInfo.whatFinger = PlayerFingers.none;
                spellInfo.whoIsBeingCastedOn = playerState.player;
                GameManager.Instance.spellsBeingCast[w, y] = spellInfo;
            }
        }

        GameManager.Instance.nextStateP1 = false;
        GameManager.Instance.nextStateP2 = false;

        hasAddedSpells = false;
        GameManager.Instance.amtOfSpellsBeingCast = 0;

        playerState.player.playerInput.SwitchCurrentActionMap("Card");
        GameManager.Instance.pickQTEType = true;
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        //Once both players are ready to move to the next state this if is entered
        if (GameManager.Instance.nextStateP1 && GameManager.Instance.nextStateP2)
        {
            //If spells haven't been added yet go into this
            if (!hasAddedSpells)
            {
                // Checks if the player is player 1 or 2
                int whoAmI = -100;
                if (player == GameManager.Instance.player1)
                {
                    whoAmI = (int)PlayerType.PLAYER1;
                }
                else
                {
                    whoAmI = (int)PlayerType.PLAYER2;
                }

                // Loops through each of the chosen spells and adds the info of each spell to the spells being cast 
                for (int i = 0; i < spellsChosen.Length; i++)
                {
                    //Moving each spell that is beings casted info over to the GameManager to move to the desicion state
                    SpellsBeingCastInfo spellInfo = new SpellsBeingCastInfo();

                    // Checks the order of spells chosen
                    if (i == 0)
                    {
                        spellInfo.numOfSpell = 1;
                    }
                    else if (i == 1)
                    {
                        spellInfo.numOfSpell = 2;
                    }
                    else if (i == 2)
                    {
                        spellInfo.numOfSpell = 3;
                    }

                    // If an actual spell has been chosen entered here
                    if (spellsChosen[i] != SpellNames.none)
                    {
                        spellInfo.whoIsCasting = playerState.player;
                        spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)spellsChosen[i]];
                        spellInfo.whatFinger = fingersChosen[i];

                        // If you  have to target the oposing players fingers
                        if (spellsChosen[i] == SpellNames.FireBolt || spellsChosen[i] == SpellNames.Rockthrow || spellsChosen[i] == SpellNames.RighteousEnvy ||
                            spellsChosen[i] == SpellNames.LefteousEnvy || spellsChosen[i] == SpellNames.Icicles || spellsChosen[i] == SpellNames.CollectorsCurse
                            || spellsChosen[i] == SpellNames.StaticBlast || spellsChosen[i] == SpellNames.Quake || spellsChosen[i] == SpellNames.RightingBolt ||
                               spellsChosen[i] == SpellNames.LeftningBolt || spellsChosen[i] == SpellNames.TidalWave || spellsChosen[i] == SpellNames.PointerOfDeath
                               || spellsChosen[i] == SpellNames.SpectralChain || spellsChosen[i] == SpellNames.VampiricSurge)
                        {
                            spellInfo.whoIsBeingCastedOn = playerState.enemy;
                        }
                        else // if you have to target your chosen fingers
                        {
                            spellInfo.whoIsBeingCastedOn = playerState.player;
                        }

                        //Fills the spells being cast with the info above
                        GameManager.Instance.spellsBeingCast[i, whoAmI] = spellInfo;
                    }
                    else // Fills the spells being cast with blank info if no spells were chosen
                    {
                        spellInfo.whoIsCasting = playerState.player;
                        spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)SpellNames.none];
                        spellInfo.whatFinger = PlayerFingers.none;
                        spellInfo.whoIsBeingCastedOn = playerState.player;
                        GameManager.Instance.spellsBeingCast[i, whoAmI] = spellInfo;
                    }

                    // Remove the spell from the players hand
                    if (spellInfo.whatSpell.spellName != SpellNames.none)
                    {
                        player.spellHand.RemoveSpells(spellInfo.whatSpell);
                        //remove cards from card slot script and set object pool back to default 

                        // keep the position of the cards to their current slot

                        //Remove past spells
                        playerState.playerHand.ClearSelected(player);
                        playerState.playerHand.FullRemove();
                    }
                }
            }

            playerState.finishedCastingImage.SetActive(false);

            // move cards to out of play position
            for (int i = 0; i < playerState.playerHand.cards.Count; i++)
            {
                if (playerState.playerHand.cards[i].isSelected == false)
                {
                    playerState.playerHand.cards[i].gameObject.transform.position = playerState.cardPlay.cardsOutOfPlayPos.position;
                }
            }

            GameManager.Instance.roundCheck = false;
            player.gameObject.GetComponent<PlayerControlHandler>().deconfirm = false;
            playerState.PerformTransition(Transition.NeedDecision);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        //If the player is ready to begin casting
        if (playerState.readyToCast)
        {
            // This is a debug thing to make check if they player has finished casting
            if (player == GameManager.Instance.player1)
            {
                if (!GameManager.Instance.nextStateP1)
                {
                    GameManager.Instance.nextStateP1 = true;
                    playerState.finishedCastingImage.SetActive(true);
                }
                else
                {
                    GameManager.Instance.nextStateP1 = false;
                    playerState.finishedCastingImage.SetActive(false);
                }
            }
            else
            {
                if (!GameManager.Instance.nextStateP2)
                {
                    GameManager.Instance.nextStateP2 = true;
                    playerState.finishedCastingImage.SetActive(true);
                }
                else
                {
                    GameManager.Instance.nextStateP2 = false;
                    playerState.finishedCastingImage.SetActive(false);
                }
            }
            // this is the the end of the debug portion the next line is important don't remove
            playerState.readyToCast = false;
        }

        //if the player has selected a card
        if (playerState.cardSelected)
        {
            //what order the spells are chosen
            if (amtOfSpells == 0)
            {
                spellsChosen[0] = playerState.currentSpellName;
                player.LoseMana(ActiveSpellCards.Instance.spellCards[(int)playerState.currentSpellName].manaCost);
                playerState.canChooseFinger = true;
            }
            else if (amtOfSpells == 1)
            {
                spellsChosen[1] = playerState.currentSpellName;
                player.LoseMana(ActiveSpellCards.Instance.spellCards[(int)playerState.currentSpellName].manaCost);
                playerState.canChooseFinger = true;
            }
            else if (amtOfSpells == 2)
            {
                spellsChosen[2] = playerState.currentSpellName;
                player.LoseMana(ActiveSpellCards.Instance.spellCards[(int)playerState.currentSpellName].manaCost);
                playerState.canChooseFinger = true;
            }
            // if the spells don't require chosing a finger go into here
            if (spellsChosen[amtOfSpells] == SpellNames.Icicles || spellsChosen[amtOfSpells] == SpellNames.LefteousEnvy || spellsChosen[amtOfSpells] == SpellNames.RighteousEnvy ||
                spellsChosen[amtOfSpells] == SpellNames.LifeDrain || spellsChosen[amtOfSpells] == SpellNames.ThumbsUp || spellsChosen[amtOfSpells] == SpellNames.EchoingMana ||
                spellsChosen[amtOfSpells] == SpellNames.CursedConversion)
            {
                playerState.currentFingerName = PlayerFingers.none;
                amtOfSpells++;
            }
            // if the spells require the player to chose the opposing players finger
            else if (spellsChosen[amtOfSpells] == SpellNames.FireBolt || spellsChosen[amtOfSpells] == SpellNames.Rockthrow || spellsChosen[amtOfSpells] == SpellNames.CollectorsCurse
                            || spellsChosen[amtOfSpells] == SpellNames.StaticBlast || spellsChosen[amtOfSpells] == SpellNames.Quake || spellsChosen[amtOfSpells] == SpellNames.RightingBolt ||
                               spellsChosen[amtOfSpells] == SpellNames.LeftningBolt || spellsChosen[amtOfSpells] == SpellNames.TidalWave || spellsChosen[amtOfSpells] == SpellNames.PointerOfDeath
                               || spellsChosen[amtOfSpells] == SpellNames.SpectralChain || spellsChosen[amtOfSpells] == SpellNames.VampiricSurge)
            {
                if (player == GameManager.Instance.player1)
                {
                    player.playerOneHands = false;
                    if (amtOfSpells < 2)
                    {
                        player.playerCameras.GetInputForced(2);
                    }
                }
                else
                {
                    player.playerOneHands = true;
                    if (amtOfSpells < 2)
                    {
                        player.playerCameras.GetInputForced(2);
                    }
                }
            }
            // If the player has to chose there finger
            else
            {
                if (player == GameManager.Instance.player1)
                {
                    player.playerOneHands = true;
                    //healing
                    if (amtOfSpells < 2)
                    {
                        player.playerCameras.GetInputForced(0);
                    }
                }
                else
                {
                    player.playerOneHands = false;
                    //healing
                    if (amtOfSpells < 2)
                    {
                        player.playerCameras.GetInputForced(0);
                    }
                }
            }
            //player.playerInput.SwitchCurrentActionMap("Player");
            playerState.cardSelected = false;

        }
        //If the player has deselected the spell
        else if (playerState.cardDeselected)
        {
            if (amtOfSpells == 1 || amtOfSpells == 2 || amtOfSpells == 3)
            {
                player.GetMana(ActiveSpellCards.Instance.spellCards[(int)spellsChosen[playerState.playerHand.selectedCards.Count - 1]].manaCost);
                CardSelect card = playerState.playerHand.selectedCards[playerState.playerHand.selectedCards.Count - 1];
                playerState.playerHand.selectedCards.Remove(card);
                amtOfSpells--;
            }
            playerState.cardDeselected = false;
        }
        //The player is now choosing a finger
        else if (playerState.fingerSelected && playerState.canChooseFinger)
        {
            playerState.canChooseFinger = false;
            if (amtOfSpells == 0)
            {
                fingersChosen[0] = playerState.currentFingerName;
                amtOfSpells++;
            }
            else if (amtOfSpells == 1)
            {
                fingersChosen[1] = playerState.currentFingerName;
                amtOfSpells++;
            }
            else if (amtOfSpells == 2)
            {
                fingersChosen[2] = playerState.currentFingerName;
                amtOfSpells++;
            }
            playerState.fingerSelected = false;
        }
    }
}
