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
    private bool npcUIstate = false;

    void Update()
    {
        // If the player is in range and right mouse button is clicked, show the main quest UI
        if (isPlayerNearby && Input.GetMouseButtonDown(1))
        {
            questUI.SetActive(true);
            // Also show the inventory UI when the main quest screen is active
            inventoryUI.SetActive(true);

            if (questUI.activeSelf)
            {
                npcUIstate = true;
            }
            else
            {
                npcUIstate = false;
            }
        }

        // Show the smaller quest UI if the quest is active but the main quest UI is not
        if (npcQuest == true)
        {
            if (!questUI.activeSelf && questTracer.quest1)
            {
                smallquestUI.SetActive(true);
            }
            else if (questUI.activeSelf)
            {
                // If the main quest UI is open, hide the smaller quest UI
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
            questUI.SetActive(false);
            smallquestUI.SetActive(false);
            // Hide the inventory UI when leaving the NPC's range
            inventoryUI.SetActive(false);
        }
    }

    /// <summary>
    /// Call this method from a UI Button in questUI to accept the quest.
    /// </summary>
    public void AcceptQuestButton()
    {
        // Call the acceptQuest method in QuestTracer
        questTracer.acceptQuest();

        // Hide the main quest UI and show the smaller UI
        questUI.SetActive(false);
        smallquestUI.SetActive(true);
        // Optionally hide the inventory UI here if desired
        inventoryUI.SetActive(false);
    }
}