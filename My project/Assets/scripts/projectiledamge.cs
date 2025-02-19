using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skeleton") || other.CompareTag("Zombie"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}