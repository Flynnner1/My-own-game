using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using UnityEditor.Experimental.GraphView; // This is likely not needed unless you are using GraphView directly

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

    // It's good practice to call this method ReportKill or IncrementKillCount
    // to be clearer about its purpose.
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

            default:
                Debug.LogWarning($"KillCounter received an unknown enemy type: {enemy}");
                break;
        }

    }

    
}