using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnIsSelected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public bool isSelected;

    public Image btnImage;

    TMP_Text text;

    [SerializeField]
    Transform topArrow, bottomArrow;

    [SerializeField]
    public Color selectedCol, defualtCol;

    MainMenu menu;

    public void OnSelect(BaseEventData eventData)
    {
        BtnSelected();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        BtnDeselected();
    }

    private void Start()
    {
        btnImage = GetComponent<Image>();
        btnImage.color = defualtCol;
        isSelected = false;
        menu = FindAnyObjectByType<MainMenu>();
        text = this.gameObject.GetComponentInChildren<TMP_Text>();
    }

    public void BtnSelected()
    {
        isSelected = true;
        btnImage.color = selectedCol;
        text.color = Color.white;
        StartCoroutine(menu.MoveArrows(this.GetComponent<Button>(), topArrow, bottomArrow));
    }

    public void BtnDeselected()
    {
        isSelected = false;
        btnImage.color = defualtCol;
        text.color = Color.black;
    }

}
