using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void goToGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void goToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
