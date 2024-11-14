using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Objek Weapon yang menerima objek secara serialized
    private Weapon weapon; // Objek Weapon yang akan mendapatkan nilai dari weaponHolder
    private static Weapon currentWeapon; // Menyimpan referensi ke senjata aktif saat ini

    // Awake: Menginisialisasi weapon dengan nilai dari weaponHolder
    void Awake()
    {
        if (weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder); // Membuat instance baru dari weaponHolder
        }
    }

    // Start: Jika weapon tidak null, inisialisasi semua method terkait dengan nilai false
    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false); // Menonaktifkan visual awal weapon
            weapon.transform.SetParent(transform, false);
            weapon.transform.localPosition = transform.position; // Set posisi lokal ke nol
            weapon.parentTransform = transform;
        }
    }

    // OnTriggerEnter2D: Jika collider yang terkena adalah player, set parent transform dari weapon ke player
    void OnTriggerEnter2D(Collider2D other) 
    { 
        if (weapon != null && other.gameObject.CompareTag("Player")) 
        {
            Transform playerTransform = other.transform;  
            if (currentWeapon != null) 
            { 
                currentWeapon.gameObject.SetActive(false);
            } 
            currentWeapon = weapon; 
            weapon.transform.SetParent(playerTransform); 
            weapon.transform.localPosition = new Vector3(0, 0, 0);
            TurnVisual(true, weapon);
            // gameObject.SetActive(false); 
        }
    }
    // TurnVisual: Method untuk mengaktifkan atau menonaktifkan semua komponen yang relevan dalam objek weapon
    private void TurnVisual(bool isEnabled)
    {
        if (weapon == null) return;

        // Mengatur Renderer (contoh: MeshRenderer atau SpriteRenderer)
        foreach (var renderer in weapon.GetComponents<Renderer>())
        {
            renderer.enabled = isEnabled; // Aktifkan/nonaktifkan renderer
        }

        // Mengatur Collider
        foreach (var collider in weapon.GetComponents<Collider>())
        {
            collider.enabled = isEnabled; // Aktifkan/nonaktifkan collider
        }

        // Jika ada komponen lain yang perlu dikelola, tambahkan di sini
    }

    // Overload TurnVisual: Polimorfisme untuk objek Weapon spesifik
    private void TurnVisual(bool isEnabled, Weapon specificWeapon)
    {
        foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
        {
            component.enabled = isEnabled;
        }

        // Mengatur Renderer (contoh: MeshRenderer atau SpriteRenderer)
        foreach (var renderer in specificWeapon.GetComponents<Renderer>())
        {
            renderer.enabled = isEnabled; // Aktifkan/nonaktifkan renderer
        }

        // Mengatur Collider
        foreach (var collider in specificWeapon.GetComponents<Collider>())
        {
            collider.enabled = isEnabled; // Aktifkan/nonaktifkan collider
        }

        // Jika ada komponen lain yang perlu dikelola, tambahkan di sini
    }
}
