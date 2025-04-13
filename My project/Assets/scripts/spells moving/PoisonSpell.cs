using System.Collections;
using UnityEngine;

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

    private float currentSpeed;
    private bool hasCollided = false;

    void Start()
    {
        currentSpeed = initialSpeed;
        // Destroy (or finalize) the projectile if it doesn't collide within the set lifetime
        Invoke(nameof(DestroyProjectileNoHit), maxLifetime);
    }

    void Update()
    {
        // Increase speed over time
        currentSpeed += acceleration * Time.deltaTime;
        // Move forward based on current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // If we've already collided, do nothing further
        if (hasCollided) return;

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            hasCollided = true;
            // Start applying damage over time
            StartCoroutine(DealDamageOverTime(enemyHealth));
        }
    }

    IEnumerator DealDamageOverTime(EnemyHealth target)
    {
        float elapsed = 0f;
        while (elapsed < damageDuration)
        {
            target.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
            elapsed += damageInterval;
        }

        DestroyProjectileWithHit();
    }

    void DestroyProjectileWithHit()
    {
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
        // If it hasn't collided within maxLifetime, instantiate the cloud and destroy it
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