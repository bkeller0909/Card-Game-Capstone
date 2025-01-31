using UnityEngine;

public class HANDTEST : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
        }
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
