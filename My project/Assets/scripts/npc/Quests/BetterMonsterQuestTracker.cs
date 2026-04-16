using TMPro;
using UnityEngine;

public class BetterMonsterQuestTracker : MonoBehaviour
{
    [Header("UI Text References (Large UI)")]
    public TMP_Text amountOfKills;
    public TMP_Text currentKills;
    public TMP_Text monsterText;

    [Header("UI Text References (Small UI)")]
    public GameObject SmallQuestUI;
    public TMP_Text smallAmountOfKills;
    public TMP_Text smallCurrentKills;
    public TMP_Text smallMonsterText;

    [Header("Quest Variables")]
    public int kills = 0;
    public int currentKillsCount = 0;
    public int randomMonster;
    public string monster = "";
    public bool isDemonArea = false;
    public bool isCowArea = false;

    [Header("Other Scripts")]
    public InventoryItem inventoryItem;
    public PlayerCoins playerCoins;

    [Header("Quest State")]
    public bool isQuestActive = false;
    public bool isQuestCompleted = false;
    public bool isQuestAccepted = false;
    public void Start()
    {
        InitializeQuest();
        Debug.LogWarning("initializeQuest");
    }

    void Update()
    {
        if(kills == currentKillsCount)
        {
            isQuestCompleted = true;
        }
        if (isQuestAccepted)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                AddKill();
            }
        }
    }

    public void InitializeQuest()
    {
        Debug.LogWarning("making the quest");  
        if (isDemonArea)
        {
            randomMonster = Random.Range(1, 5);
            int randomKillCount = Random.Range(1, 10);
            Debug.Log("amount of kills needed " + randomKillCount);
            switch (randomMonster)
            {
                case 1: monster = "Slime"; break;
                case 2: monster = "Zombie"; break;
                case 3: monster = "Demon"; break;
                default: monster = "Skeleton"; break;
            }
            kills = randomKillCount;
        }
        else if (isCowArea)
        {
            
            int randomKillCount = Random.Range(1, 10);
            monster = "Cow";
            kills = randomKillCount;
        }
        else
        {
            randomMonster = Random.Range(1, 4);
            int randomKillCount = Random.Range(1, 10);
            switch (randomMonster)
            {
                case 1: monster = "Slime"; break;
                case 2: monster = "Zombie"; break;
                default: monster = "Skeleton"; break;
            }
            kills = randomKillCount;
        }
        UpdateQuestUI();
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
    public void QuestAccepted()
    {
        if (!isQuestAccepted)
        {
            SmallQuestUI.SetActive(true);
            isQuestActive = true;
            currentKillsCount = 0;
            isQuestCompleted = false;
            isQuestAccepted = true;
            Debug.Log("Quest Accepted:");
            UpdateQuestUI();
        }

    }
    public void AddKill()
    {
        if (isQuestAccepted)
        {
            currentKillsCount++;
            UpdateQuestUI();
        }
    }

    public void CompleteQuest()
    {
        if(isQuestCompleted)
        {
            SmallQuestUI.SetActive(false);
            isQuestCompleted = false;
            isQuestActive = false;
            Debug.Log("Quest Completed!");
            int rewardCoins = Random.Range(1, 10) + kills;
            AddCoins(rewardCoins);
            Debug.Log("reseting the Quest");
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
