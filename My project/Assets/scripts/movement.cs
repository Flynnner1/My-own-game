using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndShooting : MonoBehaviour
{
    public int playerNumber = 1;
    private int pedalSpeed = 7;

    //public Transform spawnLocationProjectile;

    //public GameObject fireball1;
    //public GameObject fireball2;

    public Rigidbody2D rb;

    //public float time1 = 1f;
    //public float coolDownTimer1 = 3f;
    //public float spreadShotInterval = 0.5f; // Interval between each projectile in the spread shot

    // Enum to keep track of the shooting mode
    //private enum ShootingMode
    //{
    //    Mode1,
    //    Mode2
    //}

    // Variable to store the current shooting mode
    //private ShootingMode currentMode;

    //private bool isShootingSpread = false; // To track if the spread shooting coroutine is running

    // Start is called before the first frame update
    void Start()
    {
        // Set the default shooting mode
        //currentMode = ShootingMode.Mode1;
    }

    // Update is called once per frame
    void Update()
    {
        //time1 += Time.deltaTime;

        // Player movement
        if (Input.GetKey(KeyCode.W) && playerNumber == 1)
        {
            transform.position += new Vector3(0, pedalSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.S) && playerNumber == 1)
        {
            transform.position += new Vector3(0, -pedalSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D) && playerNumber == 1)
        {
            transform.position += new Vector3(pedalSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A) && playerNumber == 1)
        {
            transform.position += new Vector3(-pedalSpeed * Time.deltaTime, 0, 0);
        }

        // Check for input to switch shooting modes
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    currentMode = ShootingMode.Mode1;
        //    Debug.Log("Switched to shooting mode 1");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    currentMode = ShootingMode.Mode2;
        //    Debug.Log("Switched to shooting mode 2");
        //}

        // Check for input to shoot
        //if (time1 >= coolDownTimer1)
        //{
        //    if (Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        if (currentMode == ShootingMode.Mode1)
        //        {
        //            ShootSingle();
        //        }
        //        else if (currentMode == ShootingMode.Mode2 && !isShootingSpread)
        //        {
        //            StartCoroutine(ShootSpread());
        //        }
        //        time1 = 1;
        //    }
        //}
    }

    //void ShootSingle()
    //{
    //    // Instantiate a single projectile
    //    InstantiateProjectile(fireball1, spawnLocationProjectile.position, transform.rotation);
    //}

    //IEnumerator ShootSpread()
    //{
    //    isShootingSpread = true;

    //    // Instantiate 5 projectiles with slight angle differences for a spread shot
    //    float spreadAngle = 15f; // Total spread angle
    //    int projectileCount = 5; // Number of projectiles
    //    float angleStep = spreadAngle / (projectileCount - 1); // Angle difference between each projectile
    //    float startAngle = -spreadAngle / 2; // Starting angle for the spread

    //    for (int i = 0; i < projectileCount; i++)
    //    {
    //        float angle = startAngle + (i * angleStep);
    //        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward) * transform.rotation;
    //        InstantiateProjectile(fireball2, spawnLocationProjectile.position, rotation);

    //        // Wait for the spreadShotInterval before spawning the next projectile
    //        yield return new WaitForSeconds(spreadShotInterval);
    //    }

    //    isShootingSpread = false;
    //}

    //void InstantiateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    //{
    //    Debug.Log("Spawning projectile at position: " + position); // Debug log to check the spawn position
    //    GameObject spawnedProjectile = Instantiate(projectilePrefab, position, rotation);
    //    BallMovement ballMovement = spawnedProjectile.GetComponent<BallMovement>();
    //    if (ballMovement != null)
    //    {
    //        ballMovement.playerTransform = transform;
    //    }
    //}
}