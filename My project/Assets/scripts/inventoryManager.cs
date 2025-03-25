using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;

    public GameObject inventoryItemPrefab;

    public int maxCount = 4;
    public int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSelectedSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSelectedSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSelectedSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSelectedSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSelectedSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSelectedSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeSelectedSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeSelectedSlot(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeSelectedSlot(8);
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventroyItem itemInSlot = slot.GetComponentInChildren<InventroyItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxCount)//&& itemInSlot.item.stackable == true
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventroyItem itemInSlot = slot.GetComponentInChildren<InventroyItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventroyItem inventoryItem = newItemGo.GetComponent<InventroyItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventroyItem itemInSlot = slot.GetComponentInChildren<InventroyItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if ( use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }

            }
            return item;
        }
        return null;
    }
}