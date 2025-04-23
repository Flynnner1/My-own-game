using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEngine.GraphicsBuffer; // Still likely unnecessary

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    // currentHealth is now managed by Healthmanager, you might not need it here
    // public float currentHealth = 100f;
    public GameObject deathscreen;
    // REMOVED: Healthmanager healthmanager;

    void Start()
    {
        // Initialize the player's health via the manager if needed,
        // though Healthmanager already sets its own healthAmount.
        // currentHealth = maxHealth; // You might remove this if Healthmanager handles all health logic

        // REMOVED: Healthmanager healthmanager = GetComponent<Healthmanager>();
    }

    public void Die()
    {
        Debug.Log("Player has died!");
        //gameObject.SetActive(false); // Disabling the player object might stop other scripts
        if (deathscreen != null) // Add null check for safety
        {
            deathscreen.SetActive(true);
        }
        Time.timeScale = 0; // Pause the game
    }

    // This method now tells the central Healthmanager to apply the damage
    public void TakeDamage(float damage)
    {
        // Use the Singleton instance of Healthmanager
        if (Healthmanager.Instance != null)
        {
            Healthmanager.Instance.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Healthmanager Instance is null. Cannot apply damage.");
        }
    }
}