/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Title:        Enum Glossary
//  Date Created: 01/07/2025
//  Purpose:      This is to hold each type of enum created as a type of glossory
//  Instance?     Technicly Yes
//-----------------------------------------------------------------
/// </summary>

public enum PlayerFingers
{
    LH_Pinky = 0,
    LH_Ring = 1,
    LH_Middle = 2,
    LH_Index = 3,
    LH_Thumb = 4,
    RH_Pinky = 9,
    RH_Ring = 8,
    RH_Middle = 7,
    RH_Index = 6,
    RH_Thumb = 5,
    none = 15
}

public enum PlayerHands
{
    SkullHands = 0,
    StagHands
}

public enum SpellType
{
    ATTACK = 0,
    RESTORATION = 1,
    RING = 2
}

public enum PlayerType
{
    PLAYER1 = 0,
    PLAYER2 = 1,
    None = 300
}

public enum QTEButtonType
{
    Y = 0,
    X = 1,
    B = 2,
    A = 3,
    UP = 4,
    LEFT = 5,
    RIGHT = 6,
    DOWN = 7
}

public enum SpellNames
{
    none = 27,

    #region Attack Spells
    FireBolt = 0,
    Rockthrow,
    RighteousEnvy,
    LefteousEnvy,
    Icicles,
    CollectorsCurse,
    StaticBlast,
    Quake,
    RightingBolt,
    LeftningBolt,
    TidalWave,
    PointerOfDeath,
    #endregion // Attack Spells

    #region Restoration Spells
    ForTheCause,
    QuickHeal,
    ThumbsUp,
    LifeDrain,
    CursedConversion,
    GreenThumb,
    Materialise,
    EchoingMana,
    #endregion // Restoration Spells

    #region Ring Spells
    ThornsOfAgony,
    GuardiansTouch,
    SpectralChain,
    ManaMerchant,
    VengefulMirror,
    VampiricSurge,
    VeilOfFortitude
    #endregion // Ring Spells
}

public enum Decider
{
    none = 0,
    PlayerOneIsFaster = 1,
    PlayerTwoIsFaster = 2,
    Tie,
    NoSpellsChosen
}


public enum QTEOUTCOMES
{
    none = 0,
    Failure = 1,
    Half = 2,
    Success = 3,
}