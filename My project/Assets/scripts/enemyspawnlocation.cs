using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLocation : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnInterval = 10f; // Time interval between spawns
    public int gridSize = 30; // Size of the grid

    void Start()
    {
        // Start the repeated spawning of enemies
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Generate random x and y coordinates within the grid
        float x = Random.Range(-gridSize / 2, gridSize / 2);
        float y = Random.Range(-gridSize / 2, gridSize / 2);

        // Create a random position within the grid
        Vector3 spawnPosition = new Vector3(x, y, 0);

        // Instantiate the enemy at the random position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}