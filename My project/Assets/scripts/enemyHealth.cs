using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // The total health of the enemy
    public float maxHealth = 100f;
    public float currentHealth = 25f;



    // Start is called before the first frame update
    void Start()
    {
        // Initialize enemy health to the maximum health at the start
        currentHealth = maxHealth;
        Debug.Log("Enemy health initialized to " + currentHealth);
    }

    // Method to take damage
    public void TakeDamage(float amount)
    {
        // Reduce the current health by the damage amount
        currentHealth -= amount;
        Debug.Log("Enemy took damage. Current health: " + currentHealth);

        // Check if the health is less than or equal to zero
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Method to handle enemy's death
    void Die()
    {
        Debug.Log("Enemy died.");
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}