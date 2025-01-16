using System.Collections.Generic;
using UnityEngine;
/// <summary>
//----------------------------------------------------------------
//  OG Author:    Sebastian
//  Title:        QTE Manager
//  Date Created: 01/16/2025
//  Purpose:      Manage the creation and success check of all the Quick time Events of each player
//  Instance?     YES
//-----------------------------------------------------------------
/// </summary>
public class QTEManager : MonoBehaviour
{
    private static QTEManager instance;
    public static QTEManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QTEManager>();
            }
            return instance;
        }
    }

    public List<GameObject> Buttons = new List<GameObject>();
    public List<GameObject> CreatedButtons = new List<GameObject>();
    public GameObject firstPosition;
    private GameObject toDestroy;
    private Vector3 space = new Vector3(2f, 0f, 0f);
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DestoryCurrent();
        }
    }

    private GameObject RandomizeBTN()
    {
        int button = Random.Range(0, Buttons.Count);
        return Buttons[button];
    }

    public void Create(int qteAmount)
    {
        GameObject hold = new GameObject();
        firstPosition.transform.position = new Vector3(-5.21f, -1.19f, 0);
        for (int i = 0; i < qteAmount; i++)
        {
            hold = RandomizeBTN();
            Instantiate(hold, firstPosition.transform.position, Quaternion.identity);
            CreatedButtons.Add(hold);
            firstPosition.transform.position += space;
        }
    }

    public void DestoryCurrent()
    {
        for (int i = 0; i < CreatedButtons.Count; i++)
        {
            toDestroy = CreatedButtons[i];
            Destroy(toDestroy);
        }
    }

}
