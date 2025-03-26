using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public bool pickedUp = false;
    public int coins = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            PlayerCoins playerCoins = other.GetComponent<PlayerCoins>();
            if (playerCoins != null)
            {
                // Increase the coin count and mark as picked up.
                coins += 1;
                pickedUp = true;
                playerCoins.addcoins(1);

                Debug.Log("You've picked up a coin");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("PlayerCoins component not found on Player!");
            }
        }
        else
        {
            Debug.Log("You didn't pick up a coin");
        }
    }
}