using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    public CoinPickup coinPickup;
    public TMP_Text coinText;

    public int coins = 0;

    void Start()
    {
        // Find or assign references if on different objects
        if (!coinPickup)
        {
            coinPickup = FindObjectOfType<CoinPickup>();
        }
    }

    public void addcoins(int addcoin)
    {
        coins += addcoin;
        updatecointext();
    }

    public void updatecointext()
    {
        if (coinText)
        {
            coinText.text = coins.ToString();
        }
        else
        {
            Debug.LogWarning("coinText not assigned on PlayerCoins.");
        }
    }
}