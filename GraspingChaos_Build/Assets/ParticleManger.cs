using UnityEngine;
using UnityEngine.VFX;

public class ParticleManger : MonoBehaviour
{

    [SerializeField]
    private VisualEffect FireBolt;

    [SerializeField]
    private VisualEffect RockThrow;

    [SerializeField]
    private VisualEffect Envy;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartParticle(int particleID)
    {
        switch (particleID)
        {
            case 0:
                FireBolt.SendEvent("OnPlay");
                break;

            case 1:
                RockThrow.SendEvent("OnPlay");
                break;

            case 2:
                Envy.SendEvent("OnPlay");
                break;
        }
    }
}
