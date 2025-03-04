//----------------------------------------------------------------
//  OG Author:     Cooper
//  other Authors: Sebastian
//  Title:         StaticBastState
//  Date Created:  01/16/2025
//  Purpose:       Spell State for Static Blast spell
//  Instance?      no
//-----------------------------------------------------------------
/// <summary>
/// </summary>
public class StaticBlastState : FSMState
{
    //reference for player state
    PlayerState playerState;
    //reference in int value of the players
    private int playerIndex;
    //string check for the next state
    private string nextState;

    //variables to store adjacent fingers
    PlayerFingers adjacentFinger1;
    PlayerFingers adjacentFinger2;

    //damage side randomizer
    int randomSide;
    //Constructor
    public StaticBlastState(PlayerState pS)
    {
        playerState = pS;
        stateID = FSMStateID.A_StaticBlast;
        nextState = "";
        randomSide = 0;
    }

    public override void EnterStateInit()
    {
        //get the QTE Amount of the spell
        playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.StaticBlast].qteAmount;
        //reest the string of next state
        nextState = "";
        //find player and assign values
        if (playerState.player == GameManager.Instance.player1)
        {
            playerIndex = (int)PlayerType.PLAYER1;
        }
        else
        {
            playerIndex = (int)PlayerType.PLAYER2;
        }
    }

    //Reason
    public override void Reason(PlayerManager player, PlayerManager enemy)
    {
        //check the string for next state and change states if needed
        if (nextState == "Deciding")
        {
            GameManager.Instance.spellInProgress = false;
            playerState.PerformTransition(Transition.NeedDecision);
        }
        else if (nextState == "QTE")
        {
            playerState.PerformTransition(Transition.Challenge);
        }
    }
    //Act
    public override void Act(PlayerManager player, PlayerManager enemy)
    {
        //if you still havent done your qte then go in here
        if (!playerState.finishedCurrentQTE)
        {
            //create the QTE Sequence 
            playerState.currentQTEAmount = ActiveSpellCards.Instance.spellCards[(int)SpellNames.StaticBlast].qteAmount;
            //Send to QTE state
            nextState = "QTE";
        }
        else
        {
            GameManager.Instance.spellInProgress = true;
            //check if the player is player 1, if the particle has not been played and if the particle for player 1 is done
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP1Done)
            {
                //runs the QTE evaluation to see if a state needs to play a particle effect
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.spawnExtra1 = false;
                    ParticleManger.Instance.spawnExtra2 = false;
                    ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.spawnExtra1 = true;
                    ParticleManger.Instance.spawnExtra2 = false;
                    randomSide = UnityEngine.Random.Range(0, 2);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else
                    {
                        //need to check if both adjacent fingers are alive to use the rando, if only one is then use the one that can take damage (STILL NEED TO DO)
                        if (randomSide == 0)
                        {
                            adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                            ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                        }
                        else
                        {
                            adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                            ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ;
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = true;
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        adjacentFinger2 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.particleAdjacent2 = adjacentFinger2;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                }
                GameManager.Instance.particleP1Done = true;
            }
            else if (player == GameManager.Instance.player2 && !GameManager.Instance.particleWait[GameManager.Instance.spellIndex] && !GameManager.Instance.particleP2Done)
            {
                player.GetComponent<QTEHandler>().EvauateQTEResults();
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    ParticleManger.Instance.spawnExtra1 = false;
                    ParticleManger.Instance.spawnExtra2 = false;
                    ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    ParticleManger.Instance.spawnExtra1 = true;
                    ParticleManger.Instance.spawnExtra2 = false;
                    randomSide = UnityEngine.Random.Range(0, 2);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else
                    {
                        //need to check if both adjacent fingers are alive to use the rando, if only one is then use the one that can take damage (STILL NEED TO DO)
                        if (randomSide == 0)
                        {
                            adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                            ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                        }
                        else
                        {
                            adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                            ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                            ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    ;
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = false;
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                    else
                    {
                        ParticleManger.Instance.spawnExtra1 = true;
                        ParticleManger.Instance.spawnExtra2 = true;
                        adjacentFinger1 = enemy.GetAdjacentFingerRight(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        adjacentFinger2 = enemy.GetAdjacentFingerLeft(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                        ParticleManger.Instance.particleAdjacent1 = adjacentFinger1;
                        ParticleManger.Instance.particleAdjacent2 = adjacentFinger2;
                        ParticleManger.Instance.StartParticle(SpellNames.StaticBlast, GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger, player);
                    }
                }
                GameManager.Instance.particleP2Done = true;
            }


            //damage and next state
            if (player == GameManager.Instance.player1 && GameManager.Instance.particleP1Done && GameManager.Instance.coroutineWaitP1)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                nextState = "Deciding";
                GameManager.Instance.particleP1Done = false;
                GameManager.Instance.coroutineWaitP1 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else
                    {
                        //need to check if both adjacent fingers are alive to use the rando, if only one is then use the one that can take damage (STILL NEED TO DO)
                        if (randomSide == 0)
                        {
                            enemy.health.DamageFinger(adjacentFinger1);
                        }
                        else
                        {
                            enemy.health.DamageFinger(adjacentFinger1);
                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {

                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                        enemy.health.DamageFinger(adjacentFinger2);
                    }
                }
            }

            if (player == GameManager.Instance.player2 && GameManager.Instance.particleP2Done && GameManager.Instance.coroutineWaitP2)
            {
                GameManager.Instance.ChangeCurrentCaster();
                GameManager.Instance.playedSpells++;
                GameManager.Instance.spellsThatHaveBeenCast[playerIndex] = true;
                nextState = "Deciding";
                GameManager.Instance.particleP2Done = false;
                GameManager.Instance.coroutineWaitP2 = false;
                if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Failure)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Half)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else
                    {
                        //need to check if both adjacent fingers are alive to use the rando, if only one is then use the one that can take damage (STILL NEED TO DO)
                        if (randomSide == 0)
                        {
                            enemy.health.DamageFinger(adjacentFinger1);
                        }
                        else
                        {
                            enemy.health.DamageFinger(adjacentFinger1);
                        }
                    }
                }
                else if (player.GetComponent<QTEHandler>().outcome == QTEOUTCOMES.Success)
                {
                    enemy.health.DamageFinger(GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger);

                    if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Pinky)
                    {

                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.LH_Thumb)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else if (GameManager.Instance.spellsBeingCast[GameManager.Instance.spellIndex, playerIndex].whatFinger == PlayerFingers.RH_Pinky)
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                    }
                    else
                    {
                        enemy.health.DamageFinger(adjacentFinger1);
                        enemy.health.DamageFinger(adjacentFinger2);
                    }
                }
            }

        }
    }

}
