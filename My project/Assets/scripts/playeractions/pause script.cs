using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausescript : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // when you press esc the pause menu will be active
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                
                Time.timeScale = 1;
                
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
