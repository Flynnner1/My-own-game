using System.Collections;
using UnityEngine;

// ... (rest of your script above Start) ...

[RequireComponent(typeof(Rigidbody2D))]
public class PoisonSpell : MonoBehaviour
{
    // ... (Keep your Header variables and other fields) ...
    [Header("Movement Settings")]
    public float initialSpeed = 2f;
    public float acceleration = 1f;
    public float maxLifetime = 5f;

    [Header("Damage Settings")]
    public float damageAmount = 10f;
    public float damageDuration = 3f;
    public float damageInterval = 1f;

    [Header("Effects")]
    public GameObject cloudPrefab;

    private float currentSpeed;
    private bool hasCollided = false;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Transform playerTransform;

    // Layer index to move the projectile to after hit
    private const int PostHitLayerIndex = 0; // Layer 0 is "Default"

    // ... (Your Start and FixedUpdate methods remain the same) ...
    void Start()
    {
        Debug.Log("PoisonSpell Start: Initializing...");
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;

        if (playerTransform != null)
        {
            movementDirection = playerTransform.right;
            rb.velocity = movementDirection * initialSpeed;
            Debug.Log("PoisonSpell Start: Set initial velocity based on player.");
        }
        else
        {
            Debug.LogWarning("PlayerTransform not assigned to PoisonSpell. Using default direction (Vector2.right).");
            movementDirection = Vector2.right;
            rb.velocity = movementDirection * initialSpeed;
        }

        Invoke(nameof(DestroyProjectileNoHit), maxLifetime);
        Debug.Log($"PoisonSpell Start: Invoke DestroyProjectileNoHit scheduled in {maxLifetime}s.");
    }

    void FixedUpdate()
    {
        if (rb != null && !hasCollided && rb.bodyType != RigidbodyType2D.Kinematic)
        {
            rb.velocity += movementDirection * acceleration * Time.fixedDeltaTime;
            currentSpeed = rb.velocity.magnitude;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"PoisonSpell OnTriggerEnter2D: Collided with {other.gameObject.name} (Tag: {other.tag})");

        if (hasCollided)
        {
            Debug.Log("PoisonSpell OnTriggerEnter2D: Already collided, ignoring.");
            return;
        }

        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            Debug.Log($"PoisonSpell OnTriggerEnter2D: Found EnemyHealth on {other.gameObject.name}.");
            bool isEnemyTag = other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime") || other.CompareTag("Demon");

            if (isEnemyTag)
            {
                Debug.Log($"PoisonSpell OnTriggerEnter2D: Enemy tag match on {other.gameObject.name}. Processing hit.");
                hasCollided = true;

                // --- Stop Physics Interaction ---
                // 1. Disable Collider
                Collider2D projectileCollider = GetComponent<Collider2D>();
                if (projectileCollider != null)
                {
                    projectileCollider.enabled = false;
                    Debug.Log("PoisonSpell OnTriggerEnter2D: Disabled projectile collider.");
                }

                // 2. Stop Rigidbody
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true; // Make kinematic to stop all physics influence
                    Debug.Log("PoisonSpell OnTriggerEnter2D: Stopped Rigidbody and set to Kinematic.");
                }

                // 3. Change Layer to Default (Layer 0) ***** UPDATED *****
                gameObject.layer = PostHitLayerIndex; // Directly assign layer index 0
                Debug.Log($"PoisonSpell OnTriggerEnter2D: Set GameObject layer to 'Default' (Index: {PostHitLayerIndex}).");
                // Optional: Recursively set children if needed
                // SetLayerRecursively(transform, PostHitLayerIndex);

                // --- End Stop Physics Interaction ---


                Debug.Log($"PoisonSpell OnTriggerEnter2D: Starting DealDamageOverTime coroutine for {other.gameObject.name}.");
                StartCoroutine(DealDamageOverTime(enemyHealth));
            }
            else
            {
                Debug.Log($"PoisonSpell OnTriggerEnter2D: Object {other.gameObject.name} has EnemyHealth but wrong tag ({other.tag}).");
            }
        }
        else
        {
            Debug.Log($"PoisonSpell OnTriggerEnter2D: Object {other.gameObject.name} does not have EnemyHealth component.");
        }
    }

    // ... (Your DealDamageOverTime, DestroyProjectileWithHit, DestroyProjectileNoHit methods remain the same) ...
    IEnumerator DealDamageOverTime(EnemyHealth target)
    {
        string targetName = target != null ? target.gameObject.name : "Unknown (Target became null)";
        Debug.Log($"PoisonSpell Coroutine: Starting DoT loop for {targetName}. Duration: {damageDuration}s, Interval: {damageInterval}s");

        float elapsed = 0f;
        int tickCount = 0;
        while (elapsed < damageDuration)
        {
            if (target == null)
            {
                Debug.LogWarning($"PoisonSpell Coroutine: Target ({targetName}) became null during DoT. Exiting loop.");
                break;
            }

            tickCount++;
            Debug.Log($"PoisonSpell Coroutine: Applying tick {tickCount} ({damageAmount} damage) to {target.gameObject.name}. Elapsed: {elapsed:F2}s");
            target.TakeDamage(damageAmount);

            yield return new WaitForSeconds(damageInterval);
            elapsed += damageInterval;
        }

        Debug.Log($"PoisonSpell Coroutine: DoT loop finished for {targetName}. Elapsed: {elapsed:F2}s. Calling DestroyProjectileWithHit.");
        DestroyProjectileWithHit();
    }

    void DestroyProjectileWithHit()
    {
        Debug.Log("PoisonSpell DestroyProjectileWithHit: Cancelling Invoke and destroying GameObject.");
        CancelInvoke(nameof(DestroyProjectileNoHit));
        Destroy(gameObject);
    }

    void DestroyProjectileNoHit()
    {
        if (!hasCollided)
        {
            Debug.Log($"PoisonSpell DestroyProjectileNoHit: Max lifetime ({maxLifetime}s) reached without hit. Instantiating cloud and destroying GameObject.");
            if (cloudPrefab != null)
            {
                Instantiate(cloudPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("PoisonSpell DestroyProjectileNoHit: Max lifetime reached, but already collided. Doing nothing.");
        }
    }

    // Optional helper function if you need to change children layers too
    // void SetLayerRecursively(Transform parent, int layer)
    // {
    //     parent.gameObject.layer = layer;
    //     foreach (Transform child in parent)
    //     {
    //         SetLayerRecursively(child, layer);
    //     }
    // }
}
