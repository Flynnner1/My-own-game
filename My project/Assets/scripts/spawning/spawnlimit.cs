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

    public int maxEnemies = 10;           
    public float spawnInterval = 5f;     
    public string enemiesTag = "";        

    public Transform player;             
    public float detectionRadius = 50f;  

    private float lastSpawnTime;
    private BoxCollider2D spawnArea;     

    void Start()
    {
        lastSpawnTime = Time.time;
        spawnArea = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //start spawning enemies when the player is close
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            if (Time.time >= lastSpawnTime + spawnInterval)
            {
                int enemyCount = GameObject.FindGameObjectsWithTag(enemiesTag).Length;
                if (enemyCount < maxEnemies)
                {
                    Vector3 spawnPosition = GetRandomPositionWithinSpawnArea();
                    int randomNumber = Random.Range(1, 551);
                    GameObject spawnedEnemy;
                    switch (randomNumber)
                    {
                        case int n when n <= 30:
                            spawnedEnemy = Instantiate(specialEnemyPrefab, spawnPosition, Quaternion.identity);
                            break;

                        case int n when n >= 40 && n <= 50:
                            spawnedEnemy = Instantiate(SuperspecialEnemyPrefab, spawnPosition, Quaternion.identity);
                            break;

                        case int n when n >= 80 && n <= 85:
                            spawnedEnemy = Instantiate(megaspecialEnemyPrefab, spawnPosition, Quaternion.identity);
                            break;

                        case 90:
                            spawnedEnemy = Instantiate(ultimatespecialEnemyPrefab, spawnPosition, Quaternion.identity);
                            break;

                        default:
                            spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                            break;
                    }
                    // give the player's transform to the newly spawned enemy
                    EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.player = player;
                    }
                    lastSpawnTime = Time.time;
                }
            }
        }
    }
    Vector3 GetRandomPositionWithinSpawnArea()
    {
        // get the size of the spawn area
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0);
    }
}