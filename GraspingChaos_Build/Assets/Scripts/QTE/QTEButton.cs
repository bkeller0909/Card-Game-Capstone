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
    public bool revealBorder = false;
    public bool correctTest = false;
    public Color correct = Color.green;
    public Color incorrect = Color.red;


    void Update()
    {
        if (revealBorder)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if (correctTest)
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = correct;
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = incorrect;
            }
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        Destroy(gameObject, GameManager.Instance.timerQTE);
    }
}
