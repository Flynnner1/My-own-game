using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject gameOverScreen; // The UI element for game over
    public GameObject inventoryScreen; // The UI element for inventory
    //public Inventory inventory;

    public Sprite coinIcon; // Reference to the coin icon sprite
    public Sprite wandIcon; // Reference to the wand icon sprite

    public bool inventorystate = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        inventoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the inventory screen
            inventoryScreen.SetActive(!inventoryScreen.activeSelf);
            if (inventoryScreen.activeSelf)
            {
                inventorystate = true;
            }
            else
            {
                inventorystate = false;
            }
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
}