using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool debugDmg = false;
    public bool debugHp = false;
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
    public void OnDebugHealth(InputValue value)
    {
        debugHp = value.isPressed;
    }

    //player
    public void OnDebugDamage(InputValue value)
    {
        debugDmg = value.isPressed;
    }

    //player
    public void OnDebugHandSwap(InputValue value)
    {
        debugSwap = value.isPressed;
    }

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

}
