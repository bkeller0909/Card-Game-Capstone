using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardHover : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float hoverSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private bool enableSpin = false;

    private float MAX_HEIGHT;   // max height of the object
    private float MIN_HEIGHT;   // min height of the object
    private float direction;

    private void Awake()
    {
        direction = -1f;
        MAX_HEIGHT = transform.position.y + 0.2f;
        MIN_HEIGHT = transform.position.y - 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > MAX_HEIGHT)
        {
            direction = -1f;
        }
        if (transform.position.y < MIN_HEIGHT)
        {
            direction = 1f;
        }
    }

    private void FixedUpdate()
    {
        HoverCard();
        if (enableSpin == true)
        {
            SpinCard();
        }
    }

    // Make the object hover up to the max height and float down to the min height
    private void HoverCard()
    {
        Vector3 dirVect;
        Vector3 newPosition = rb.position;

        dirVect.y = hoverSpeed * direction * Time.deltaTime;
        dirVect.x = 0f;
        dirVect.z = 0f;

        newPosition = newPosition + dirVect;
        rb.MovePosition(newPosition);
    }

    // spin the object continuously 360 degrees
    private void SpinCard()
    {
        Vector3 rotVect;
        Vector3 newPosition = rb.position;

        rotVect.y = rotationSpeed * direction * Time.deltaTime;
        rotVect.x = 0f;
        rotVect.z = 0f;

        newPosition = newPosition + rotVect;
        transform.Rotate(0, newPosition.y, 0);
    }
}
