using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Co-Auther:   
//  Title:        AnimationHandler
//  Date Created: 02/20/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// Plays an animation for scenes
/// </summary>
public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    public bool openBook = false;


    private void Update()
    {
        animator.SetBool("openBook", openBook); // activates the book open animation
    }
}
