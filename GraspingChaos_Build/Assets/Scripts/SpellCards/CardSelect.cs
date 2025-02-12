using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

//----------------------------------------------------------------
//  Author:       Keller
//  Title:        CardSelect
//  Date Created: 01/07/2025
//  Instance:     No
//-----------------------------------------------------------------

/// <summary>
/// A script that will be applied to all spell cards.
/// Handles card selection and will apply small animations to cards when they are selected and highlighted.
/// Implements IPointer interfaces to allow control over card select and deselect.
/// </summary>
public class CardSelect : MonoBehaviour
{
    [Tooltip("Child object of the Card prefab.")]
    [SerializeField] private MeshRenderer cardGlowRender;
    private MeshRenderer cardGlowRenderDefault;

    [Header("Card Adjustments")]
    [Tooltip("How high the card will travel once selected.")]
    [SerializeField] private float verticalAdjustAmount = 2f;
    [Tooltip("Scale size of the card once it is selected.")]
    [SerializeField, Range(1, 2)] private float scaleAmount = 1.2f;

    private readonly float adjustTime = 0.2f;

    private Vector3 startPosition;
    private Vector3 startScale;

    [Header("States of the cards")]
    public bool isHovered = false;
    public bool isSelected = false;

    void Start()
    {
        cardGlowRender.enabled = false;
        cardGlowRenderDefault = cardGlowRender.GetComponent<MeshRenderer>();
        cardGlowRenderDefault.material.color = cardGlowRenderDefault.material.GetColor("_GlowColour");
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    /// <summary>
    /// Will adjust the card to be slightly enlarged over a very short period of time. 
    /// </summary>
    /// <param name="start">Start for the card animation.</param>
    private IEnumerator AdjustCard(bool start)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float timer = 0.0f;
        while (timer < adjustTime) // while the timer for the card being adjusted is less than the timer
        {
            timer += Time.deltaTime; // incriment the timer
            if (start) // if the animation has started
            {
                endPosition = startPosition + new Vector3(0f, verticalAdjustAmount, 0f);
                endScale = startScale * scaleAmount;
            }
            else // no animation reset the card
            {
                endPosition = startPosition;
                endScale = startScale;
            }

            // Lerp amounts
            Vector3 lerpPosition = Vector3.Lerp(transform.position, endPosition, (timer / adjustTime));
            Vector3 lerpScale = Vector3.Lerp(transform.localScale, endScale, (timer / adjustTime));

            // apply lerp
            transform.position = lerpPosition;
            transform.localScale = lerpScale;

            yield return null;
        }
    }
    public void OnHoverCard()
    {
        // card is hovered
        isHovered = true;
        //StartCoroutine(AdjustCard(isHovered));
        CardGlow(isHovered);
    }

    public void OffHoverCard()
    {
        // card is no longer hovered
        isHovered = false;
        //StartCoroutine(AdjustCard(isHovered));
        CardGlow(isHovered);
    }

    public void SelectCard()
    {
        isHovered = false;
        isSelected = true;
        cardGlowRender.material.SetColor("_GlowColour", Color.white);
        //StartCoroutine(AdjustCard(isSelected));
        CardGlow(isSelected);
    }

    public void DeselectCard()
    {
        isHovered = true;
        isSelected = false;
        cardGlowRender.material.SetColor("_GlowColour", cardGlowRenderDefault.material.color);
        //StartCoroutine(AdjustCard(isSelected));
        CardGlow(isSelected);
    }

    public void CardGlow(bool glow)
    {
        cardGlowRender.enabled = glow;
    }
}
