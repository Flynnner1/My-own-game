using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTracer : MonoBehaviour
{
    [Header("UI Text References (Large UI)")]
    public TMP_Text amountOfKills;
    public TMP_Text currentKills;
    public TMP_Text monsterText;

    [Header("UI Text References (Cow)")]
    public TMP_Text amountOfKillsOfCow;
    public TMP_Text currentKillsOfCow;
    public TMP_Text monsterTextOfCow;

    [Header("UI Text References (Small UI)")]
    public TMP_Text smallamountOfKills;
    public TMP_Text smallcurrentKills;
    public TMP_Text smallmonsterText;

    [Header("Quest Variables")]
    // Monster quest (generic)
    public int kills = 0;            // goal for generic monster quest
    public int currentkils = 0;      // progress for generic monster quest
    public int randomMonster;
    public string monster = "";
    public bool Quest = false;
    public bool quest1 = false;      // accepted/active flag for monster quest
    public bool succes = false;

    // Cow quest
    public int cowkills = 0;         // goal for cow quest
    public int currentCowkils = 0;   // progress for cow quest
    public bool Quest2 = false;      // cow quest exists flag
    public bool quest2 = false;      // accepted/active flag for cow quest
    public bool succes1 = false;

    public int randomNum;

    private float time1;
    private float cooldownTimer = 0.2f;

    private bool accepted = false;
    private bool accepted1 = false;

    // Remove GetComponent usage to avoid null references if on different objects
    public InventoryItem inventroyItem;
    public PlayerCoins playerCoins;

    void Start()
    {
        if (!inventroyItem) inventroyItem = FindObjectOfType<InventoryItem>();
        if (!playerCoins) playerCoins = FindObjectOfType<PlayerCoins>();

        currentQuest();
        currentQuestCow();
    }

    void Update()
    {
        time1 += Time.deltaTime;
        if (time1 >= cooldownTimer)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                AddKillCow();
                time1 = 0f;
            }
        }

        // Check completion for monster quest
        if (kills > 0 && currentkils >= kills)
        {
            QuestCompleted();
        }

        // Check completion for cow quest
        if (cowkills > 0 && currentCowkils >= cowkills)
        {
            QuestCompletedCow();
        }
    }

    // Start or generate a new generic monster quest (if none accepted)
    public void currentQuest()
    {
        if (!accepted)
        {
            randomMonster = Random.Range(1, 4);  // 1 to 3
            randomNum = Random.Range(1, 10);     // 1 to 9
            Quest = true;

            switch (randomMonster)
            {
                case 1: monster = "Slime"; break;
                case 2: monster = "Zombie"; break;
                default: monster = "Skeleton"; break;
            }

            if (Quest)
            {
                kills = randomNum;
            }

            UpdateQuest();
        }
    }

    // Update UI for generic monster quest + small UI
    public void UpdateQuest()
    {
        if (amountOfKills) amountOfKills.text = kills.ToString();
        if (currentKills) currentKills.text = currentkils.ToString();
        if (monsterText) monsterText.text = monster;

        // Small UI should reflect the same monster quest
        if (smallamountOfKills) smallamountOfKills.text = kills.ToString();
        if (smallcurrentKills) smallcurrentKills.text = currentkils.ToString();
        if (smallmonsterText) smallmonsterText.text = monster;
    }

    public void AddKill()
    {
        if (quest1)
        {
            currentkils++;
            UpdateQuest();
        }
    }

    public void acceptQuest()
    {
        quest1 = true;
        currentkils = 0;
        succes = false;
        accepted = true;
        UpdateQuest();
    }

    public void QuestCompleted()
    {
        succes = true;
        Debug.Log("Monster quest completed!");
    }

    public void collectQuest()
    {
        if (quest1 && succes)
        {
            int coinsCollected = Random.Range(5, 20);
            coinsCollected += kills;

            Debug.Log("You get " + coinsCollected + " coins");
            AddCoins(coinsCollected);

            // reset monster quest state
            currentkils = 0;
            kills = 0;
            quest1 = false;
            succes = false;
            randomNum = 0;
            accepted = false;
            currentQuest();
        }
    }

    public void AddCoins(int coin)
    {
        if (playerCoins)
        {
            playerCoins.addcoins(coin);
        }
        else
        {
            Debug.LogWarning("No PlayerCoins reference found.");
        }
    }

    // Accept cow quest
    public void acceptQuestCow()
    {
        quest2 = true;
        currentCowkils = 0;
        succes1 = false;
        accepted1 = true;
        UpdateQuest1();
    }

    // Called when a cow is killed (or when Q pressed in your test)
    void AddKillCow()
    {
        if (quest2)
        {
            currentCowkils++;
            UpdateQuest1();
        }
    }

    // Update UI for cow quest
    public void UpdateQuest1()
    {
        if (amountOfKillsOfCow) amountOfKillsOfCow.text = cowkills.ToString();
        if (currentKillsOfCow) currentKillsOfCow.text = currentCowkils.ToString();
        if (monsterTextOfCow) monsterTextOfCow.text = "Cow";
    }

    // Generate a new cow quest if none accepted
    public void currentQuestCow()
    {
        if (!accepted1)
        {
            // Use a separate random goal for cows
            int randomCowNum = Random.Range(1, 11); // 1 to 10
            Quest2 = true;

            if (Quest2)
            {
                cowkills = randomCowNum;
            }

            UpdateQuest1();
        }
    }

    public void collectCowQuest()
    {
        if (quest2 && succes1)
        {
            int coinsCollected = Random.Range(5, 20);
            coinsCollected += cowkills;

            Debug.Log("You get " + coinsCollected + " coins");
            AddCoins(coinsCollected);

            // reset cow quest state
            currentCowkils = 0;
            cowkills = 0;
            quest2 = false;
            succes1 = false;
            randomNum = 0;
            accepted1 = false;
            currentQuestCow();
        }
    }

    public void QuestCompletedCow()
    {
        succes1 = true;
        Debug.Log("Cow quest completed!");
    }
}