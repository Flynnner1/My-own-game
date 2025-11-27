using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthmanager : MonoBehaviour
{
    public static Healthmanager Instance;
    public Image healthBar;
    public float healthAmount = 100f;
    public float SecrethealthAmount = 100f;

    public PlayerHealth playerHealth;
    public PlayerCoins playerCoins; // Add this reference

    void Awake()
    {
        // Implement singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        //LoadHealthData();
        //UpdateHealthUI();

        // Find the player and its PlayerHealth component
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            // Also find or assign PlayerCoins
            if (!playerCoins)
            {
                playerCoins = playerObject.GetComponent<PlayerCoins>();
            }
        }
        else
        {
            Debug.LogError("Player not found in scene!");
        }
    }

    void OnApplicationQuit()
    {
        //SaveHealthData();
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / SecrethealthAmount;
        if (healthAmount <= 0f)
        {
            Debug.Log("Player died via Healthmanager.");
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
        }
        //SaveHealthData();
    }

    public void Heal(float healingAmount)
    {
        if (playerCoins != null)
        {
            healthAmount += healingAmount;
            healthAmount = Mathf.Clamp(healthAmount, 0, 100);
            healthBar.fillAmount = healthAmount / 100f;
            playerCoins.coins -= 10;    // Safe to update now
            playerCoins.updatecointext();
            //SaveHealthData();
        }
        else
        {
            Debug.LogError("playerCoins is not assigned. Cannot charge the player.");
        }
    }

    void SaveHealthData()
    {
        PlayerPrefs.SetFloat("Healthmanager_healthAmount", healthAmount);
        PlayerPrefs.SetFloat("Healthmanager_SecrethealthAmount", SecrethealthAmount);
        PlayerPrefs.Save();
    }

    void LoadHealthData()
    {
        healthAmount = PlayerPrefs.GetFloat("Healthmanager_healthAmount", 100f);
        SecrethealthAmount = PlayerPrefs.GetFloat("Healthmanager_SecrethealthAmount", 100f);
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.fillAmount = healthAmount / 100f;
    }
}