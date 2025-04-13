using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLimit : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject specialEnemyPrefab;
    public GameObject SuperspecialEnemyPrefab;
    public GameObject megaspecialEnemyPrefab;
    public GameObject ultimatespecialEnemyPrefab;

    public int maxEnemies = 10;           // Maximum number of enemies allowed
    public float spawnInterval = 5f;      // Time interval between spawns
    public string enemiesTag = "";        // Tag for zombies

    public Transform player;             // Reference to the player's Transform
    public float detectionRadius = 50f;  // Distance threshold for spawning

    private float lastSpawnTime;
    private BoxCollider2D spawnArea;     // Collider to define the spawn area

    void Start()
    {
        // Initialize the last spawn time
        lastSpawnTime = Time.time;

        // Get the BoxCollider2D component attached to the GameObject
        spawnArea = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Only proceed if the player is within detectionRadius
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Check if enough time has passed since the last spawn
            if (Time.time >= lastSpawnTime + spawnInterval)
            {
                // Count the number of enemies currently in the scene
                int enemyCount = GameObject.FindGameObjectsWithTag(enemiesTag).Length;

                // Check if the number of enemies is less than the maximum allowed
                if (enemyCount < maxEnemies)
                {
                    // Spawn a new enemy at a random position within the spawn area
                    Vector3 spawnPosition = GetRandomPositionWithinSpawnArea();

                    // Generate a random number
                    int randomNumber = Random.Range(1, 551);

                    GameObject spawnedEnemy;
                    if (randomNumber <= 30)
                    {
                        spawnedEnemy = Instantiate(specialEnemyPrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("upgrade version has spawned");
                    }
                    else if (randomNumber >= 40 && randomNumber <= 50)
                    {
                        spawnedEnemy = Instantiate(SuperspecialEnemyPrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("rare version has spawned");
                    }
                    else if (randomNumber >= 80 && randomNumber <= 85)
                    {
                        spawnedEnemy = Instantiate(megaspecialEnemyPrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("legendary version has spawned");
                    }
                    else if (randomNumber == 90)
                    {
                        spawnedEnemy = Instantiate(ultimatespecialEnemyPrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("ultimate version has spawned");
                    }
                    else
                    {
                        spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("normal version has spawned");
                    }

                    // Pass the player's transform to the newly spawned enemy
                    EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.player = player;
                    }

                    // Update the last spawn time
                    lastSpawnTime = Time.time;
                }
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