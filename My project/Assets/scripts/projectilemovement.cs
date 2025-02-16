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

    // Define layer masks for Skeleton and Zombie
    public LayerMask skeletonLayer;
    public LayerMask zombieLayer;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is on the Skeleton or Zombie layer
        //if ((skeletonLayer == (skeletonLayer | (1 << collision.gameObject.layer))) ||
        //    (zombieLayer == (zombieLayer | (1 << collision.gameObject.layer))))
        //{
        //    Debug.Log("Projectile hit a Skeleton or Zombie. Dealing damage.");

        //    // Check if the collided object has an EnemyHealth component
        //    EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        //    if (enemyHealth != null)
        //    {
        //        // Apply damage
        //        enemyHealth.TakeDamage(damageAmount);
        //    }
        //}
        //else
        //{
        //    Debug.Log("Projectile hit something without the Skeleton or Zombie layer.");
        //}

        //// Destroy the ball upon collision
        //Destroy(gameObject);
        //void OnTriggerEnter2d(Collision collision)
        //{
        //    GameObject EnemyObject = GameObject.FindGameObjectWithTag("Skeleton");
        //    GameObject Enemy1Object = GameObject.FindGameObjectWithTag("Zombie");
        //    EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        //    if (enemyHealth != null)
        //    {
        //        // Apply damage
        //        int damageAmount = 10; // Example damage amount
        //        enemyHealth.TakeDamage(damageAmount);
        //    }

        //    // Destroy the projectile upon collision
        //    Destroy(gameObject);
        //}

    }


    // This method is called when the collider enters a trigger collider attached to another object
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the EnemyHealth component from the collided object
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Apply damage
            int damageAmount = 10; // Example damage amount
            enemyHealth.TakeDamage(damageAmount);
        }

        // Destroy the projectile upon collision
        Destroy(gameObject);
    }

}
