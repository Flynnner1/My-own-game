using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Item currentWeapon; // Start with the fireball
    public int pebbleCount = 5;
    public float shotInterval = 0.5f; // Interval between each projectile in the spread shot
    public float switchCooldown = 1f; // Cooldown time for switching weapons

    public GameObject fireball;
    public GameObject pebble;
    public GameObject beam;
    public GameObject slash;
    public GameObject poison;

    public Transform spawnLocationProjectile;

    public float cooldownTimerfireball = 4f; // Cooldown time between shots
    public float cooldownTimerpebble = 5f; // Cooldown time between shots
    public float cooldownTimerBeam = 15f; // Cooldown time between shots
    public float cooldownTimerslash = 2.5f; // Cooldown time between shots

    public Item emptyItem;

    public float time1 = 0f;
    public float time2 = 0f;
    public float time3 = 0f;
    public float time4 = 0f;

    public gameManager gameManager;
    public NPCController NPCcontroller;

    public InventoryManager inventoryManager;

    Item recievedItem;
    // Start is called before the first frame update
    void Start()
    {
        time1 = cooldownTimerfireball;
        time2 = cooldownTimerpebble;
        if (inventoryManager == null)
        {
            inventoryManager = GetComponent<InventoryManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        recievedItem = inventoryManager.GetSelectedItem(false);
        if (recievedItem == null)
        {
            recievedItem = emptyItem;
        }

        currentWeapon = recievedItem;

        //LastItem = currentWeapon;

        //currentWeapon = newweapon;

        //string input;
        //input = "" + recievedItem;

        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        time3 += Time.deltaTime;
        time4 += Time.deltaTime;

        

        // Check for input to shoot
        if (gameManager.inventorystate == false)//|| NPCcontroller.npcUIstate == false
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (currentWeapon.name == "fireball")
                {
                    if (time1 >= cooldownTimerfireball)
                    {
                        ShootFireball();
                        time1 = 0f;
                    }
                }
                else if (currentWeapon.name == "pebble")
                {
                    if (time2 >= cooldownTimerpebble)
                    {
                        StartCoroutine(ShootPebble());
                        time2 = 0f;
                    }
                }
                else if (currentWeapon.name == "beam")
                {
                    if (time3 >= cooldownTimerBeam)
                    {
                        ShootBeam();
                        time3 = 0f;
                    }
                }
                else if (currentWeapon.name == "slash")
                {
                    if (time4 >= cooldownTimerslash)
                    {
                        ShootSlash();
                        time4 = 0f;
                    }
                }
                else if (currentWeapon.name == "poison")
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
    void ShootPoison()
    {
        Instantiate(poison, spawnLocationProjectile.position, transform.rotation);

    }
}