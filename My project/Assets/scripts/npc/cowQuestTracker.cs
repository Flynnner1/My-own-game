using TMPro;
using UnityEngine;

public class CowQuestTracker : MonoBehaviour
{
    [Header("UI Text References")]
    public TMP_Text amountOfKillsOfCow;
    public TMP_Text currentKillsOfCow;
    public TMP_Text monsterTextOfCow;

    [Header("Quest Variables")]
    public int cowKillGoal = 0;
    public int currentCowKills = 0;
    public bool isQuestActive = false;
    public bool isQuestCompleted = false;
    public bool isQuestAccepted = false;

    public InventoryItem inventoryItem;
    public PlayerCoins playerCoins;

    public string monster = "Cow";

    void Start()
    {
        if (!inventoryItem) inventoryItem = FindObjectOfType<InventoryItem>();
        if (!playerCoins) playerCoins = FindObjectOfType<PlayerCoins>();
        InitializeQuest();
    }

    void Update()
    {
        if (cowKillGoal > 0 && currentCowKills >= cowKillGoal)
        {
            CompleteQuest();
        }
    }

    public void InitializeQuest()
    {
        if (!isQuestAccepted)
        {
            cowKillGoal = Random.Range(1, 11);
            isQuestActive = true;
            UpdateQuestUI();
        }
    }

    public void AcceptQuest()
    {
        isQuestActive = true;
        currentCowKills = 0;
        isQuestCompleted = false;
        isQuestAccepted = true;
        Debug.Log("Quest Accepted:");
        UpdateQuestUI();
    }

    public void AddKill()
    {
        if (isQuestActive)
        {
            currentCowKills++;
            UpdateQuestUI();
            Debug.Log($"Added a kill to cow quest");
        }
    }

    public void UpdateQuestUI()
    {
        if (amountOfKillsOfCow) amountOfKillsOfCow.text = cowKillGoal.ToString();
        if (currentKillsOfCow) currentKillsOfCow.text = currentCowKills.ToString();
        if (monsterTextOfCow) monsterTextOfCow.text = "Cow";
    }

    public void CompleteQuest()
    {
        isQuestCompleted = true;
    }

    public void CollectQuestReward()
    {
        if (isQuestActive && isQuestCompleted)
        {
            int coinsCollected = Random.Range(5, 20) + cowKillGoal;
            AddCoins(coinsCollected);
            currentCowKills = 0;
            cowKillGoal = 0;
            isQuestActive = false;
            isQuestCompleted = false;
            isQuestAccepted = false;
            Debug.Log("Youve collected the quest");
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