using System.Collections;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f;  // Kecepatan musuh
    private Transform player;  // Transform pemain
    private Vector2 direction;  // Arah pergerakan musuh

    // Start is called before the first frame update
    new void Start()
    {
        // Mendapatkan referensi ke transform pemain
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Jika pemain ditemukan
        if (player != null)
        {
            MoveTowardsPlayer();  // Panggil fungsi untuk bergerak menuju pemain
        }
    }

    private void MoveTowardsPlayer()
    {
        // Hitung arah menuju pemain
        direction = (player.position - transform.position).normalized;

        // Gerakkan musuh menuju pemain
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Deteksi jika musuh bersentuhan dengan pemain
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pastikan bahwa objek yang bersentuhan adalah pemain
        if (collision.gameObject.CompareTag("Player"))
        {
            // Jika bertabrakan dengan pemain, musuh mati
            Destroy(gameObject);  // Hapus objek musuh
        }
    }
}
