using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Identification")]
    public string EnemyId = "Zombie1"; // Assign this in the Inspector for each enemy prefab variant

    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Drops")]
    public GameObject coinSpawn;
    public GameObject XPSpawn;
    public GameObject rareSpellItem; // Single spell item to drop
    public int MoreCoins = 4;           // Maximum number of coins to drop
    public int amountXp = 10;
    public Vector3 spawnOffset;

    [Header("Behavior")]
    public bool destroyWhenFar = true; // <-- NEW: Set to false to prevent despawning
    public float distanceThreshold = 40f; // Distance to the player after which the enemy dies
    public Transform player;            // Reference to the player's Transform (Assign or find)

    // --- References to Scene Managers ---
    private QuestTracer questTracer;
    public KillCounter killCounter;

    private bool isDying = false;

    void Start()
    {
        currentHealth = maxHealth;

        // --- Find Scene Managers ---
        killCounter = FindObjectOfType<KillCounter>();
        if (killCounter == null)
        {
            Debug.LogError($"KillCounter script not found in the scene! Kills from {gameObject.name} will not be counted.", this);
        }

        questTracer = FindObjectOfType<QuestTracer>();
        if (questTracer == null)
        {
            Debug.LogWarning($"QuestTracer script not found in the scene! Quests might not track kills from {gameObject.name}.", this);
        }

        // Find Player if not assigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning($"Player with tag 'Player' not found. Distance check for {gameObject.name} might not work.", this);
            }
        }

        // Generate a random Y offset for the spawn position
        int randomY = Random.Range(0, 3);
        spawnOffset = new Vector3(0, randomY, 0);
    }

    void Update()
    {
        // --- UPDATED LOGIC ---
        // Only perform the distance check if 'destroyWhenFar' is true.
        if (destroyWhenFar)
        {
            // If the player reference is set, enemy not already dying, and is too far away, despawn it.
            if (!isDying && player != null && Vector3.Distance(transform.position, player.position) > distanceThreshold)
            {
                Die(false); // Pass false: Don't count as kill, don't drop loot
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDying) return;

        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die(true); // Pass true: Count as kill, drop loot
        }
    }

    void Die(bool wasKilled)
    {
        if (isDying) return;
        isDying = true;

        if (wasKilled && killCounter != null)
        {
            killCounter.addcount(EnemyId);
        }

        if (wasKilled && questTracer != null)
        {
            if (questTracer.monster == "Zombie" && gameObject.CompareTag("Zombie")) questTracer.AddKill();
            else if (questTracer.monster == "Skeleton" && gameObject.CompareTag("Skeleton")) questTracer.AddKill();
            else if (questTracer.monster == "Slime" && gameObject.CompareTag("Slime")) questTracer.AddKill();
            else if (questTracer.monster == "Demon" && gameObject.CompareTag("Demon")) questTracer.AddKill();
            else if (questTracer.monster == "Cow" && gameObject.CompareTag("Cow")) questTracer.AddKill();
        }

        if (wasKilled)
        {
            RandomDrop();
        }

        Destroy(gameObject);
    }

    void RandomDrop()
    {
        int randomNumber = Random.Range(1, MoreCoins + 1);
        for (int t = 0; t < randomNumber; t++)
        {
            if (coinSpawn != null)
            {
                Instantiate(coinSpawn, transform.position + spawnOffset, Quaternion.identity);
            }
        }

        int dropChance = Random.Range(1, 101);
        if (dropChance == 1 && rareSpellItem != null)
        {
            Instantiate(rareSpellItem, transform.position + spawnOffset, Quaternion.identity);
        }

        int XpChange = Random.Range(1, amountXp + 1);
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

    public void Buff(float heal)
    {
        maxHealth *= 1.2f;
        currentHealth += heal;
    }
}