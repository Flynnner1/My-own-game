using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickUpItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("added item");
        }
        else if (result == false)
        {
            Debug.Log("item not added");
        }
    }
    public void GetSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem();
        if (recievedItem != null)
        {
            Debug.Log("recieved item");
        }
        else
        {
            Debug.Log("didnt recieve a item");

        }

    }
}
