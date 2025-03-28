using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class BonusDissolve : MonoBehaviour
{

    CanvasGroup canvasGroup;
    ControlDissolve dissolveScript;


    [SerializeField]
    float dissolveTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        dissolveScript = GetComponent<ControlDissolve>();


    }

    // Update is called once per frame
    void Update()
    {
        BonusFade();
    }

    public void BonusFade()
    {
        if(dissolveScript.dissolveCard)
        {
            StartCoroutine(DissolveBonusEffect(dissolveTime));
        }
    }


    IEnumerator DissolveBonusEffect(float dissolveTime)
    {
        float timeElapsed = 0f;

        while(timeElapsed < dissolveTime)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, timeElapsed / dissolveTime);
            yield return null;
        }


        yield return null;
    }
}
