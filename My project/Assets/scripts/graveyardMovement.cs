using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graveyardMovement : MonoBehaviour
{
    public float initialSpeed = 7f;
    public float acceleration = 2f;
    public float destroyTime = 3.3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Set the initial forward velocity
            rb.velocity = transform.right * initialSpeed;

            // --- THIS IS THE KEY CHANGE ---
            // Freeze the rotation so the object doesn't spin
            rb.freezeRotation = true;
        }
        else
        {
            Debug.LogError("Rigidbody2D component is missing on this projectile!");
        }

        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        if (rb != null)
        {
            // Accelerate the projectile over time
            rb.velocity += (Vector2)(transform.right * acceleration * Time.deltaTime);
        }
    }
}