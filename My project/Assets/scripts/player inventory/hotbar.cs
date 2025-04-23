using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Removed: using Microsoft.Unity.VisualStudio.Editor;
// Removed: using UnityEditor.Tilemaps;

public class hotbar : MonoBehaviour
{
    public int slotNum = 1;
    GameObject currentslot;
    public InventoryManager inventoryManager;
    Item recievedItem;
    // Start is called before the first frame update
    void Start()
    {
        if (inventoryManager == null)
        {
            // It's generally better practice to get the component directly
            // if it's on the same GameObject or a parent/child.
            // Assuming InventoryManager is on the same GameObject:
            inventoryManager = GetComponent<InventoryManager>();

            // If InventoryManager might be elsewhere, you might need a different approach
            // like FindObjectOfType<InventoryManager>() but GetComponent is often preferred.
            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager not found on the GameObject!", this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryManager == null) return; // Don't proceed if InventoryManager wasn't found

        // The commented-out code seems like it was for handling input
        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //    inventoryManager.GetSelectedItem(false); // Assuming GetSelectedItem(true) uses the item
        // }

        // It looks like you want to get the currently selected item
        recievedItem = inventoryManager.GetSelectedItem(false); // Assuming false means don't consume/use

        // Finding GameObjects by string concatenation in Update is inefficient.
        // Consider setting this up in Start or using a more direct reference if possible.
        // However, keeping the original logic for now:
        string slotTag = "slot " + slotNum;
        currentslot = GameObject.FindGameObjectWithTag(slotTag);

        if (currentslot != null)
        {
            // item = currentslot.GetComponentInChildren<Item>(); // Assuming Item is a component script
            // This Debug.LogWarning might be spammy in Update. Consider if you really need it every frame.
            // Debug.LogWarning("Current slot GameObject: " + currentslot.name, this);
        }
        else
        {
            // Log an error if the slot wasn't found, this helps debugging.
            // Only log once or less frequently if this can happen normally.
            Debug.LogWarning("Could not find GameObject with tag: " + slotTag, this);
        }

        // This Debug.LogWarning might also be spammy.
        // Debug.LogWarning("Received item: " + (recievedItem != null ? recievedItem.name : "null"), this);
    }
}