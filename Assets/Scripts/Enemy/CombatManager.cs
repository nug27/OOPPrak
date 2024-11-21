using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    void Start()
    {
        StartCoroutine(ManageWaves());
    }

    private IEnumerator ManageWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveInterval);
            if (totalEnemies <= 0)
            {
                ReleaseEnemies();
            }
        }
    }

    void Update()
    {
        if (totalEnemies == 0)
        {
            timer += Time.deltaTime;
        }

        if (timer >= waveInterval && totalEnemies <= 0)
        {
            ReleaseEnemies();
            timer = 0;
        }

        Debug.Log("Wave Interval: " + waveInterval);
    }

    private void ReleaseEnemies()
    {
        waveNumber++;
        foreach (var spawner in enemySpawners)
        {
            spawner.IncreaseSpawnCount(spawner.defaultSpawnCount * spawner.spawnCountMultiplier);
            spawner.StartSpawning();
        }
    }

    public void AddKill()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.AddKill();
        }
    }

    public void onDeath()
    {
        totalEnemies--;
        Debug.Log("Total Enemies Remaining: " + totalEnemies);
    }
}