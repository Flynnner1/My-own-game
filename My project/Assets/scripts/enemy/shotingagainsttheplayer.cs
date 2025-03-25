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
            // This value is used to determine whether to shoot rather than for projectile homing
            // (Projectile movement is handled in EnemyProjectileMovement)
        }
    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            // Check the distance again before shooting
            if (player != null && Vector3.Distance(transform.position, player.position) <= range)
            {
                // Instantiate the projectile prefab at the projectile container's position and rotation
                GameObject projectile = Instantiate(projectilePrefab, projectileContainer.position, projectileContainer.rotation);
                // (Do NOT try to assign a PlayerHealth component here since the projectile
                // should have an EnemyProjectileMovement component which handles finding the player.)

                // Set the projectile's parent to the projectile container if desired
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