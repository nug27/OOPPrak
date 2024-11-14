using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    [Header("Movement Settings")]
    public float speed = 5f;  // Kecepatan musuh
    private bool movingRight = true;  // Menentukan apakah musuh bergerak ke kanan atau kiri
    private Vector2 screenBounds;  // Batas layar untuk deteksi

    [Header("Shooting Settings")]
    public Bullet bulletPrefab;  // Prefab peluru yang akan ditembakkan
    public Transform bulletSpawnPoint;  // Titik spawn peluru
    public float shootIntervalInSeconds = 2f;  // Interval waktu antar tembakan
    private IObjectPool<Bullet> objectPool;  // Object Pool untuk peluru
    private float shootTimer;  // Timer untuk menghitung interval tembakan

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();  // Memanggil Start() dari kelas Enemy

        // Mendapatkan batas layar berdasarkan kamera
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Inisialisasi object pool untuk peluru
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, 10, 50);

        // Tentukan posisi spawn acak untuk EnemyBoss
        transform.position = GetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Move();  // Panggil fungsi Move setiap frame
        CheckBounds();  // Cek apakah musuh sudah melewati batas

        // Tambahkan delta waktu ke timer
        shootTimer += Time.deltaTime;

        // Cek apakah sudah waktunya untuk menembak
        if (shootTimer >= shootIntervalInSeconds)
        {
            shootTimer = 0f;  // Reset timer
            Shoot();  // Panggil fungsi tembak
        }
    }

    private void Move()
    {
        // Gerakkan musuh berdasarkan arah
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void CheckBounds()
    {
        // Cek apakah musuh telah melewati batas layar, jika ya, lakukan respawn di ujung layar
        if (transform.position.x >= screenBounds.x && movingRight)
        {
            // Respawn ke sisi kiri layar
            transform.position = GetSpawnPosition();
            movingRight = false; // Ubah arah ke kiri
        }
        else if (transform.position.x <= -screenBounds.x && !movingRight)
        {
            // Respawn ke sisi kanan layar
            transform.position = GetSpawnPosition();
            movingRight = true; // Ubah arah ke kanan
        }
    }

    // Fungsi untuk menentukan posisi spawn musuh di ujung layar
    private Vector3 GetSpawnPosition()
    {
        // Tentukan sisi spawn secara acak (kiri atau kanan)
        bool spawnOnLeft = Random.value > 0.5f;

        // Tentukan posisi spawn untuk X (ujung kiri atau kanan layar)
        float xPosition = spawnOnLeft ? -screenBounds.x + 1 : screenBounds.x - 1;

        // Tentukan posisi spawn untuk Y (antara atas dan bawah layar)
        float yPosition = -1;

        return new Vector3(xPosition, yPosition, 0f);  // Kembalikan posisi spawn
    }

    // Fungsi untuk menembakkan peluru
    private void Shoot()
    {
        Bullet bulletInstance = objectPool.Get();  // Ambil peluru dari pool
        bulletInstance.transform.position = bulletSpawnPoint.position;  // Tentukan posisi spawn peluru
        bulletInstance.transform.rotation = bulletSpawnPoint.rotation;  // Tentukan rotasi spawn peluru
        bulletInstance.Initialize();  // Inisialisasi pergerakan peluru
    }

    // Fungsi untuk membuat peluru baru
    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        return bulletInstance;
    }

    // Fungsi ketika peluru diambil dari pool
    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    // Fungsi ketika peluru dikembalikan ke pool
    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // Fungsi ketika peluru dihancurkan (misalnya saat pool sedang mengecil)
    private void OnDestroyPooledObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
