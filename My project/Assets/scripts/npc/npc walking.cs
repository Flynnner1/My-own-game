using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcwalking : MonoBehaviour
{
    public float walkRadius = 5f; // Radius within which the NPC will walk to random points
    public float walkSpeed = 2f; // Speed at which the NPC walks
    private Vector3 targetPosition; // The current target position the NPC is walking towards
    private bool questUIActive = false; // Flag to check if the quest UI is active

    // Reference to the NPCController to check the quest UI status
    public NPCController npcController;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (npcController.questUI.activeSelf) // Check if the quest UI is active
        {
            questUIActive = true;
        }
        else
        {
            questUIActive = false;
        }

        // If the quest UI is active, stop walking
        if (questUIActive)
        {
            return;
        }

        // Move the NPC towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);

        // If the NPC reached the target position, set a new random target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    // Method to set a new random target position within the walk radius
    void SetRandomTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Keep the y position the same (for 2D or top-down view)
        targetPosition = randomDirection;
    }
}