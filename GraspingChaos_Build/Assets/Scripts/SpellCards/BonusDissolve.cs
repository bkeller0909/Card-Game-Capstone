using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class BonusDissolve : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    ControlDissolve dissolveScript;
    public GameObject leftHand;
    public GameObject rightHand;

    [SerializeField]
    float dissolveTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        dissolveScript = GetComponentInParent<ControlDissolve>();
        leftHand.GetComponent<Image>().material.SetFloat("_Alpha", 1);
        rightHand.GetComponent<Image>().material.SetFloat("_Alpha", 1);
    }

    // Update is called once per frame
    void Update()
    {
        BonusFade();
    }

    public void BonusFade()
    {
        if (dissolveScript.dissolveCard)
        {
            StartCoroutine(DissolveBonusEffect(dissolveTime));
        }
    }


    IEnumerator DissolveBonusEffect(float dissolveTime)
    {
        float timeElapsed = 0f;

        while (timeElapsed < dissolveTime)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, timeElapsed / dissolveTime);
            leftHand.GetComponent<Image>().material.SetFloat("_Alpha", canvasGroup.alpha);
            rightHand.GetComponent<Image>().material.SetFloat("_Alpha", canvasGroup.alpha);

            yield return null;
        }


    }
}
