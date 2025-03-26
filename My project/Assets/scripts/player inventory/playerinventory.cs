using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coins = 1;

    //public InventroyItem inventroyItem;

    //public void Start()
    //{
    //    if (inventroyItem == null)
    //    {
    //        inventroyItem = GetComponent<InventroyItem>();
    //    }
    //}

    //public void CoinPickup()
    //{
    //    coins += 1;
    //    Debug.Log("+1 coin");
    //    // Only add one coin to the InventroyItem count
    //    if (inventroyItem != null)
    //    {
    //        inventroyItem.addcoins(1);
    //    }
    //}

    //public void AddCoinsQuest(float AmountCoins)
    //{
    //    // Increment total coins
    //    coins += (int)AmountCoins;
    //    Debug.LogWarning("Added " + AmountCoins + " quest coins");

    //    // Reflect in InventroyItem if needed
    //    if (inventroyItem != null)
    //    {
    //        inventroyItem.addcoins((int)AmountCoins);
    //    }
    //}
    public void CoinPickup()
    {
        //amountOfKills.text = kills.ToString();
    }
}