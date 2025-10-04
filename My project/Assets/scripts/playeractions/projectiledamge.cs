using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal

    // 2D trigger method!
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime") || other.CompareTag("Demon") || other.CompareTag("Cow"))
        {
            Debug.Log("Projectile hit: " + other.name);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            CowHealth cowHealth = other.GetComponent<CowHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("the enemy is hit (EnemyHealth)");
            }
            if (cowHealth != null)
            {
                cowHealth.TakeDamage(damageAmount);
                Debug.Log("the cow is hit (CowHealth)");
            }
        }
    }
}