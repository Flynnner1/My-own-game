using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public PlayerCoins playerCoins;
    public Item[] itemsToPickup;
    public int[] itemCosts;
    void Start()
    {
        if (!playerCoins)
        {
            playerCoins = FindObjectOfType<PlayerCoins>();
        }
    }
    public void PickUpItem(int id)
    {
        if (id < 0 || id >= itemsToPickup.Length || id >= itemCosts.Length)
        {
            Debug.Log("Invalid item ID");
            return;
        }
        if (playerCoins.coins >= itemCosts[id])
        {
            bool result = inventoryManager.AddItem(itemsToPickup[id]);
            if (result == true)
            {
                Debug.Log("added item");
                playerCoins.coins -= itemCosts[id];
                playerCoins.updatecointext();
            }
            else
            {
                Debug.Log("item not added");
            }
        }
        else
        {
            Debug.Log("not enough coins");
        }
    }
    public void GetSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(false);
        if (recievedItem != null)
        {
            Debug.Log("recieved item " + recievedItem);
        }
        else
        {
            Debug.Log("didnt recieve a item");
        }
    }
    public void UseGetSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(true);
        if (recievedItem != null)
        {
            Debug.Log("used item" + recievedItem);
        }
        else
        {
            Debug.Log("didnt use a item");
        }
    }
}
