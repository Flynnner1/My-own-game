using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float initialSpeed = 7f;
    public float acceleration = 2f;
    public float destroyTime = 5f;
    public float damageAmount = 10f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Transform playerTransform;

    public EnemyHealth EnemyHealth;
    public NpcHealth npcHealth;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (playerTransform != null)
        {
            movementDirection = playerTransform.right;
            rb.linearVelocity = movementDirection * initialSpeed;
        }
        else
        {
            Debug.LogError("Player transform is not assigned!");
        }

        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        if (rb != null)
        {
            rb.linearVelocity += movementDirection * acceleration * Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skeleton") || other.CompareTag("Zombie") || other.CompareTag("Slime") || other.CompareTag("Demon") || other.CompareTag("Npc") || other.CompareTag("COW"))
        {
            //Debug.Log("Projectile hit: " + other.name);
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

            NpcHealth npcHealth = other.gameObject.GetComponent<NpcHealth>();
            if (npcHealth != null)
            {
                npcHealth.TakeDamage(damageAmount);
            }

            CowHealth cowHealth = other.gameObject.GetComponent<CowHealth>();
            if (cowHealth != null)
            {
                cowHealth.TakeDamage(damageAmount);
                //Debug.Log(other.name + " was hit (CowHealth)");
            }

            Destroy(gameObject);
        }
    }
}
