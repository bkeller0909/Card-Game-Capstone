using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Title:        FingerColumns
//  Date Created: 01/09/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// This script is applied on the parent game object of the finger joints.\
/// Creates a list of all the game objects that represent the joints of a finger.
/// </summary>
[Serializable]
public class FingerColumns : MonoBehaviour
{
    [Tooltip("List of finger joints, this will act as rows.")]
    public List<GameObject> fingerJoints;
}
