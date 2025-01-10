using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

//----------------------------------------------------------------
//  Author:       Keller
//  Title:        SelectableFinger
//  Date Created: 01/09/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Navigation ability between fingers and finger joints. This is organized by using a multi dimensionl array
/// that takes the fingers as columns and the joints of the finger as a row. The ability to fully select the finger or joint
/// can be toggled. Each selected finger or joint is highlighted by changing the colour of the gamee object through the use of the
/// array.
/// </summary>
public class SelectableFinger : MonoBehaviour
{
    [Tooltip("List of fingers which act as columns, each containing the finger joint game objects.")]
    public List<FingerColumns> fingers;

    [Tooltip("Color for the selected object.")]
    public Color selectedColor = Color.green;

    private Color defaultColor;

    private int currentFingerIndex = 0;         // columns
    private int currentFingerJointIndex = 0;    // rows

    private bool toggleFullFingerSelect = true; // start by fully selecting fingers

    void Start()
    {
        defaultColor = gameObject.GetComponentInChildren<Renderer>().material.color;
        UpdateSelection();
    }

    void Update()
    {
        // the button to toggle between full finger selection or joint selection
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleFingerSelection();
        }

        // we only want to select full fingers individually left and right on our hand
        if (toggleFullFingerSelect)
        {
            if (InputManager.instance.playerControls.Player.NavigateFingerLeft.WasPressedThisFrame())
            {
                MoveSelection(-1, 0); // move left
            }
            else if (InputManager.instance.playerControls.Player.NavigateFingerRight.WasPressedThisFrame())
            {
                MoveSelection(1, 0); // move right
            }
        }

        // once we select a finger only move up and down the finger for each joint
        else
        {
            if (InputManager.instance.playerControls.Player.NavigateFingerUp.WasPressedThisFrame())
            {
                MoveSelection(0, -1); // move up
            }
            else if (InputManager.instance.playerControls.Player.NavigateFingerDown.WasPressedThisFrame())
            {
                MoveSelection(0, 1); // move down
            }
        }
    }

    /// <summary>
    /// Updates the current selected finger and also the finger joint. 
    /// Visually represent the change by changing the colour of the selected game object.
    /// </summary>
    /// <param name="currentFinger">Horizontal index for the finger.</param>
    /// <param name="currentFingerJoint">Vertical index for the finger joint.</param>
    private void MoveSelection(int currentFinger, int currentFingerJoint)
    {
        // deselect all finger joints for the finger
        for (int i = 0; i < fingers[currentFingerIndex].fingerJoints.Count; i++)
        {
            SetObjectColor(fingers[currentFingerIndex].fingerJoints[i], defaultColor);
        }

        // update the index
        currentFingerIndex = Mathf.Clamp(currentFingerIndex + currentFinger, 0, fingers.Count - 1); // clamp to stay in bounds
        currentFingerJointIndex = Mathf.Clamp(currentFingerJointIndex + currentFingerJoint, 0, fingers[currentFingerIndex].fingerJoints.Count - 1);

        // apply a new selected finger
        UpdateSelection();
    }

    /// <summary>
    /// Switch between fully selecting a finger, and the single joints on the finger.
    /// </summary>
    private void ToggleFingerSelection()
    {
        // only reset the finger if we are not already on a joint
        if (toggleFullFingerSelect)
        {
            // deselect all joints in the current finger if switching from full column selection
            for (int i = 0; i < fingers[currentFingerIndex].fingerJoints.Count; i++)
            {
                SetObjectColor(fingers[currentFingerIndex].fingerJoints[i], defaultColor);
            }
        }

        // toggle the selection state
        toggleFullFingerSelect = !toggleFullFingerSelect;

        // if switching to the joint selection, stay on the same finger
        if (toggleFullFingerSelect)
        {
            currentFingerJointIndex = 0;  // select the first joint of the finger
        }
        else
        {
            currentFingerIndex = Mathf.Clamp(currentFingerIndex, 0, fingers.Count - 1); // copied the clamp to stay in bounds
            currentFingerJointIndex = 0;  // reset to the first cube in the column
        }

        // update the selection after toggling
        UpdateSelection();
    }

    /// <summary>
    /// Visually updates the finger for if the entire finger is selected or just the joint.
    /// </summary>
    private void UpdateSelection()
    {
        if (toggleFullFingerSelect)
        {
            // highlight the entire finger
            for (int i = 0; i < fingers[currentFingerIndex].fingerJoints.Count; i++)
            {
                SetObjectColor(fingers[currentFingerIndex].fingerJoints[i], selectedColor);
            }
        }
        else
        {
            // highlight the selected joint of the finger
            SetObjectColor(fingers[currentFingerIndex].fingerJoints[currentFingerJointIndex], selectedColor);
        }
    }

    /// <summary>
    /// Changes the colour of the game object to show if it's selected or not.
    /// </summary>
    /// <param name="obj">The game object whose colour is being changed.</param>
    /// <param name="color">The colour that will be applied.</param>
    private void SetObjectColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
