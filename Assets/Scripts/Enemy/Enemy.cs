using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int level;  // Level dari enemy

    // Start is called before the first frame update
    public void Start()
    {
        // Menetapkan sprite ke Renderer

        // Menonaktifkan pengaruh gravitasi dengan mengatur Gravity Scale menjadi 0
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;  // Tidak terpengaruh gravitasi
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Logika dasar atau pengecekan level dapat ditambahkan di sini (jika perlu)
    }
}
