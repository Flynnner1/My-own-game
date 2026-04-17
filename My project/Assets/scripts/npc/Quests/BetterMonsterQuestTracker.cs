using System.Threading;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BetterMonsterQuestTracker : MonoBehaviour
{
    public enum QuestType
    {
        None,
        Monster,
        Demon,
        Cow
    }
    [Header("UI Text References For Monster (Large UI)")]
    public TMP_Text MonsteramountOfKills;
    public TMP_Text MonstercurrentKills;
    public TMP_Text MonstermonsterText;

    [Header("UI Text References For Cow (Large UI)")]
    public TMP_Text CowamountOfKills;
    public TMP_Text CowcurrentKills;
    public TMP_Text CowmonsterText;

    //[Header("UI Text References For Demon (Large UI)")]
    //public TMP_Text DemonamountOfKills;
    //public TMP_Text DemoncurrentKills;
    //public TMP_Text DemonmonsterText;

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

    QuestType SortQuest = QuestType.None;
    public void Start()
    {
        monster = "Monster:";
        kills = 10;
        UpdateQuestUI();
        InitializeQuest();
        Debug.LogWarning("initializeQuest");
    }

    void Update()
    {
        Debug.Log(SortQuest);
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
        if (isDemonArea && SortQuest == QuestType.Demon)
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
        else if (isCowArea && SortQuest == QuestType.Cow)
        {
            
            int randomKillCount = Random.Range(1, 10);
            monster = "Cow";
            kills = randomKillCount;
        }
        else if (SortQuest == QuestType.Monster)
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
        if (CowamountOfKills) CowamountOfKills.text = kills.ToString();
        if (CowcurrentKills) CowcurrentKills.text = currentKillsCount.ToString();
        if (CowmonsterText) CowmonsterText.text = monster;

        if (MonsteramountOfKills) MonsteramountOfKills.text = kills.ToString();
        if (MonstercurrentKills) MonstercurrentKills.text = currentKillsCount.ToString();
        if (MonstermonsterText) MonstermonsterText.text = monster;

        //if (DemonamountOfKills) DemonamountOfKills.text = kills.ToString();
        //if (DemoncurrentKills) DemoncurrentKills.text = currentKillsCount.ToString();
        //if (DemonmonsterText) DemonmonsterText.text = monster;

        if (smallAmountOfKills) smallAmountOfKills.text = kills.ToString();
        if (smallCurrentKills) smallCurrentKills.text = currentKillsCount.ToString();
        if (smallMonsterText) smallMonsterText.text = monster;
    }
    
    public void AddKill()
    {
        if (isQuestAccepted)
        {
            Debug.Log("added 1 to quest");
            currentKillsCount++;
            UpdateQuestUI();
        }
    }

    public void CollectQuest()
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
            UpdateQuestUI();
        }
    }
    public void AddCoins(int coin)
    {
        if (playerCoins)
        {
            playerCoins.addcoins(coin);
        }
    }
    public void CowQuestAccepted()
    {
        if (!isQuestAccepted )
        {
            
            isCowArea = true;
            SortQuest = QuestType.Cow;
            SmallQuestUI.SetActive(true);
            isQuestActive = true;
            currentKillsCount = 0;
            isQuestCompleted = false;
            isQuestAccepted = true;
            InitializeQuest();
            Debug.Log("Quest Accepted:");
            UpdateQuestUI();
        }

    }
    public void MonsterQuestAccepted()
    {
        if (!isQuestAccepted)
        {
            
            isDemonArea = false;
            isCowArea = false;
            SortQuest = QuestType.Monster;
            SmallQuestUI.SetActive(true);
            isQuestActive = true;
            currentKillsCount = 0;
            isQuestCompleted = false;
            isQuestAccepted = true;
            InitializeQuest();
            Debug.Log("Quest Accepted:");
            UpdateQuestUI();
        }

    }
    public void DemonQuestAccepted()
    {
        if (!isQuestAccepted)
        {
            
            isDemonArea = true;
            SortQuest = QuestType.Demon;
            SmallQuestUI.SetActive(true);
            isQuestActive = true;
            currentKillsCount = 0;
            isQuestCompleted = false;
            isQuestAccepted = true;
            InitializeQuest();
            Debug.Log("Quest Accepted:");
            UpdateQuestUI();
        }

    }
}
