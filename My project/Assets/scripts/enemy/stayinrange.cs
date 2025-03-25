using UnityEngine;

public class StayInRange : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float speed = 5f; // Speed of the movement
    public float stoppingDistance = 6f; // Distance to maintain from the player
    public float maxDistance = 30f; // Maximum distance to follow the player

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

            // Move towards the player only if the distance is within the maxDistance and greater than the stopping distance
            if (distanceToPlayer <= maxDistance && distanceToPlayer > stoppingDistance)
            {
                // Calculate the direction towards the player
                Vector3 direction = (player.position - transform.position).normalized;

                // Move the object towards the player
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}