using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtAndMoveTowardsClosestDemon : MonoBehaviour
{
    // The target we are currently looking at and moving towards
    private Transform targetDemon;

    // How fast the object moves towards the target
    public float speed = 5f;

    // Offset to adjust the rotation (if the sprite isn't aligned with default orientation)
    public float rotationOffset = 0f;

    // How often to search for the closest demon (in seconds)
    public float searchInterval = 0.25f;

    void Start()
    {
        // Repeatedly call the FindClosestDemon method
        InvokeRepeating("FindClosestDemon", 0f, searchInterval);
    }

    void FindClosestDemon()
    {
        // Find all game objects with the "Demon" tag
        GameObject[] demons = GameObject.FindGameObjectsWithTag("Demon");

        float closestDistance = Mathf.Infinity;
        GameObject closestDemonObject = null;

        // Loop through all found demons to find the closest one
        foreach (GameObject demon in demons)
        {
            float distance = Vector3.Distance(transform.position, demon.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDemonObject = demon;
            }
        }

        // If a closest demon was found, set it as the new target
        if (closestDemonObject != null)
        {
            targetDemon = closestDemonObject.transform;
        }
        else
        {
            // If no demons are left, there is no target
            targetDemon = null;
        }
    }

    void Update()
    {
        // If we have a valid target, rotate and move towards it
        if (targetDemon != null)
        {
            // --- ROTATION LOGIC (same as before) ---
            Vector3 direction = targetDemon.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + rotationOffset));

            // --- MOVEMENT LOGIC (newly added) ---
            // Move our position a step closer to the target.
            // The step size is equal to speed * Time.deltaTime.
            transform.position = Vector3.MoveTowards(transform.position, targetDemon.position, speed * Time.deltaTime);
        }
    }
}