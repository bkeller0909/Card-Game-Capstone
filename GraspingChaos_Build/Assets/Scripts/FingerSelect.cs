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

        if (selectedFingerIndex == 0)
        {
            GameManager.Instance.player1.arrayOfFingers[0].GetComponent<Fingers>().SelectedFinger();
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Navigate between objects (e.g., using controller input)
        if (InputManager.instance.playerControls.Player.NavigateFingerDown.WasPressedThisFrame())
        {
            selectedFingerIndex++;
            if (selectedFingerIndex == 1)
            {
                GameManager.Instance.player1.arrayOfFingers[1].GetComponent<Fingers>().SelectedFinger();
            }
            else if (selectedFingerIndex == 2)
            {
                GameManager.Instance.player1.arrayOfFingers[2].GetComponent<Fingers>().SelectedFinger();
            }
            else if (selectedFingerIndex == 3)
            {
                GameManager.Instance.player1.arrayOfFingers[3].GetComponent<Fingers>().SelectedFinger();
            }
            else if (selectedFingerIndex == 4)
            {
                GameManager.Instance.player1.arrayOfFingers[4].GetComponent<Fingers>().SelectedFinger();
            }
            else if (selectedFingerIndex == 5)
            {
                GameManager.Instance.player1.arrayOfFingers[5].GetComponent<Fingers>().SelectedFinger();
            }
            else if (selectedFingerIndex == 6)
            {
                GameManager.Instance.player1.arrayOfFingers[6].GetComponent<Fingers>().SelectedFinger();

            }
            else if (selectedFingerIndex == 7)
            {
                GameManager.Instance.player1.arrayOfFingers[7].GetComponent<Fingers>().SelectedFinger();

            }
            else if (selectedFingerIndex == 8)
            {
                GameManager.Instance.player1.arrayOfFingers[8].GetComponent<Fingers>().SelectedFinger();

            }
            else if (selectedFingerIndex == 9)
            {
                GameManager.Instance.player1.arrayOfFingers[9].GetComponent<Fingers>().SelectedFinger();
            }
        }
        if (InputManager.instance.playerControls.Player.NavigateFingerUp.WasPressedThisFrame())
        {
            //selectedFingerIndex--;
            //if (selectedFingerIndex == 1)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[1].GetComponent<Fingers>().DeselectFinger();
            //}
            //else if (selectedFingerIndex == 2)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[2].GetComponent<Fingers>().DeselectFinger();
            //}
            //else if (selectedFingerIndex == 3)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[3].GetComponent<Fingers>().DeselectFinger();
            //}
            //else if (selectedFingerIndex == 4)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[4].GetComponent<Fingers>().DeselectFinger();
            //}
            //else if (selectedFingerIndex == 5)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[5].GetComponent<Fingers>().DeselectFinger();
            //}
            //else if (selectedFingerIndex == 6)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[6].GetComponent<Fingers>().DeselectFinger();

            //}
            //else if (selectedFingerIndex == 7)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[7].GetComponent<Fingers>().DeselectFinger();

            //}
            //else if (selectedFingerIndex == 8)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[8].GetComponent<Fingers>().DeselectFinger();

            //}
            //else if (selectedFingerIndex == 9)
            //{
            //    GameManager.Instance.currentCaster.arrayOfFingers[9].GetComponent<Fingers>().DeselectFinger();
            //}
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
