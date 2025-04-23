using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject inventoryScreen;
    public GameObject deathscreen;

    public PlayerCoins playerCoins;
    public Healthmanager healthmanager;

    public bool inventorystate = false;

    public GameObject player;

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
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventorystate = !inventorystate;
            Debug.Log($"E pressed. Inventory state toggled to: {inventorystate} at Time: {Time.time}");
            inventoryScreen.SetActive(inventorystate);
            Debug.Log($"Inventory Screen Active set to: {inventoryScreen.activeSelf} at Time: {Time.time}");

            // Uncomment this if you want to pause the game
            // Time.timeScale = inventorystate ? 0 : 1;
        }
        // Add another log outside the if to see if something else changes it
        // Debug.Log($"End of Update - Inventory Screen Active: {inventoryScreen.activeSelf}");
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

    public void respawn()
    {
        if (player != null)
        {
            // Teleport the player to the respawn position
            player.transform.position = new Vector3(45, -45, 0);

            // Deactivate the death screen and resume the game
            deathscreen.SetActive(false);
            Time.timeScale = 1;
            healthmanager.healthAmount = healthmanager.SecrethealthAmount;
            healthmanager.TakeDamage(0);
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned.");
        }
    }

    public void toggleDeathScreen()
    {
        // Toggle the deathscreen visibility and adjust the game time accordingly
        bool isDeathscreenActive = deathscreen.activeSelf;

        deathscreen.SetActive(!isDeathscreenActive);
        Time.timeScale = isDeathscreenActive ? 1 : 0;
    }
}