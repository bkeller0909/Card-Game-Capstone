using UnityEngine;

/// <summary>
///----------------------------------------------------------------
///  Author: Keller
///  Date Created: 01/07/2025
///  Purpose: 
///  Instance: No
///-----------------------------------------------------------------
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
    }

    // Update is called once per frame
    void Update()
    {
        // Navigate between objects (e.g., using controller input)
        if (InputManager.instance.playerControls.Player.NavigateFingerDown.WasPressedThisFrame())
        {
            if (GameManager.Instance.currentCaster.leftHand.fingersInHand[0])
            {

            }
        }
        if (InputManager.instance.playerControls.Player.NavigateFingerUp.WasPressedThisFrame())
        {
            DeselectFinger();
        }
    }

    public void CycleFingers()
    {
        for (int i = 0; i < GameManager.Instance.currentCaster.leftHand.fingersInHand.Count; i++)
        {

        }
    }

    void SelectFinger()
    {
        selectedFingerIndex = (selectedFingerIndex + 1) % fingerParts.Length;
        OutlineSelectedFinger();
    }

    void SelectPreviousFinger()
    {
        selectedFingerIndex = (selectedFingerIndex - 1 + fingerParts.Length) % fingerParts.Length;
        OutlineSelectedFinger();
    }

    void OutlineSelectedFinger()
    {
        for (int i = 0; i < fingerParts.Length; i++)
        {
            var selectedFinger = fingerParts[i].GetComponent<Renderer>();
            if (selectedFinger != null)
            {
                // Enable the outline for the currently selected object
                if (i == selectedFingerIndex)
                {
                    selectedFinger.material.color = selectedFingerColour;
                }
                else
                {
                    selectedFinger.material.color = defaultFingerColor;
                }
            }
        }
    }
}
