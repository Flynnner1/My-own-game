using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XpManager : MonoBehaviour
{
    public Image healthBar;
    public float xpAmount = 0f;

    public PlayerXp playerXp;
    public int level = 0;
    public TMP_Text Level;
    public int MaxXp = 100;
    public float MaxXpF = 100f;

    void Start()
    {
        LoadXpData();
        UpdateUI();

        // Find the player and its PlayerXp component
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerXp = playerObject.GetComponent<PlayerXp>();
            if (!playerXp)
            {
                playerXp = playerObject.GetComponent<PlayerXp>();
            }
        }
        else
        {
            Debug.LogError("Player not found in scene!");
        }
    }

    void OnApplicationQuit()
    {
        SaveXpData();
    }

    public void PlusXp(float amount)
    {
        xpAmount += amount;
        xpAmount = Mathf.Clamp(xpAmount, 0, MaxXp);

        // Update the UI fill amount
        healthBar.fillAmount = xpAmount / MaxXpF;

        // Optionally sync with PlayerXp if you want both to track
        if (playerXp != null)
        {
            playerXp.addXp((int)amount);
        }

        // If the XP bar reaches MaxXp, reset and level up
        if (xpAmount >= MaxXp)
        {
            xpAmount = 0;
            level++;
            MaxXp += 5;
            MaxXpF += 5f;
            updateleveltext();
            healthBar.fillAmount = 0f;
        }
        SaveXpData(); // Optionally save after every XP change
    }

    public void updateleveltext()
    {
        if (Level)
        {
            Level.text = level.ToString();
        }
        else
        {
            Debug.LogWarning("Level not assigned on XpManager.");
        }
    }

    void SaveXpData()
    {
        PlayerPrefs.SetFloat("xpAmount", xpAmount);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("MaxXp", MaxXp);
        PlayerPrefs.SetFloat("MaxXpF", MaxXpF);
        PlayerPrefs.Save();
    }

    void LoadXpData()
    {
        xpAmount = PlayerPrefs.GetFloat("xpAmount", 0f);
        level = PlayerPrefs.GetInt("level", 0);
        MaxXp = PlayerPrefs.GetInt("MaxXp", 100);
        MaxXpF = PlayerPrefs.GetFloat("MaxXpF", 100f);
    }

    void UpdateUI()
    {
        if (healthBar != null)
            healthBar.fillAmount = xpAmount / MaxXpF;
        updateleveltext();
    }
}