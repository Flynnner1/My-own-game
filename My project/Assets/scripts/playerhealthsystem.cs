using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class playerhealthsystem : MonoBehaviour
{
    public int score = 4;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    public void deducthealth()
    {
        score--;
        scoreText.text = score.ToString();
    }
}
