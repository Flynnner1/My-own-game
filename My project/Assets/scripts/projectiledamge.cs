using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal

    // This method is called when another collider enters the trigger collider attached to the object
    void OnTriggerEnter(Collider other)
    {
        // Check if the other object is tagged as a Skeleton, Zombie, or Slime
        if (other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime") || other.CompareTag("Demon"))
        {
            Debug.Log("it can hit the skeleton, zombie or slime");
            // Try to get the EnemyHealth component from the collided object
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("the enemy is hit");
            }
        }
    }
}