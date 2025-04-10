using System.Collections;
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
    public ControlDissolve cardDissolve;

    [Tooltip("Child object of the Card prefab.")]
    [SerializeField] private MeshRenderer cardGlowRender;
    private MeshRenderer cardGlowRenderDefault;

    [Header("Card Adjustments")]
    [Tooltip("How high the card will travel once selected.")]
    [SerializeField] private float verticalAdjustAmount = 2f;
    [Tooltip("Scale size of the card once it is selected.")]
    [SerializeField, Range(1, 2)] private float scaleAmount = 1.2f;

    [SerializeField, Tooltip("This is the finger bonus for the cards that have it")]
    public bool hasAFingerBonus;

    [SerializeField, Tooltip("This is the regular info that will appear on the card")]
    public GameObject fingerNormal;

    [SerializeField, Tooltip("This is the finger bonus for the cards that have it")]
    public GameObject fingerBonus;

    [SerializeField, Tooltip("This is the particle system for selecting")]
    public ParticleSystem particle;

    private readonly float adjustTime = 0.2f;

    private Vector3 startPosition;
    private Vector3 startScale;

    [SerializeField] private Color cardSelectColor;

    [Header("States of the cards")]
    public bool isHovered = false;
    public bool isSelected = false;

    public bool alreadySelected = false;

    void Start()
    {
        cardDissolve = GetComponent<ControlDissolve>();
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

    /// <summary>
    /// When a card is hovered.
    /// </summary>
    public void OnHoverCard(PlayerManager player)
    {
        // card is hovered
        isHovered = true;
        //StartCoroutine(AdjustCard(isHovered));
        CardGlow(isHovered);

        if (hasAFingerBonus)
        {
            FingerBonusFingerStatus(player, SpellNames.Rockthrow, PlayerFingers.LH_Index, PlayerFingers.RH_Index, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.Icicles, PlayerFingers.LH_Pinky, PlayerFingers.LH_Pinky, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.TidalWave, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.StaticBlast, PlayerFingers.LH_Middle, PlayerFingers.RH_Middle, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.PointerOfDeath, PlayerFingers.LH_Index, PlayerFingers.RH_Index, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.ThumbsUp, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.CursedConversion, PlayerFingers.LH_Ring, PlayerFingers.RH_Ring, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.GreenThumb, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb, 1.0f);
            FingerBonusFingerStatus(player, SpellNames.Materialise, PlayerFingers.LH_Pinky, PlayerFingers.RH_Pinky, 1.0f);
        }
    }

    /// <summary>
    /// When a card is no longer hovered.
    /// </summary>
    public void OffHoverCard(PlayerManager player, bool fromDealing)
    {
        // card is no longer hovered
        isHovered = false;
        //StartCoroutine(AdjustCard(isHovered));

        bool selected;
        if (isSelected)
        {
            selected = true;
        }
        else
        {
            selected = false;
        }

        CardGlow(selected);

        if (hasAFingerBonus)
        {
            FingerBonusFingerStatus(player, SpellNames.Rockthrow, PlayerFingers.LH_Index, PlayerFingers.RH_Index, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.Icicles, PlayerFingers.LH_Pinky, PlayerFingers.LH_Pinky, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.TidalWave, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.StaticBlast, PlayerFingers.LH_Middle, PlayerFingers.RH_Middle, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.PointerOfDeath, PlayerFingers.LH_Index, PlayerFingers.RH_Index, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.ThumbsUp, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.CursedConversion, PlayerFingers.LH_Ring, PlayerFingers.RH_Ring, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.GreenThumb, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb, 0.0f);
            FingerBonusFingerStatus(player, SpellNames.Materialise, PlayerFingers.LH_Pinky, PlayerFingers.RH_Pinky, 0.0f);

            if (fromDealing)
            {
                FingerBonusCardStatus(player, SpellNames.Rockthrow, PlayerFingers.LH_Index, PlayerFingers.RH_Index);
                FingerBonusCardStatus(player, SpellNames.Icicles, PlayerFingers.LH_Pinky, PlayerFingers.LH_Pinky);
                FingerBonusCardStatus(player, SpellNames.TidalWave, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb);
                FingerBonusCardStatus(player, SpellNames.StaticBlast, PlayerFingers.LH_Middle, PlayerFingers.RH_Middle);
                FingerBonusCardStatus(player, SpellNames.PointerOfDeath, PlayerFingers.LH_Index, PlayerFingers.RH_Index);
                FingerBonusCardStatus(player, SpellNames.ThumbsUp, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb);
                FingerBonusCardStatus(player, SpellNames.CursedConversion, PlayerFingers.LH_Ring, PlayerFingers.RH_Ring);
                FingerBonusCardStatus(player, SpellNames.GreenThumb, PlayerFingers.LH_Thumb, PlayerFingers.RH_Thumb);
                FingerBonusCardStatus(player, SpellNames.Materialise, PlayerFingers.LH_Pinky, PlayerFingers.RH_Pinky);
            }
        }
    }

    /// <summary>
    /// When a card is selected.
    /// </summary>
    public void SelectCard()
    {
        isHovered = false;
        isSelected = true;
        cardGlowRender.material.SetColor("_GlowColour", cardSelectColor);
        cardGlowRender.material.SetFloat("_GlowMultiplier", 50f);
        //StartCoroutine(AdjustCard(isSelected));

        CardGlow(isSelected);
        SoundFXManager.Instance.PlaySoundFX(SoundFXManager.Instance.cardSelect, 0.25f);
    }

    /// <summary>
    /// When a card is no longer selected.
    /// </summary>
    public void DeselectCard()
    {
        isHovered = false;
        isSelected = false;
        cardGlowRender.material.SetColor("_GlowColour", cardGlowRenderDefault.material.color);
        //StartCoroutine(AdjustCard(isSelected));
        CardGlow(isSelected);
        SoundFXManager.Instance.PlaySoundFX(SoundFXManager.Instance.cardSelect, 0.25f);
    }

    /// <summary>
    /// Sets the rendered border of a card.
    /// </summary>
    /// <param name="glow">Turns the renderer on or off based on the passed in bool.</param>
    public void CardGlow(bool glow)
    {
        cardGlowRender.enabled = glow;
    }

    public void fadeInOutBonus(bool areWeFadingIn)
    {
        Color bonCol = fingerBonus.gameObject.GetComponent<MeshRenderer>().material.color;

        if (areWeFadingIn)
        {
            StartCoroutine(fadeInBonus(bonCol));
        }
        else
        {
            StartCoroutine(fadeOutBonus(bonCol));
        }
    }

    private IEnumerator fadeInBonus(Color baseCol)
    {
        while (baseCol.a < 1)
        {
            baseCol.a += 5 * Time.deltaTime;
            fingerBonus.gameObject.GetComponent<MeshRenderer>().material.color = baseCol;
            yield return new WaitForSeconds(1);
        }
        baseCol.a = 1;
        fingerBonus.gameObject.GetComponent<MeshRenderer>().material.color = baseCol;
        yield return null;
    }

    private IEnumerator fadeOutBonus(Color baseCol)
    {
        while (baseCol.a > 0)
        {
            baseCol.a -= 5 * Time.deltaTime;
            fingerBonus.gameObject.GetComponent<MeshRenderer>().material.color = baseCol;
            yield return new WaitForSeconds(1);
        }
        baseCol.a = 0;
        fingerBonus.gameObject.GetComponent<MeshRenderer>().material.color = baseCol;
        yield return null;
    }

    /// <summary>
    /// Sets the current status of the finger bonus highlight on fingers. 
    /// </summary>
    /// <param name="player">The player that will be affected.</param>
    /// <param name="spellName">Name of the spell that has a finger bonus.</param>
    /// <param name="fingerWithBonus1">First finger required to have the bonus.</param>
    /// <param name="fingerWithBonus2">Second finger required to have the bonus.</param>
    /// <param name="bonusStatus">If the bonus is turned on or off - 1.0f ON, 0.0f OFF</param>
    private void FingerBonusFingerStatus(PlayerManager player, SpellNames spellName, PlayerFingers fingerWithBonus1, PlayerFingers fingerWithBonus2, float bonusStatus)
    {
        if (gameObject.GetComponent<SpellCard>().spellName == spellName)
        {
            if (player.AreTheseFingersAlive(fingerWithBonus1, fingerWithBonus2) && bonusStatus == 1.0f &&
        (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)fingerWithBonus1] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)fingerWithBonus1] != true) &&
        (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)fingerWithBonus2] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)fingerWithBonus2] != true))
            {
                particle.Play();
            }
            SkinnedMeshRenderer rendererL, rendererR;
            for (int i = 0; i < player.skullHands.fingers[(int)fingerWithBonus1].fingerJoints.Count; i++)
            {
                if (player == GameManager.Instance.player1)
                {
                    rendererL = player.skullHands.fingers[(int)fingerWithBonus1].fingerJoints[i].GetComponent<SkinnedMeshRenderer>();
                    rendererR = player.skullHands.fingers[(int)fingerWithBonus2].fingerJoints[i].GetComponent<SkinnedMeshRenderer>();
                }
                else
                {
                    rendererL = player.stagHands.fingers[(int)fingerWithBonus1].fingerJoints[i].GetComponent<SkinnedMeshRenderer>();
                    rendererR = player.stagHands.fingers[(int)fingerWithBonus2].fingerJoints[i].GetComponent<SkinnedMeshRenderer>();
                }

                if (rendererL != null)
                {
                    rendererL.material.SetFloat("_BonusColourOn", bonusStatus);
                }
                if (rendererR != null)
                {
                    rendererR.material.SetFloat("_BonusColourOn", bonusStatus);
                }
            }
            //}
        }
    }

    /// <summary>
    /// Sets the current status of the finger bonus on card. 
    /// </summary>
    /// <param name="player">The player that will be affected.</param>
    /// <param name="spellName">Name of the spell that has a finger bonus.</param>
    /// <param name="fingerWithBonus1">First finger required to have the bonus.</param>
    /// <param name="fingerWithBonus2">Second finger required to have the bonus.</param>
    /// <param name="bonusStatus">If the bonus is turned on or off true ON, false OFF</param>
    public void FingerBonusCardStatus(PlayerManager player, SpellNames spellName, PlayerFingers fingerWithBonus1, PlayerFingers fingerWithBonus2)
    {
        if (gameObject.GetComponent<SpellCard>().spellName == spellName)
        {
            if (player.AreTheseFingersAlive(fingerWithBonus1, fingerWithBonus2) &&
        (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)fingerWithBonus1] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)fingerWithBonus1] != true) &&
        (player.ringHandler.ringsActive[(int)Rings.SpectralChainFull, (int)fingerWithBonus2] != true) && (player.ringHandler.ringsActive[(int)Rings.SpectralChainFail, (int)fingerWithBonus2] != true))
            {
                fingerBonus.gameObject.SetActive(true);
                fingerNormal.gameObject.SetActive(false);
            }
            else
            {
                fingerBonus.gameObject.SetActive(false);
                fingerNormal.gameObject.SetActive(true);
            }
        }
    }

    //DONT DELETE THIS KELLER THIS HELPS RESET THE VALUES OF THE CARDS WHEN THEY GO BACK INTO THE OBJECT POOL
    public void OnDisable()
    {
        isHovered = false;
        isSelected = false;
        cardGlowRender.material.SetColor("_GlowColour", cardGlowRenderDefault.material.color);
        CardGlow(false);
        cardDissolve.dissolveCard = false;
        cardDissolve.RestoreCard = true;
        cardDissolve.RestoreSpellCard();
        cardDissolve.RestoreCard = false;
    }
}
