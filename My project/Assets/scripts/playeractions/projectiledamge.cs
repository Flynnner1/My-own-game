using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 10;

    // 2D trigger method!
    void OnTriggerEnter2D(Collider2D other)
    {       
        if (other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime") || other.CompareTag("Demon") || other.CompareTag("Cow"))
        {
            Debug.Log("Projectile hit: " + other.name);           
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log(other.name + " was hit (EnemyHealth)");
            }
            
        }
    }
}