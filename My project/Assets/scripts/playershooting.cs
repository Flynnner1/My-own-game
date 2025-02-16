using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershooting : MonoBehaviour
{
    private int currentWeapon = 1; // Start with the fireball
    public int pebbleCount = 5;
    public float ShotInterval = 0.5f; // Interval between each projectile in the spread shot
    public float angle = 45f;

    public GameObject fireball;
    public GameObject pebble;

    public Transform spawnLocationProjectile;

    MovementAndShooting MovementAndShooting;
    BallMovement BallMovement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            SwitchWeapon(2);
        }

    }
    //void shootFireBall(GameObject fireball, Vector3 position, Quaternion rotation)
    //{
    //    Debug.Log("Spawning projectile at position: " + position); // Debug log to check the spawn position
    //    GameObject spawnedProjectile = Instantiate(fireball, position, rotation);
    //    BallMovement ballMovement = spawnedProjectile.GetComponent<BallMovement>();
    //    if (ballMovement != null)
    //    {
    //        ballMovement.playerTransform = transform;
    //    }
    //}

    void Shootfireball()
    {
        // Instantiate a single projectile
        Instantiate(fireball, spawnLocationProjectile.position, transform.rotation);
    }
    public IEnumerator ShootPebble()
    {
        for (int i = 0; i < pebbleCount; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward) * transform.rotation;
            Instantiate(pebble, spawnLocationProjectile.position, rotation);

            // Wait for the spreadShotInterval before spawning the next projectile
            yield return new WaitForSeconds(ShotInterval);
        }
    }
    void SwitchWeapon(int weaponNumber)
    {
        currentWeapon = weaponNumber;

        switch (currentWeapon)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shootfireball();
                }
                Debug.Log("Switched to fireball");
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    ShootPebble();
                }
                Debug.Log("Switched to pebble");
                break;

            default:
                Debug.Log("Invalid weapon selection");
                break;
        }
    }
}
