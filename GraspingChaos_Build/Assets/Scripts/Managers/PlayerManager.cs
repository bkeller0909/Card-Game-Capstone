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

    // Start is called before the first frame update
    void Start()
    {
        entireHP = 28;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
