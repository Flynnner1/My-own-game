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
            Debug.Log("I dropped something on)");
            GameObject dropped = eventData.pointerDrag;
            InventoryItem draggableItem = dropped.GetComponent<InventoryItem>();
            if (draggableItem == null)
                return;

            draggableItem.parentAfterDrag = transform;
            draggableItem.SwitchSlot(SlotNum);

            if (ArmorBar != null && draggableItem.item != null)
            {
                int armorIndex = draggableItem.item.armorNum;
                if (armorIndex == 0)
                {
                    ArmorBar.UnequipArmor(armorIndex);
                }
                else
                {
                    ArmorBar.EquipArmor(armorIndex);
                }
            }
        }
    }
}