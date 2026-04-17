using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xppickup : MonoBehaviour
{
    public bool pickedUp = false;
    private XpManager xpManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            xpManager = other.GetComponent<XpManager>();
            if (xpManager != null)
            {
                xpManager.PlusXp(1);
                pickedUp = true;

                Debug.Log("You've picked up Xp");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("XpManager component not found on Player!");
            }
        }
        //else
        //{
        //    Debug.Log("You didn't pick up Xp");
        //}
    }
}