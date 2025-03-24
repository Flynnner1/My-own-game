using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // The total health of the enemy
    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject itemToSpawn;
    public GameObject enemy;

    private float randomPlace = 0.5f;


    public Vector3 spawnOffset;

    private int randomNumber;

    public int MoreCoins = 4;
    
    public QuestTracer questTracer;

    private bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        questTracer = FindObjectOfType<QuestTracer>();
        // Initialize enemy health to the maximum health at the start
        currentHealth = maxHealth;
        Debug.Log("Enemy health initialized to " + currentHealth);

        // Generate a random number between 0 and 3 (inclusive) for the spawn offset
        int randomPlace = Random.Range(0, 3);
        spawnOffset = new Vector3(0, randomPlace, 0);
    }
    

    // Method to take damage
    public void TakeDamage(float amount)
    {
        // Reduce the current health by the damage amount
        currentHealth -= amount;
        Debug.Log("Enemy took damage. Current health: " + currentHealth);

        // Check if the health is less than or equal to zero
        if (currentHealth <= 0f && !pickedUp)
        {
            pickedUp = true;
            Die();
        }
    }

    // Method to handle enemy's death
    void Die()
    {
        if (questTracer == null)
        {
            Debug.Log("Quest tracer null");
        }
        if (questTracer.monster == "Zombie" && enemy.tag == "Zombie")
        {
            Debug.Log("Zombo ded");
           
            questTracer.AddKill();
        }
        else if (questTracer.monster == "Skeleton" && gameObject.tag == "Skeleton")
        {
            Debug.Log("skeleton  ded");
            questTracer.AddKill();
        }
        else if (questTracer.monster == "Slime" && gameObject.tag == "Slime")
        {
            Debug.Log("slime ded");
            questTracer.AddKill();
        }
        else if (questTracer.monster == "Demon" && gameObject.tag == "Demon")
        {
            Debug.Log("demon ded");
            questTracer.AddKill();
        }
        Debug.Log("Enemy died.");
        // Spawn the item at the enemy's position with the specified offset
        if (itemToSpawn != null)
        {
            RandomDrop();
           // buttonreaction.AddKill();
        }
        // Destroy the enemy GameObject
        //help
    }

    // Method to spawn a random number of items
    void RandomDrop()
    {
        randomNumber = Random.Range(1, MoreCoins); // Generate a random number between 1 and 3 (inclusive)
        for (int t = 0; t < randomNumber; t++)
        {
            Instantiate(itemToSpawn, transform.position + spawnOffset, Quaternion.identity);
            destroyObject();
        }
    }
    public void destroyObject()
    {
        Destroy(gameObject);
    }
    public void Addmorecoins()
    {
        MoreCoins += 4;
    }
    
}