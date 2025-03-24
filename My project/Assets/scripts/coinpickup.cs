using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private Inventory inventory;
    private bool pickedUp = false;

    void Start()
    {
        Debug.Log("Coin created");
        // Initialization if needed
    }

    void Update()
    {
        // Update logic if needed
    }

    // Use OnTriggerEnter2D for 2D colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                pickedUp = true;
                inventory.CoinPickup();
                Debug.Log("You've picked up a coin");
                Destroy(gameObject);

            }
            else
            {
                Debug.LogError("Inventory component not found on Player!");
            }
        }
        else
        {
            Debug.Log("You didn't pick up a coin");
        }
    }
}