using System.Collections;
using UnityEngine;

public class QTECounter : MonoBehaviour
//----------------------------------------------------------------
//  Author: Carly Faulds
//  Title: QTECounter
//  Date Created: 02/20/2025
//  Purpose: Change the QTECounter material shader Y-offset so that the bottle fills and empty's
//  Instance:
//-----------------------------------------------------------------
{
    const int MAX_COUNTER_AMOUNT = 3;
    const int MIN_COUNTER_AMOUNT = 0;

    public Material qteCounterMaterial;

    public int desiredCounterStep;
    public float fillOrEmptyTime = 1.0f;

    //====
    private int counterIndex = 0; //Players counter starts at 0 (Low)
    private Vector2 rectOffset; //The rectangle in the shader that controls if you see the counter or not
    private float yOffsetValue = 0;


    private float minOffsetValue = 1.0f; //Backwards : 1.1 = see nothing (Black)
    private float maxOffsetValue = 0.0f; //Backwards : 0.0 = see everything (White)


    private float[] counterStages = new float[MAX_COUNTER_AMOUNT];

    [SerializeField] private QTEHandler playerQTEHandler;

    [SerializeField] private GameObject tick1, tick2, tick3, tick4, tick5;


    // Start is called before the first frame update
    void Start()
    {
        desiredCounterStep = counterIndex; //Start at 0 which is Low

        rectOffset = qteCounterMaterial.GetVector("_OffsetValue");

        SetUpCounterStages();

        tick5.SetActive(true);
        tick4.SetActive(true);
        tick3.SetActive(true);
        tick2.SetActive(true);
        tick1.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerQTEHandler.QTEHasStarted)
        {
            playerQTEHandler.EvaluateQTEResultsInRealTime();
            if (playerQTEHandler.outcome == QTEOUTCOMES.Failure)
            {
                desiredCounterStep = 0;
            }
            else if (playerQTEHandler.outcome == QTEOUTCOMES.Half)
            {
                desiredCounterStep = 1;
            }
            else if (playerQTEHandler.outcome == QTEOUTCOMES.Success)
            {
                desiredCounterStep = 2;
            }
            ChangeCounter();
        }



        if(playerQTEHandler.remainingTime < 5 && playerQTEHandler.remainingTime > 4)
        {
            tick1.SetActive(false);
        }
        else if(playerQTEHandler.remainingTime < 4 && playerQTEHandler.remainingTime > 3)
        {
            tick2.SetActive(false);
        }
        else if(playerQTEHandler.remainingTime < 3 && playerQTEHandler.remainingTime > 2)
        {
            tick3.SetActive(false);
        }
        else if(playerQTEHandler.remainingTime < 2 && playerQTEHandler.remainingTime > 1)
        {
            tick4.SetActive(false);
        }
        else if(playerQTEHandler.remainingTime <= 0)
        {
            tick5.SetActive(false);
        }
    }

    private void OnEnable()
    {
        desiredCounterStep = 0;
        tick5.SetActive(true);
        tick4.SetActive(true);
        tick3.SetActive(true);
        tick2.SetActive(true);
        tick1.SetActive(true);
    }

    void SetUpCounterStages()
    {
        counterStages[0] = minOffsetValue;
        counterStages[1] = 0.5f;
        counterStages[2] = maxOffsetValue;
    }

    void ChangeCounter()
    {
        if (counterIndex > MAX_COUNTER_AMOUNT) //To prevent strange rect offset and array bounds
        {
            counterIndex = MAX_COUNTER_AMOUNT;
        }
        else if (counterIndex < MIN_COUNTER_AMOUNT) //To prevent strange rect offset and array bounds
        {
            counterIndex = MIN_COUNTER_AMOUNT;
        }

        //Account for desired counter amount also
        if (desiredCounterStep > (MAX_COUNTER_AMOUNT - 1))
        {
            desiredCounterStep = (MAX_COUNTER_AMOUNT - 1);
        }
        else if (desiredCounterStep < MIN_COUNTER_AMOUNT)
        {
            desiredCounterStep = MIN_COUNTER_AMOUNT;
        }

        if (counterIndex != desiredCounterStep)
        {
            ShiftToCounterStep();
        }

        rectOffset = new Vector2(0, counterStages[counterIndex]);
        qteCounterMaterial.SetVector("_OffsetValue", rectOffset);

    }

    void ShiftToCounterStep()
    {
        float currentValue = counterStages[counterIndex];
        float desiredValue = counterStages[desiredCounterStep];

        StartCoroutine(FillCounter(currentValue, desiredValue, fillOrEmptyTime));

    }

    IEnumerator FillCounter(float currentCounterValue, float desiredCounterValue, float time)
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < time)
        {
            float changingValue = Mathf.Lerp(currentCounterValue, desiredCounterValue, timeElapsed / time);

            changingValue = Mathf.Clamp(changingValue, 0.0f, 1.0f);

            rectOffset = new Vector2(0, changingValue);
            qteCounterMaterial.SetVector("_OffsetValue", rectOffset);

            timeElapsed += Time.deltaTime;

            yield return null;

        }

        rectOffset = new Vector2(0, counterStages[desiredCounterStep]);
        qteCounterMaterial.SetVector("_OffsetValue", rectOffset);

        counterIndex = desiredCounterStep;
    }
}
