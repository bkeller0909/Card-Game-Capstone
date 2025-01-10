using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Title:        CardSelect
//  Date Created: 01/09/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// 
/// </summary>
public class FingerSelect : Fingers
{

    // colours for the fingers
    private Color selectedFingerColour = Color.red;
    private Color defaultFingerColor = Color.white;

    private int selectedFingerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        OutlineSelectedFinger();

        if (selectedFingerIndex == 0)
        {
            //GameManager.Instance.player1.arrayOfFingers[0].GetComponent<Fingers>().SelectedFinger();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CycleFingers()
    {
        for (int i = 0; i < GameManager.Instance.currentCaster.leftHand.fingersInHand.Count; i++)
        {

        }
    }

    void SelectFinger()
    {
        //selectedFingerIndex = (selectedFingerIndex + 1) % fingerParts.Length;
        OutlineSelectedFinger();
    }

    void SelectPreviousFinger()
    {
        // selectedFingerIndex = (selectedFingerIndex - 1 + fingerParts.Length) % fingerParts.Length;
        OutlineSelectedFinger();
    }

    void OutlineSelectedFinger()
    {
        //for (int i = 0; i < fingerParts.Length; i++)
        //{
        //    var selectedFinger = fingerParts[i].GetComponent<Renderer>();
        //    if (selectedFinger != null)
        //    {
        //        // Enable the outline for the currently selected object
        //        if (i == selectedFingerIndex)
        //        {
        //            selectedFinger.material.color = selectedFingerColour;
        //        }
        //        else
        //        {
        //            selectedFinger.material.color = defaultFingerColor;
        //        }
        //    }
        //}
    }
}
