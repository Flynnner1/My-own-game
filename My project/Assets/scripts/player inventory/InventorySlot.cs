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
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem draggableItem = dropped.GetComponent<InventoryItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }

}