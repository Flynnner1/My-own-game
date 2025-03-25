using UnityEngine;

public class WatchPlayer : MonoBehaviour
{
    // Reference to the player's Transform
    public Transform player;

    // Offset to adjust the rotation (if the sprite isn't aligned with default orientation)
    public float rotationOffset = 0f;

    public float speed = 5f;

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
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - transform.position;

            // Get the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply rotation with offset
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + rotationOffset));

            // Uncomment the following line if you want the object to move towards the player
            // transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

    }
}