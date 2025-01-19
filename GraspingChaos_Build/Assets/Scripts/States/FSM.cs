using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: FSM (Finite State Machine)
//  Date Created: 01/19/2025
//  Purpose: This is the base setup of a Finite State Machine
//  Instance: no
//-----------------------------------------------------------------

/// <summary>
/// This class is the base setup of a Finite State Machine
/// </summary>

public class FSM : MonoBehaviour
{
    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
