using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int layerMask; // Set this to the player's layer in the inspector
    public PlayerHealth playerHealth; // Reference to the PlayerHealth component
    public float cooldownTimer = 3f; // Cooldown time between hits
    private float time = 0f;

    healthmanager healthmanager;
    void Start()
    {
        // Try to find the PlayerHealth component in the scene
        if (playerHealth == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerHealth = playerObject.GetComponent<PlayerHealth>();
            }

            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component is not assigned and could not be found!");
            }
        }
    }

    void Update()
    {
        // Increment the time
        time += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (time >= cooldownTimer)
        {
            Debug.Log("Hit");

            if (col.gameObject.layer == layerMask)
            {
                if (playerHealth != null)
                {
                    healthmanager.takeDamage(10f); // Apply damage to the player
                    Debug.Log("Hit Player");
                }
                else
                {
                    Debug.LogError("PlayerHealth component is missing!");
                }

                // Reset the cooldown timer
                time = 0f;
            }
        }
    }
}