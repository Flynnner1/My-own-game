using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private int currentWeapon = 1; // Start with the fireball
    public int pebbleCount = 5;
    public float shotInterval = 0.5f; // Interval between each projectile in the spread shot
    public float switchCooldown = 1f; // Cooldown time for switching weapons

    public GameObject fireball;
    public GameObject pebble;
    public GameObject beam;
    public GameObject slash;


    public Transform spawnLocationProjectile;

    public float cooldownTimerfireball = 4f; // Cooldown time between shots
    public float cooldownTimerpebble = 5f; // Cooldown time between shots
    public float cooldownTimerBeam = 15f; // Cooldown time between shots
    public float cooldownTimerslash = 2.5f; // Cooldown time between shots


    public float time1 = 0f;
    public float time2 = 0f;
    public float time3 = 0f;
    public float time4 = 0f;

    public gameManager gameManager;
    public NPCController NPCcontroller;

    // Start is called before the first frame update
    void Start()
    {
        time1 = cooldownTimerfireball;
        time2 = cooldownTimerpebble;

    }

    // Update is called once per frame
    void Update()
    {
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        time3 += Time.deltaTime;
        time4 += Time.deltaTime;


        // Check for input to switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
             SwitchWeapon(2);            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(4);
        }

        // Check for input to shoot
        if (gameManager.inventorystate == false )//|| NPCcontroller.npcUIstate == false
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (currentWeapon == 1)
                {
                    if (time1 >= cooldownTimerfireball)
                    {
                        ShootFireball();
                        time1 = 0f;
                    }
                }
                else if (currentWeapon == 2)
                {
                    if (time2 >= cooldownTimerpebble)
                    {
                        StartCoroutine(ShootPebble());
                        time2 = 0f;
                    }
                }
                else if (currentWeapon == 3)
                {
                    if (time3 >= cooldownTimerBeam)
                    {
                        ShootBeam();
                        time3 = 0f;
                    }
                }
                else if (currentWeapon == 4)
                {
                    if (time4 >= cooldownTimerslash)
                    {
                        ShootSlash();
                        time4 = 0f;
                    }
                }
            }
        }
    }

    void ShootFireball()
    {
        // Instantiate a single projectile
        Instantiate(fireball, spawnLocationProjectile.position, transform.rotation);
    }

    public IEnumerator ShootPebble()
    {
        for (int i = 0; i < pebbleCount; i++)
        {
            Instantiate(pebble, spawnLocationProjectile.position, transform.rotation);

            // Wait for the shotInterval before spawning the next projectile
            yield return new WaitForSeconds(shotInterval);
        }
    }

    void ShootBeam()
    {
        Instantiate(beam, spawnLocationProjectile.position, transform.rotation);
    }
    void ShootSlash()
    {
        Instantiate(slash, spawnLocationProjectile.position, transform.rotation);

    }
    void SwitchWeapon(int weaponNumber)
    {
        currentWeapon = weaponNumber;

        switch (currentWeapon)
        {
            case 1:
                Debug.Log("Switched to fireball");
                break;
            case 2:
                Debug.Log("Switched to pebble");
                break;
            case 3:
                Debug.Log("Switched to Beam");
                break;
            case 4:
                Debug.Log("Switched to slash");
                break;

            default:
                Debug.Log("Invalid weapon selection");
                break;
        }
    }
}