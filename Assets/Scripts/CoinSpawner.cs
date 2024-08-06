using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _coinPrefab; // Reference to the enemy prefab
    public float spawnInterval = 0.02f; // Time interval between spawns (20 enemies per minute)

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
            SpawnCoin();
            timeUntilNextSpawn = spawnInterval; // Reset the timer
        }
    }

    void SpawnCoin()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(_coinPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        
        // Generate random position within viewport
        float randomX = Random.Range(-53f, 7.5f);
        float randomZ = Random.Range(-15f, 15f);
        // Convert to world coordinates
        Vector3 spawnPosition = new Vector3(randomX, 0.69f, randomZ);
        return spawnPosition;
    }

}
