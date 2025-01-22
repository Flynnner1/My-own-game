using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLimit : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public int maxEnemies = 10; // Maximum number of enemies allowed
    public float spawnInterval = 5f; // Time interval between spawns
    public string enemy;
    private float lastSpawnTime;
    private BoxCollider2D spawnArea; // Collider to define the spawn area

    void Start()
    {
        // Initialize the last spawn time
        lastSpawnTime = Time.time;

        // Get the BoxCollider2D component attached to the GameObject
        spawnArea = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Check if enough time has passed since the last spawn
        if (Time.time >= lastSpawnTime + spawnInterval)
        {
            // Count the number of enemies currently in the scene
            int enemyCount = GameObject.FindGameObjectsWithTag(enemy).Length;

            // Check if the number of enemies is less than the maximum allowed
            if (enemyCount < maxEnemies)
            {
                // Spawn a new enemy at a random position within the spawn area
                Vector3 spawnPosition = GetRandomPositionWithinSpawnArea();
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Update the last spawn time
                lastSpawnTime = Time.time;
            }
        }
    }

    Vector3 GetRandomPositionWithinSpawnArea()
    {
        // Get the bounds of the BoxCollider2D
        Bounds bounds = spawnArea.bounds;

        // Generate a random position within the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(x, y, 0);
    }
}