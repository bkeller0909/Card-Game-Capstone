using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsHandler : MonoBehaviour
{
    [Tooltip("This is who will be seeing this set of hands"), SerializeField]
    public PlayerHands whoseHands;

    [Tooltip("The fingers of these hands"), SerializeField]
    public List<Fingers> fingers;
}
