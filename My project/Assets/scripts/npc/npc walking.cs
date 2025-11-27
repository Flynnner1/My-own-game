using UnityEngine;

[RequireComponent(typeof(NPCController))]
[RequireComponent(typeof(Rigidbody2D))]
public class NPCMovement2D : MonoBehaviour
{
    public Transform[] waypoints; // Points the NPC walks between
    public float moveSpeed = 2f; // Speed of the NPC

    private NPCController npcController;
    private Rigidbody2D rb;
    private int currentWaypointIndex = 0;
    private bool isMoving = false; // Flag to control movement state

    void Start()
    {
        npcController = GetComponent<NPCController>();
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody2D settings are appropriate (e.g., Kinematic or Dynamic with Gravity Scale 0)
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.gravityScale = 0; // Prevent falling if dynamic
        }

        // Start moving if waypoints exist
        if (waypoints.Length > 0)
        {
            isMoving = true;
        }
        else
        {
            Debug.LogWarning("NPCMovement2D: No waypoints assigned.", this);
            isMoving = false;
        }
    }

    void FixedUpdate() // Use FixedUpdate for Rigidbody operations
    {
        // --- Check UI State ---
        if (npcController.IsUIOpen)
        {
            // If UI is open, stop movement
            if (isMoving)
            {
                rb.linearVelocity = Vector2.zero; // Stop immediately if dynamic
                isMoving = false; // Prevent further movement calculations
            }
            return; // Exit FixedUpdate early
        }
        else
        {
            // If UI is closed and we were previously stopped by UI, resume moving
            if (!isMoving && waypoints.Length > 0)
            {
                isMoving = true;
            }
        }
        // --- End Check UI State ---


        // --- Movement Logic ---
        if (!isMoving || waypoints.Length == 0)
        {
            // Ensure velocity is zero if not supposed to be moving
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.linearVelocity = Vector2.zero;
            }
            return; // Don't move if flag is false or no waypoints
        }

        // Get the target waypoint position
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 targetPosition = targetWaypoint.position;

        // Calculate direction and distance
        Vector2 currentPosition = rb.position; // Use rb.position for kinematic or dynamic
        Vector2 direction = (targetPosition - currentPosition).normalized;
        float distance = Vector2.Distance(currentPosition, targetPosition);

        // Check if close enough to the target waypoint
        if (distance < 0.1f) // Adjust threshold as needed
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            // Move towards the target waypoint
            Vector2 newPos = currentPosition + direction * moveSpeed * Time.fixedDeltaTime;

            if (rb.bodyType == RigidbodyType2D.Kinematic)
            {
                rb.MovePosition(newPos); // Use MovePosition for kinematic bodies
            }
            else // Dynamic
            {
                rb.linearVelocity = direction * moveSpeed; // Set velocity for dynamic bodies
                // Optional: Face the direction of movement (add sprite flipping logic if needed)
            }
        }
        // --- End Movement Logic ---
    }
}