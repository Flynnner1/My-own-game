using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public int SlotNum;
    public hotbar hotBar;
    public armorinventory ArmorBar;     // assign the player's armorinventory here for armor slots
    // helmetNum / shoulderNum are unused now but kept if you want per-slot indices
    public int helmetNum;
    public int shoulderNum;

    private void Awake()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        // Ensure alpha is at least 1 for visibility.
        if (selectedColor.a < 1f)
        {
            selectedColor = new Color(selectedColor.r, selectedColor.g, selectedColor.b, 1f);
        }
        if (notSelectedColor.a < 1f)
        {
            notSelectedColor = new Color(notSelectedColor.r, notSelectedColor.g, notSelectedColor.b, 1f);
        }
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
        hotBar.slotNum = SlotNum;
        //ArmorBar.helmetNum = helmetNum;
        //ArmorBar.shoulderNum = shoulderNum;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && eventData.pointerDrag != null)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem draggableItem = dropped.GetComponent<InventoryItem>();
            if (draggableItem == null)
                return;

            // move the UI element into this slot
            draggableItem.parentAfterDrag = transform;

            // If this slot is configured as an armor slot, attempt to equip the associated armor.
            // The Item script has an 'armorNum' field — use that to select which armor piece to enable.
            if (ArmorBar != null && draggableItem.item != null)
            {
                int armorIndex = draggableItem.item.armorNum;
                // Only equip if armorIndex is meaningful; you can choose convention (e.g., -1 = none, >=0 valid)
                if (armorIndex >= 0)
                {
                    ArmorBar.EquipArmor(armorIndex);
                }
            }
        }
    }
}