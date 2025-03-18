using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookGlow : MonoBehaviour
{
    //----------------------------------------------------------------
    //  Author: Carly Faulds
    //  Title: SpellBookGlow
    //  Date Created: 03/16/2025
    //  Purpose: Fades in and Out the Spell Book Glow linked to if the book is currently animating in the ControlSpellBook script 
    //  Instance:
    //-----------------------------------------------------------------

    /// <summary>
    /// If the book's pages are animating in the ControlSpellBook script linked to this script, the Spell Book
    /// will begin to glow by changing the properties in the current shaders via coroutines
    /// 
    /// The Book base and pages glow between 0.0f and 10.0f
    /// The Canvas text glows between 0.0f and 50.0f
    /// </summary>
    /// 


    //pre set these values in the Shader graph 
    const float MIN_GLOW = 0.0f;
    const float MAX_GLOW = 10.0f;

    const float MAX_RUNE_VALUE = 50.0f;

    const float PULSE_CAP_VALUE = 1.0f;

    //Get from body of book
    [SerializeField]
    ControlSpellBook controlSpellBookScript;
     

    [SerializeField]
    Material glowingBookMaterial;

    [SerializeField]
    Material glowingPagesMaterial;

    [SerializeField]
    Material glowingRunesMaterial;

    float glowBrightness;

    bool allowGlow;
    float allowGlowFloatValue;

    bool BeginAllGlow = false;

    [SerializeField]
    float buildUpTime = 1.0f;

    private void Start()
    {
        //For Glowing
        allowGlow = false;

        //Base Book Material
        glowingBookMaterial.SetFloat("_EmissionAmount", MIN_GLOW);
        glowingBookMaterial.SetFloat("_GlowPulse", MIN_GLOW);


        //Book Pages Material
        glowingPagesMaterial.SetFloat("_EmissionAmount", MIN_GLOW);
        glowingPagesMaterial.SetFloat("_GlowPulse", MIN_GLOW);


        //Canvas Runes Material
        glowingRunesMaterial.SetFloat("_Brightness", 0.0f); //Going from 0 to 50
    }

    private void Update()
    {
        if (controlSpellBookScript.isFlipping)
        {

            StartCoroutine(BringInGlowEffect());

        }

        if(controlSpellBookScript.beginFadeOut)
        {
            StopCoroutine(BringInGlowEffect());
            StartCoroutine(BringOutGlowEffect());

            controlSpellBookScript.beginFadeOut = false;
        }

        
    }

    //Fade in the glow set up in material shader
    IEnumerator BringInGlowEffect()
    {
        float timeElapsed = 0.0f;

        float currentValue = glowingBookMaterial.GetFloat("_EmissionAmount");
        float runesCurrentValue = glowingRunesMaterial.GetFloat("_Brightness"); 

        while (timeElapsed < buildUpTime)
        {
            float changingGlowValue = Mathf.Lerp(currentValue, PULSE_CAP_VALUE, timeElapsed / buildUpTime);
            float runesChangingValue = Mathf.Lerp(runesCurrentValue, MAX_RUNE_VALUE, timeElapsed / buildUpTime);

            changingGlowValue = Mathf.Clamp(changingGlowValue, 0.0f, 1.0f);

            //Base Book Material
            glowingBookMaterial.SetFloat("_EmissionAmount", changingGlowValue);
            glowingBookMaterial.SetFloat("_GlowPulse", changingGlowValue);


            //Book Pages Material
            glowingPagesMaterial.SetFloat("_EmissionAmount", changingGlowValue);
            glowingPagesMaterial.SetFloat("_GlowPulse", changingGlowValue);


            //Canvas Runes Material
            glowingRunesMaterial.SetFloat("_Brightness", runesChangingValue);




            timeElapsed += Time.deltaTime;

            yield return null;

        }

        //Base Book Material
        glowingBookMaterial.SetFloat("_EmissionAmount", PULSE_CAP_VALUE);
        glowingBookMaterial.SetFloat("_GlowPulse", PULSE_CAP_VALUE);

        //Book Pages Material
        glowingPagesMaterial.SetFloat("_EmissionAmount", PULSE_CAP_VALUE);
        glowingPagesMaterial.SetFloat("_GlowPulse", PULSE_CAP_VALUE);

        //Canvas Runes Material
        glowingRunesMaterial.SetFloat("_Brightness", MAX_RUNE_VALUE);
    }


    //Fade out the glow set up in material shader
    IEnumerator BringOutGlowEffect()
    {
        float timeElapsed = 0.0f;

        float currentValue = glowingBookMaterial.GetFloat("_EmissionAmount");
        float runesCurrentValue = glowingRunesMaterial.GetFloat("_Brightness");

        while (timeElapsed < buildUpTime)
        {
            float changingGlowValue = Mathf.Lerp(currentValue, MIN_GLOW, timeElapsed / buildUpTime);
            float runesChangingValue = Mathf.Lerp(runesCurrentValue, MIN_GLOW, timeElapsed / buildUpTime);

            //changingGlowValue = Mathf.Clamp(changingGlowValue, 0.0f, 1.0f);

            //Base Book Material
            glowingBookMaterial.SetFloat("_EmissionAmount", changingGlowValue);
            glowingBookMaterial.SetFloat("_GlowPulse", changingGlowValue);


            //Book Pages Material
            glowingPagesMaterial.SetFloat("_EmissionAmount", changingGlowValue);
            glowingPagesMaterial.SetFloat("_GlowPulse", changingGlowValue);


            //Canvas Runes Material
            glowingRunesMaterial.SetFloat("_Brightness", runesChangingValue);




            timeElapsed += Time.deltaTime;

            yield return null;

        }

        //Base Book Material
        glowingBookMaterial.SetFloat("_EmissionAmount", MIN_GLOW);
        glowingBookMaterial.SetFloat("_GlowPulse", MIN_GLOW);

        //Book Pages Material
        glowingPagesMaterial.SetFloat("_EmissionAmount", MIN_GLOW);
        glowingPagesMaterial.SetFloat("_GlowPulse", MIN_GLOW);

        //Canvas Runes Material
        glowingRunesMaterial.SetFloat("_Brightness", MIN_GLOW);
    }
}
