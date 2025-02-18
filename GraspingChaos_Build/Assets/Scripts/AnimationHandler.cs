using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    public bool openBook = false;


    private void Update()
    {
        animator.SetBool("openBook", openBook);
    }
}
