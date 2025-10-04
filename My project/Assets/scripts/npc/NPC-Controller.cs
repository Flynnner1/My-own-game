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
        // Check whether the player is near the NPC and if right mouse is clicked
        if (isPlayerNearby && Input.GetMouseButtonDown(1))
        {
            // Make sure questUI is assigned and not null
            if (questUI != null)
            {
                questUI.SetActive(true);
            }
            // Also show the inventory UI
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(true);
            }
        }

        // Update the IsUIOpen state based on whether questUI or inventoryUI is active
        IsUIOpen = (questUI != null && questUI.activeSelf) || (inventoryUI != null && inventoryUI.activeSelf);

        // --- Small Quest UI Logic ---
        // Decide whether to show the small quest UI based on quest status and main UI visibility
        bool showSmallQuestUI = npcQuest && questTracer != null && questTracer.quest1 && !IsUIOpen; // Don't show if main UI is open

        if (smallquestUI != null)
        {
            if (showSmallQuestUI)
            {
                smallquestUI.SetActive(true);
            }
            else
            {
                smallquestUI.SetActive(false); // <<-- SET TO FALSE HERE
            }
        }
        // --- End Small Quest UI Logic ---
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
            // Close all UIs when player leaves
            if (questUI != null)
                questUI.SetActive(false);
            if (smallquestUI != null)
                smallquestUI.SetActive(false); // <<-- SET TO FALSE HERE
            if (inventoryUI != null)
                inventoryUI.SetActive(false);

            // Ensure IsUIOpen is updated when player leaves
            IsUIOpen = false;
        }
    }

    public void AcceptQuestButton()
    {
        // Call the acceptQuest method in QuestTracer
        if (questTracer != null)
            questTracer.acceptQuest();

        // Close main quest and inventory UIs
        if (questUI != null)
            questUI.SetActive(false);
        if (inventoryUI != null)
            inventoryUI.SetActive(false);

        // Optionally show small quest UI immediately after accepting
        if (smallquestUI != null && npcQuest && questTracer != null && questTracer.quest1)
            smallquestUI.SetActive(true);

        // Update IsUIOpen state
        IsUIOpen = false;
    }
}