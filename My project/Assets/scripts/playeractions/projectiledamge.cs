using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal

    // 2D trigger method!
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is any of the enemy types.
        if (other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime") || other.CompareTag("Demon") || other.CompareTag("COW"))
        {
            Debug.Log("Projectile hit: " + other.name);

            // Try to get the EnemyHealth component.
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // If it exists, deal damage.
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log(other.name + " was hit (EnemyHealth)");
            }

            // Try to get the CowHealth component.
            
        }
    }
}