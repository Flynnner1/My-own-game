using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerXp : MonoBehaviour
{
    public Xppickup xppickup;
    public TMP_Text XpText;
    public int currentXp = 0;

    void Start()
    {
        updateXptext();
        // Try to find an Xppickup in the scene if not assigned
        if (!xppickup)
        {
            xppickup = FindObjectOfType<Xppickup>();
        }
    }

    public void addXp(int addXp)
    {
        currentXp += addXp;
        updateXptext();
    }

    public void updateXptext()
    {
        if (XpText)
        {
            XpText.text = currentXp.ToString();
        }
        else
        {
            Debug.LogWarning("XpText not assigned on PlayerXp.");
        }
    }
}