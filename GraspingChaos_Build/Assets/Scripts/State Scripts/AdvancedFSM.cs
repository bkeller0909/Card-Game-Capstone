using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: AdvancedFSM (Finite State Machine)
//  Date Created: 01/19/2025
//  Purpose: This class handles the creation of states and how they switch inbetween each other
//  Instance: no
//-----------------------------------------------------------------

/// <summary>
/// This class is adapted and modified from the FSM implementation class available on UnifyCommunity website
/// The license for the code is Creative Commons Attribution Share Alike.
/// It's originally the port of C++ FSM implementation mentioned in Chapter01 of Game Programming Gems 1
/// The entire titile and Author of the book is DeLoura, Mark. "Game Programming Gems. Charles River Media." Inc., Rockland, Massachusetts (2000)
/// 
/// This class handles the creation of states and how they switch inbetween each other
/// </summary>

public enum Transition
{
    None = 0,
    Start,
    StatsGained,
    NeedDecision,
    FasterSpell,
    SlowerSpell,
    Challenge,
    EndOfRound,
    died,

    #region The Casting Spell States
    ///////////////////////////////
    // C A S T I N G   TRANSITIONS
    //////////////////////////////
    //Attack Spells
    TA_FireBolt,
    TA_RockThrow,
    TA_RighteousEnvy,
    TA_LefteousEnvy,
    TA_Icicles,
    TA_CollectorsCurse,
    TA_StaticBlast,
    TA_RightningBolt,
    TA_LeftningBolt,
    TA_TidalWave,
    TA_PointerOfDeath,
    TA_Quake,
    ////////////////////////////
    //Restoration Spells
    TRe_ForTheCause,
    TRe_QuickHeal,
    TRe_ThumbsUp,
    TRe_LifeDrain,
    TRe_CursedConversion,
    TRe_GreenThumb,
    TRe_Materialise,
    TRe_EchoingMana,
    ////////////////////////////
    //Ring Spells
    TRi_ThornsOfAgony,
    TRi_GuardiansTouch,
    TRi_SpectralChain,
    TRi_ManaMerchant,
    TRi_VengefulMirror,
    TRi_VampiricSurge,
    TRi_VeilOfFortitude,
    #endregion

}

public enum FSMStateID
{
    None = 0,
    Defualt = 1,
    DealStats = 2,
    ChoosingSpells = 3,
    Deciding = 4,
    QTE = 5,
    Dead = 6,

    #region The Casting Spell States
    ///////////////////////////////
    // C A S T I N G   S T A T E S
    //////////////////////////////
    //Attack Spells
    A_FireBolt,
    A_RockThrow,
    A_RighteousEnvy,
    A_LefteousEnvy,
    A_Icicles,
    A_CollectorsCurse,
    A_StaticBlast,
    A_RightningBolt,
    A_LeftningBolt,
    A_TidalWave,
    A_PointerOfDeath,
    A_Quake,
    ////////////////////////////
    //Restoration Spells
    Re_ForTheCause,
    Re_QuickHeal,
    Re_ThumbsUp,
    Re_LifeDrain,
    Re_CursedConversion,
    Re_GreenThumb,
    Re_Materialise,
    Re_EchoingMana,
    ////////////////////////////
    //Ring Spells
    Ri_ThornsOfAgony,
    Ri_GuardiansTouch,
    Ri_SpectralChain,
    Ri_ManaMerchant,
    Ri_VengefulMirror,
    Ri_VampiricSurge,
    Ri_VeilOfFortitude,
    #endregion
}

public class AdvancedFSM : FSM
{
    private List<FSMState> fsmStates;

    //The fsmStates are not changing directly but updated by using transitions
    private FSMStateID currentStateID;
    public FSMStateID CurrentStateID { get { return currentStateID; } }

    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }

    public AdvancedFSM()
    {
        fsmStates = new List<FSMState>();
    }

    /// <summary>
    /// Add New State into the list
    /// </summary>
    public void AddFSMState(FSMState fsmState)
    {
        // Check for Null reference before deleting
        if (fsmState == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        // First State inserted is also the Initial state
        //   the state the machine is in when the simulation begins
        if (fsmStates.Count == 0)
        {
            fsmStates.Add(fsmState);
            currentState = fsmState;
            currentStateID = fsmState.ID;
            return;
        }

        // Add the state to the List if it´s not inside it
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == fsmState.ID)
            {
                Debug.LogError("FSM ERROR: Trying to add a state that was already inside the list: " + state.ToString());
                return;
            }
        }

        //If no state in the current then add the state to the list
        fsmStates.Add(fsmState);
    }

    /// <summary>
    /// This method delete a state from the FSM List if it exists, 
    ///   or prints an ERROR message if the state was not on the List.
    /// </summary>
    public void DeleteState(FSMStateID fsmState)
    {
        // Check for NullState before deleting
        if (fsmState == FSMStateID.None)
        {
            Debug.LogError("FSM ERROR: bull id is not allowed");
            return;
        }

        // Search the List and delete the state if it´s inside it
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == fsmState)
            {
                fsmStates.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: The state passed was not on the list. Impossible to delete it");
    }

    /// <summary>
    /// This method tries to change the state the FSM is in based on
    /// the current state and the transition passed. If current state
    ///  doesn´t have a target state for the transition passed, 
    /// an ERROR message is printed.
    /// </summary>
    public void PerformTransition(Transition trans)
    {
        // Check for NullTransition before changing the current state
        if (trans == Transition.None)
        {
            Debug.LogError("FSM ERROR: Null transition is not allowed");
            return;
        }

        // Check if the currentState has the transition passed as argument
        FSMStateID id = currentState.GetOutputState(trans);
        if (id == FSMStateID.None)
        {
            Debug.LogError("FSM ERROR: Current State does not have a target state for this transition");
            return;
        }

        // Update the currentStateID and currentState		
        currentStateID = id;
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == currentStateID)
            {
                currentState = state;
                currentState.EnterStateInit();
                break;
            }
        }
    }
}
