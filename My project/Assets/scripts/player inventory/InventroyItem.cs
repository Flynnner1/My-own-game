using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public TMP_Text countText;

    [Header("UI")]
    [SerializeField] private Image image;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        // If you haven't assigned 'image' in the Inspector, this fetches it from the GameObject
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        if (item.image != null)
        {
            image.sprite = newItem.image;
        }
        RefreshCount();
    }

    public void RefreshCount()
    {
        if (countText != null)
        {
            countText.text = count.ToString();
            countText.gameObject.SetActive(count > 1);
        }
        else
        {
            Debug.LogWarning("No TMP_Text assigned for countText in InventoryItem prefab.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        if (image != null) image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        if (image != null) image.raycastTarget = true;
    }

    public void AddCoins(int coins)
    {
        count += coins;
        RefreshCount();
    }
}