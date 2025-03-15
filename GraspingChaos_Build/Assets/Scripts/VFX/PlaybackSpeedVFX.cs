using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class NewBehaviourScript : MonoBehaviour
{
    public VisualEffect vfx;

    [Range(0, 1)] public float playRate;

    private void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        vfx.playRate = playRate;
    }
}
