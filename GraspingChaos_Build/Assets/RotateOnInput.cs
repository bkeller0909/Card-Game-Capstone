using UnityEngine;

public class RotateOnInput : MonoBehaviour
{
    InputHandler playerInput;
    Animator m_Animator;

    bool cardNavPressed = false;

    //gets animator reference on awake
    private void Awake()
    {
        playerInput = GetComponentInParent<InputHandler>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    //Gets player input and plays the appropriate animation
    private void GetInput()
    {
        // Handle card navigation (left/right).
        if (playerInput.cardMoveRight && !cardNavPressed)
        {
            cardNavPressed = true;
            m_Animator.SetTrigger("Rotate Right");
        }
        else if (playerInput.cardMoveLeft && !cardNavPressed)
        {
            cardNavPressed = true;
            m_Animator.SetTrigger("Rotate Left");
        }
        else if (!playerInput.cardMoveRight && !playerInput.cardMoveLeft)
        {
            cardNavPressed = false;
        }
    }
}
