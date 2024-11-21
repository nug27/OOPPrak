using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isSpawning && spawnCount > 0)
            {
                for (int i = 0; i < defaultSpawnCount * spawnCountMultiplier; i++)
                {
                    if (spawnCount <= 0)
                    {
                        break;
                    }
                    Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
                    spawnCount--;
                    combatManager.totalEnemies++;
                    yield return new WaitForSeconds(spawnInterval);
                }
                isSpawning = false;
            }
            yield return null;
        }
    }

    public void AddKill()
    {
        totalKill++;
        totalKillWave++;
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            spawnCountMultiplier += multiplierIncreaseCount;
            totalKillWave = 0;
        }
        combatManager.onDeath();
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void IncreaseSpawnCount(int amount)
    {
        spawnCount += amount;
    }

    public void onDeath()
    {
        combatManager.onDeath();
    }
}