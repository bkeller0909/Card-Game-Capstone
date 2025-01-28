using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public bool moveLeft = false;


    public void OnNavigateFingerLeft(InputValue value)
    {
        moveLeft = value.isPressed;
    }

}
