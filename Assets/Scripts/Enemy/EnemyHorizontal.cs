using System.Collections;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [Header("Movement Settings")]
    public float speed = 5f;  // Kecepatan musuh
    private bool movingRight = true;  // Menentukan apakah musuh bergerak ke kanan atau kiri
    private Vector2 screenBounds;  // Batas layar untuk deteksi

    // Start is called before the first frame update
    private void Start()
    {
          // Memanggil Start() dari kelas Enemy

        // Mendapatkan batas layar berdasarkan kamera
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Tentukan posisi spawn acak untuk EnemyHorizontal
        transform.position = GetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Move();  // Panggil fungsi Move setiap frame
        CheckBounds();  // Cek apakah musuh sudah melewati batas
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
        // Tentukan posisi spawn untuk X (ujung kiri atau kanan layar)
        float xPosition = Random.Range(-screenBounds.x + 1, screenBounds.x - 1);

        // Tentukan posisi spawn untuk Y (antara atas dan bawah layar)
        float yPosition = Random.Range(-3f, 3f);  // Memungkinkan spawn di area vertikal yang lebih luas

        return new Vector3(xPosition, yPosition, 0f);  // Kembalikan posisi spawn
    }

    // Fungsi untuk menangani respawn setelah dihancurkan
    private void Respawn()
    {
        // Tentukan posisi spawn yang baru
        transform.position = GetSpawnPosition();
    }

    // Fungsi ini akan dipanggil ketika musuh dihancurkan
    // private void OnDestroy()
    // {
    //     // Respawn musuh dengan posisi yang berbeda setelah dihancurkan
    //     Respawn();
    // }
}