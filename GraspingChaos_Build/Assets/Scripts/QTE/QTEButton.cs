using System.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        QTE Button
//  Date Created: 01/16/2025
//  Purpose:      Script to crontrol the functionallity of QTE usage
//  Instance?     No
//-----------------------------------------------------------------
/// </summary>
public class QTEButton : MonoBehaviour
{
    // If you wish to test the new input actions I made for you. Un-comment everything and try it out.
    // ABXY are WASD on the keyboard and the DPad is the arrow keys. 

    // private PlayerManager playerQTE;
    // private InputActionMap actionQTE;
    // private InputActionAsset inputActionAsset;

    public bool dir = false;
    public Color correct = Color.green;
    public Color incorrect = Color.red;
    public bool pressed = false;
    public KeyCode AssignedBTN;             // replace this with IsActionPressed() in the if statements.


    private void Start()
    {
        // playerQTE = GetComponent<PlayerManager>;
        // actionQTE = inputActionAsset.FindActionMap("QTE", true);
    }

    void Update()
    {
        /*if (playerQTE.playerInput.actions["South"].triggered || playerQTE.playerInput.actions["East"].triggered
            || playerQTE.playerInput.actions["West"].triggered || playerQTE.playerInput.actions["North"].triggered
            || playerQTE.playerInput.actions["DPad Down"].triggered || playerQTE.playerInput.actions["DPad Up"].triggered
            || playerQTE.playerInput.actions["DPad Left"].triggered || playerQTE.playerInput.actions["DPad Right"].triggered)*/

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKeyDown(AssignedBTN))
            {
                pressed = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                if (dir)
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
                }
            }
            else if (!Input.GetKeyDown(AssignedBTN))
            {
                pressed = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                if (dir)
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
                }
            }
        }
    }

    /// <summary>
    /// Checks if the action map is triggered.
    /// </summary>
    /// <returns>True is action is triggered. False if not.</returns>
    /*private bool IsActionPressed()
    {
        for (int i = 0; i < actionQTE.actions.Count; i++)
        {
            if (actionQTE.actions[i].triggered)
            {
                return true;
            }
        }
        return false;
    }*/
}
