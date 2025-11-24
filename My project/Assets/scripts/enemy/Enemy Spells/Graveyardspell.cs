using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardSpell : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject specialEnemyPrefab;
    public GameObject SuperspecialEnemyPrefab;
    public GameObject megaspecialEnemyPrefab;
    public GameObject ultimatespecialEnemyPrefab;

    private float lastSpawnTime;
    public float spawnInterval = 1f;

    public int maxEnemies = 3;

    public float destroyTime = 3.3f;

    // The enemiesTag is no longer needed for counting, but might be used by other systems.
    public string enemiesTag = "";

    public Transform player;

    public Transform[] spawnPoints;
    private int nextSpawnPointIndex = 0;

    // A private list to track only the enemies spawned by this specific spell instance.
    private List<GameObject> spawnedEnemies;

    void Start()
    {
        // Initialize the list of enemies for this spell instance.
        spawnedEnemies = new List<GameObject>();

        // Set the timer to destroy this spell object. This is now only called once.
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        if (Time.time >= lastSpawnTime + spawnInterval)
        {
            // Before counting, remove any enemies from the list that may have been destroyed (e.g., by the player).
            spawnedEnemies.RemoveAll(item => item == null);

            // Count only the enemies that this instance has spawned.
            int enemyCount = spawnedEnemies.Count;

            // Check if the number of enemies is less than the maximum allowed for this spell.
            if (enemyCount < maxEnemies)
            {
                if (spawnPoints == null || spawnPoints.Length == 0)
                {
                    Debug.LogError("Spawn points are not set up in the GraveyardSpell script!");
                    return;
                }

                Vector3 spawnPosition = spawnPoints[nextSpawnPointIndex].position;
                nextSpawnPointIndex = (nextSpawnPointIndex + 1) % spawnPoints.Length;

                int randomNumber = Random.Range(1, 551);

                GameObject spawnedEnemy = null;
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

                // Add the newly created enemy to this spell's personal list.
                if (spawnedEnemy != null)
                {
                    spawnedEnemies.Add(spawnedEnemy);
                }

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