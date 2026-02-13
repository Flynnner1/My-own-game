using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterQuestTracker : MonoBehaviour
{
    [Header("UI Text References (Large UI)")]
    public TMP_Text amountOfKills;
    public TMP_Text currentKills;
    public TMP_Text monsterText;

    [Header("UI Text References (Small UI)")]
    public TMP_Text smallAmountOfKills;
    public TMP_Text smallCurrentKills;
    public TMP_Text smallMonsterText;

    [Header("Quest Variables")]
    public int kills = 0;
    public int currentKillsCount = 0; 
    public int randomMonster;
    public string monster = "";
    public bool isQuestActive = false;  
    public bool isQuestCompleted = false;

    private bool isQuestAccepted = false;
    public InventoryItem inventoryItem;
    public PlayerCoins playerCoins;
    void Start()
    {
        if (!inventoryItem) inventoryItem = FindObjectOfType<InventoryItem>();
        if (!playerCoins) playerCoins = FindObjectOfType<PlayerCoins>();
        InitializeQuest();
    }

    void Update()
    {
        if (kills > 0 && currentKillsCount >= kills)
        {
            CompleteQuest();
        }
    }
    public void InitializeQuest()
    {
        if (!isQuestAccepted)
        {
            randomMonster = Random.Range(1, 4);
            int randomKillCount = Random.Range(1, 10);
            isQuestActive = true;
            switch (randomMonster)
            {
                case 1: monster = "Slime"; break;
                case 2: monster = "Zombie"; break;
                default: monster = "Skeleton"; break;
            }
            kills = randomKillCount;
            UpdateQuestUI();
        }
    }
    public void UpdateQuestUI()
    {
        if (amountOfKills) amountOfKills.text = kills.ToString();
        if (currentKills) currentKills.text = currentKillsCount.ToString();
        if (monsterText) monsterText.text = monster;

        if (smallAmountOfKills) smallAmountOfKills.text = kills.ToString();
        if (smallCurrentKills) smallCurrentKills.text = currentKillsCount.ToString();
        if (smallMonsterText) smallMonsterText.text = monster;
    }
    public void AddKill()
    {
        if (isQuestActive)
        {
            currentKillsCount++;
            UpdateQuestUI();
        }
    }
    public void AcceptQuest()
    {
        isQuestActive = true;
        currentKillsCount = 0;
        isQuestCompleted = false;
        isQuestAccepted = true;
        UpdateQuestUI();
    }
    public void CompleteQuest()
    {
        isQuestCompleted = true;
        Debug.Log("Monster quest completed!");
    }
    public void CollectQuestReward()
    {
        if (isQuestActive && isQuestCompleted)
        {
            int coinsCollected = Random.Range(5, 20) + kills;
            Debug.Log("You get " + coinsCollected + " coins");
            AddCoins(coinsCollected);
            // Reset
            currentKillsCount = 0;
            kills = 0;
            isQuestActive = false;
            isQuestCompleted = false;
            isQuestAccepted = false;
            InitializeQuest();
        }
    }
    public void AddCoins(int coin)
    {
        if (playerCoins)
        {
            playerCoins.addcoins(coin);
        }       
    }
}