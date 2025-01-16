using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleManger : MonoBehaviour
{

    [SerializeField]
    private List<VisualEffect> effects;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartParticle(int particleID)
    {
        effects[particleID].SendEvent("OnPlay");
    }
}
