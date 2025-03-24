using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndShooting : MonoBehaviour
{
    public int playerNumber = 1;
    private int pedalSpeed = 7;
    public Rigidbody2D rb;
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}