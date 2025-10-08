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

    [Header("Quest Variables")]
    public int kills = 0;
    public int cowkills = 0;
    public int currentkils = 0;
    public int randomMonster;
    public string monster = "";
    public bool Quest = false;
    public bool Quest2 = false;

    public int randomNum;
    public bool quest1 = false;
    public bool quest2 = false;
    public bool succes = false;
    public bool succes1 = false;

    public int currentCowkils = 0;

    private float time1;

    private bool accepted = false;
    private bool accepted1 = false;

    private float cooldownTimer = 0.2f;

    // Remove GetComponent usage to avoid null references if on different objects
    //public Inventory inventory;
    public InventoryItem inventroyItem;
    public PlayerCoins playerCoins;

    void Start()
    {
        // Assign references by finding them in the scene, or wire them in the Inspector
        //if (!inventory) inventory = FindObjectOfType<Inventory>();
        if (!inventroyItem) inventroyItem = FindObjectOfType<InventoryItem>();
        if (!playerCoins) playerCoins = FindObjectOfType<PlayerCoins>();

        currentQuest();
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

        if (kills == currentkils && kills > 0)
        {
            QuestCompleted();
        }
        if (kills == currentCowkils && cowkills > 0)
        {
            QuestCompletedCow();
        }
    }

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
                    //default: monster = "Demon"; break;
            }

            if (Quest)
            {
                kills += randomNum;
            }
            UpdateQuest();
        }
    }

    public void UpdateQuest()
    {
        amountOfKills.text = kills.ToString();
        currentKills.text = currentkils.ToString();
        monsterText.text = monster;

        smallamountOfKills.text = kills.ToString();
        smallcurrentKills.text = currentkils.ToString();
        smallmonsterText.text = monster;
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
        //currentkils = 0;
        //kills = 0;
        //currentQuest();
        UpdateQuest();
        accepted = true;
    }

    public void QuestCompleted()
    {
        succes = true;
        Debug.Log("Quest completed!");
    }

    public void collectQuest()
    {
        //if (!inventory)
        //{
        //    Debug.LogWarning("There is no inventory");
        //    return;
        //}

        if (quest1 && succes)
        {
            int coinsCollected = Random.Range(5, 20);
            coinsCollected += kills;

            Debug.Log("You get " + coinsCollected + " coins");
            AddCoins(coinsCollected);

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
    public void acceptQuestCow()
    {
        quest2 = true;
        //currentkils = 0;
        //kills = 0;
        //currentQuest();
        UpdateQuest1();
        accepted1 = true;
    }

    void AddKillCow()
    {
        if (quest2)
        {
            currentCowkils++;
            UpdateQuest1();
        }
    }
    public void UpdateQuest1()
    {
        amountOfKills.text = kills.ToString();
        currentKills.text = currentCowkils.ToString();
        monsterText.text = "Cow";

        smallamountOfKills.text = kills.ToString();
        smallcurrentKills.text = currentCowkils.ToString();
        smallmonsterText.text = "Cow";
    }
    public void currentQuestCow()
    {
        if (!accepted1)
        {
            
            randomNum = Random.Range(1, 11);     
            Quest = true;

            

            if (Quest2)
            {
                kills += randomNum;
            }
            UpdateQuest1();
        }
    }
    public void collectCowQuest()
    {
        //if (!inventory)
        //{
        //    Debug.LogWarning("There is no inventory");
        //    return;
        //}

        if (quest2 && succes1)
        {
            int coinsCollected = Random.Range(5, 20);
            coinsCollected += kills;

            Debug.Log("You get " + coinsCollected + " coins");
            AddCoins(coinsCollected);

            currentkils = 0;
            kills = 0;
            quest2 = false;
            succes1 = false;
            randomNum = 0;
            accepted = false;
            currentQuestCow();
        }
    }
    public void QuestCompletedCow()
    {
        succes1 = true;
        Debug.Log("Quest completed!");
    }
}