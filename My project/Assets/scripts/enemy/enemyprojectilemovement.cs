using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMovement : MonoBehaviour
{
    public float initialSpeed = 7f;
    public float acceleration = 2f;
    public float destroyTime = 5f;
    public float damage = 15f; 
    private Rigidbody2D rb; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.right * initialSpeed;
        }
        Destroy(gameObject, destroyTime);
    }
    void FixedUpdate()
    {
        if (rb != null)
        {
            // Accelerate the projectile forward over time in the FixedUpdate loop (for physics)
            rb.linearVelocity += (Vector2)(transform.right * acceleration * Time.fixedDeltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Healthmanager.Instance != null)
            {
                Healthmanager.Instance.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}