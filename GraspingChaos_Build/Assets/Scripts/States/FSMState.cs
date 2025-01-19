using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: FSMState (Finite State Machine)
//  Date Created: 01/19/2025
//  Purpose: This class represents the States in the Finite State System.
//  Instance: no
//-----------------------------------------------------------------

/// <summary>
/// This class is adapted and modified from the FSM implementation class available on UnifyCommunity website
/// The license for the code is Creative Commons Attribution Share Alike.
/// It's originally the port of C++ FSM implementation mentioned in Chapter01 of Game Programming Gems 1
/// The entire titile and Author of the book is DeLoura, Mark. "Game Programming Gems. Charles River Media." Inc., Rockland, Massachusetts (2000)

/// This class represents the States in the Finite State System.
/// Each state has a Dictionary with pairs (transition-state) showing
/// which state the FSM should be if a transition is fired while this state
/// is the current state.
/// Reason method is used to determine which transition should be fired .
/// Act method has the code to perform the actions the current player is supposed to do if it�s on this state.
/// </summary>
public abstract class FSMState
{
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();
    protected FSMStateID stateID;
    public FSMStateID ID { get { return stateID; } }

    public void AddTransition(Transition transition, FSMStateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == Transition.None || id == FSMStateID.None)
        {
            Debug.LogWarning("FSMState : Null transition not allowed");
            return;
        }

        //Since this is a Deterministc FSM,
        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        Debug.Log("Added : " + transition + " with ID : " + id);
    }

    /// <summary>
    /// This method deletes a pair transition-state from this state�s map.
    /// If the transition was not inside the state�s map, an ERROR message is printed.
    /// </summary>
    public void DeleteTransition(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition passed was not on this State�s List");
    }


    /// <summary>
    /// This method returns the new state the FSM should be if
    ///    this state receives a transition  
    /// </summary>
    public FSMStateID GetOutputState(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return FSMStateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the State was not on the list");
        return FSMStateID.None;
    }

    /// <summary>
    /// Used to initialize variables when re-entering state
    /// </summary>
    public virtual void EnterStateInit()
    {

    }
    /// <summary>
    /// Decides if the state should transition to another on its list
    /// enemy is a reference to the other player that is in the duel
    /// </summary>
    public abstract void Reason(PlayerManager currentPlayer, PlayerManager enemy);

    /// <summary>
    /// This method controls what the player is doing in the game World.
    /// Every action, movement or communication the player does should be placed here
    /// enemy is a reference to the other player that is in the duel
    /// </summary>
    public abstract void Act(PlayerManager currentPlayer, PlayerManager enemy);
}
