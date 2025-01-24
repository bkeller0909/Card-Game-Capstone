using TMPro;
using UnityEngine;

public class TestStates : MonoBehaviour
{

    [SerializeField]
    public TMP_Text aOSC_text;

    public void addSpell()
    {
        if (GameManager.Instance.amtOfSpellsBeingCast < 3)
        {
            GameManager.Instance.amtOfSpellsBeingCast++;
            aOSC_text.text = "Amt of spells Chosen: " + GameManager.Instance.amtOfSpellsBeingCast.ToString();
        }
    }

    public void minSpell()
    {
        if (GameManager.Instance.amtOfSpellsBeingCast > 0)
        {
            GameManager.Instance.amtOfSpellsBeingCast--;
            aOSC_text.text = "Amt of spells Chosen: " + GameManager.Instance.amtOfSpellsBeingCast.ToString();
        }
    }

    public void startCasting()
    {
        if (GameManager.Instance.amtOfSpellsBeingCast != 0)
        {
            GameManager.Instance.nextTestState = true;
        }
    }
}
