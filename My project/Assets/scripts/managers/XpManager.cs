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

    void Start()
    {
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlusXp(10);
        }
    }

    public void MinusXp(float damage)
    {
        xpAmount -= damage;
        xpAmount = Mathf.Clamp(xpAmount, 0, 100);
        healthBar.fillAmount = xpAmount / 100f;
    }

    public void PlusXp(float amount)
    {
        xpAmount += amount;
        xpAmount = Mathf.Clamp(xpAmount, 0, 100);

        // Update the UI fill amount
        healthBar.fillAmount = xpAmount / 100f;

        // Optionally sync with PlayerXp if you want both to track
        if (playerXp != null)
        {
            playerXp.addXp((int)amount);
        }

        // If the XP bar reaches 100, reset and level up
        if (xpAmount >= 100)
        {
            xpAmount = 0;
            level++;
            updateleveltext();
            healthBar.fillAmount = 0f;
        }
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
}