using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsGO;
    [SerializeField] private GameObject backIcon;
    [SerializeField] private float creditSpeed;

    private Vector3 creditsStartPos;
    private Rigidbody rb;
    private bool wait = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        backIcon.SetActive(false);
        creditsStartPos = gameObject.transform.position;
    }

    private void Update()
    {
        MoveCreditsUp();
        StartCoroutine(BackIcon());

        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            gameObject.transform.position = creditsStartPos;
            mainMenu.SetActive(true);
            creditsGO.SetActive(false);
            backIcon.SetActive(false);
            wait = true;
        }
    }

    private void MoveCreditsUp()
    {
        rb.velocity = Vector3.up * creditSpeed;
    }

    private IEnumerator BackIcon()
    {
        if (wait == true)
        {
            yield return new WaitForSeconds(5f);
            backIcon.SetActive(true);
            wait = false;
        }
    }
}
