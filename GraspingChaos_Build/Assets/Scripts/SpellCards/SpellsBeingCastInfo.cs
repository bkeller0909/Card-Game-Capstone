using UnityEngine;

public class SpellsBeingCastInfo : MonoBehaviour
{
    [Tooltip("Who is currently casting the spell")]
    public PlayerManager whoIsCasting;
    [Tooltip("Was the spell the first one chosen, the second, or the third")]
    public int numOfSpell;
    [Tooltip("What is the spell name that is currently beiing casted")]
    public SpellCard whatSpell;
    [Tooltip("What finger is the spell being cast on")]
    public PlayerFingers whatFinger;
    [Tooltip("Who is this spell being casted on")]
    public PlayerManager whoIsBeingCastedOn;
}
