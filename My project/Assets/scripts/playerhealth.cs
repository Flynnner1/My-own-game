using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    private float currentHealth;

    void Start()
    {
        // Initialize the player's health to the maximum health at the start
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(float amount)
    {
        // Reduce the current health by the damage amount
        currentHealth -= amount;

        // Check if the health is less than or equal to zero
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Method to handle player's death
    void Die()
    {
        // Handle what happens when the player dies
        Debug.Log("Player has died!");
        // For example, you might disable the player object or trigger a respawn
        gameObject.SetActive(false);
    }
}