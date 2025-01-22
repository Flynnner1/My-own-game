using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset from the player

    void Start()
    {
        // Initialize the offset based on the initial positions
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update the camera position to follow the player with the offset
        transform.position = player.position + offset;
    }
}