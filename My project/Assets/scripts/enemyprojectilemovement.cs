using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMovement : MonoBehaviour
{
    public float initialSpeed = 7f;
    public float acceleration = 2f;
    public float destroyTime = 5f;

    public float damage = 15f; // Damage dealt by the projectile

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * initialSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D component is missing on this projectile!");
        }

        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        if (rb != null)
        {
            // Accelerate the projectile over time
            rb.velocity += (Vector2)(transform.right * acceleration * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the projectile hits the player, deal damage
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Healthmanager.Instance != null)
            {
                Healthmanager.Instance.TakeDamage(damage);
            }
        }

        // Destroy the projectile on any collision
        Destroy(gameObject);
    }
}