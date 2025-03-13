using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Hands Handler
//  Date Created: 01/07/2025
//  Purpose:      This hold each fingers of a set of hands
//  Instance?     Yes
//-----------------------------------------------------------------

/// <summary>
/// This hold each fingers of a set of hands
/// </summary>
public class HandsHandler : MonoBehaviour
{
    [Tooltip("This is who will be seeing this set of hands"), SerializeField]
    public PlayerHands whoseHands;

    [Tooltip("The fingers of these hands"), SerializeField]
    public List<Fingers> fingers;

    public VisualEffect[] boneVisualEffects;
}
