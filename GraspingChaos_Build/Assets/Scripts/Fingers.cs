using System.Collections;
using UnityEngine;
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        Fingers
//  Date Created: 01/07/2025
//  Purpose:      finger class for damage dealing and healing
//  Instance?     No
//-----------------------------------------------------------------
public class Fingers : MonoBehaviour
{
    [Tooltip("Type of Finger")]
    public PlayerFingers finger;
    [Tooltip("the finger segments should be serielized backwards, the last segments should be the first in teh editor")]
    public GameObject[] fingerParts;
    Stack fingerSegments = new Stack();
    Stack fingerSegmentsRemoved = new Stack();

    private void Start()
    {
        Fill();
    }


    //fill stack
    private void Fill()
    {
        for (int i = 0; i < fingerParts.Length; i++)
        {
            fingerSegments.Push(fingerParts[i]);
        }
    }

    //add (reveal) finger segment
    public void addSegment()
    {
        GameObject added = (GameObject)fingerSegmentsRemoved.Pop();
        fingerSegments.Push(added);
        added.GetComponent<MeshRenderer>().enabled = true;

    }

    //test bool
    public bool remove = false;

    public bool add = false;

    //remove (hide) finger segment
    public void removeCurrentSegment()
    {
        GameObject removed = (GameObject)fingerSegments.Pop();

        //removed.SetActive(false);
        fingerSegmentsRemoved.Push(removed);
        removed.GetComponent<MeshRenderer>().enabled = false;
    }

    public void SelectedFinger()
    {
        for (int i = 0; i < fingerParts.Length; i++)
        {
            fingerParts[i].GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void DeselectFinger()
    {
        for (int i = 0; i < fingerParts.Length; i++)
        {
            fingerParts[i].GetComponent<Renderer>().material.color = Color.white;
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
