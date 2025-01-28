using UnityEngine;

public class RotateOnInput : MonoBehaviour
{
    Animator m_Animator;

    //gets animator reference on awake
    private void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_Animator.SetTrigger("Rotate Left");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_Animator.SetTrigger("Rotate Right");
        }
    }
}
