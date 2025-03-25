using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float speed = 5f; // Speed of the movement
    public float stopRange = 30f; // Range within which the object will move towards the player

    void Start()
    {
        // Find the player object in the scene using the tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // Check if the player reference is assigned
        if (player != null)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Move towards the player if within the specified range
            if (distanceToPlayer <= stopRange)
            {
                // Move the object towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }
}