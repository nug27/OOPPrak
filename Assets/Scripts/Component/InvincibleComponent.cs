using System.Collections;
using UnityEngine;

public class InvincibilityComponent : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [SerializeField] private int blinkingCount = 7;  // Jumlah blinking
    [SerializeField] private float blinkInterval = 0.1f;  // Interval antara blinking
    [SerializeField] private Material blinkMaterial;  // Material yang digunakan saat blinking

    #endregion

    #region Private Fields

    private SpriteRenderer spriteRenderer;  // Referensi ke SpriteRenderer
    private Material originalMaterial;  // Material asli objek
    public bool isInvincible = false;  // Status invincible

    #endregion

    #endregion

    #region Methods

    #region Unity Callbacks

    // Start is called before the first frame update
    void Start()
    {
        // Mendapatkan SpriteRenderer yang digunakan untuk mengganti material
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on this GameObject!");
        }

        // Mendapatkan material yang digunakan SpriteRenderer saat ini
        originalMaterial = spriteRenderer.material;
    }

    #endregion

    // Coroutine untuk membuat efek blinking
    private IEnumerator BlinkingRoutine()
    {
        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;  // Ganti material ke blinkMaterial
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.material = originalMaterial;  // Kembalikan material asli
            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false;  // Set status invincible ke false setelah selesai blinking
    }

    // Method untuk memulai efek blinking dari kelas lain
    public void StartBlinkingEffect()
    {
        if (!isInvincible)
        {
            isInvincible = true;  // Set objek menjadi invincible
            StartCoroutine(BlinkingRoutine());  // Mulai coroutine blinking
        }
    }

    #endregion
}
