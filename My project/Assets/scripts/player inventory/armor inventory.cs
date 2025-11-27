using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class armorinventory : MonoBehaviour
{
    [Header("Helmets")]
    public GameObject letherHelmet;
    public GameObject ironHelmet;
    public GameObject diamondHelmet;
    public GameObject superHelmet;

    [Header("Shoulderplates")]
    public GameObject letherShoulderplate;
    public GameObject ironShoulderplate;
    public GameObject diamondShoulderplate;
    public GameObject superShoulderplate;

    private const int helmetSlot = 10;
    private const int shoulderplateSlot = 11;

    public Healthmanager healthmanager;
    public static armorinventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void EquipArmor(int armorNum)
    {
        switch (armorNum)
        {
            // helmets
            case 0:
                //letherHelmet.SetActive(true);
                break;
            case 2:
                letherHelmet.SetActive(true);
                healthmanager.healthAmount += 20;
                healthmanager.SecrethealthAmount += 20;
                break;
            case 4:
                ironHelmet.SetActive(true);
                healthmanager.healthAmount += 30;
                healthmanager.SecrethealthAmount += 30;
                break;
            case 6:
                diamondHelmet.SetActive(true);
                healthmanager.healthAmount += 40;
                healthmanager.SecrethealthAmount += 40;
                break;
            case 8:
                superHelmet.SetActive(true);
                healthmanager.healthAmount += 50;
                healthmanager.SecrethealthAmount += 50;
                break;

            // shoulderplates 
            case 1:
                letherShoulderplate.SetActive(true);
                healthmanager.healthAmount += 25;
                healthmanager.SecrethealthAmount += 25;
                break;
            case 3:
                ironShoulderplate.SetActive(true);
                healthmanager.healthAmount += 35;
                healthmanager.SecrethealthAmount += 35;
                break;
            case 5:
                diamondShoulderplate.SetActive(true);
                healthmanager.healthAmount += 45;
                healthmanager.SecrethealthAmount += 45;
                break;
            case 7:
                superShoulderplate.SetActive(true);
                healthmanager.healthAmount += 55;
                healthmanager.SecrethealthAmount += 55;
                break;
            default:
                Debug.LogWarning("unknown armorNum");
                break;
        }
    }

    public void UnequipArmor(int armorNum)
    {
        switch (armorNum)
        {

            // helmets
            case 0:
                //letherHelmet.SetActive(false);
                break;
            case 2:
                letherHelmet.SetActive(false);
                healthmanager.healthAmount -= 20;
                healthmanager.SecrethealthAmount -= 20;
                break;
            case 4:
                ironHelmet.SetActive(false);
                healthmanager.healthAmount -= 30;
                healthmanager.SecrethealthAmount -= 30;
                break;
            case 6:
                diamondHelmet.SetActive(false);
                healthmanager.healthAmount -= 40;
                healthmanager.SecrethealthAmount -= 40;
                break;
            case 8:
                superHelmet.SetActive(false);
                healthmanager.healthAmount -= 50;
                healthmanager.SecrethealthAmount -= 50;
                break;

            // shoulderplates
            case 1:
                letherShoulderplate.SetActive(false);
                healthmanager.healthAmount -= 25;
                healthmanager.SecrethealthAmount -= 25;
                break;
            case 3:
                ironShoulderplate.SetActive(false);
                healthmanager.healthAmount -= 35;
                healthmanager.SecrethealthAmount -= 35;
                break;
            case 5:
                diamondShoulderplate.SetActive(false);
                healthmanager.healthAmount -= 45;
                healthmanager.SecrethealthAmount -= 45;
                break;
            case 7:
                superShoulderplate.SetActive(false);
                healthmanager.healthAmount -= 55;
                healthmanager.SecrethealthAmount -= 55;
                break;
            default:
                Debug.LogWarning("unknown armorNum");
                break;
        }
    }
}