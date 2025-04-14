using System.Collections;
using UnityEngine;

// Require Rigidbody2D to ensure it exists
[RequireComponent(typeof(Rigidbody2D))]
public class PoisonSpell : MonoBehaviour
{
    [Header("Movement Settings")]
    public float initialSpeed = 2f;
    public float acceleration = 1f;
    public float maxLifetime = 5f; // After this time, it destroys itself if it hasn't collided

    [Header("Damage Settings")]
    public float damageAmount = 10f;   // Damage per hit interval
    public float damageDuration = 3f;  // How long the damage lasts in seconds
    public float damageInterval = 1f;  // How often damage is applied (seconds)

    [Header("Effects")]
    public GameObject cloudPrefab;     // Instantiated if no collision occurs before maxLifetime

    private float currentSpeed; // Keep track for potential future use, but velocity handles movement
    private bool hasCollided = false;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Transform playerTransform; // Assign the player's transform in the Inspector

    // Removed unused class-level EnemyHealth variable

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed; // Initialize currentSpeed, though velocity is primary driver

        // Ensure playerTransform is assigned before using it
        if (playerTransform != null)
        {
            // Set initial direction based on player's facing direction (assuming right is forward)
            movementDirection = playerTransform.right;
            rb.velocity = movementDirection * initialSpeed;
        }
        else
        {
            // Default direction if playerTransform isn't set (e.g., forward in world space)
            Debug.LogWarning("PlayerTransform not assigned to PoisonSpell. Using default direction (Vector2.right).");
            movementDirection = Vector2.right;
            rb.velocity = movementDirection * initialSpeed;
        }

        // Destroy (or finalize) the projectile if it doesn't collide within the set lifetime
        // Using Invoke is fine, or you could use a coroutine timer
        Invoke(nameof(DestroyProjectileNoHit), maxLifetime);
    }

    void FixedUpdate() // Use FixedUpdate for physics calculations
    {
        // Only accelerate if the Rigidbody exists and we haven't collided
        if (rb != null && !hasCollided)
        {
            // Accelerate the projectile over time using velocity
            // Note: Directly adding velocity continuously can lead to very high speeds.
            // Consider capping the speed or using AddForce for more controlled acceleration.
            rb.velocity += movementDirection * acceleration * Time.fixedDeltaTime;

            // Optional: Update currentSpeed if needed elsewhere, based on velocity magnitude
            currentSpeed = rb.velocity.magnitude;
        }
    }

    // Use OnTriggerEnter2D for 2D collisions
    void OnTriggerEnter2D(Collider2D other)
    {
        // If we've already collided and started the damage process, do nothing further
        if (hasCollided) return;

        // Check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Check if the collider belongs to an enemy tag (optional, but good practice)
            if (other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime")) // Add other enemy tags if needed
            {
                hasCollided = true; // Mark as collided to stop movement/further checks

                // Disable the projectile's collider so it doesn't hit multiple things
                Collider2D projectileCollider = GetComponent<Collider2D>();
                if (projectileCollider != null)
                {
                    projectileCollider.enabled = false;
                }

                // Stop the projectile's movement
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true; // Stop physics interactions
                }

                // Start applying damage over time
                StartCoroutine(DealDamageOverTime(enemyHealth));

                // --- IMPORTANT: Do NOT destroy the GameObject here ---
                // The coroutine will handle destruction after the DoT effect.
            }
        }
        // Optional: Add collision logic for other object types (e.g., walls) here if needed
        // else if (other.CompareTag("Wall")) { ... DestroyProjectileWithHit(); ... }
    }

    IEnumerator DealDamageOverTime(EnemyHealth target)
    {
        float elapsed = 0f;
        while (elapsed < damageDuration && target != null) // Check if target still exists
        {
            target.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
            elapsed += damageInterval;
        }

        // Effect finished, now destroy the projectile
        DestroyProjectileWithHit();
    }

    void DestroyProjectileWithHit()
    {
        // Cancel the timed destruction in case it was hit just before maxLifetime
        CancelInvoke(nameof(DestroyProjectileNoHit));

        // Optionally instantiate a cloud or an effect upon successful collision
        // (Uncomment if you also want a cloud to appear when you do hit something)
        // if (cloudPrefab != null)
        // {
        //     Instantiate(cloudPrefab, transform.position, Quaternion.identity);
        // }

        Destroy(gameObject);
    }

    void DestroyProjectileNoHit()
    {
        // This is called by Invoke if maxLifetime is reached without a collision
        // Check !hasCollided again just to be safe
        if (!hasCollided)
        {
            if (cloudPrefab != null)
            {
                Instantiate(cloudPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}