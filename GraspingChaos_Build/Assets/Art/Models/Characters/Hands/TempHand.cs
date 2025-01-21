using UnityEngine;

public class TempHand : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            resetTriggers();
            animator.SetTrigger("ToIn");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            resetTriggers();
            animator.SetTrigger("ToOut");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            resetTriggers();
            animator.SetTrigger("ToPose1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            resetTriggers();
            animator.SetTrigger("ToPose2");
        }
    }

    void resetTriggers()
    {
        animator.ResetTrigger("ToIn");
        animator.ResetTrigger("ToOut");
        animator.ResetTrigger("ToPose1");
        animator.ResetTrigger("ToPose2");
    }


}
