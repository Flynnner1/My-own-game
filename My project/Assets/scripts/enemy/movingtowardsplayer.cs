using Unity.VisualScripting;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float speed = 5f; // Speed of the movement
    public float stopRange = 30f; // Range within which the object will move towards the player

    public float Cowspeed = 1f;
    public float runSpeed = 3f;
    public float runDuration = 2f;

    private float currentSpeed;
    private bool isRunning = false;
    private float runTimer = 0f;

    private Vector2 target;
    private float changeTargetTime = 2f;
    private float timer;

    public bool Cow = false;
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
        if (Cow)
        {


            void Start()
            {
                currentSpeed = Cowspeed;
                PickNewTarget();
            }


            timer += Time.deltaTime;
            if (timer > changeTargetTime)
            {
                PickNewTarget();
                timer = 0f;
            }

            // Handle running
            if (isRunning)
            {
                runTimer += Time.deltaTime;
                if (runTimer >= runDuration)
                {
                    isRunning = false;
                    currentSpeed = Cowspeed;
                }
            }

            Vector2 currentPosition = transform.position;
            Vector2 direction = (target - currentPosition).normalized;

            // Only move if not at target
            if ((target - currentPosition).sqrMagnitude > 0.01f)
            {
                transform.position = Vector2.MoveTowards(currentPosition, target, currentSpeed * Time.deltaTime);

                // Calculate angle and apply rotation
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }


        }
    }
    public void Run()
    {
        Debug.Log("Cow starts running!");
        isRunning = true;
        currentSpeed = runSpeed;
        runTimer = 0f;
    }

    void PickNewTarget()
    {
        float range = 3f;
        target = new Vector2(
            transform.position.x + Random.Range(-range, range),
            transform.position.y + Random.Range(-range, range)
        );
    }
}
