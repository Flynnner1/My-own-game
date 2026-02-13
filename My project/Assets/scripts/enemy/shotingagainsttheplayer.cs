using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAgainstThePlayer : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab; 
    public float range = 10f; 
    public float shootingInterval = 3f; 
    public Transform projectileContainer;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        StartCoroutine(ShootAtPlayer());
    }
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        }
    }
    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            if (player != null && Vector3.Distance(transform.position, player.position) <= range)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileContainer.position, projectileContainer.rotation);
            }
            yield return new WaitForSeconds(shootingInterval);
        }
    }
}