using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//----------------------------------------------------------------
//  Author: Keller
//  Co-Author: 
//  Title: PauseButton
//  Date Created: 03/24/2025
//  Instance: No
//-----------------------------------------------------------------

/// <summary>
/// Performs changes to buttons that are selected and deselected
/// </summary>
public class PauseButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool isSelected = false; // bool for is the button selected

    private Image btnImage;  // image of the button

    private TMP_Text text;          // text of the button

    [SerializeField] private Transform arrowPos;       // arrows that will point to the button

    [SerializeField] private Color selectedCol, defualtCol;   // default colour of the button and the colour it will change to when selected

    private MenuManager menuManager;

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

    private void Awake()
    {
        menuManager = FindAnyObjectByType<MenuManager>();
        btnImage = gameObject.GetComponent<Image>();
        btnImage.color = defualtCol;
        text = gameObject.GetComponentInChildren<TMP_Text>();
    }

    /// <summary>
    /// Make changes to the button when it is selected.
    /// </summary>
    public void BtnSelected()
    {
        isSelected = true;
        btnImage.color = selectedCol;
        text.color = Color.white;
        StartCoroutine(menuManager.MoveArrows(gameObject.GetComponent<Button>(), arrowPos));
    }

    /// <summary>
    /// Set the button back to it's default state when it is no longer selected.
    /// </summary>
    public void BtnDeselected()
    {
        isSelected = false;
        btnImage.color = defualtCol;
        text.color = Color.black;
    }
}
