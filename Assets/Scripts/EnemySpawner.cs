using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; 
    public float spawnInterval = 0.2f; // Time interval between spawns (20 enemies per minute)

    private float timeUntilNextSpawn;

    void Start()
    {
        timeUntilNextSpawn = spawnInterval;
    }

    void Update()
    {
        timeUntilNextSpawn -= Time.deltaTime;

        if (timeUntilNextSpawn <= 0f)
        {
            SpawnEnemy();
            timeUntilNextSpawn = spawnInterval; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            // Instantiate the enemy prefab at the position of the spawner with no rotation
            Instantiate(enemyPrefab, new Vector3(Random.Range(-120f, 12.9f), 0.69f, Random.Range(-120f, 12.9f)), Quaternion.identity);
        }
        
    }

}
