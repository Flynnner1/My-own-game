using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellpickup : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    private bool pickedUp = false;

    // Set this to true in the inspector if you want a random spell
    public bool useRandomSpell = false;
    // Otherwise set a default ID for the spell
    public int spellID = 0;

    void Start()
    {
        Debug.Log("spell created");
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            inventoryManager = other.GetComponent<InventoryManager>();
            if (inventoryManager != null)
            {
                pickedUp = true;
                if (useRandomSpell)
                {
                    int randomID = Random.Range(0, itemsToPickup.Length);
                    PickUpspell(randomID);
                }
                else
                {
                    PickUpspell(spellID);
                }
            }
            else if (inventoryManager == null)
            {
                inventoryManager = other.GetComponent<InventoryManager>();
                Debug.LogError("Inventory component not found on Player!");
            }
        }
        else
        {
            Debug.Log("You didn't pick up a spell");
        }
    }

    public void PickUpspell(int id)
    {
        if (id < 0 || id >= itemsToPickup.Length)
        {
            Debug.LogError("Invalid spell ID: " + id);
            return;
        }
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Spell added");
        }
        else
        {
            Debug.Log("Spell not added");
        }
    }
}