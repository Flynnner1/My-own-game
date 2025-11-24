using UnityEngine;
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

    public void EquipArmor(int armorNum)
    {
        switch (armorNum)
        {
            case 0: 
                SetActiveHelmet(letherHelmet);
                break;
            case 2:
                SetActiveHelmet(ironHelmet);
                break;
            case 4: 
                SetActiveHelmet(diamondHelmet);
                break;
            case 6: 
                SetActiveHelmet(superHelmet);
                break;
            case 1: 
                SetActiveShoulder(letherShoulderplate);
                break;
            case 3:
                SetActiveShoulder(ironShoulderplate);
                break;
            case 5:
                SetActiveShoulder(diamondShoulderplate);
                break;
            case 7:
                SetActiveShoulder(superShoulderplate);
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
            case 0:
                SafeSetActive(letherHelmet, false);
                break;
            case 2:
                SafeSetActive(ironHelmet, false);
                break;
            case 4:
                SafeSetActive(diamondHelmet, false);
                break;
            case 6:
                SafeSetActive(superHelmet, false);
                break;
            case 1:
                SafeSetActive(letherShoulderplate, false);
                break;
            case 3:
                SafeSetActive(ironShoulderplate, false);
                break;
            case 5:
                SafeSetActive(diamondShoulderplate, false);
                break;
            case 7:
                SafeSetActive(superShoulderplate, false);
                break;
            default:
                Debug.LogWarning("unknown armorNum");
                break;
        }
    }
    void SetActiveHelmet(GameObject toEnable)
    {
        SafeSetActive(letherHelmet, letherHelmet == toEnable);
        SafeSetActive(ironHelmet, ironHelmet == toEnable);
        SafeSetActive(diamondHelmet, diamondHelmet == toEnable);
        SafeSetActive(superHelmet, superHelmet == toEnable);
    }
    void SetActiveShoulder(GameObject toEnable)
    {
        SafeSetActive(letherShoulderplate, letherShoulderplate == toEnable);
        SafeSetActive(ironShoulderplate, ironShoulderplate == toEnable);
        SafeSetActive(diamondShoulderplate, diamondShoulderplate == toEnable);
        SafeSetActive(superShoulderplate, superShoulderplate == toEnable);
    }

    void SafeSetActive(GameObject go, bool active)
    {
        go.SetActive(active);
    }
}