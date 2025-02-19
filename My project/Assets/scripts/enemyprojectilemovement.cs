using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMovement : MonoBehaviour
{
    public float speed = 7f; // Speed of the projectile
    public float lifetime = 1.5f; // Time before the projectile is destroyed
    public float damage = 3f; // Damage dealt by the projectile

    private Rigidbody2D rb;
    public PlayerHealth playerHealth; // Reference to the PlayerHealth component

    private Transform target; // Updated to private

    healthmanager healthmanager;
    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
            return;
        }

        // Find the player object and set it as the target
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }

        if (target == null || playerHealth == null)
        {
            Debug.LogError("Target or PlayerHealth component is not assigned and could not be found!");
            return;
        }

        // Set the velocity of the projectile
        rb.velocity = transform.right * speed;

        // Rotate the projectile to face the direction it is moving
        RotateProjectile();

        // Destroy the projectile after its lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned!");
            return;
        }

        // Move the projectile towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // If the projectile is close enough to the target, hit it
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // Apply damage to the player
            healthmanager.takeDamage(damage);
            // Destroy the projectile
            Destroy(gameObject);
        }

        // Continuously rotate the projectile to face the direction it is moving
        RotateProjectile();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has a PlayerHealth component
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Apply damage
            healthmanager.takeDamage(damage);
        }

        // Destroy the projectile upon collision
        Destroy(gameObject);
    }

    void RotateProjectile()
    {
        // Calculate the direction of the velocity
        Vector2 direction = rb.velocity;

        // Calculate the angle between the x-axis and the velocity vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the projectile to face the direction of the velocity
        rb.rotation = angle;
    }
}