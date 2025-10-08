using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonshoting : MonoBehaviour
{
    public GameObject fireball;
    public GameObject firewall;
    public GameObject CrimsonBeam;

    public float timer;
    public float cooldowntimer = 7;

    public int randomInt = 0;

    public int range = 10;

    public GameObject spawnPlace;

    public Transform player;

    public void Update()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        if (player != null && Vector3.Distance(transform.position, player.position) <= range)
        {
            timer += Time.deltaTime;
            if (timer >= cooldowntimer)
            {
                int randomInt = Random.Range(1, 101);
                if (randomInt <= 60)
                {
                    Instantiate(fireball, spawnPlace.transform.position, spawnPlace.transform.rotation);
                }
                // 30% chance for Firewall (if number is 61-90)
                else if (randomInt <= 90)
                {
                    Instantiate(firewall, spawnPlace.transform.position, spawnPlace.transform.rotation);
                }
                // 10% chance for Crimson Beam (if number is 91-100)
                else
                {
                    Instantiate(CrimsonBeam, spawnPlace.transform.position, spawnPlace.transform.rotation);
                }
                timer = 0f;
            }
        }
    }
}
