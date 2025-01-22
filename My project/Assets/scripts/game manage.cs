using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject gameOverScreen; // The UI element for game over
    public TMP_Text hpscoreText;      // Text displaying the player's HP or score
    

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameEnd()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;

    }

    public void LoadScene(int sceneId)
    {
        Debug.Log("Loading scene: " + sceneId);
        SceneManager.LoadScene(sceneId);
    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        hpscoreText.text = "4";
        
        Time.timeScale = 1;
    }
}
