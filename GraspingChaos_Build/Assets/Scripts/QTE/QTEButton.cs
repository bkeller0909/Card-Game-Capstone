using UnityEngine;
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
    public bool dir = false;
    public Color correct = Color.green;
    public Color incorrect = Color.red;
    public bool pressed = false;
    public KeyCode AssignedBTN;

    void Update()
    {
        if(Input.GetKeyDown(AssignedBTN))
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
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //still gotta figure out a way to check for other buttons without hardcode
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
