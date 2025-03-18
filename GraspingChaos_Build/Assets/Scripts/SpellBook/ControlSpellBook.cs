using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpellBook : MonoBehaviour
{
    //----------------------------------------------------------------
    //  Author: Carly Faulds
    //  Title: ControlSpellBook
    //  Date Created: 03/15/2025
    //  Purpose: Starts/triggers the pages to flip from the spell book, also tells the book to start glowing (Connected to a SpellBookGlow script) 
    //  Instance:
    //-----------------------------------------------------------------

    /// <summary>
    /// When started the pages of the book will begin to loop and flip
    /// The script grabs the animators of the pages (I Set it up to have 3) to flip and begins (F Key) and stops them (S Key)
    /// The isFlipping bool will tell the book to start glowing in the SpellBookGlow script and the beginFadeOut will 
    /// tell the book to stop glowing in the SpellBookGlow script
    /// </summary>
    /// 


    [SerializeField]
    GameObject[] pagesToFlip; //Game Object that has the animated pages child to it

    public Animator[] animators;

    [HideInInspector]
    public float delayTime = 1.0f;

    [HideInInspector]
    public bool isFlipping = false;

    [HideInInspector]
    public bool beginFadeOut = false;



    // Start is called before the first frame update
    void Start()
    {
        //For animations
        animators = new Animator[pagesToFlip.Length];
        SetUpAnimators();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            //Play all pages
            for (int i = 0; i < pagesToFlip.Length; i++)
            {
                animators[i].SetBool("StartFlipping", true);


            }
            isFlipping = true;


            StartCoroutine(DelayPageFlips());

        };



        if (Input.GetKeyDown(KeyCode.S))
        {

            for (int i = 0; i < pagesToFlip.Length; i++)
            {
                animators[i].SetBool("StartFlipping", false);


            }
            isFlipping = false;

            beginFadeOut = true;
        }

    }

    void SetUpAnimators()
    {
        for (int i = 0; i < pagesToFlip.Length; i++)
        {
            animators[i] = pagesToFlip[i].GetComponent<Animator>();


        }
    }

    IEnumerator DelayPageFlips()
    {


        for (int i = 0; i < pagesToFlip.Length; i++)
        {

            animators[i].Play("PageFlipMotion", 0, 0.0f);

            yield return new WaitForSeconds(delayTime);
        }

    }

}
