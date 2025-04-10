using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class hotbar : MonoBehaviour
{
    public int slotNum = 1;
    GameObject currentslot;
    public InventoryManager inventoryManager;
    Item recievedItem;
    // Start is called before the first frame update
    void Start()
    {   if (inventoryManager == null)
        {
            inventoryManager = GetComponent<InventoryManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    inventoryManager.GetSelectedItem(false);
        //}
        //else
        //{
        //    inventoryManager.GetSelectedItem(false);
        //}
        recievedItem = inventoryManager.GetSelectedItem(false);
        currentslot = GameObject.FindGameObjectWithTag("slot " + slotNum);
        //item = currentslot.GetComponentInChildren<Item>();
        Debug.LogWarning("this is " + currentslot);
        Debug.LogWarning("this is the " + recievedItem);
    }
}
