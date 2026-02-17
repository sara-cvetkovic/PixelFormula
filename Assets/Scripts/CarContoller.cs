using UnityEngine;

public class CarController : MonoBehaviour
{
    // Brzine
    public float moveSpeed = 5f;      // Brzina napred/nazad
    public float rotationSpeed = 200f; // Brzina rotacije

    private Rigidbody2D rb;

    void Start()
    {
        // Uzmi Rigidbody2D komponentu
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log("Update radi!");

        // Proveri input za Player 1 (strelice)
        float moveInput = Input.GetAxis("Vertical");     // ↑↓
        float rotateInput = Input.GetAxis("Horizontal"); // ←→

        Debug.Log("Move input: " + moveInput);

        // Rotacija
        float rotation = -rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        // Kretanje
        Vector2 moveDirection = transform.up * moveInput * moveSpeed;
        rb.linearVelocity = moveDirection;
    }
}