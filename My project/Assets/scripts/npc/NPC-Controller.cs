using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Tooltip("Reference to the main Quest UI panel.")]
    public GameObject questUI;
    [Tooltip("Reference to the smaller Quest UI panel.")]
    public GameObject smallquestUI;
    [Tooltip("Reference to the inventory UI panel.")]
    public GameObject inventoryUI;

    private bool isPlayerNearby = false;

    [Tooltip("Reference to the QuestTracer script.")]
    public QuestTracer questTracer;

    public bool npcQuest = false;

    // Public property to check if any relevant UI is open
    public bool IsUIOpen { get; private set; }

    void Update()
    {
        if (isPlayerNearby && Input.GetMouseButtonDown(1))
        {
            if (questUI != null)
            {
                questUI.SetActive(true);
            }
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(true);
            }
        }

        IsUIOpen = (questUI != null && questUI.activeSelf) || (inventoryUI != null && inventoryUI.activeSelf);

        bool showSmallQuestUI = npcQuest && questTracer != null && questTracer.quest1 && !IsUIOpen; // Don't show if main UI is open

        if (smallquestUI != null)
        {
            if (showSmallQuestUI)
            {
                smallquestUI.SetActive(true);
            }
            else
            {
                smallquestUI.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (questUI != null)
                questUI.SetActive(false);
            if (smallquestUI != null)
                smallquestUI.SetActive(false);
            if (inventoryUI != null)
                inventoryUI.SetActive(false);
            IsUIOpen = false;
        }
    }

    public void AcceptQuestButton()
    {
        if (questTracer != null)
            questTracer.acceptQuest();
        if (questUI != null)
            questUI.SetActive(false);
        if (inventoryUI != null)
            inventoryUI.SetActive(false);
        if (smallquestUI != null && npcQuest && questTracer != null && questTracer.quest1)
            smallquestUI.SetActive(true);
        IsUIOpen = false;
    }
}