using UnityEngine;
using UnityEngine.InputSystem;

//----------------------------------------------------------------
//  Author:       Sebastian
//  Contributors: Keller
//  Title:        InputHandler
//  Date Created: 02/06/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Attached to the Player GameObject.
/// 
/// Handles all input from the Input Actions on each Input Action Map.
/// </summary>
public class InputHandler : MonoBehaviour
{
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool selectFinger = false;
    public bool cancelFingerSelect = false;
    public bool debugSwap = false;
    public bool cardMoveRight = false;
    public bool cardMoveLeft = false;
    public bool finishSelection = false;
    public bool Abtn = false;
    public bool Bbtn = false;
    public bool Xbtn = false;
    public bool Ybtn = false;
    public bool UpBtn = false;
    public bool DownBtn = false;
    public bool LeftBtn = false;
    public bool RightBtn = false;
    public bool debugQTE = false;
    public bool selectCard = false;
    public bool cameraUp = false;
    public bool cameraDown = false;
    public bool deselectCard = false;

    #region Player Controls
    //player
    public void OnFingerLeft(InputValue value)
    {
        moveLeft = value.isPressed;
    }

    //player
    public void OnFingerRight(InputValue value)
    {
        moveRight = value.isPressed;
    }

    //player
    public void OnCancel(InputValue value)
    {
        cancelFingerSelect = value.isPressed;
    }

    //player
    public void OnSelectFinger(InputValue value)
    {
        selectFinger = value.isPressed;
    }

    //player
    public void OnDebugHandSwap(InputValue value)
    {
        debugSwap = value.isPressed;
    }

    public void OnCameraUp(InputValue value)
    {
        cameraUp = value.isPressed;
    }

    public void OnCameraDown(InputValue value)
    {
        cameraDown = value.isPressed;
    }
    #endregion

    #region Card Controls
    //card
    public void OnNavCardRight(InputValue value)
    {
        cardMoveRight = value.isPressed;
    }

    //card
    public void OnNavCardLeft(InputValue value)
    {
        cardMoveLeft = value.isPressed;
    }

    //card
    public void OnSetFinal(InputValue value)
    {
        finishSelection = value.isPressed;
    }

    //card
    public void OnSelect(InputValue value)
    {
        selectCard = value.isPressed;
    }

    public void OnDeselect(InputValue value)
    {
        deselectCard = value.isPressed;
    }

    #endregion

    #region QTE Controls
    //qte
    public void OnSouth(InputValue value)
    {
        Abtn = value.isPressed;
    }

    public void OnNorth(InputValue value)
    {
        Ybtn = value.isPressed;
    }

    public void OnEast(InputValue value)
    {
        Bbtn = value.isPressed;
    }

    public void OnWest(InputValue value)
    {
        Xbtn = value.isPressed;
    }

    public void OnDPadRight(InputValue value)
    {
        RightBtn = value.isPressed;
    }

    public void OnDPadLeft(InputValue value)
    {
        LeftBtn = value.isPressed;
    }

    public void OnDPadDown(InputValue value)
    {
        DownBtn = value.isPressed;
    }

    public void OnDPadUp(InputValue value)
    {
        UpBtn = value.isPressed;
    }

    public void OnDebugTriggerQTE(InputValue value)
    {
        debugQTE = value.isPressed;
    }
    #endregion
}
