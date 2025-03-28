using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerState : AdvancedFSM
{

    public PlayerManager player, enemy;
    public CardPlay cardPlay;
    public Camera playerCam;
    public Animator camAnim;
    public Tutorial tutorialEvent;
    public DialogueEvent dialogueEvent;
    public bool cardSelected, cardDeselected, readyToCast, canChooseFinger;
    public bool fingerSelected;
    public bool finishedCurrentQTE;
    public int currentQTEAmount;
    [SerializeField]
    public TMP_Text test_text;

    public bool debugActive;

    public GameObject finishedCastingImage;

    public SpellNames currentSpellName;
    public PlayerFingers currentFingerName;

    [SerializeField] public CardHandSlot playerHand;

    [SerializeField] public Canvas howToPlay;

    [SerializeField] public ControlSpellBook spellBook;

    [SerializeField] public List<GameObject> prompts;

    [SerializeField] public PlayerControlHandler playerControlHandler;

    private string GetStateString()
    {
        string state = "NONE";
        if (CurrentState.ID == FSMStateID.Defualt)
        {
            state = "Defualt";
        }
        else if (CurrentState.ID == FSMStateID.DealStats)
        {
            state = "Dealing Stats";
        }
        else if (CurrentState.ID == FSMStateID.ChoosingSpells)
        {
            state = "Choosing Spells";
        }
        else if (CurrentState.ID == FSMStateID.Deciding)
        {
            state = "Deciding";
        }
        else if (CurrentState.ID == FSMStateID.QTE)
        {
            state = "QTE";
        }
        else if (CurrentState.ID == FSMStateID.Dead)
        {
            state = "Dead";
        }
        ///////////////////////////////////////////////////
        // Spell Casting
        else if (CurrentState.ID == FSMStateID.A_FireBolt)
        {
            state = "Casting FireBolt";
        }
        else if (CurrentState.ID == FSMStateID.A_RockThrow)
        {
            state = "Casting Rock Throw";
        }
        else if (CurrentState.ID == FSMStateID.A_RighteousEnvy)
        {
            state = "Casting Righteous Envy";
        }
        else if (CurrentState.ID == FSMStateID.A_LefteousEnvy)
        {
            state = "Casting Lefteous Envy";
        }
        else if (CurrentState.ID == FSMStateID.A_Icicles)
        {
            state = "Casting Icicles";
        }
        else if (CurrentState.ID == FSMStateID.A_CollectorsCurse)
        {
            state = "Casting Collectors Curse";
        }
        else if (CurrentState.ID == FSMStateID.A_StaticBlast)
        {
            state = "Casting Static Blast";
        }
        else if (CurrentState.ID == FSMStateID.A_RightningBolt)
        {
            state = "Casting Rightning Bolt";
        }
        else if (CurrentState.ID == FSMStateID.A_LeftningBolt)
        {
            state = "Casting Leftning Bolt";
        }
        else if (CurrentState.ID == FSMStateID.A_TidalWave)
        {
            state = "Casting TidalWave";
        }
        else if (CurrentState.ID == FSMStateID.A_PointerOfDeath)
        {
            state = "Casting Pointer Of Death";
        }
        else if (CurrentState.ID == FSMStateID.A_Quake)
        {
            state = "Casting Quake";
        }
        else if (CurrentState.ID == FSMStateID.Re_ForTheCause)
        {
            state = "Casting For The Cause";
        }
        else if (CurrentState.ID == FSMStateID.Re_QuickHeal)
        {
            state = "Casting Quick Heal";
        }
        else if (CurrentState.ID == FSMStateID.Re_ThumbsUp)
        {
            state = "Casting Thumbs Up";
        }
        else if (CurrentState.ID == FSMStateID.Re_LifeDrain)
        {
            state = "Casting Life Drain";
        }
        else if (CurrentState.ID == FSMStateID.Re_CursedConversion)
        {
            state = "Casting Cursed Conversion";
        }
        else if (CurrentState.ID == FSMStateID.Re_GreenThumb)
        {
            state = "Casting Green Thumb";
        }
        else if (CurrentState.ID == FSMStateID.Re_Materialise)
        {
            state = "Casting Materialise";
        }
        else if (CurrentState.ID == FSMStateID.Re_EchoingMana)
        {
            state = "Casting Echoing Mana";
        }
        else if (CurrentState.ID == FSMStateID.Ri_ThornsOfAgony)
        {
            state = "Casting Thorns Of Agony";
        }
        else if (CurrentState.ID == FSMStateID.Ri_GuardiansTouch)
        {
            state = "Casting Guardians Touch";
        }
        else if (CurrentState.ID == FSMStateID.Ri_SpectralChain)
        {
            state = "Casting Spectral Chain";
        }
        else if (CurrentState.ID == FSMStateID.Ri_ManaMerchant)
        {
            state = "Casting Mana Merchant";
        }
        else if (CurrentState.ID == FSMStateID.Ri_VengefulMirror)
        {
            state = "Casting Vengeful Mirror";
        }
        else if (CurrentState.ID == FSMStateID.Ri_VampiricSurge)
        {
            state = "Casting Vampiric Surge";
        }
        else if (CurrentState.ID == FSMStateID.Ri_VeilOfFortitude)
        {
            state = "Casting Veil Of Fortitude";
        }
        return state;
    }

    protected override void Initialize()
    {
        if (debugActive)
        {
            test_text.gameObject.SetActive(true);
        }
        else
        {
            test_text.gameObject.SetActive(false);
        }

        finishedCurrentQTE = false;

        player = this.gameObject.GetComponent<PlayerManager>();

        cardPlay = GetComponentInChildren<CardPlay>();

        cardSelected = false;

        canChooseFinger = false;

        finishedCastingImage.SetActive(false);

        playerCam = player.gameObject.GetComponentInChildren<Camera>();
        camAnim = playerCam.GetComponent<Animator>();

        if (player.playerNum == PlayerType.PLAYER1)
        {
            enemy = GameManager.Instance.player2;
        }
        else if (player.playerNum == PlayerType.PLAYER2)
        {
            enemy = GameManager.Instance.player1;
        }

        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        if (CurrentState == null)
        {
            return;
        }

        CurrentState.Reason(player, enemy);
        CurrentState.Act(player, enemy);

        test_text.text = "State: " + GetStateString();
    }

    private void ConstructFSM()
    {
        DefualtState defualt = new DefualtState(this);
        defualt.AddTransition(Transition.Start, FSMStateID.DealStats);

        DealStatsState dealStats = new DealStatsState(this);
        dealStats.AddTransition(Transition.StatsGained, FSMStateID.ChoosingSpells);

        #region Deciding State
        DecidingState deciding = new DecidingState(this);
        deciding.AddTransition(Transition.died, FSMStateID.Dead);
        deciding.AddTransition(Transition.EndOfRound, FSMStateID.DealStats);
        deciding.AddTransition(Transition.Challenge, FSMStateID.QTE);
        // Casting Attack Spell State transitions
        deciding.AddTransition(Transition.TA_FireBolt, FSMStateID.A_FireBolt);
        deciding.AddTransition(Transition.TA_RockThrow, FSMStateID.A_RockThrow);
        deciding.AddTransition(Transition.TA_RighteousEnvy, FSMStateID.A_RighteousEnvy);
        deciding.AddTransition(Transition.TA_LefteousEnvy, FSMStateID.A_LefteousEnvy);
        deciding.AddTransition(Transition.TA_Icicles, FSMStateID.A_Icicles);
        deciding.AddTransition(Transition.TA_CollectorsCurse, FSMStateID.A_CollectorsCurse);
        deciding.AddTransition(Transition.TA_StaticBlast, FSMStateID.A_StaticBlast);
        deciding.AddTransition(Transition.TA_RightningBolt, FSMStateID.A_RightningBolt);
        deciding.AddTransition(Transition.TA_LeftningBolt, FSMStateID.A_LeftningBolt);
        deciding.AddTransition(Transition.TA_TidalWave, FSMStateID.A_TidalWave);
        deciding.AddTransition(Transition.TA_PointerOfDeath, FSMStateID.A_PointerOfDeath);
        deciding.AddTransition(Transition.TA_Quake, FSMStateID.A_Quake);
        // Casting Restoration Spell State transitions
        deciding.AddTransition(Transition.TRe_ForTheCause, FSMStateID.Re_ForTheCause);
        deciding.AddTransition(Transition.TRe_QuickHeal, FSMStateID.Re_QuickHeal);
        deciding.AddTransition(Transition.TRe_ThumbsUp, FSMStateID.Re_ThumbsUp);
        deciding.AddTransition(Transition.TRe_LifeDrain, FSMStateID.Re_LifeDrain);
        deciding.AddTransition(Transition.TRe_CursedConversion, FSMStateID.Re_CursedConversion);
        deciding.AddTransition(Transition.TRe_GreenThumb, FSMStateID.Re_GreenThumb);
        deciding.AddTransition(Transition.TRe_Materialise, FSMStateID.Re_Materialise);
        deciding.AddTransition(Transition.TRe_EchoingMana, FSMStateID.Re_EchoingMana);
        // Casting Ring Spell State transitions
        deciding.AddTransition(Transition.TRi_ThornsOfAgony, FSMStateID.Ri_ThornsOfAgony);
        deciding.AddTransition(Transition.TRi_GuardiansTouch, FSMStateID.Ri_GuardiansTouch);
        deciding.AddTransition(Transition.TRi_SpectralChain, FSMStateID.Ri_SpectralChain);
        deciding.AddTransition(Transition.TRi_ManaMerchant, FSMStateID.Ri_ManaMerchant);
        deciding.AddTransition(Transition.TRi_VengefulMirror, FSMStateID.Ri_VengefulMirror);
        deciding.AddTransition(Transition.TRi_VampiricSurge, FSMStateID.Ri_VampiricSurge);
        deciding.AddTransition(Transition.TRi_VeilOfFortitude, FSMStateID.Ri_VeilOfFortitude);
        #endregion

        ChoosingSpellsState choosingSpells = new ChoosingSpellsState(this);
        choosingSpells.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);

        #region QTE State
        QTEState qte = new QTEState(this);
        qte.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        // Casting Attack Spell State transitions
        qte.AddTransition(Transition.TA_FireBolt, FSMStateID.A_FireBolt);
        qte.AddTransition(Transition.TA_RockThrow, FSMStateID.A_RockThrow);
        qte.AddTransition(Transition.TA_RighteousEnvy, FSMStateID.A_RighteousEnvy);
        qte.AddTransition(Transition.TA_LefteousEnvy, FSMStateID.A_LefteousEnvy);
        qte.AddTransition(Transition.TA_Icicles, FSMStateID.A_Icicles);
        qte.AddTransition(Transition.TA_CollectorsCurse, FSMStateID.A_CollectorsCurse);
        qte.AddTransition(Transition.TA_StaticBlast, FSMStateID.A_StaticBlast);
        qte.AddTransition(Transition.TA_RightningBolt, FSMStateID.A_RightningBolt);
        qte.AddTransition(Transition.TA_LeftningBolt, FSMStateID.A_LeftningBolt);
        qte.AddTransition(Transition.TA_TidalWave, FSMStateID.A_TidalWave);
        qte.AddTransition(Transition.TA_PointerOfDeath, FSMStateID.A_PointerOfDeath);
        qte.AddTransition(Transition.TA_Quake, FSMStateID.A_Quake);
        // Casting Restoration Spell State transitions
        qte.AddTransition(Transition.TRe_ForTheCause, FSMStateID.Re_ForTheCause);
        qte.AddTransition(Transition.TRe_QuickHeal, FSMStateID.Re_QuickHeal);
        qte.AddTransition(Transition.TRe_ThumbsUp, FSMStateID.Re_ThumbsUp);
        qte.AddTransition(Transition.TRe_LifeDrain, FSMStateID.Re_LifeDrain);
        qte.AddTransition(Transition.TRe_CursedConversion, FSMStateID.Re_CursedConversion);
        qte.AddTransition(Transition.TRe_GreenThumb, FSMStateID.Re_GreenThumb);
        qte.AddTransition(Transition.TRe_Materialise, FSMStateID.Re_Materialise);
        qte.AddTransition(Transition.TRe_EchoingMana, FSMStateID.Re_EchoingMana);
        // Casting Ring Spell State transitions
        qte.AddTransition(Transition.TRi_ThornsOfAgony, FSMStateID.Ri_ThornsOfAgony);
        qte.AddTransition(Transition.TRi_GuardiansTouch, FSMStateID.Ri_GuardiansTouch);
        qte.AddTransition(Transition.TRi_SpectralChain, FSMStateID.Ri_SpectralChain);
        qte.AddTransition(Transition.TRi_ManaMerchant, FSMStateID.Ri_ManaMerchant);
        qte.AddTransition(Transition.TRi_VengefulMirror, FSMStateID.Ri_VengefulMirror);
        qte.AddTransition(Transition.TRi_VampiricSurge, FSMStateID.Ri_VampiricSurge);
        qte.AddTransition(Transition.TRi_VeilOfFortitude, FSMStateID.Ri_VeilOfFortitude);
        #endregion

        DeadState dead = new DeadState(this);

        #region Attack Spell States
        //////////////////////////////////////////////////////////////////////////////
        //Attack Spell States
        FireBoltState fireBolt = new FireBoltState(this);
        fireBolt.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        fireBolt.AddTransition(Transition.Challenge, FSMStateID.QTE);

        RockThrowState rockThrow = new RockThrowState(this);
        rockThrow.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        rockThrow.AddTransition(Transition.Challenge, FSMStateID.QTE);

        RighteousEnvyState righteousEnvy = new RighteousEnvyState(this);
        righteousEnvy.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        righteousEnvy.AddTransition(Transition.Challenge, FSMStateID.QTE);

        LefteousEnvyState lefteousEnvy = new LefteousEnvyState(this);
        lefteousEnvy.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        lefteousEnvy.AddTransition(Transition.Challenge, FSMStateID.QTE);

        IciclesState icicles = new IciclesState(this);
        icicles.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        icicles.AddTransition(Transition.Challenge, FSMStateID.QTE);

        CollectorsCurseState collectorsCurse = new CollectorsCurseState(this);
        collectorsCurse.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        collectorsCurse.AddTransition(Transition.Challenge, FSMStateID.QTE);

        StaticBlastState staticBlast = new StaticBlastState(this);
        staticBlast.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        staticBlast.AddTransition(Transition.Challenge, FSMStateID.QTE);

        RightningBoltState rightningBolt = new RightningBoltState(this);
        rightningBolt.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        rightningBolt.AddTransition(Transition.Challenge, FSMStateID.QTE);

        LeftningBoltState leftningBolt = new LeftningBoltState(this);
        leftningBolt.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        leftningBolt.AddTransition(Transition.Challenge, FSMStateID.QTE);

        TidalWaveState tidalWave = new TidalWaveState(this);
        tidalWave.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        tidalWave.AddTransition(Transition.Challenge, FSMStateID.QTE);

        PointerOfDeathState pointerOfDeath = new PointerOfDeathState(this);
        pointerOfDeath.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        pointerOfDeath.AddTransition(Transition.Challenge, FSMStateID.QTE);

        QuakeState quake = new QuakeState(this);
        quake.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        quake.AddTransition(Transition.Challenge, FSMStateID.QTE);
        #endregion

        #region Restoration Spell States
        //////////////////////////////////////////////////////////////////////////////
        //Restoration Spell States
        ForTheCauseState forTheCause = new ForTheCauseState(this);
        forTheCause.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        forTheCause.AddTransition(Transition.Challenge, FSMStateID.QTE);

        QuickHealState quickHeal = new QuickHealState(this);
        quickHeal.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        quickHeal.AddTransition(Transition.Challenge, FSMStateID.QTE);

        ThumbsUpState thumbsUp = new ThumbsUpState(this);
        thumbsUp.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        thumbsUp.AddTransition(Transition.Challenge, FSMStateID.QTE);

        LifeDrainState lifeDrain = new LifeDrainState(this);
        lifeDrain.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        lifeDrain.AddTransition(Transition.Challenge, FSMStateID.QTE);

        CursedConversionState cursedConversion = new CursedConversionState(this);
        cursedConversion.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        cursedConversion.AddTransition(Transition.Challenge, FSMStateID.QTE);

        GreenThumbState greenThumb = new GreenThumbState(this);
        greenThumb.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        greenThumb.AddTransition(Transition.Challenge, FSMStateID.QTE);

        MaterialiseState materialise = new MaterialiseState(this);
        materialise.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        materialise.AddTransition(Transition.Challenge, FSMStateID.QTE);

        EchoingManaState echoingMana = new EchoingManaState(this);
        echoingMana.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        echoingMana.AddTransition(Transition.Challenge, FSMStateID.QTE);
        #endregion

        #region Ring Spell States
        //////////////////////////////////////////////////////////////////////////////
        //Ring Spell States
        ThornsOfAgonyState thornsOfAgony = new ThornsOfAgonyState(this);
        thornsOfAgony.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        thornsOfAgony.AddTransition(Transition.Challenge, FSMStateID.QTE);

        GuardiansTouchState guardiansTouch = new GuardiansTouchState(this);
        guardiansTouch.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        guardiansTouch.AddTransition(Transition.Challenge, FSMStateID.QTE);

        SpectralChainState spectralChain = new SpectralChainState(this);
        spectralChain.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        spectralChain.AddTransition(Transition.Challenge, FSMStateID.QTE);

        ManaMerchantState manaMerchant = new ManaMerchantState(this);
        manaMerchant.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        manaMerchant.AddTransition(Transition.Challenge, FSMStateID.QTE);

        VengefulMirrorState vengefulMirror = new VengefulMirrorState(this);
        vengefulMirror.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        vengefulMirror.AddTransition(Transition.Challenge, FSMStateID.QTE);

        VampiricSurgeState vampiricSurge = new VampiricSurgeState(this);
        vampiricSurge.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        vampiricSurge.AddTransition(Transition.Challenge, FSMStateID.QTE);

        VeilOfFortitudeState veilOfFortitude = new VeilOfFortitudeState(this);
        veilOfFortitude.AddTransition(Transition.NeedDecision, FSMStateID.Deciding);
        veilOfFortitude.AddTransition(Transition.Challenge, FSMStateID.QTE);
        #endregion

        //Add States to the state list
        AddFSMState(defualt);
        AddFSMState(dealStats);
        AddFSMState(deciding);
        AddFSMState(choosingSpells);
        AddFSMState(qte);
        AddFSMState(dead);
        AddFSMState(fireBolt);
        AddFSMState(rockThrow);
        AddFSMState(righteousEnvy);
        AddFSMState(lefteousEnvy);
        AddFSMState(icicles);
        AddFSMState(collectorsCurse);
        AddFSMState(rightningBolt);
        AddFSMState(leftningBolt);
        AddFSMState(staticBlast);
        AddFSMState(tidalWave);
        AddFSMState(pointerOfDeath);
        AddFSMState(quake);
        AddFSMState(forTheCause);
        AddFSMState(quickHeal);
        AddFSMState(thumbsUp);
        AddFSMState(lifeDrain);
        AddFSMState(cursedConversion);
        AddFSMState(greenThumb);
        AddFSMState(materialise);
        AddFSMState(echoingMana);
        AddFSMState(thornsOfAgony);
        AddFSMState(guardiansTouch);
        AddFSMState(spectralChain);
        AddFSMState(manaMerchant);
        AddFSMState(vengefulMirror);
        AddFSMState(vampiricSurge);
        AddFSMState(veilOfFortitude);
    }

    public void CardHasBeenSelected()
    {
        cardSelected = true;
    }

    public void FingerHasBeenSelected()
    {
        fingerSelected = true;
    }

    public void CardHasBeenDeselected()
    {
        cardDeselected = true;
    }
    public void ReadyToCast()
    {
        readyToCast = true;
    }

    public void StartQTECreation(int currentQTEAmount, PlayerManager player)
    {
        StartCoroutine(QTECreation(currentQTEAmount, player));
    }

    IEnumerator QTECreation(int currentQTEAmount, PlayerManager player)
    {
        player.playerInput.SwitchCurrentActionMap("QTE");
        player.GetComponentInChildren<CameraPositionChange>().GetInputForced(3);
        player.gameObject.GetComponent<QTEHandler>().countDownObject.SetActive(true);
        player.gameObject.GetComponent<QTEHandler>().CountDownManager(3);
        yield return new WaitForSecondsRealtime(1);
        player.gameObject.GetComponent<QTEHandler>().CountDownManager(2);
        yield return new WaitForSecondsRealtime(1);
        player.gameObject.GetComponent<QTEHandler>().CountDownManager(1);
        yield return new WaitForSecondsRealtime(1);
        player.gameObject.GetComponent<QTEHandler>().CountDownManager(4);
        yield return new WaitForSecondsRealtime(0.5f);
        player.gameObject.GetComponent<QTEHandler>().countDownObject.SetActive(false);
        if (player.gameObject.GetComponent<QTEHandler>().mashing)
        {
            player.gameObject.GetComponent<QTEHandler>().MashingTest(7, player);
        }
        else
        {
            player.gameObject.GetComponent<QTEHandler>().CreateSequence(currentQTEAmount, player);
        }
    }
}
