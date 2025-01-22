using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAgainstThePlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float range = 10f; // Range within which the enemy can shoot
    public float shootingInterval = 3f; // Interval between shots
    public Transform projectileContainer; // Reference to the empty container for projectiles

    private bool isPlayerInRange = false; // To track if the player is in range

    void Start()
    {
        // Find the player object in the scene using the tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Start the shooting coroutine
        StartCoroutine(ShootAtPlayer());
    }

    void Update()
    {
        // Check if the player reference is assigned
        if (player != null)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the player is within range
            isPlayerInRange = (distanceToPlayer <= range);
        }
    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            if (isPlayerInRange)
            {
                // Instantiate the projectile prefab at the enemy's position
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

                // Set the projectile's parent to the projectile container
                if (projectileContainer != null)
                {
                    projectile.transform.parent = projectileContainer;
                }
            }

            // Wait for the shooting interval before the next shot
            yield return new WaitForSeconds(shootingInterval);
        }
    }
}