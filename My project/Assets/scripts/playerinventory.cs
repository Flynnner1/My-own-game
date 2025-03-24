using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float coins = 0f;
    public TMP_Text coinCountText; // Reference to the UI Text component
   

    public void CoinPickup()
    {
        coins += 1f;
        Debug.Log("+1 coin");
        updateCoins();
    }
    public void AddCoinsQuest(float AmountCoins)
    {
        Debug.LogWarning("added the " + AmountCoins);
        updateCoins();
    }
    public void updateCoins()
    {
        coinCountText.text = "" + coins;

    }
}
