using UnityEngine;

public class HANDTEST : MonoBehaviour
{
    InputHandler playerInput;
    Animator animator;

    bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInParent<InputHandler>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.Abtn)
        {
            if (!isPressed)
            {
                isPressed = true;
                ResetTriggers();
                animator.SetTrigger("QTE1");
            }
        }
        else if (playerInput.Bbtn)
        {
            if (!isPressed)
            {
                isPressed = true;
                ResetTriggers();
                animator.SetTrigger("QTE2");
            }
        }
        else if (playerInput.Xbtn)
        {
            if (!isPressed)
            {
                isPressed = true;
                ResetTriggers();
                animator.SetTrigger("QTE3");
            }
        }
        else if (playerInput.Ybtn)
        {
            if (!isPressed)
            {
                isPressed = true;
                ResetTriggers();
                animator.SetTrigger("QTE4");
            }
        }
        else if (playerInput.debugQTE)
        {
            if (!isPressed)
            {
                isPressed = true;
                ResetTriggers();
                animator.SetTrigger("IDLE");
            }
        }
        else
        {
            isPressed = false;
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetTriggers();
            animator.SetTrigger("IDLE");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetTriggers();
            animator.SetTrigger("QTE1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetTriggers();
            animator.SetTrigger("QTE2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResetTriggers();
            animator.SetTrigger("QTE3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ResetTriggers();
            animator.SetTrigger("QTE4");
        }*/
    }


    void ResetTriggers()
    {
        animator.ResetTrigger("IDLE");
        animator.ResetTrigger("QTE1");
        animator.ResetTrigger("QTE2");
        animator.ResetTrigger("QTE3");
        animator.ResetTrigger("QTE4");
    }
}
