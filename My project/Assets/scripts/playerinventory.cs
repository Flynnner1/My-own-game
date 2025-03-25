using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coins = 1;

    public InventroyItem inventroyItem;

    public void Start()
    {
        if (inventroyItem == null)
        {
            inventroyItem = GetComponent<InventroyItem>();
        }
    }
    public void CoinPickup()
    {
        coins += 1;
        Debug.Log("+1 coin");
        inventroyItem.addcoins(coins);
        
    }
    public void AddCoinsQuest(float AmountCoins)
    {
        Debug.LogWarning("added the " + AmountCoins);
        
    }
    
}
