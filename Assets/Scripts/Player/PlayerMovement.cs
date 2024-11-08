using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed = new Vector2(5f, 5f);
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1f, 1f);
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 stopClamp = new Vector2(0.1f, 0.1f);

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        // Mengambil informasi dari Rigidbody2D dan menyimpannya di rb
        rb = GetComponent<Rigidbody2D>();

        // Mengambil informasi dari Camera utama
        mainCamera = Camera.main;

        // Menghitung screenBounds yang benar berdasarkan ukuran layar dan kamera
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z * -1));
        
        // Kalkulasi awal untuk moveVelocity, moveFriction, dan stopFriction
        moveVelocity = new Vector2(
            2 * maxSpeed.x / timeToFullSpeed.x,
            2 * maxSpeed.y / timeToFullSpeed.y
        );

        moveFriction = new Vector2(
            2 * (-maxSpeed.x) / (timeToFullSpeed.x * timeToFullSpeed.x),
            2 * (-maxSpeed.y) / (timeToFullSpeed.y * timeToFullSpeed.y)
        );

        stopFriction = new Vector2(
            2 * (-maxSpeed.x) / (timeToStop.x * timeToStop.x),
            2 * (-maxSpeed.y) / (timeToStop.y * timeToStop.y)
        );
    }

    public void Move()
    {
        // Capture input for movement
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Calculate target velocity based on input
        Vector2 targetVelocity = moveDirection * maxSpeed;

        if (moveDirection != Vector2.zero)
        {
            // Gradually increase velocity towards target velocity
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, maxSpeed.magnitude * Time.fixedDeltaTime);
        }
        else
        {
            // Apply friction to gradually slow down the player
            float frictionFactor = timeToStop.x * 500;
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, frictionFactor * Time.fixedDeltaTime);

            // Stop movement if velocity is very low to avoid sliding
            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }

        // Clamp the velocity separately on each axis to avoid excessive speed
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        // Panggil fungsi moveBound untuk membatasi pergerakan
        moveBound();
    }

    public void moveBound()
    {
        // Membatasi posisi Player berdasarkan batas kamera
        Vector3 pos = transform.position;
        float boundaryOffsetx = 0.3f;
        float boundaryOffsetatas = 0.8f;

        // Batasi pada sumbu X dan Y sesuai screenBounds
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + boundaryOffsetx, screenBounds.x - boundaryOffsetx);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y - boundaryOffsetatas);

        // Update posisi Player
        transform.position = pos;
    }

    public bool IsMoving()
    {
        // Mengembalikan true jika Player bergerak, dan false jika tidak
        return rb.velocity != Vector2.zero;
    }
}
