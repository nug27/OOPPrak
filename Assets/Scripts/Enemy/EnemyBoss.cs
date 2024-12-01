using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 2f;
    private Vector2 direction; // Arah gerakan awal
    private Weapon weapon; // Senjata musuh
    private float shootInterval = 2f; // Interval penembakan dalam detik
    private float shootTimer;
    
    private Camera mainCamera;
    private float xMax, yMax;

    private void Start()
    {
        // Mendapatkan referensi kamera utama
        mainCamera = Camera.main;

        // Hitung batas layar berdasarkan kamera
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMax = screenBounds.x;
        yMax = screenBounds.y;

        // Tambahkan komponen Weapon ke EnemyBoss tanpa perlu WeaponPickup
        weapon = gameObject.AddComponent<Weapon>();

        // Inisialisasi arah gerakan awal
        InitializeDirection();

        // Atur posisi EnemyBoss di bagian atas layar dengan spawn acak
        Respawn();

        // Inisialisasi timer
        shootTimer = shootInterval;
    }

    private void Update()
    {
        // Gerakan horizontal bolak-balik
        transform.Translate(direction * speed * Time.deltaTime);

        // Periksa posisi dan ubah arah jika mencapai batas layar secara horizontal
        if (transform.position.x < -xMax || transform.position.x > xMax)
        {
            // Balik arah gerakan dan reposisi sedikit dari batas untuk menghindari 'stuck'
            direction = -direction;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xMax, xMax), transform.position.y);
        }

        // Update timer untuk penembakan
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootDownwards(); // Tembak ke bawah
            shootTimer = shootInterval; // Reset timer
        }
    }

    private void ShootDownwards()
    {
        weapon.Shoot();
        GameObject bullet = GameObject.FindWithTag("Bullet");
        if (bullet != null)
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(0, -5f);
            }
        }
    }

    private void Respawn()
    {
        float yPos = Random.Range(-yMax, yMax);
        if (Random.value > 0.5f)
        {
            transform.position = new Vector2(-xMax, yPos);
            direction = Vector2.right;
        }
        else
        {
            transform.position = new Vector2(xMax, yPos);
            direction = Vector2.left;
        }
    }

    private void InitializeDirection()
    {
        // Randomly choose initial direction to start moving
        direction = Random.value > 0.5f ? Vector2.right : Vector2.left;
    }
}