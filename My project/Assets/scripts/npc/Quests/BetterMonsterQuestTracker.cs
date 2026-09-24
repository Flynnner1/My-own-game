using TMPro;
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

    [Header("Welke quest geeft deze NPC?")]
    public QuestType questType = QuestType.Monster;

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
    public bool isQuestAccepted = false;
    public bool isQuestCompleted = false;

    QuestType SortQuest = QuestType.None;
    void Start()
    {
        monster = "No quest";
        UpdateQuestUI();
    }
    public void AcceptQuest()
    {
        StartQuest(questType);
    }
    void Update()
    {
        //Debug.Log(SortQuest);
        //if(kills == currentKillsCount)
        //{
        //    isQuestCompleted = true;
        //}
        //if (isQuestAccepted)
        //{
        //    if(Input.GetKeyDown(KeyCode.Z))
        //    {
        //        AddKill();
        //    }
        //}
    }

    //public void InitializeQuest()
    //{
    //    Debug.LogWarning("making the quest");  
    //    if (isDemonArea && SortQuest == QuestType.Demon)
    //    {
    //        randomMonster = Random.Range(1, 5);
    //        int randomKillCount = Random.Range(1, 10);
    //        Debug.Log("amount of kills needed " + randomKillCount);
    //        switch (randomMonster)
    //        {
    //            case 1: monster = "Slime"; break;
    //            case 2: monster = "Zombie"; break;
    //            case 3: monster = "Demon"; break;
    //            default: monster = "Skeleton"; break;
    //        }
    //        kills = randomKillCount;
    //    }
    //    else if (isCowArea && SortQuest == QuestType.Cow)
    //    {
            
    //        int randomKillCount = Random.Range(1, 10);
    //        monster = "Cow";
    //        kills = randomKillCount;
    //    }
    //    else if (SortQuest == QuestType.Monster)
    //    {
    //        randomMonster = Random.Range(1, 4);
    //        int randomKillCount = Random.Range(1, 10);
    //        switch (randomMonster)
    //        {
    //            case 1: monster = "Slime"; break;
    //            case 2: monster = "Zombie"; break;
    //            default: monster = "Skeleton"; break;
    //        }
    //        kills = randomKillCount;
    //    }
    //    UpdateQuestUI();
    //}
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
    
    //public void AddKill(string killedMonster)
    //{
    //    if (!isQuestAccepted || isQuestCompleted) return;
    //    if (killedMonster != monster) return;

    //    if (currentKillsCount >= kills)
    //    {
    //        isQuestCompleted = true;
    //        Debug.Log("Quest done! Go back to the NPC.");
    //    }
    //    UpdateQuestUI();
        
    //}

    public void CollectQuest()
    {
        if (!isQuestCompleted) return;

        

        Debug.Log("Quest Completed!");
        int rewardCoins = Random.Range(1, 10) + kills;
        AddCoins(rewardCoins);

        Debug.Log("reseting the Quest");
        SmallQuestUI.SetActive(false);
        //isQuestCompleted = false;
        //isQuestActive = false;

        isQuestAccepted = false;
        isQuestCompleted = false;
        currentKillsCount = 0;
        kills = 0;
        monster = "No quest";
        SortQuest = QuestType.None;
        //InitializeQuest();
        UpdateQuestUI();
        
    }
    public void AddCoins(int coin)
    {
        if (playerCoins)
        {
            playerCoins.addcoins(coin);
        }
    }
    public void MonsterQuestAccepted()
    {
        StartQuest(QuestType.Monster);
    }
    public void DemonQuestAccepted()
    {
        StartQuest(QuestType.Demon);
    }
    public void CowQuestAccepted()
    {
        StartQuest(QuestType.Cow);
    }
    void StartQuest(QuestType type)
    {
        if (isQuestAccepted) return; 

        SortQuest = type;
        isQuestAccepted = true;
        isQuestCompleted = false;
        currentKillsCount = 0;
        kills = Random.Range(1, 10);

        if (type == QuestType.Cow)
        {
            monster = "Cow";
        }
        else if (type == QuestType.Monster)
        {
            int randomMonster = Random.Range(1, 4);
            switch (randomMonster)
            {
                case 1: monster = "Slime"; break;
                case 2: monster = "Zombie"; break;
                default: monster = "Skeleton"; break;
            }
        }
        else if (type == QuestType.Demon)
        {
            int randomMonster = Random.Range(1, 5);
            switch (randomMonster)
            {
                case 1: monster = "Slime"; break;
                case 2: monster = "Zombie"; break;
                case 3: monster = "Demon"; break;
                default: monster = "Skeleton"; break;
            }
        }

        SmallQuestUI.SetActive(true);
        Debug.Log("Quest accepted: kill " + kills + " " + monster);
        UpdateQuestUI();
    }

    public void AddKill(string killedMonster)
    {
        if (!isQuestAccepted || isQuestCompleted) return;
        if (killedMonster != monster) return; 

        currentKillsCount++;
        if (currentKillsCount >= kills)
        {
            isQuestCompleted = true;
            Debug.Log("Quest done! Go back to the NPC.");
        }
        UpdateQuestUI();
    }

}
