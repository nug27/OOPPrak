using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField]private int damage;
    [SerializeField] public Bullet bullet;

    // Mendapatkan damage dari AttackComponent
    public int GetDamage()
    {
        return damage;
    }

    // Method untuk menangani colliders dan pengecekan lainnya
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            // Jika tag-nya sama, tidak melakukan damage
            return;
        }
        // Mengecek apakah objek yang terkena memiliki InvincibilityComponent
        InvincibilityComponent invincibilityComponent = other.GetComponent<InvincibilityComponent>();
        if (invincibilityComponent != null && !invincibilityComponent.isInvincible)
        {
            // Jika objek memiliki InvincibilityComponent dan tidak sedang invincible,
            // mulai efek blinking pada objek tersebut
            invincibilityComponent.StartBlinkingEffect();
        }

        // Implementasi lainnya
    }
}
