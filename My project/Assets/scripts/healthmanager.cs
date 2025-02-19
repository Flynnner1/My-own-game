using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthmanager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    PlayerHealth PlayerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
        if (healthAmount <= 0f)
        {
            PlayerHealth.Die();
        }
    }
    public void heal(float healingAmount)
    {
        healthAmount += healthAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;

    }
}
