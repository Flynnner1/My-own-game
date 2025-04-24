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
    public float distanceThreshold = 40f; // Distance to the player after which the enemy dies
    public Transform player;            // Reference to the player's Transform (Assign or find)

    // --- References to Scene Managers ---
    private QuestTracer questTracer;
    // Make this public if you want the *option* to assign it in the inspector later,
    // but FindObjectOfType will still be the primary way it gets set in Start()
    public KillCounter killCounter;

    private bool isDying = false; // Renamed from pickedUp for clarity

    void Start()
    {
        currentHealth = maxHealth;

        // --- Find Scene Managers ---
        // Find the single KillCounter instance in the scene
        killCounter = FindObjectOfType<KillCounter>(); // Find the manager in the scene
        if (killCounter == null)
        {
            // Log the error if not found
            Debug.LogError($"KillCounter script not found in the scene! Kills from {gameObject.name} will not be counted.", this);
        }

        // Find QuestTracer
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
        // If the player reference is set, enemy not already dying, and is too far away, despawn it.
        if (!isDying && player != null && Vector3.Distance(transform.position, player.position) > distanceThreshold)
        {
            Die(false); // Pass false: Don't count as kill, don't drop loot
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDying) return; // Already dying, do nothing

        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die(true); // Pass true: Count as kill, drop loot
        }
    }

    // Added bool parameter to distinguish true kills from despawns
    void Die(bool wasKilled)
    {
        if (isDying) return; // Prevent multiple calls
        isDying = true;

        // --- Report Kill ---
        // Only report to KillCounter if it was a true kill and the counter was found
        if (wasKilled && killCounter != null)
        {
            killCounter.addcount(EnemyId); // Call the counter method
        }
        // Optional: You could log an error here again if wasKilled is true but killCounter is null,
        // but the Start() method already warned you.
        // else if (wasKilled && killCounter == null)
        // {
        //     Debug.LogError($"Attempted to report kill for {EnemyId}, but KillCounter is missing!", this);
        // }

        // --- Quest Logic ---
        // Only update quests if it was a true kill and questTracer was found
        if (wasKilled && questTracer != null)
        {
            // Using CompareTag is good practice!
            // Consider if EnemyId could simplify quest tracking further.
            if (questTracer.monster == "Zombie" && gameObject.CompareTag("Zombie"))
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
            // Consider adding an 'else' or default case if needed
        }

        // --- Drops ---
        // Only drop loot if it was a true kill
        if (wasKilled)
        {
            RandomDrop();
        }

        // --- Cleanup ---
        Destroy(gameObject); // Destroy the enemy object at the end
    }

    void RandomDrop()
    {
        // Spawn a random number of coins
        int randomNumber = Random.Range(1, MoreCoins + 1);
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

        // Spawn XP
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
}