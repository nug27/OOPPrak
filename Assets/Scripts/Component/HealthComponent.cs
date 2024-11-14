using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Max Health")]
    [SerializeField] private float maxHealth = 100f;  // Gunakan SerializeField untuk memberi akses pada Inspector
  // Nilai maksimum health, dapat diatur lewat Unity Inspector
    private float health;

    // Getter untuk health
    public float Health
    {
        get { return health; }
    }

    // Constructor (opsional jika ingin menambahkan inisialisasi lebih lanjut)
    void Start()
    {
        // Inisialisasi health dengan maxHealth pada saat object dimulai
        health = maxHealth;
    }

    // Setter untuk mengurangi health
    public void Subtract(float amount)
    {
        health -= amount;

        // Cek jika health kurang dari 0, maka hancurkan objek ini
        if (health <= 0)
        {  // Pastikan health tidak kurang dari 0
            Destroy(gameObject);  // Menghancurkan objek ini
        }
    }

    // Setter untuk mengatur nilai health secara langsung
    public void SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, maxHealth);  // Pastikan health berada dalam rentang 0 sampai maxHealth
    }

    // Getter untuk maxHealth
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
