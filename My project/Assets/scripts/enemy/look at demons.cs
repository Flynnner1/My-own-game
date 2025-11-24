using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtAndMoveTowardsRandomDemon : MonoBehaviour
{
    public string enemiesTag = "Demon";
    public float moveSpeed = 5f;
    public float detectionRadius = 30f;
    public float rotationSpeed = 180f;
    public float searchInterval = 0.25f;
    public float switchTargetInterval = 3f; // Time in seconds before switching to a new target

    private Transform currentTarget;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!", this);
            this.enabled = false;
            return;
        }
        // Start the targeting coroutine
        StartCoroutine(TargetingRoutine());
    }

    void FixedUpdate()
    {
        if (currentTarget == null)
        {
            // Stop moving if there is no target
            rb.velocity = Vector2.zero;
            return;
        }

        // Move towards and rotate to face the current target
        Vector2 direction = ((Vector2)currentTarget.position - rb.position).normalized;
        rb.velocity = direction * moveSpeed;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float newAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(newAngle);
    }

    private IEnumerator TargetingRoutine()
    {
        // This loop runs indefinitely to handle target selection
        while (true)
        {
            // Find all valid targets within the detection radius
            List<Transform> validTargets = FindValidTargets();

            // --- TARGET SWITCHING LOGIC ---
            // If we have targets, pick one. If not, the target becomes null.
            SwitchTarget(validTargets);

            // Wait for the specified interval before trying to switch again
            yield return new WaitForSeconds(switchTargetInterval);
        }
    }

    private List<Transform> FindValidTargets()
    {
        // Find all GameObjects with the specified tag
        GameObject[] demons = GameObject.FindGameObjectsWithTag(enemiesTag);
        List<Transform> targetsInRange = new List<Transform>();
        float detectionRadiusSqr = detectionRadius * detectionRadius;

        foreach (GameObject demon in demons)
        {
            // Check if the demon is within the detection radius
            if ((demon.transform.position - transform.position).sqrMagnitude < detectionRadiusSqr)
            {
                targetsInRange.Add(demon.transform);
            }
        }
        return targetsInRange;
    }

    private void SwitchTarget(List<Transform> availableTargets)
    {
        // Create a list of potential new targets, excluding the current one
        List<Transform> potentialNewTargets = availableTargets.Where(t => t != currentTarget).ToList();

        if (potentialNewTargets.Count > 0)
        {
            // At least one different target is available, pick one randomly
            int newIndex = Random.Range(0, potentialNewTargets.Count);
            Transform newTarget = potentialNewTargets[newIndex];

            if (newTarget != currentTarget)
            {
                currentTarget = newTarget;
                Debug.Log(gameObject.name + " has switched to a new target: " + currentTarget.name);
            }
        }
        else if (availableTargets.Count > 0)
        {
            // No *different* target is available, but there's at least one target in range.
            // We'll just stick with the first one in the list (which might be the same as the current one).
            currentTarget = availableTargets[0];
        }
        else
        {
            // No targets are in range at all
            currentTarget = null;
        }
    }
}