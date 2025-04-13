using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject coinSpawn;
    public GameObject XPSpawn;
    public GameObject enemy;

    // Single spell item to drop
    public GameObject rareSpellItem;

    public int MoreCoins = 4;           // Maximum number of coins to drop
    public int amountXp = 10;
    public Vector3 spawnOffset;

    // Reference to the quest system
    public QuestTracer questTracer;

    // Distance to the player after which the enemy dies
    public float distanceThreshold = 40f;
    public Transform player;            // Reference to the player's Transform

    private bool pickedUp = false;

    void Start()
    {
        questTracer = FindObjectOfType<QuestTracer>();
        currentHealth = maxHealth;

        // Generate a random Y offset for the spawn position
        int randomY = Random.Range(0, 3);
        spawnOffset = new Vector3(0, randomY, 0);
    }

    void Update()
    {
        // If the player reference is set and the enemy is too far away, kill the enemy
        if (player != null && Vector3.Distance(transform.position, player.position) > distanceThreshold && !pickedUp)
        {
            pickedUp = true;
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f && !pickedUp)
        {
            pickedUp = true;
            Die();
        }
    }

    void Die()
    {
        // Update quest kills if the monster name matches the tag
        if (questTracer != null)
        {
            if (questTracer.monster == "Zombie" && enemy.CompareTag("Zombie"))
            {
                questTracer.AddKill();
            }
            else if (questTracer.monster == "Skeleton" && gameObject.CompareTag("Skeleton"))
            {
                questTracer.AddKill();
            }
            else if (questTracer.monster == "Slime" && gameObject.CompareTag("Slime"))
            {
                questTracer.AddKill();
            }
            else if (questTracer.monster == "Demon" && gameObject.CompareTag("Demon"))
            {
                questTracer.AddKill();
            }
        }

        RandomDrop();
        Destroy(gameObject);
    }

    void RandomDrop()
    {
        // Spawn a random number of coins
        int randomNumber = Random.Range(1, MoreCoins);
        for (int t = 0; t < randomNumber; t++)
        {
            if (coinSpawn != null)
            {
                Instantiate(coinSpawn, transform.position + spawnOffset, Quaternion.identity);
            }
        }

        // 1-in-100 chance to drop the single rare spell item
        int dropChance = Random.Range(1, 101);
        if (dropChance == 1 && rareSpellItem != null)
        {
            Instantiate(rareSpellItem, transform.position + spawnOffset, Quaternion.identity);
        }

        int XpChange = Random.Range(1, amountXp);
        for (int t = 0; t < XpChange; t++)
        {
            if (XPSpawn != null)
            {
                Instantiate(XPSpawn, transform.position + spawnOffset, Quaternion.identity);
            }
        }
    }

    public void Addmorecoins()
    {
        MoreCoins += 4;
    }
}