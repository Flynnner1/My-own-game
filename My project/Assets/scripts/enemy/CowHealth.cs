using UnityEngine;

public class CowHealth : MonoBehaviour
{
    [Header("Identification")]
    public string EnemyId = "Cow";

    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Drops")]
    public GameObject coinSpawn;
    public GameObject XPSpawn;
    public GameObject rareSpellItem;
    public int MoreCoins = 4;
    public int amountXp = 10;
    public Vector3 spawnOffset;

    [Header("Behavior")]
    public float distanceThreshold = 40f;
    public Transform player;

    private QuestTracer questTracer;
    private KillCounter killCounter;

    private bool isDying = false;

    void Start()
    {
        currentHealth = maxHealth;

        killCounter = FindObjectOfType<KillCounter>();
        questTracer = FindObjectOfType<QuestTracer>();

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null) player = playerObject.transform;
        }

        int randomY = Random.Range(0, 3);
        spawnOffset = new Vector3(0, randomY, 0);
    }

    void Update()
    {
        if (!isDying && player != null && Vector3.Distance(transform.position, player.position) > distanceThreshold)
        {
            Die(false);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDying) return;

        currentHealth -= amount;
        CowMovement movement = GetComponent<CowMovement>();
        if (currentHealth < maxHealth && movement != null)
        {
            movement.Run();
        }

        if (currentHealth <= 0f)
        {
            Die(true);
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

        if (wasKilled && questTracer != null && questTracer.monster == "Cow" && gameObject.CompareTag("Cow"))
        {
            questTracer.AddKill();
        }

        CowQuestTracker cowQuestTracker = FindObjectOfType<CowQuestTracker>();
        if (wasKilled && cowQuestTracker != null && gameObject.CompareTag("Cow"))
        {
            cowQuestTracker.AddKill();
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
}