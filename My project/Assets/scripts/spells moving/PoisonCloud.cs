using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using static UnityEngine.GraphicsBuffer; // This line is likely unnecessary unless you use GraphicsBuffer directly

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PoisonCloud : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 5f;
    public float damageInterval = 2f;

    [Header("Targeting")]
    public List<string> targetTags = new List<string> { "Player", "Skeleton", "Slime", "Zombie", "Demon", "Npc", "COW" };

    private List<GameObject> targetsInCloud = new List<GameObject>();

    void Start()
    {
        // REMOVED: Healthmanager playerHealth = target.GetComponent<Healthmanager>();
        // REMOVED: PlayerHealth enemyhealth = target.GetComponent<PlayerHealth>();

        // Setup Collider and Rigidbody
        Collider2D col = GetComponent<Collider2D>();
        if (!col.isTrigger) { col.isTrigger = true; }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        // Start damage ticks and self-destruction
        InvokeRepeating(nameof(ApplyDamageToTargets), 0f, damageInterval);
        Destroy(gameObject, 7f);
        Debug.Log($"PoisonCloud {gameObject.name}: Activated. Destroying in 7 seconds. Damage: {damageAmount} every {damageInterval}s.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetTags.Contains(other.tag))
        {
            // Check if it has a component capable of taking damage
            // *** Use the correct health scripts here ***
            // This checks if the entering object has EITHER PlayerHealth OR EnemyHealth
            bool canTakeDamage = other.GetComponent<PlayerHealth>() != null || other.GetComponent<EnemyHealth>() != null;

            if (canTakeDamage && !targetsInCloud.Contains(other.gameObject))
            {
                targetsInCloud.Add(other.gameObject);
                Debug.Log($"PoisonCloud: {other.gameObject.name} entered the cloud.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (targetsInCloud.Contains(other.gameObject))
        {
            targetsInCloud.Remove(other.gameObject);
            Debug.Log($"PoisonCloud: {other.gameObject.name} left the cloud.");
        }
    }

    void ApplyDamageToTargets()
    {
        for (int i = targetsInCloud.Count - 1; i >= 0; i--)
        {
            GameObject target = targetsInCloud[i];

            if (target == null)
            {
                targetsInCloud.RemoveAt(i);
                continue;
            }

            // --- Integration Point ---
            // **Make this consistent with OnTriggerEnter2D**
            // If your player uses the PlayerHealth script:
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            else
            {
                // If not PlayerHealth, check for EnemyHealth
                EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);
                }
                else
                {
                    // This case should ideally not be reached if OnTriggerEnter2D is working correctly
                    Debug.LogWarning($"PoisonCloud: Target {target.name} is in the list but has no PlayerHealth or EnemyHealth component.", target);
                    targetsInCloud.RemoveAt(i);
                }
            }
            // --- End Integration Point ---
        }
    }

    void OnDestroy()
    {
        CancelInvoke(nameof(ApplyDamageToTargets));
        Debug.Log($"PoisonCloud {gameObject.name}: Destroyed.");
    }
}