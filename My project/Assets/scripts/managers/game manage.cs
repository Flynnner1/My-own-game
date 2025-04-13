using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject inventoryScreen;

    // Just make these public or properly assign them in code:
    public PlayerCoins playerCoins;
    public Healthmanager healthmanager;

    public bool inventorystate = false;

    void Start()
    {
        gameOverScreen.SetActive(false);
        inventoryScreen.SetActive(false);

        // If not already assigned in Inspector, find them at runtime:
        if (!playerCoins)
        {
            playerCoins = FindObjectOfType<PlayerCoins>();
        }
        if (!healthmanager)
        {
            healthmanager = FindObjectOfType<Healthmanager>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryScreen.SetActive(!inventoryScreen.activeSelf);
            inventorystate = inventoryScreen.activeSelf;
        }
    }

    public void GameEnd()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void BuyHeal()
    {
        // Check if references are valid before calling
        if (playerCoins && healthmanager)
        {
            if (playerCoins.coins >= 10)
            {
                healthmanager.Heal(10);
            }
        }
        else
        {
            Debug.LogError("References to PlayerCoins or Healthmanager are not set.");
        }
    }
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