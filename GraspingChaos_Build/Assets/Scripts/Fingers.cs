using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//----------------------------------------------------------------
//  Author:       Sebastian
//  Contributors: Keller
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

    [SerializeField]
    public Material bone_Mat, damP1_Mat, damP2_Mat;

    [Tooltip("Type of Finger")]
    public PlayerFingers finger;
    [Tooltip("the finger segments should be serielized backwards, the last segments should be the first in teh editor")]
    Stack fingerSegments = new Stack();
    Stack fingerSegmentsRemoved = new Stack();

    PlayerManager player;

    //testing
    public int fingerHP;


    private void Awake()
    {
        Fill();
    }

    private void Start()
    {
        player = GetComponentInParent<PlayerManager>();
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
            //player.health.playerHealthStats[(int)finger]++;
            //fingerHP += 1;

            //added.GetComponent<SkinnedMeshRenderer>().material = bone_Mat;
            added.GetComponent<SkinnedMeshRenderer>().enabled = true;

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
            //player.health.playerHealthStats[(int)finger]--;
            //player.health.DamageFinger(finger);
            //fingerHP -= 1;

            //if (player == GameManager.Instance.player1)
            //{
            //    removed.GetComponent<SkinnedMeshRenderer>().material = damP2_Mat;
            //}
            //else
            //{
            //    removed.GetComponent<SkinnedMeshRenderer>().material = damP2_Mat;
            //}
            removed.GetComponent<SkinnedMeshRenderer>().enabled = false;
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
