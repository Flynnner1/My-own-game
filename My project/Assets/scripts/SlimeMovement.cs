using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public Transform player;

    public float Speed = 5f;       // Speed at which the object moves forward.
    public float moveDuration = 1f;    // Time to move forward before stopping.
    public float stopDuration = 1f;
    public float stopRange = 20f;// Time to stop before moving again.

    private bool isMoving = false;     // Flag to control movement.

    void Start()
    {
        // Start the movement cycle coroutine.
        StartCoroutine(MoveStopCycle());
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    IEnumerator MoveStopCycle()
    {
        while (true)
        {
            // Set the object to move forward.
            isMoving = true;
            yield return new WaitForSeconds(moveDuration);

            // Stop the object.
            isMoving = false;
            yield return new WaitForSeconds(stopDuration);
        }
    }

    
    void Update()
    {
        if (isMoving)
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
                    transform.position = Vector3.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
                }
            }
        }
    }
}
