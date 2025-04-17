using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    public float currentHealth = 100f;
    public GameObject deathscreen;

    void Start()
    {
        // Initialize the player's health to the maximum health at the start
        currentHealth = maxHealth;
    }

    public void Die()
    {
        // Handle what happens when the player dies
        Debug.Log("Player has died!");
        //gameObject.SetActive(false);
        deathscreen.SetActive(true);
        Time.timeScale = 0;
    }
}