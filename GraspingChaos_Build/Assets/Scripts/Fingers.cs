using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//----------------------------------------------------------------
//  Author:       Sebastian
//  Title:        Fingers
//  Date Created: 01/07/2025
//  Instance?     No
//-----------------------------------------------------------------

/// <summary>
/// Script for damage dealing and healing.
/// Apply to the fingers in the hand not the joints
/// </summary>

[Serializable]
public class Fingers : MonoBehaviour
{
    [Tooltip("List of finger joints, this will act as rows, the finger segments should be serielized backwards, the last segments should be the first in teh editor")]
    public List<GameObject> fingerJoints;

    [Tooltip("Type of Finger")]
    public PlayerFingers finger;
    [Tooltip("the finger segments should be serielized backwards, the last segments should be the first in teh editor")]
    Stack fingerSegments = new Stack();
    Stack fingerSegmentsRemoved = new Stack();

    //testing
    public int fingerHP;


    private void Awake()
    {
        Fill();
    }


    //fill stack
    private void Fill()
    {
        for (int i = 0; i < fingerJoints.Count; i++)
        {
            fingerSegments.Push(fingerJoints[i]);
        }
    }

    //add (reveal) finger segment
    public void addSegment()
    {
        if (fingerJoints.Count != fingerSegments.Count)
        {
            GameObject added = (GameObject)fingerSegmentsRemoved.Pop();
            fingerSegments.Push(added);
            added.GetComponent<MeshRenderer>().enabled = true;
            fingerHP += 1;
        }

    }

    //test bool
    public bool remove = false;

    public bool add = false;

    //remove (hide) finger segment
    public void removeCurrentSegment()
    {
        if (fingerSegments.Count > 0)
        {
            GameObject removed = (GameObject)fingerSegments.Pop();

            //removed.SetActive(false);
            fingerSegmentsRemoved.Push(removed);
            removed.GetComponent<MeshRenderer>().enabled = false;
            fingerHP -= 1;
        }
    }

    private void Update()
    {
        if (remove)
        {
            removeCurrentSegment();
            remove = false;
        }

        if (add)
        {
            addSegment();
            add = false;
        }
    }

}
