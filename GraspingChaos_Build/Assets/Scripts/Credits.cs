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
        float time = Time.deltaTime;

        rb.AddForce(Vector3.up * time * speed);
    }
}
