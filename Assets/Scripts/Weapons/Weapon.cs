using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    private void Awake()
    {
        // Initialize the object pool for bullets
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    private void FixedUpdate()
    {
    // Tambahkan delta waktu ke timer
    timer += Time.fixedDeltaTime;

    if (transform.parent == parentTransform)
    {
        return; // Jika belum diambil (masih di WeaponPickup), keluar dari metode FixedUpdate
    }
    // Cek apakah timer lebih besar atau sama dengan shootIntervalInSeconds
    if (timer >= shootIntervalInSeconds)
    {
        // Panggil fungsi Shoot dan reset timer
        timer = 0f;
        Shoot();
    }
    }


    public void Shoot()
    {
        Bullet bulletInstance = objectPool.Get();
        bulletInstance.transform.position = bulletSpawnPoint.position;
        bulletInstance.transform.rotation = bulletSpawnPoint.rotation;
        bulletInstance.Initialize(); // Initialize bullet movement
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, parentTransform);
        bulletInstance.SetPool(objectPool); // Set the object pool reference in Bullet
        return bulletInstance;
    }

    // Actions when a bullet is taken from the pool
    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    // Actions when a bullet is released back to the pool
    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // Actions when destroying pooled bullet if the pool is shrinking
    private void OnDestroyPooledObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}