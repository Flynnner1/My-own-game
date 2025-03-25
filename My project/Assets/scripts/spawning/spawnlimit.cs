using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLimit : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the default enemy prefab
    public GameObject specialEnemyPrefab;
    public GameObject SuperspecialEnemyPrefab;// Reference to the special enemy prefab
    public GameObject megaspecialEnemyPrefab;// Reference to the special enemy prefab
    public GameObject ultimatespecialEnemyPrefab;
    
    public int maxEnemies = 10; // Maximum number of enemies allowed
    public float spawnInterval = 5f; // Time interval between spawns
    public string enemiesTag = ""; // Tag for zombies
    private float lastSpawnTime;
    private BoxCollider2D spawnArea; // Collider to define the spawn area
    public EnemyHealth enemyHealth;
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
            int enemyCount = GameObject.FindGameObjectsWithTag(enemiesTag).Length;

            // Check if the number of enemies is less than the maximum allowed
            if (enemyCount < maxEnemies)
            {
                // Spawn a new enemy at a random position within the spawn area
                Vector3 spawnPosition = GetRandomPositionWithinSpawnArea();

                // Generate a random number
                int randomNumber = Random.Range(1, 551); // Generate a number between 1 and 20

                // Check if the random number is equal to 10
                if (randomNumber <= 30)
                {
                    GameObject specialenemySpawned = Instantiate(specialEnemyPrefab, spawnPosition, Quaternion.identity);
                    enemyHealth = specialenemySpawned.GetComponent<EnemyHealth>();

                    // Instantiate the special enemy at the random position
                    Debug.Log("upgrade version has spawned");
                    //enemyHealth.Addmorecoins();
                }
                else if (randomNumber >= 40 && randomNumber <=50) 
                {
                    GameObject superspecialenemySpawned = Instantiate(SuperspecialEnemyPrefab, spawnPosition, Quaternion.identity);
                    enemyHealth = superspecialenemySpawned.GetComponent<EnemyHealth>();

                    // Instantiate the default enemy at the random position
                    Debug.Log("rare version has spawned");

                }
                else if (randomNumber >= 80 && randomNumber <= 85)
                {
                    GameObject megaspecialenemySpawned = Instantiate(megaspecialEnemyPrefab, spawnPosition, Quaternion.identity);
                    enemyHealth = megaspecialenemySpawned.GetComponent<EnemyHealth>();

                   
                    Debug.Log("legandary version has spawned");

                }
                else if (randomNumber == 90 )
                {
                    GameObject ultimatespecialenemySpawned = Instantiate(ultimatespecialEnemyPrefab, spawnPosition, Quaternion.identity);
                    enemyHealth = ultimatespecialenemySpawned.GetComponent<EnemyHealth>();

                    // Instantiate the default enemy at the random position
                    Debug.Log("ultimate version has spawned");

                }
                else
                {
                    GameObject enemySpawned = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    enemyHealth = enemySpawned.GetComponent<EnemyHealth>();

                    //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    Debug.Log("normal version has spawned");

                }

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