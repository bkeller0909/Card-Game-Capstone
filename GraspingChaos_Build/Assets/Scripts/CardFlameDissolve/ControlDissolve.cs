using System.Collections;
using UnityEngine;

public class ControlDissolve : MonoBehaviour
{
    //----------------------------------------------------------------
    //  Author: Carly Faulds
    //  Title: ControlDissolve
    //  Date Created: 02/26/2025
    //  Purpose: Controls the dissolve when a spell is finished
    //  Instance:
    //-----------------------------------------------------------------

    /// <summary>
    /// 
    /// The dissolve animation happens with a Step node in the materials shader graph material.
    /// When the Step value is at 1 it is completely white so the spell card material looks full and normal
    /// When the Step value is at 0 it is completely black so the spall card material is fully transparent
    /// 
    /// 
    /// The Step in the shader graph will use a coroutine to make a smooth dissolve effect
    /// making the spell card turn transparent overtime.
    /// 
    /// </summary>
    /// 

    const float FULL_WHITE_VALUE = 1.0f;
    const float FULL_BLACK_VALUE = -1.0f; // negative amount to make sure all white is fully gone for transparency


    const float MAX_FLAME_ALPHA = 7.0f; //====
    const float MIN_FLAME_ALPHA = 0.0f;

    //public Material cardMaterial; //This will be for the card itself

    //The Base Cards Material
    Material cardMaterial;


    public GameObject frontFlameEffectObject; //Needed a lit shader to work but the cards have to stay unlit so added extra card body, front and back
    public GameObject backFlameEffectObject; //Needed a lit shader to work but the cards have to stay unlit so added extra card body, front and back


    Material frontFlameEffectMaterial;
    Material backFlameEffectMaterial;


    public float dissolveTime = 1.0f; //Controls both the dissolve and flame movement / animation

    public float flameTime = 1.0f;

    public bool dissolveCard;

    public bool RestoreCard = false;

    float cardDissolveAmount;

    float flameAlphaAmount;

    private void Start()
    {
        //NOTE:
        //This way of grabbing a shader material makes sure it only affects this material, otherwise it will affect all instances of the
        //shader graph which is not a W
        cardMaterial = GetComponent<Renderer>().material;

        frontFlameEffectMaterial = frontFlameEffectObject.GetComponent<Renderer>().material;
        backFlameEffectMaterial = backFlameEffectObject.GetComponent<Renderer>().material;

        //flameEffectObject.SetActive(false);

        //Base Card Dissolve
        cardDissolveAmount = FULL_WHITE_VALUE;
        cardMaterial.SetFloat("_DissolveAmount", cardDissolveAmount);

        //Flame Effect (Alpha fade in and out (Brightness property))
        flameAlphaAmount = MIN_FLAME_ALPHA;

        frontFlameEffectMaterial.SetFloat("_Brightness", flameAlphaAmount);
        backFlameEffectMaterial.SetFloat("_Brightness", flameAlphaAmount);

    }

    void Update()
    {
        RestoreSpellCard();
        DissolveSpellCard();

    }

    public void DissolveSpellCard()
    {
        if (dissolveCard)
        {
            RestoreCard = false; //Has to be off

            //flameEffectObject.SetActive(true);
            StartCoroutine(DissolveCardOverTime(dissolveTime));
            StartCoroutine(FlameEffect(flameTime));
        }
    }

    public void RestoreSpellCard()
    {
        if (RestoreCard)
        {
            dissolveCard = false;

            cardDissolveAmount = FULL_WHITE_VALUE;
            cardMaterial.SetFloat("_DissolveAmount", cardDissolveAmount);

            //In case its not at 0.0
            flameAlphaAmount = MIN_FLAME_ALPHA;
            frontFlameEffectMaterial.SetFloat("_Brightness", flameAlphaAmount);
            backFlameEffectMaterial.SetFloat("_Brightness", flameAlphaAmount);

        }
    }


    IEnumerator DissolveCardOverTime(float time)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < time)
        {
            //card head towards all black for alpha and flame move down the card
            float changingCardValue = Mathf.Lerp(cardDissolveAmount, FULL_BLACK_VALUE, timeElapsed / time);


            cardMaterial.SetFloat("_DissolveAmount", changingCardValue);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        cardMaterial.SetFloat("_DissolveAmount", FULL_BLACK_VALUE);
        cardDissolveAmount = cardMaterial.GetFloat("_DissolveAmount");


        //flameEffectObject.SetActive(false);

        dissolveCard = false;
    }

    IEnumerator FlameEffect(float time)
    {
        float timeElapsed = 0.0f;

        bool reachedFullAlpha = false;

        while (timeElapsed < time && !reachedFullAlpha)
        {
            //card head towards all black for alpha and flame move down the card
            float changingFlameValue = Mathf.Lerp(flameAlphaAmount, MAX_FLAME_ALPHA, timeElapsed / time);


            frontFlameEffectMaterial.SetFloat("_Brightness", changingFlameValue);
            backFlameEffectMaterial.SetFloat("_Brightness", changingFlameValue);

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= time)
            {
                reachedFullAlpha = true;
            }

            yield return null;
        }

        timeElapsed = 0.0f;

        flameAlphaAmount = frontFlameEffectMaterial.GetFloat("_Brightness");

        while (timeElapsed < time && reachedFullAlpha)
        {
            //card head towards all black for alpha and flame move down the card
            float changingFlameValueBack = Mathf.Lerp(flameAlphaAmount, MIN_FLAME_ALPHA, timeElapsed / time);


            frontFlameEffectMaterial.SetFloat("_Brightness", changingFlameValueBack);
            backFlameEffectMaterial.SetFloat("_Brightness", changingFlameValueBack);

            timeElapsed += Time.deltaTime;


            yield return null;
        }
    }
}
