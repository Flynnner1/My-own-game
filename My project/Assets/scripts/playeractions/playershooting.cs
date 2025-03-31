using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public GameObject fireballPrefab;
    public GameObject pebblePrefab;
    public GameObject beamPrefab;
    public GameObject slashPrefab;
    public Transform spawnLocationProjectile;

    public float fireballCooldown = 4f;
    public float pebbleCooldown = 5f;
    public float beamCooldown = 15f;
    public float slashCooldown = 2.5f;

    private float fireballTimer;
    private float pebbleTimer;
    private float beamTimer;
    private float slashTimer;

    public int pebbleCount = 5;
    public float shotInterval = 0.5f;

    private void Start()
    {
        fireballTimer = fireballCooldown;
        pebbleTimer = pebbleCooldown;
        beamTimer = beamCooldown;
        slashTimer = slashCooldown;
    }

    private void Update()
    {
        fireballTimer += Time.deltaTime;
        pebbleTimer += Time.deltaTime;
        beamTimer += Time.deltaTime;
        slashTimer += Time.deltaTime;

        // On left-click, check currently selected item to decide which projectile to fire:
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Item selectedItem = inventoryManager.GetSelectedItem(false);
            if (selectedItem != null)
            {
                string itemName = selectedItem.name.ToLower();
                if (itemName.Contains("fireball"))
                {
                    if (fireballTimer >= fireballCooldown)
                    {
                        Instantiate(fireballPrefab, spawnLocationProjectile.position, transform.rotation);
                        fireballTimer = 0f;
                    }
                }
                else if (itemName.Contains("pebble"))
                {
                    if (pebbleTimer >= pebbleCooldown)
                    {
                        StartCoroutine(ShootPebbleSpread());
                        pebbleTimer = 0f;
                    }
                }
                else if (itemName.Contains("beam"))
                {
                    if (beamTimer >= beamCooldown)
                    {
                        Instantiate(beamPrefab, spawnLocationProjectile.position, transform.rotation);
                        beamTimer = 0f;
                    }
                }
                else if (itemName.Contains("slash"))
                {
                    if (slashTimer >= slashCooldown)
                    {
                        Instantiate(slashPrefab, spawnLocationProjectile.position, transform.rotation);
                        slashTimer = 0f;
                    }
                }
            }
        }
    }

    private IEnumerator ShootPebbleSpread()
    {
        for (int i = 0; i < pebbleCount; i++)
        {
            Instantiate(pebblePrefab, spawnLocationProjectile.position, transform.rotation);
            yield return new WaitForSeconds(shotInterval);
        }
    }
}