using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Attach this to your InventoryManager GameObject.
/// Make sure all possible Item ScriptableObjects are in the "allItems" array for look-up.
/// </summary>
public class InventorySaveSystem : MonoBehaviour
{
    //made by chatgpt tried to make it so it saves and loads inventory items
    [Header("Reference to all Item ScriptableObjects in the game")]
    public Item[] allItems; // Populate this list in the Inspector with all items.

    public InventoryManager inventoryManager;

    private string SaveFilePath => Path.Combine(Application.persistentDataPath, "inventory_save.json");

    [System.Serializable]
    public class InventorySaveData
    {
        public List<SlotData> slots = new List<SlotData>();
    }

    [System.Serializable]
    public class SlotData
    {
        public string itemName; // Use a unique identifier if possible
        public int count;
        public int slotIndex;
    }

    // Call this to save the inventory
    public void SaveInventory()
    {
        var saveData = new InventorySaveData();

        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
        {
            InventorySlot slot = inventoryManager.inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item != null)
            {
                saveData.slots.Add(new SlotData
                {
                    itemName = itemInSlot.item.name,
                    count = itemInSlot.count,
                    slotIndex = i
                });
            }
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SaveFilePath, json);
        Debug.Log("Inventory saved to " + SaveFilePath);
    }

    // Call this to load the inventory at startup
    public void LoadInventory()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.Log("No inventory save file found.");
            return;
        }

        string json = File.ReadAllText(SaveFilePath);
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);

        // Clear existing inventory
        foreach (var slot in inventoryManager.inventorySlots)
        {
            InventoryItem item = slot.GetComponentInChildren<InventoryItem>();
            if (item != null)
                Destroy(item.gameObject);
        }

        // Restore saved items
        foreach (var slotData in saveData.slots)
        {
            Item item = GetItemByName(slotData.itemName);
            if (item != null)
            {
                // Spawn a new item in the correct slot
                GameObject newItemGo = Instantiate(inventoryManager.inventoryItemPrefab, inventoryManager.inventorySlots[slotData.slotIndex].transform);
                InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
                inventoryItem.InitialiseItem(item);
                inventoryItem.count = slotData.count;
                inventoryItem.RefreshCount();
            }
            else
            {
                Debug.LogWarning("Could not find item with name: " + slotData.itemName);
            }
        }
        Debug.Log("Inventory loaded from " + SaveFilePath);
    }

    // Helper to find an Item ScriptableObject by name
    private Item GetItemByName(string name)
    {
        foreach (var item in allItems)
        {
            if (item.name == name)
                return item;
        }
        return null;
    }

    // Example: call SaveInventory when closing the game or on demand
    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    // Example: call LoadInventory at start
    private void Start()
    {
        LoadInventory();
    }
}