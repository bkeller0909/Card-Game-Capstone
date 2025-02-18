public class ChoosingSpellsState : FSMState
{
    PlayerState playerState;
    bool hasAddedSpells;
    bool firstEnter;

    public SpellNames[] spellsChosen;
    public PlayerFingers[] fingersChosen;
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

        firstEnter = false;

        amtOfSpells = 0;

        for (int i = 0; i < 3; i++)
        {
            spellsChosen[i] = SpellNames.none;
            fingersChosen[i] = PlayerFingers.none;
        }

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
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        if (GameManager.Instance.nextStateP1 && GameManager.Instance.nextStateP2)
        {
            if (!hasAddedSpells)
            {
                int whoAmI = -100;
                if (player == GameManager.Instance.player1)
                {
                    whoAmI = (int)PlayerType.PLAYER1;
                }
                else
                {
                    whoAmI = (int)PlayerType.PLAYER2;
                }

                for (int i = 0; i < spellsChosen.Length; i++)
                {
                    //Moving each spell that is beings casted info over to the GameManager to move to the desicion state
                    SpellsBeingCastInfo spellInfo = new SpellsBeingCastInfo();

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

                    if (spellsChosen[i] != SpellNames.none)
                    {
                        spellInfo.whoIsCasting = playerState.player;
                        spellInfo.whatSpell = ActiveSpellCards.Instance.spellCards[(int)spellsChosen[i]];
                        spellInfo.whatFinger = fingersChosen[i];

                        if (spellsChosen[i] == SpellNames.FireBolt || spellsChosen[i] == SpellNames.Rockthrow || spellsChosen[i] == SpellNames.RighteousEnvy ||
                            spellsChosen[i] == SpellNames.LefteousEnvy || spellsChosen[i] == SpellNames.Icicles || spellsChosen[i] == SpellNames.CollectorsCurse
                            || spellsChosen[i] == SpellNames.StaticBlast || spellsChosen[i] == SpellNames.Quake || spellsChosen[i] == SpellNames.RightingBolt ||
                               spellsChosen[i] == SpellNames.LeftningBolt || spellsChosen[i] == SpellNames.TidalWave || spellsChosen[i] == SpellNames.PointerOfDeath
                               || spellsChosen[i] == SpellNames.SpectralChain)
                        {
                            spellInfo.whoIsBeingCastedOn = playerState.enemy;
                        }
                        else
                        {
                            spellInfo.whoIsBeingCastedOn = playerState.player;
                        }

                        GameManager.Instance.spellsBeingCast[i, whoAmI] = spellInfo;
                    }
                    else
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
                        playerState.playerHand.ClearSelected();
                        playerState.playerHand.FullRemove();
                    }
                }
            }

            playerState.finishedCastingImage.SetActive(false);

            GameManager.Instance.roundCheck = false;
            playerState.PerformTransition(Transition.NeedDecision);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        if (playerState.readyToCast)
        {
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
            playerState.readyToCast = false;
        }
        else if (playerState.cardSelected)
        {
            if (amtOfSpells == 0)
            {
                spellsChosen[0] = playerState.currentSpellName;
                playerState.canChooseFinger = true;
            }
            else if (amtOfSpells == 1)
            {
                spellsChosen[1] = playerState.currentSpellName;
                playerState.canChooseFinger = true;
            }
            else if (amtOfSpells == 2)
            {
                spellsChosen[2] = playerState.currentSpellName;
                playerState.canChooseFinger = true;
            }
            if (spellsChosen[amtOfSpells] == SpellNames.FireBolt || spellsChosen[amtOfSpells] == SpellNames.Rockthrow || spellsChosen[amtOfSpells] == SpellNames.RighteousEnvy ||
                            spellsChosen[amtOfSpells] == SpellNames.LefteousEnvy || spellsChosen[amtOfSpells] == SpellNames.Icicles || spellsChosen[amtOfSpells] == SpellNames.CollectorsCurse
                            || spellsChosen[amtOfSpells] == SpellNames.StaticBlast || spellsChosen[amtOfSpells] == SpellNames.Quake || spellsChosen[amtOfSpells] == SpellNames.RightingBolt ||
                               spellsChosen[amtOfSpells] == SpellNames.LeftningBolt || spellsChosen[amtOfSpells] == SpellNames.TidalWave || spellsChosen[amtOfSpells] == SpellNames.PointerOfDeath
                               || spellsChosen[amtOfSpells] == SpellNames.SpectralChain)
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
        else if (playerState.cardDeselected)
        {
            if (amtOfSpells == 0)
            {
                spellsChosen[GameManager.Instance.currentDeselectedCard - 1] = SpellNames.none;
                fingersChosen[GameManager.Instance.currentDeselectedCard - 1] = PlayerFingers.none;
                amtOfSpells--;
            }
            else if (amtOfSpells == 1)
            {
                spellsChosen[GameManager.Instance.currentDeselectedCard - 1] = SpellNames.none;
                fingersChosen[GameManager.Instance.currentDeselectedCard - 1] = PlayerFingers.none;
                amtOfSpells--;
            }
            else if (amtOfSpells == 2)
            {
                spellsChosen[GameManager.Instance.currentDeselectedCard - 1] = SpellNames.none;
                fingersChosen[GameManager.Instance.currentDeselectedCard - 1] = PlayerFingers.none;
                amtOfSpells--;
            }
            playerState.cardDeselected = false;
        }
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
