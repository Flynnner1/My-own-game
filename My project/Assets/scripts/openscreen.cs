using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openscreen : MonoBehaviour
{
    public GameObject controls;

    private void Start()
    {
        controls.SetActive(false);
    }
    public void openControls()
    {
        controls.SetActive(true);
    }
    public void closeControls()
    {
        controls.SetActive(false);
    }
}
