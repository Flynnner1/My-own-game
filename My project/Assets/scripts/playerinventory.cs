using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinventory : MonoBehaviour
{
    public float coins = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void coinpickup()
    {
        coins += 0.5f;
        Debug.Log("+1 coin");
    }
}
