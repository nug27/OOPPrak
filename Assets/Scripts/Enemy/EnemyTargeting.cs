using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 3f;
    private Transform player;

    private void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player's position
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the enemy upon collision with the player
        if (collision.CompareTag("Player"))
        { // Alternatively, you can pool this object instead
        }
    }
}