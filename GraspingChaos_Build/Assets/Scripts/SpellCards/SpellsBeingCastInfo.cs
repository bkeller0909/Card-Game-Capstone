using UnityEngine;

public class SpellsBeingCastInfo : MonoBehaviour
{
    [Tooltip("Who is currently casting the spell")]
    public PlayerManager whoIsCasting;
    [Tooltip("What is the spell name that is currently beiing casted")]
    public SpellNames whatSpellName;
    [Tooltip("What finger is the spell being cast on")]
    public PlayerFingers whatFinger;
    [Tooltip("Who is this spell being casted on")]
    public PlayerManager whoIsBeingCastedOn;
}
