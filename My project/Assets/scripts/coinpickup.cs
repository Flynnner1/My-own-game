using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private playerinventory playerInventory;

    void Start()
    {
        // Initialization if needed
    }

    void Update()
    {
        // Update logic if needed
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<playerinventory>();
            if (playerInventory != null)
            {
                playerInventory.coinpickup();
                Debug.Log("You've picked up a coin");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("PlayerInventory component not found on Player!");
            }
        }
        else
        {
            Debug.Log("You didn't pick up a coin");
        }
    }
}