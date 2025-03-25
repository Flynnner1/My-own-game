using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world space
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0; // Keep the player on the same plane

        // Calculate the rotation needed to look at the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}