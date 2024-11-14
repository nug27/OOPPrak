using System.Collections;
using UnityEngine;

public class EnemyForward : Enemy
{
    [Header("Movement Settings")]
    public float speed = 5f;  // Kecepatan musuh
    private bool movingDown = true;  // Menentukan apakah musuh bergerak ke bawah atau ke atas
    private Vector2 screenBounds;  // Batas layar untuk deteksi

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();  // Memanggil Start() dari kelas Enemy

        // Mendapatkan batas layar berdasarkan kamera
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Tentukan posisi spawn acak untuk EnemyForward (selalu di atas layar)
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
        // Gerakkan musuh berdasarkan arah vertikal
        if (movingDown)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void CheckBounds()
    {
        // Cek apakah musuh telah melewati batas layar, jika ya, respawn ke posisi baru
        if (transform.position.y <= -screenBounds.y && movingDown)
        {
            Respawn();  // Panggil Respawn jika musuh telah melewati batas bawah layar
        }
    }

    // Fungsi untuk menentukan posisi spawn musuh di ujung atas layar
    private Vector3 GetSpawnPosition()
    {
        // Tentukan posisi spawn untuk Y (selalu di ujung atas layar)
        float yPosition = screenBounds.y;  // Spawn di atas layar

        // Tentukan posisi spawn untuk X (acak antara kiri dan kanan layar)
        float xPosition = Random.Range(-screenBounds.x, screenBounds.x);  // Posisi X acak di sepanjang layar

        return new Vector3(xPosition, yPosition, 0f);  // Kembalikan posisi spawn
    }

    // Fungsi untuk melakukan respawn musuh pada posisi baru
    private void Respawn()
    {
        // Tentukan posisi spawn yang baru
        transform.position = GetSpawnPosition();
    }

    // Fungsi ini dipanggil ketika objek dihancurkan
    private void OnDestroy()
    {
        // Setelah musuh dihancurkan, lakukan respawn pada posisi baru
        Respawn();
    }
}
