using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//----------------------------------------------------------------
//  Author: Wyatt
//  Co-Author: Keller
//  Title: BtnIsSelected
//  Date Created: 02/18/2025
//  Instance: No
//-----------------------------------------------------------------

/// <summary>
/// Performs changes to buttons that are selected and deselected on the Main Menu
/// </summary>
public class BtnIsSelected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public bool isSelected; // bool for is the button selected

    public Image btnImage;  // image of the button

    TMP_Text text;          // text of the button

    [SerializeField]
    Transform topArrow, bottomArrow;        // arrows that will point to the button

    [SerializeField]
    public Color selectedCol, defualtCol;   // default colour of the button and the colour it will change to when selected

    MainMenu menu;

    /// <summary>
    /// Event function that is called when the button is selected.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSelect(BaseEventData eventData)
    {
        BtnSelected();
    }

    /// <summary>
    /// Event function that is called when the button is deselected.
    /// </summary>
    /// <param name="eventData"></param>
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
        text = gameObject.GetComponentInChildren<TMP_Text>();
        text.color = defualtCol;
    }

    /// <summary>
    /// Make changes to the button when it is selected.
    /// </summary>
    public void BtnSelected()
    {
        isSelected = true;
        text.color = selectedCol;
        StartCoroutine(menu.MoveArrows(GetComponent<Button>(), topArrow, bottomArrow));
    }

    /// <summary>
    /// Set the button back to it's default state when it is no longer selected.
    /// </summary>
    public void BtnDeselected()
    {
        isSelected = false;
        btnImage.color = defualtCol;
        text.color = defualtCol;
    }

}
