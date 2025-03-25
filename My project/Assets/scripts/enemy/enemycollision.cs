using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to the PlayerHealth component
    public float cooldownTimer = 0.5f; // Cooldown time between hits
    private float time = 0f;
    public float damage = 10f;

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
            Debug.Log("The zombie collides with the player");

            if (col.gameObject.CompareTag("Player"))
            {
                if (playerHealth != null)
                {
                    Debug.Log("ouch!");
                    Healthmanager.Instance.TakeDamage(damage); // Apply damage to the player
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