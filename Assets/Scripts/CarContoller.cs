using UnityEngine;

public class CarController : MonoBehaviour
{
    // Brzine
    public float moveSpeed = 8f;      // Brzina napred/nazad
    public float rotationSpeed = 180f; // Brzina rotacije


    public KeyCode forwardKey = KeyCode.UpArrow;
    public KeyCode backKey = KeyCode.DownArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;

    private Rigidbody2D rb;

    void Start()
    {
        // Uzmi Rigidbody2D komponentu
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float rotateInput = 0f;
        if (Input.GetKey(leftKey)) rotateInput = 1f;
        if (Input.GetKey(rightKey)) rotateInput = -1f;

        float rotation = rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        // Kretanje
        float moveInput = 0f;
        if (Input.GetKey(forwardKey)) moveInput = 1f;
        if (Input.GetKey(backKey)) moveInput = -1f;

        Vector2 moveDirection = transform.up * moveInput * moveSpeed;
        rb.linearVelocity = moveDirection;
    }


}