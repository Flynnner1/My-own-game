using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffDemons : MonoBehaviour
{
    public float heal = 20f; // Amount to heal the demon
    public float multi = 1.02f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Demon"))
        {
            // Access the DemonBehavior script on the collided demon and call the Buff method
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.Buff(heal, multi);
            }
        }
    }
}

