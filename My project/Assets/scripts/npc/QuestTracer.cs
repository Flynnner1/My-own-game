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

    [Header("UI Text References (Small UI)")]
    public TMP_Text smallamountOfKills;
    public TMP_Text smallcurrentKills;
    public TMP_Text smallmonsterText;

    // Example: Another script that reacts to a UI button
    

    [Header("Quest Variables")]
    public int kills = 0;
    public int currentkils = 0;
    public int randomMonster;
    public string monster = "";
    public bool Quest = false;

    private float cooldownTimerfireball = 0.5f;
    private float time1;

    public Inventory inventory;
    public InventroyItem inventroyItem;

    // This will decide how many kills the player needs
    public int randomNum;

    // Flags to show if a quest is accepted or completed
    public bool quest1 = false;
    public bool succes = false;

    void Start()
    {
        if (inventory == null)
        {
            inventory = GetComponent<Inventory>();
        }
        if (inventroyItem == null)
        {
            inventroyItem = GetComponent<InventroyItem>();
        }

        // Generate an initial quest
        currentQuest();
    }

    void Update()
    {
        time1 += Time.deltaTime;
        // Example input to add kills: Press Q
        if (time1 >= cooldownTimerfireball)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                AddKill();
                time1 = 0f;
            }
        }

        // Check if the kill requirement has been met
        if (kills == currentkils && kills > 0)
        {
            QuestCompleted();
        }
    }

    /// <summary>
    /// Generate and display a new quest.
    /// </summary>
    public void currentQuest()
    {
        randomMonster = Random.Range(1, 5);  // 1 to 4
        randomNum = Random.Range(1, 10);     // 1 to 9

        Quest = true;
        if (randomMonster == 1)
        {
            monster = "Slime";
        }
        else if (randomMonster == 2)
        {
            monster = "Zombie";
        }
        else if (randomMonster == 3)
        {
            monster = "Skeleton";
        }
        else
        {
            monster = "Demon";
        }

        if (Quest)
        {
            kills += randomNum;
        }

        UpdateQuest();
    }

    /// <summary>
    /// Updates the quest UI text elements (both large and small).
    /// </summary>
    public void UpdateQuest()
    {
        // Large UI
        amountOfKills.text = kills.ToString();
        currentKills.text = currentkils.ToString();
        monsterText.text = monster;

        // Small UI
        smallamountOfKills.text = kills.ToString();
        smallcurrentKills.text = currentkils.ToString();
        smallmonsterText.text = monster;
    }

    /// <summary>
    /// Adds a kill if the quest is active.
    /// </summary>
    public void AddKill()
    {
        if (quest1)
        {
            currentkils++;
            UpdateQuest();
        }
    }

    /// <summary>
    /// Called from NPCController or a UI button to accept the quest.
    /// </summary>
    public void acceptQuest()
    {
        
        quest1 = true;
        
        currentkils = 0;
        kills = 0;
        currentQuest();
        UpdateQuest();
    }

    /// <summary>
    /// Every time the requirements are met, mark the quest as completed.
    /// </summary>
    public void QuestCompleted()
    {
        succes = true;
        // Here you might show a "Quest Complete" message or
        // prompt the player to collect the reward.
        Debug.Log("Quest completed!");
    }

    /// <summary>
    /// Collects the quest reward when the player has completed the quest.
    /// </summary>
    public void collectQuest()
    {
        if (inventory == null)
        {
            Debug.LogWarning("there is no inventory");
        }
        else
        {
            if (quest1 == true && succes == true)
            {
                int coins = Random.Range(5, 20);
                coins += kills;
                

                // Reset quest data
                currentkils = 0;
                kills = 0;
                quest1 = false;
                succes = false;
                randomNum = 0;

                Debug.Log("you get" +  coins + "amount of coins");
                AddCoins(coins);
                // Generate a new quest
                currentQuest();
            }
        }

    }
    public void AddCoins(int coin)
    {
        Debug.Log("added " + coin);
        inventroyItem.addcoins(coin);
        
    }
}