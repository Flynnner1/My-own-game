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
    //public GameObject rareSpellItem;
    public int MoreCoins = 4;           
    public int amountXp = 10;
    public Vector3 spawnOffset;

    [Header("Behavior")]
    public float distanceThreshold = 40f; 
    public Transform player;


    private BetterMonsterQuestTracker[] questTrackers;

    public KillCounter killCounter;

    private bool isDying = false; 

    void Start()
    {
        currentHealth = maxHealth;
        killCounter = FindObjectOfType<KillCounter>();
        if (killCounter == null)
        {
            Debug.LogError($"KillCounter script not found in the scene! Kills from {gameObject.name} will not be counted.", this);
        }
        questTrackers = FindObjectsByType<BetterMonsterQuestTracker>(FindObjectsSortMode.None);
        if (questTrackers == null)
        {
            Debug.LogWarning($"QuestTracer script not found in the scene! Quests might not track kills from {gameObject.name}.", this);
        }
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
        int randomY = Random.Range(0, 3);
        spawnOffset = new Vector3(0, randomY, 0);
    }

    void Update()
    {
        if (!isDying && player != null && Vector3.Distance(transform.position, player.position) > distanceThreshold)
        {
            Die(false); // Pass false: Don't count as kill, don't drop loot
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
        //if (wasKilled && betterMonsterQuestTracker != null)
        //{
        //    // Using CompareTag is good practice!
        //    // Consider if EnemyId could simplify quest tracking further.
        //    if (betterMonsterQuestTracker.monster == "Zombie" && gameObject.CompareTag("Zombie"))
        //    {
        //        betterMonsterQuestTracker.AddKill();
        //    }
        //    else if (betterMonsterQuestTracker.monster == "Skeleton" && gameObject.CompareTag("Skeleton"))
        //    {
        //        betterMonsterQuestTracker.AddKill();
        //    }
        //    else if (betterMonsterQuestTracker.monster == "Slime" && gameObject.CompareTag("Slime"))
        //    {
        //        betterMonsterQuestTracker.AddKill();
        //    }
        //    else if (betterMonsterQuestTracker.monster == "Demon" && gameObject.CompareTag("Demon"))
        //    {
        //        betterMonsterQuestTracker.AddKill();
        //    }
        //    else if (betterMonsterQuestTracker.monster == "Cow" && gameObject.CompareTag("Cow"))
        //    {
        //        betterMonsterQuestTracker.AddKill();
        //    }
        //    else
        //    {
        //        betterMonsterQuestTracker.AddKill();
        //    }
        //    //if (betterMonsterQuestTracker.monster == "Cow" && gameObject.CompareTag("Cow"))
        //    //{
        //    //    betterMonsterQuestTracker.AddKill();
        //    //}
        //    // Consider adding an 'else' or default case if needed
        //}
        if (wasKilled)
        {
            foreach (BetterMonsterQuestTracker tracker in questTrackers)
            {
                tracker.AddKill(gameObject.tag);
            }
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
        //int dropChance = Random.Range(1, 101);
        //if (dropChance == 1 && rareSpellItem != null)
        //{
        //    Instantiate(rareSpellItem, transform.position + spawnOffset, Quaternion.identity);
        //}

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
    public void Buff(float heal, float multi)
    {
        maxHealth *= multi; 
        currentHealth += heal; 
    }
}