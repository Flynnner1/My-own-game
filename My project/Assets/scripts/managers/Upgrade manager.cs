using UnityEngine;

public class Upgrademanager : MonoBehaviour
{
    // Make variables public so you can assign them in the Inspector
    public XpManager xpManager;
    public Healthmanager healthmanager;
    public ProjectileDamage projectileDamage; // Make sure this script exists somewhere
    public MovementAndShooting movementAndShooting;

    // You can remove the Awake method now if you only used it for GetComponent
    void Awake()
    {
        xpManager = FindObjectOfType<XpManager>();
        healthmanager = FindObjectOfType<Healthmanager>();
        projectileDamage = FindObjectOfType<ProjectileDamage>();
        movementAndShooting = FindObjectOfType<MovementAndShooting>();
        // ... etc for others ...
    }
    // Add null checks before using the variables in your methods,
    // just in case you forget to assign them in the Inspector.
    public void Hp()
    {
        if (xpManager == null || healthmanager == null)
        {
            Debug.LogError("Hp Upgrade failed: xpManager or healthmanager not assigned in the Inspector!");
            return; // Exit the method early
        }

        if (xpManager.level >= 1)
        {
            healthmanager.SecrethealthAmount += 5f;
            healthmanager.healthAmount += 5f;
            xpManager.level--;
            xpManager.updateleveltext();
        }
    }

    public void Damage()
    {
        if (xpManager == null || projectileDamage == null)
        {
            Debug.LogError("Damage Upgrade failed: xpManager or projectileDamage not assigned in the Inspector!");
            return; // Exit the method early
        }

        if (xpManager.level >= 1)
        {
            projectileDamage.damageAmount += 2;
            xpManager.level--;
            xpManager.updateleveltext();
        }
    }

    public void Movement()
    {
        if (xpManager == null || movementAndShooting == null)
        {
            Debug.LogError("Movement Upgrade failed: xpManager or movementAndShooting not assigned in the Inspector!");
            return; // Exit the method early
        }

        if (xpManager.level >= 5)
        {
            movementAndShooting.pedalSpeed += 1;
            xpManager.level -= 5;
            xpManager.updateleveltext();
        }
    }
}