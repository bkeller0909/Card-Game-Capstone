using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Player Manager
//  Date Created: 01/07/2025
//  Purpose:      This is to manage everything that envolves a player and their stats
//  Instance?     no
//-----------------------------------------------------------------
/// </summary>
public class PlayerManager : MonoBehaviour
{
    // The players input system
    [HideInInspector]
    public PlayerInput playerInput;
    public Gamepad gamepad;
    public PlayerHealth health;

    [HideInInspector]
    public PlayerCameraHandler cameraHandler;

    //debug utilty value, just to reflect current health
    public TMP_Text healthValue;

    [Tooltip("The players hp as a whole")]
    public int entireHP;
    [Tooltip("The players mana value")]
    public int Mana;
    [Tooltip("The players Spell Hand")]
    public SpellHand spellHand;

    [Tooltip("The players Ring handler, deals with everything ring related to this player")]
    public RingsHandler ringHandler;

    [Tooltip("What number of player they are")]
    [SerializeField] public PlayerType playerNum;
    [Tooltip("List of fingers which act as columns, each containing the finger joint game objects.")]
    public List<Fingers> fingers;
    [Tooltip("This is the fingers the other player sees")]
    public List<Fingers> visualFingers;

    [Tooltip("What number of player they are")]
    public List<VisualEffect> spellEffects;

    [Tooltip("damage tracker of each player per round")]
    public int DamageTrackedPerTurn;

    [Tooltip("check for curse convertion on dealing state")]
    public bool SucessfulCursedConversion;

    [Tooltip("Check for if the player played cursed Convertion")]
    public bool PlayedCursedConvertion = false;

    [Tooltip("mana tracker of each player per round")]
    public int ManaTrackedPerTurn;

    [Tooltip("Check for if the player played Echoing Mana")]
    public bool SucessfulEchoingMana;

    [Tooltip("Check for if the player played Echoing Mana")]
    public bool PlayedEchoingMana = false;

    public bool playerOneHands = false;

    public int cardsAmountSelected = 0;

    public int attackCardAmount = 0;
    public int restCardAmount = 0;
    public int ringCardAmount = 0;

    [Header("Mana")]
    [Tooltip("The mana value that is shown to the players")]
    public ManaVisual manaVisual;
    public TMP_Text manaText;

    public HandsHandler skullHands, stagHands;

    public List<Fingers> healthyFingers;

    public CameraPositionChange playerCameras;

    [SerializeField]
    public VisualEffect eyes;

    private void Awake()
    {
        if (playerNum == PlayerType.PLAYER1)
        {
            GameManager.Instance.player1 = this;
        }
        else if (playerNum == PlayerType.PLAYER2)
        {
            GameManager.Instance.player2 = this;
        }
        fingers = new List<Fingers>();
        visualFingers = new List<Fingers>();
        healthyFingers = new List<Fingers>();
        eyes.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gamepad = playerInput.GetDevice<Gamepad>();
        ringHandler = gameObject.GetComponent<RingsHandler>();
        cameraHandler = GetComponent<PlayerCameraHandler>();

        if (playerNum == PlayerType.PLAYER1)
        {
            foreach (Fingers finger in skullHands.fingers)
            {
                fingers.Add(finger);
            }
            foreach (Fingers finger in stagHands.fingers)
            {
                visualFingers.Add(finger);
            }
        }
        else if (playerNum == PlayerType.PLAYER2)
        {
            foreach (Fingers finger in stagHands.fingers)
            {
                fingers.Add(finger);
            }
            foreach (Fingers finger in skullHands.fingers)
            {
                visualFingers.Add(finger);
            }
        }

        gameObject.GetComponent<SelectableFinger>().SetUpAuras();

        spellHand = new SpellHand();
        health = new PlayerHealth();
        health.SetUpHealth(this);

        entireHP = 28;

    }

    // Update is called once per frame
    void Update()
    {
        manaVisual.desiredManaStep = Mana;
        manaText.text = Mana.ToString();
    }

    public void GetMana(int manaAmount)
    {
        Mana += manaAmount;
        if (Mana > 12)
        {
            Mana = 12;
        }
    }

    public void LoseMana(int manaAmount)
    {
        Mana -= manaAmount;
        if (Mana < 0)
        {
            Mana = 0;
        }
    }

    public int TrackMana()
    {
        if (!PlayedEchoingMana)
        {
            //anything but sucess
            ManaTrackedPerTurn = ManaTrackedPerTurn / 2;
            return ManaTrackedPerTurn;
        }
        else
        {
            //on success
            return ManaTrackedPerTurn;
        }
    }

    public void TrackDamage()
    {
        if (!SucessfulCursedConversion)
        {
            //anything but success
            DamageTrackedPerTurn = DamageTrackedPerTurn / 2;
            Mana += DamageTrackedPerTurn;
            if (Mana > 12)
            {
                Mana = 12;
            }
        }
        else
        {
            //on success
            Mana += DamageTrackedPerTurn;
            if (Mana > 12)
            {
                Mana = 12;
            }
        }
    }

    public PlayerFingers GetRandomFingersForQuake(PlayerFingers exception, PlayerFingers exception2)
    {
        int rand = 0;
        if (healthyFingers.Count != 0)
        {
            healthyFingers.Clear();
        }

        for (int i = 0; i < fingers.Count; i++)
        {
            if (health.playerHealthStats[i] != 0)
            {
                healthyFingers.Add(fingers[i]);
            }
        }

        if (exception != PlayerFingers.none)
        {
            for (int i = 0; i < healthyFingers.Count; i++)
            {
                //only if you found the finger eception or if the amount of fingers possible to hit is higher than 1 (otherwise it might break)
                if (healthyFingers[i].finger == exception && healthyFingers.Count > 1)
                {
                    healthyFingers.RemoveAt(i);
                    break;
                }
            }
        }

        if (exception2 != PlayerFingers.none)
        {
            for (int i = 0; i < healthyFingers.Count; i++)
            {
                //only if you found the finger eception or if the amount of fingers possible to hit is higher than 1 (otherwise it might break)
                if (healthyFingers[i].finger == exception && healthyFingers.Count > 1)
                {
                    healthyFingers.RemoveAt(i);
                    break;
                }
            }
        }

        if (healthyFingers.Count != 0)
        {
            rand = UnityEngine.Random.Range(0, healthyFingers.Count);
            return healthyFingers[rand].finger;
        }

        return PlayerFingers.none;
    }


    public PlayerFingers GetRandomFinger(PlayerFingers exception)
    {
        int rand = 0;
        if (healthyFingers.Count != 0)
        {
            healthyFingers.Clear();
        }

        for (int i = 0; i < fingers.Count; i++)
        {
            if (health.playerHealthStats[i] != 0)
            {
                healthyFingers.Add(fingers[i]);
            }
        }

        if (exception != PlayerFingers.none)
        {
            for (int i = 0; i < healthyFingers.Count; i++)
            {
                //only if you found the finger eception or if the amount of fingers possible to hit is higher than 1 (otherwise it might break)
                if (healthyFingers[i].finger == exception && healthyFingers.Count > 1)
                {
                    healthyFingers.RemoveAt(i);
                    break;
                }
            }
        }

        if (healthyFingers.Count != 0)
        {
            rand = UnityEngine.Random.Range(0, healthyFingers.Count);
            return healthyFingers[rand].finger;
        }

        return PlayerFingers.none;

    }

    public PlayerFingers GetAdjacentFingerLeft(PlayerFingers selectedFinger)
    {
        int newFingerValue = (int)selectedFinger - 1;
        return (PlayerFingers)newFingerValue;
    }

    public PlayerFingers GetAdjacentFingerRight(PlayerFingers selectedFinger)
    {
        int newFingerValue = (int)selectedFinger + 1;
        return (PlayerFingers)newFingerValue;
    }

    public int GetLeftHandFingerDeath()
    {
        PlayerFingers currentFingers = PlayerFingers.LH_Pinky;
        int damageDealt = 0;
        for (int i = 0; i < 5; i++)
        {
            if (currentFingers == PlayerFingers.LH_Pinky)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Ring;
            }
            else if (currentFingers == PlayerFingers.LH_Ring)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Middle;
            }
            else if (currentFingers == PlayerFingers.LH_Middle)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Index;
            }
            else if (currentFingers == PlayerFingers.LH_Index)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.LH_Thumb;
            }
            else if (currentFingers == PlayerFingers.LH_Thumb)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
            }
        }
        return damageDealt;
    }

    public int GetRightHandFingerDeath()
    {
        PlayerFingers currentFingers = PlayerFingers.RH_Pinky;
        int damageDealt = 0;
        for (int i = 0; i < 5; i++)
        {
            if (currentFingers == PlayerFingers.RH_Pinky)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Ring;
            }
            else if (currentFingers == PlayerFingers.RH_Ring)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Middle;
            }
            else if (currentFingers == PlayerFingers.RH_Middle)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Index;
            }
            else if (currentFingers == PlayerFingers.RH_Index)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
                currentFingers = PlayerFingers.RH_Thumb;
            }
            else if (currentFingers == PlayerFingers.RH_Thumb)
            {
                if (health.playerHealthStats[(int)currentFingers] == 0)
                {
                    damageDealt += 1;
                }
            }
        }
        return damageDealt;
    }

    public void ToggleRing(bool turnOn, Rings whatRing, PlayerFingers whatFinger)
    {
        fingers[(int)whatFinger].rings[(int)whatRing].gameObject.SetActive(turnOn);
        visualFingers[(int)whatFinger].rings[(int)whatRing].gameObject.SetActive(turnOn);

        if (turnOn)
        {
            if ((Rings)whatRing == Rings.GuardiansTouchFull || (Rings)whatRing == Rings.GuardiansTouchFail)
            {
                ringHandler.EffectGuardiansTouch(whatFinger);
            }
            else if ((Rings)whatRing == Rings.ManaMerchantFull)
            {
                ringHandler.manaMerchantSuccess = true;
            }
            else if ((Rings)whatRing == Rings.ManaMerchantFail)
            {
                ringHandler.manaMerchantFailure = true;
            }
        }
        else if (!turnOn)
        {
            if ((Rings)whatRing == Rings.GuardiansTouchFull || (Rings)whatRing == Rings.GuardiansTouchFail)
            {
                ringHandler.GuardiansTouchStolenOrOver(whatFinger);
            }
            else if ((Rings)whatRing == Rings.ManaMerchantFull)
            {
                ringHandler.manaMerchantSuccess = false;
            }
            else if ((Rings)whatRing == Rings.ManaMerchantFail)
            {
                ringHandler.manaMerchantFailure = false;
            }
            else if ((Rings)whatRing == Rings.VeilOfFortitudeFull || (Rings)whatRing == Rings.VeilOfFortitudeFail)
            {
                ringHandler.veilOfFortitudeLeft = false;
                ringHandler.veilOfFortitudeRight = false;
            }
        }
    }

    public bool AreTheseFingersAlive(PlayerFingers f1, PlayerFingers f2)
    {
        if (health.getFingerHealth(f1) >= 1 && health.getFingerHealth(f2) >= 1)
        {
            return true;
        }
        return false;
    }
}
