using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealth : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public float health = 100f;
    public float agresion = 99f;
    public float peaceful = 100f;
    public bool fighting = false;

    public Transform player;

    public float speed = 5f; // Speed of the movement
    public float stopRange = 30f; // Range within which the object will move towards the player

    public float time = 0f;
    public float time1 = 0f;
    public float cooldownTimer = 0f;
    public float cooldownTimer1 = 0f;

    public float damage = 10f;

    void Start()
    {
        if (playerHealth == null)
        {
            GameObject playerObject1 = GameObject.FindGameObjectWithTag("Player");
            if (playerObject1 != null)
            {
                playerHealth = playerObject1.GetComponent<PlayerHealth>();
            }

            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component is not assigned and could not be found!");
            }
        }
        // Find the player object in the scene using the tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= agresion)
        {
            fighting = true;
        }
        else
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                fighting = false;
            }
            
        }
        if (fighting)
        {
            time += Time.deltaTime;
            if (time == cooldownTimer)
            {
                health++;
                if (health >= peaceful)
                {
                    fighting = true;
                }
            }
        }
        if (player != null && fighting)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Move towards the player if within the specified range
            if (distanceToPlayer <= stopRange)
            {
                // Move the object towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (fighting)
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
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
