using System.Collections;
using UnityEngine;

//----------------------------------------------------------------
//  Author: Keller
//  Co-Author:
//  Title: DissolveObject
//  Date Created: 02/25/2025
//  Instance: No
//-----------------------------------------------------------------

/// <summary>
/// Attach to a game object to run the dissolve effect.
/// </summary> 
[RequireComponent(typeof(Renderer))]
public class DissolveObject : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        StartCoroutine(Dissolve());
    }

    /// <summary>
    /// Dissolves the game object
    /// </summary>
    /// <returns></returns>
    public IEnumerator Dissolve()
    {
        var time = Time.time * Mathf.PI * 0.25f;

        float height = transform.position.y;
        height += Mathf.Sin(time) * (objectHeight / 2.0f);
        SetHeight(height);
        yield return null;
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}
