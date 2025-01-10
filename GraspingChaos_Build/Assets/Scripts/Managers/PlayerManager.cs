using UnityEngine;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Player Manager
//  Date Created: 01/07/2025
//  Purpose:      This is to manage everything that envolves a player and their stats
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class PlayerManager : MonoBehaviour
{

    public PlayerHealth health;

    public int entireHP;
    public int Mana;

    [Tooltip("Players right hand")]
    public Hands leftHand;
    [Tooltip("players left Hand")]
    public Hands rightHand;

    [Tooltip("Array of all fingers of players")]
    public GameObject[] arrayOfFingers;

    // Start is called before the first frame update
    void Start()
    {
        arrayOfFingers = new GameObject[10];
        entireHP = 28;
        fillFingers();
    }

    public void fillFingers()
    {
        int index = 0;
        foreach (GameObject finger in leftHand.fingersInHand)
        {
            arrayOfFingers[index] = finger;
            index++;
        }

        foreach (GameObject finger in rightHand.fingersInHand)
        {
            arrayOfFingers[index] = finger;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
