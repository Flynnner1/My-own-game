using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMovement : MonoBehaviour
{
    public float speed = 1f;
    public float runSpeed = 3f;
    public float runDuration = 2f;

    private float currentSpeed;
    private bool isRunning = false;
    private float runTimer = 0f;

    private Vector2 target;
    private float changeTargetTime = 2f;
    private float timer;

    void Start()
    {
        currentSpeed = speed;
        PickNewTarget();
    }

    void Update()
    {
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
                currentSpeed = speed;
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