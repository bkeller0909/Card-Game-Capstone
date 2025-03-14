using UnityEngine;

public class RingRotation : MonoBehaviour
{
    [SerializeField]
    float rotationsPerMinute = 10.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 6.0f * rotationsPerMinute * Time.deltaTime);
    }
}
