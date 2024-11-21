using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    
    public void SetPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    public void Initialize()
    {
        // Set velocity of bullet to move upwards
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    // private void ReturnToPool()
    // {
    //     objectPool.Release(this);
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {

        // Return to pool upon collision with another object
        if (collision.CompareTag("Enemy") )
        {
            if(objectPool != null)
                objectPool.Release(this);
        
        }
    }

    private void OnBecameInvisible()
    {
        // Return bullet to pool when it leaves the screen
        if(objectPool != null)
            objectPool.Release(this);
    }
}