using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [Header("Zombie Kills")]
    public int ZombieCount1 = 0;
    public int ZombieCount2 = 0;
    public int ZombieCount3 = 0;
    public int ZombieCount4 = 0;
    public int ZombieCount5 = 0;
    public TMP_Text Zombie1Count;
    public TMP_Text Zombie2Count;
    public TMP_Text Zombie3Count;
    public TMP_Text Zombie4Count;
    public TMP_Text Zombie5Count;

    [Header("Slime Kills")]
    public int SlimeCount1 = 0;
    public int SlimeCount2 = 0;
    public int SlimeCount3 = 0;
    public int SlimeCount4 = 0;
    public int SlimeCount5 = 0;
    public TMP_Text Slime1Count;
    public TMP_Text Slime2Count;
    public TMP_Text Slime3Count;
    public TMP_Text Slime4Count;
    public TMP_Text Slime5Count;

    [Header("Skeleton Kills")]
    public int SkeletonCount1 = 0;
    public int SkeletonCount2 = 0;
    public int SkeletonCount3 = 0;
    public int SkeletonCount4 = 0;
    public int SkeletonCount5 = 0;
    public TMP_Text Skeleton1Count;
    public TMP_Text Skeleton2Count;
    public TMP_Text Skeleton3Count;
    public TMP_Text Skeleton4Count;
    public TMP_Text Skeleton5Count;

    [Header("Demon Kills")]
    public int DemonCount1 = 0;
    public int DemonCount2 = 0;
    public int DemonCount3 = 0;
    public int DemonCount4 = 0;
    public int DemonCount5 = 0;
    public TMP_Text Demon1Count;
    public TMP_Text Demon2Count;
    public TMP_Text Demon3Count;
    public TMP_Text Demon4Count;
    public TMP_Text Demon5Count;

    [Header("Cow Kills")]
    public int CowCount1 = 0;
    public int CowCount2 = 0;
    public int CowCount3 = 0;
    public int CowCount4 = 0;
    public int CowCount5 = 0;
    public TMP_Text Cow1Count;
    public TMP_Text Cow2Count;
    public TMP_Text Cow3Count;
    public TMP_Text Cow4Count;
    public TMP_Text Cow5Count;

    // Load counts on start
    void Start()
    {
        LoadCounts();
        UpdateAllUI();
    }

    // Save counts when the application quits
    void OnApplicationQuit()
    {
        SaveCounts();
    }

    public void addcount(string enemy)
    {
        switch (enemy)
        {
            // --- Zombies ---
            case "Zombie1":
                ZombieCount1++;
                if (Zombie1Count != null) Zombie1Count.text = ZombieCount1.ToString();
                break;
            case "Zombie2":
                ZombieCount2++;
                if (Zombie2Count != null) Zombie2Count.text = ZombieCount2.ToString();
                break;
            case "Zombie3":
                ZombieCount3++;
                if (Zombie3Count != null) Zombie3Count.text = ZombieCount3.ToString();
                break;
            case "Zombie4":
                ZombieCount4++;
                if (Zombie4Count != null) Zombie4Count.text = ZombieCount4.ToString();
                break;
            case "Zombie5":
                ZombieCount5++;
                if (Zombie5Count != null) Zombie5Count.text = ZombieCount5.ToString();
                break;

            // --- Slimes ---
            case "Slime1":
                SlimeCount1++;
                if (Slime1Count != null) Slime1Count.text = SlimeCount1.ToString();
                break;
            case "Slime2":
                SlimeCount2++;
                if (Slime2Count != null) Slime2Count.text = SlimeCount2.ToString();
                break;
            case "Slime3":
                SlimeCount3++;
                if (Slime3Count != null) Slime3Count.text = SlimeCount3.ToString();
                break;
            case "Slime4":
                SlimeCount4++;
                if (Slime4Count != null) Slime4Count.text = SlimeCount4.ToString();
                break;
            case "Slime5":
                SlimeCount5++;
                if (Slime5Count != null) Slime5Count.text = SlimeCount5.ToString();
                break;

            // --- Skeletons ---
            case "Skeleton1":
                SkeletonCount1++;
                if (Skeleton1Count != null) Skeleton1Count.text = SkeletonCount1.ToString();
                break;
            case "Skeleton2":
                SkeletonCount2++;
                if (Skeleton2Count != null) Skeleton2Count.text = SkeletonCount2.ToString();
                break;
            case "Skeleton3":
                SkeletonCount3++;
                if (Skeleton3Count != null) Skeleton3Count.text = SkeletonCount3.ToString();
                break;
            case "Skeleton4":
                SkeletonCount4++;
                if (Skeleton4Count != null) Skeleton4Count.text = SkeletonCount4.ToString();
                break;
            case "Skeleton5":
                SkeletonCount5++;
                if (Skeleton5Count != null) Skeleton5Count.text = SkeletonCount5.ToString();
                break;

            // --- Demons ---
            case "Demon1":
                DemonCount1++;
                if (Demon1Count != null) Demon1Count.text = DemonCount1.ToString();
                break;
            case "Demon2":
                DemonCount2++;
                if (Demon2Count != null) Demon2Count.text = DemonCount2.ToString();
                break;
            case "Demon3":
                DemonCount3++;
                if (Demon3Count != null) Demon3Count.text = DemonCount3.ToString();
                break;
            case "Demon4":
                DemonCount4++;
                if (Demon4Count != null) Demon4Count.text = DemonCount4.ToString();
                break;
            case "Demon5":
                DemonCount5++;
                if (Demon5Count != null) Demon5Count.text = DemonCount5.ToString();
                break;

            // --- Cows ---
            case "Cow1":
                CowCount1++;
                if (Cow1Count != null) Cow1Count.text = CowCount1.ToString();
                break;
            case "Cow2":
                CowCount2++;
                if (Cow2Count != null) Cow2Count.text = CowCount2.ToString();
                break;
            case "Cow3":
                CowCount3++;
                if (Cow3Count != null) Cow3Count.text = CowCount3.ToString();
                break;
            case "Cow4":
                CowCount4++;
                if (Cow4Count != null) Cow4Count.text = CowCount4.ToString();
                break;
            case "Cow5":
                CowCount5++;
                if (Cow5Count != null) Cow5Count.text = CowCount5.ToString();
                break;

            default:
                Debug.LogWarning($"KillCounter received an unknown enemy type: {enemy}");
                break;
        }
    }

    void SaveCounts()
    {
        // Zombies
        PlayerPrefs.SetInt("ZombieCount1", ZombieCount1);
        PlayerPrefs.SetInt("ZombieCount2", ZombieCount2);
        PlayerPrefs.SetInt("ZombieCount3", ZombieCount3);
        PlayerPrefs.SetInt("ZombieCount4", ZombieCount4);
        PlayerPrefs.SetInt("ZombieCount5", ZombieCount5);

        // Slimes
        PlayerPrefs.SetInt("SlimeCount1", SlimeCount1);
        PlayerPrefs.SetInt("SlimeCount2", SlimeCount2);
        PlayerPrefs.SetInt("SlimeCount3", SlimeCount3);
        PlayerPrefs.SetInt("SlimeCount4", SlimeCount4);
        PlayerPrefs.SetInt("SlimeCount5", SlimeCount5);

        // Skeletons
        PlayerPrefs.SetInt("SkeletonCount1", SkeletonCount1);
        PlayerPrefs.SetInt("SkeletonCount2", SkeletonCount2);
        PlayerPrefs.SetInt("SkeletonCount3", SkeletonCount3);
        PlayerPrefs.SetInt("SkeletonCount4", SkeletonCount4);
        PlayerPrefs.SetInt("SkeletonCount5", SkeletonCount5);

        // Demons
        PlayerPrefs.SetInt("DemonCount1", DemonCount1);
        PlayerPrefs.SetInt("DemonCount2", DemonCount2);
        PlayerPrefs.SetInt("DemonCount3", DemonCount3);
        PlayerPrefs.SetInt("DemonCount4", DemonCount4);
        PlayerPrefs.SetInt("DemonCount5", DemonCount5);

        // Cows
        PlayerPrefs.SetInt("CowCount1", CowCount1);
        PlayerPrefs.SetInt("CowCount2", CowCount2);
        PlayerPrefs.SetInt("CowCount3", CowCount3);
        PlayerPrefs.SetInt("CowCount4", CowCount4);
        PlayerPrefs.SetInt("CowCount5", CowCount5);

        PlayerPrefs.Save();
    }

    void LoadCounts()
    {
        // Zombies
        ZombieCount1 = PlayerPrefs.GetInt("ZombieCount1", 0);
        ZombieCount2 = PlayerPrefs.GetInt("ZombieCount2", 0);
        ZombieCount3 = PlayerPrefs.GetInt("ZombieCount3", 0);
        ZombieCount4 = PlayerPrefs.GetInt("ZombieCount4", 0);
        ZombieCount5 = PlayerPrefs.GetInt("ZombieCount5", 0);

        // Slimes
        SlimeCount1 = PlayerPrefs.GetInt("SlimeCount1", 0);
        SlimeCount2 = PlayerPrefs.GetInt("SlimeCount2", 0);
        SlimeCount3 = PlayerPrefs.GetInt("SlimeCount3", 0);
        SlimeCount4 = PlayerPrefs.GetInt("SlimeCount4", 0);
        SlimeCount5 = PlayerPrefs.GetInt("SlimeCount5", 0);

        // Skeletons
        SkeletonCount1 = PlayerPrefs.GetInt("SkeletonCount1", 0);
        SkeletonCount2 = PlayerPrefs.GetInt("SkeletonCount2", 0);
        SkeletonCount3 = PlayerPrefs.GetInt("SkeletonCount3", 0);
        SkeletonCount4 = PlayerPrefs.GetInt("SkeletonCount4", 0);
        SkeletonCount5 = PlayerPrefs.GetInt("SkeletonCount5", 0);

        // Demons
        DemonCount1 = PlayerPrefs.GetInt("DemonCount1", 0);
        DemonCount2 = PlayerPrefs.GetInt("DemonCount2", 0);
        DemonCount3 = PlayerPrefs.GetInt("DemonCount3", 0);
        DemonCount4 = PlayerPrefs.GetInt("DemonCount4", 0);
        DemonCount5 = PlayerPrefs.GetInt("DemonCount5", 0);

        // Cows
        CowCount1 = PlayerPrefs.GetInt("CowCount1", 0);
        CowCount2 = PlayerPrefs.GetInt("CowCount2", 0);
        CowCount3 = PlayerPrefs.GetInt("CowCount3", 0);
        CowCount4 = PlayerPrefs.GetInt("CowCount4", 0);
        CowCount5 = PlayerPrefs.GetInt("CowCount5", 0);
    }

    void UpdateAllUI()
    {
        if (Zombie1Count != null) Zombie1Count.text = ZombieCount1.ToString();
        if (Zombie2Count != null) Zombie2Count.text = ZombieCount2.ToString();
        if (Zombie3Count != null) Zombie3Count.text = ZombieCount3.ToString();
        if (Zombie4Count != null) Zombie4Count.text = ZombieCount4.ToString();
        if (Zombie5Count != null) Zombie5Count.text = ZombieCount5.ToString();

        if (Slime1Count != null) Slime1Count.text = SlimeCount1.ToString();
        if (Slime2Count != null) Slime2Count.text = SlimeCount2.ToString();
        if (Slime3Count != null) Slime3Count.text = SlimeCount3.ToString();
        if (Slime4Count != null) Slime4Count.text = SlimeCount4.ToString();
        if (Slime5Count != null) Slime5Count.text = SlimeCount5.ToString();

        if (Skeleton1Count != null) Skeleton1Count.text = SkeletonCount1.ToString();
        if (Skeleton2Count != null) Skeleton2Count.text = SkeletonCount2.ToString();
        if (Skeleton3Count != null) Skeleton3Count.text = SkeletonCount3.ToString();
        if (Skeleton4Count != null) Skeleton4Count.text = SkeletonCount4.ToString();
        if (Skeleton5Count != null) Skeleton5Count.text = SkeletonCount5.ToString();

        if (Demon1Count != null) Demon1Count.text = DemonCount1.ToString();
        if (Demon2Count != null) Demon2Count.text = DemonCount2.ToString();
        if (Demon3Count != null) Demon3Count.text = DemonCount3.ToString();
        if (Demon4Count != null) Demon4Count.text = DemonCount4.ToString();
        if (Demon5Count != null) Demon5Count.text = DemonCount5.ToString();

        if (Cow1Count != null) Cow1Count.text = CowCount1.ToString();
        if (Cow2Count != null) Cow2Count.text = CowCount2.ToString();
        if (Cow3Count != null) Cow3Count.text = CowCount3.ToString();
        if (Cow4Count != null) Cow4Count.text = CowCount4.ToString();
        if (Cow5Count != null) Cow5Count.text = CowCount5.ToString();
    }
}