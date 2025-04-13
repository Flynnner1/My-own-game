using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float initialSpeed = 7f;
    public float acceleration = 2f;
    public float destroyTime = 5f;
    public float damageAmount = 10f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Transform playerTransform;

    public EnemyHealth EnemyHealth;

    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (playerTransform != null)
        {
            movementDirection = playerTransform.right; // Assuming the player faces right by default
            rb.velocity = movementDirection * initialSpeed;
        }
        else
        {
            Debug.LogError("Player transform is not assigned!");
        }

        // Destroy the ball after a certain time
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        if (rb != null)
        {
            // Accelerate the ball over time
            rb.velocity += movementDirection * acceleration * Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime"))
        {
            // Get the EnemyHealth component from the collided object
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                
                enemyHealth.TakeDamage(damageAmount);
            }

            // Destroy the projectile upon collision
            Destroy(gameObject);
        }
    }

}
