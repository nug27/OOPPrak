using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed; // Seberapa cepat Portal Asteroid bergerak
    [SerializeField] private float rotateSpeed; // Seberapa cepat Portal Asteroid berputar
    Vector3 newPosition; // Posisi yang dapat di-travel oleh asteroid
    

    void Start()
    {
        // Inisialisasi nilai newPosition
        ChangePosition();
    }

    void Update()
    {
        // Cek jarak antara posisi asteroid saat ini dengan posisi newPosition
        if (Vector3.Distance(transform.position, newPosition) < 0.5f)
        {
            // Buat posisi baru dengan ChangePosition
            ChangePosition();
        }
        else
        {
            // Gerakkan asteroid menuju newPosition
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            // Rotasikan asteroid
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }

        // Cek apakah Player memiliki weapon
        if (GameObject.Find("Player").GetComponentInChildren<Weapon>() != null)
        {
            // Tampilkan asteroid dan collidernya
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            // Sembunyikan asteroid dan collidernya
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Load scene Main using GameManager's LevelManager
            if (GameManager.Instance != null && GameManager.Instance.LevelManager != null)
            {
                GameManager.Instance.LevelManager.LoadScene("Main");
            }
            else
            {
                Debug.LogError("GameManager or LevelManager not found in the scene!");
            }
        }
    }

    void ChangePosition()
    {
        // Masukkan nilai random kepada newPosition agar asteroid dapat bergerak secara dinamik
        newPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
    }
}
