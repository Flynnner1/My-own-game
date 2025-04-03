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

            npcUIstate = (questUI != null && questUI.activeSelf);
        }

        // If the quest is active, show/hide the smallquestUI
        if (npcQuest && questTracer != null && questTracer.quest1)
        {
            if (questUI != null && !questUI.activeSelf)
            {
                if (smallquestUI != null)
                    smallquestUI.SetActive(true);
            }
            else
            {
                if (smallquestUI != null)
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
        }
    }

   
    public void AcceptQuestButton()
    {
        // Call the acceptQuest method in QuestTracer
        if (questTracer != null)
            questTracer.acceptQuest();

        if (questUI != null)
            questUI.SetActive(false);

        if (smallquestUI != null)
            smallquestUI.SetActive(true);

        if (inventoryUI != null)
            inventoryUI.SetActive(false);
    }
}