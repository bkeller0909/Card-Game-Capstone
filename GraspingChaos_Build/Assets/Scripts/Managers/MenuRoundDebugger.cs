using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MenuRoundDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.startingEndRound = 1;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.startingEndRound = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.Instance.startingEndRound = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.Instance.startingEndRound = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameManager.Instance.startingEndRound = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            GameManager.Instance.startingEndRound = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            GameManager.Instance.startingEndRound = 7;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            GameManager.Instance.startingEndRound = 8;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GameManager.Instance.startingEndRound = 9;
        }
    }
}
