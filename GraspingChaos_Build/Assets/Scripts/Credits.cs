using UnityEngine;

public class Credits : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveCreditsUp();
    }

    public void MoveCreditsUp()
    {
        rb.velocity = Vector3.up * speed;
    }
}
