using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilecollision : MonoBehaviour
{
    public int layerMask;
    public playerhealthsystem playerhealth;
    public float time = 1f;
    public float cooldowntimer = 3f;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D col)
        {
        time += Time.deltaTime;
            if (time >= cooldowntimer)
            {
                Debug.Log("Hit");
                if (col.gameObject.layer == layerMask)
                {
                    if (layerMask == 2)
                    {
                        playerhealth.deducthealth();
                        Debug.Log("Hit Player");
                    }

                }
            }
        }

    // Update is called once per frame
    
}
