using System;
using System.Collections;
using UnityEngine;

public class ManaVisual : MonoBehaviour
{
    //----------------------------------------------------------------
    //  Author: Carly Faulds
    //  Title: ManaVisual
    //  Date Created: 02/12/2025
    //  Purpose: Change the mana material shader Y-offset so that the bottle fills and empty's
    //  Instance:
    //-----------------------------------------------------------------

    /// <summary>
    /// Backwards logic
    /// When Shader Y-Offset is at 0 the mana bottle will be completely full (0 = White)
    /// When Shader Y-Offset is at 1 the mana bottle will be completely empty (1 = Black)
    /// 
    /// This script will take the Y-Offset property value of the manaShader_MAT and blend it towards 
    /// a new value based off the amount of mana the player currently has.
    /// 
    /// NOTE:
    /// Since the Max amount of Mana is 12, 1/12 = 0.0833. An array is created to hold all these values at each step 0-12 [13 index]
    /// This will have to be backwards since 1 is black -> 1 - (1/12 * how much mana I have)
    /// The purpose of the array : If I have 5 mana at the moment go to the value that's in the array at 5 and set that to be the Y-offset
    /// 
    /// </summary>
    /// 

    const int MAX_MANA_AMOUNT = 12;
    const int MIN_MANA_AMOUNT = 0;

    public Material manaMaterial;

    public int desiredManaStep;
    public float fillOrEmptyTime = 1.0f;

    //====
    private int manaIndex = 5; //Players start with 5 mana
    private Vector2 rectOffset;
    private float yOffsetValue = 0;

    private int minOffsetValue = 1;
    private int maxOffsetValue = 0;


    private float[] manaStages = new float[MAX_MANA_AMOUNT + 1]; //Account for no mana



    // Start is called before the first frame update
    void Start()
    {
        desiredManaStep = manaIndex;

        rectOffset = manaMaterial.GetVector("_ManaOffsetPosition"); //name of Vector 2 control in ShaderGraph

        SetUpManaStages();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeMana();

    }

    private void SetUpManaStages()
    {
        manaStages[0] = minOffsetValue;
        manaStages[manaStages.Length - 1] = maxOffsetValue;


        float divValue = 1.0f / 12.0f;

        for (int i = 1; i < manaStages.Length - 1; i++)
        {
            //Round to two decimal places
            manaStages[i] = (float)Math.Round(1.0f - (divValue * i), 2);

        }

    }

    private void ChangeMana()
    {
        if (manaIndex > MAX_MANA_AMOUNT) //To prevent strange rect offset and array bounds
        {
            manaIndex = MAX_MANA_AMOUNT;
        }
        else if (manaIndex < MIN_MANA_AMOUNT) //To prevent strange rect offset and array bounds
        {
            manaIndex = MIN_MANA_AMOUNT;
        }

        //Account for the desired mana also
        if (desiredManaStep > MAX_MANA_AMOUNT) //To prevent strange rect offset and array bounds
        {
            desiredManaStep = MAX_MANA_AMOUNT;
        }
        else if (desiredManaStep < MIN_MANA_AMOUNT) //To prevent strange rect offset and array bounds
        {
            desiredManaStep = MIN_MANA_AMOUNT;
        }



        if (manaIndex != desiredManaStep)
        {
            ShifToStep();
        }

        rectOffset = new Vector2(0, manaStages[manaIndex]);
        manaMaterial.SetVector("_ManaOffsetPosition", rectOffset);
    }

    private void ShifToStep()
    {
        float currentValue = manaStages[manaIndex];
        float desiredValue = manaStages[desiredManaStep];

        StartCoroutine(FillOrEmpty(currentValue, desiredValue, fillOrEmptyTime));
    }

    IEnumerator FillOrEmpty(float currentManaValue, float desiredManaValue, float fillOrEmptyTime)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < fillOrEmptyTime)
        {
            float changingValue = Mathf.Lerp(currentManaValue, desiredManaValue, timeElapsed / fillOrEmptyTime);

            changingValue = Mathf.Clamp(changingValue, 0.0f, 1.0f);

            rectOffset = new Vector2(0, changingValue);
            manaMaterial.SetVector("_ManaOffsetPosition", rectOffset);

            timeElapsed += Time.deltaTime;

            yield return null;
        }


        rectOffset = new Vector2(0, desiredManaValue);
        manaMaterial.SetVector("_ManaOffsetPosition", rectOffset);

        manaIndex = desiredManaStep;

    }
}
