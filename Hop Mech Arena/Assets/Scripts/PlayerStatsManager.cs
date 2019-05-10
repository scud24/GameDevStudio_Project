using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public float currentHealth;
    public float MAX_HEALTH = 100;
    public float currentShields;
    public float MAX_SHIELDS;
    public float currentShieldChargeDelay;
    public float MAX_CHARGE_DELAY;
    public float chargeRate;
    public int currentLives;
    public int currentRespawnTime;
    public int MAX_RESPAWN_TIME;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentShieldChargeDelay <= 0)
        {
            if(currentShields < MAX_SHIELDS)
            {
                currentShields += chargeRate;
                if(currentShields > MAX_SHIELDS)
                {
                    currentShields = MAX_SHIELDS;
                }
            }
        }
        else
        {
            currentShieldChargeDelay--;
        }
    }

    public void ApplyDamage(float damageAmount)
    {
        currentShields -= damageAmount;
        if (currentShields < 0)
        {
            currentHealth += currentShields;
            currentShields = 0;
            if(currentHealth <= 0)
            {
                HandleDeath();
            }
        }
    }

    public void ApplyHealing(float healingAmount)
    {
        currentHealth += healingAmount;
        if(currentHealth > MAX_HEALTH)
        {
            currentHealth = MAX_HEALTH;
        }
    }

    public void ApplyShieldCharge(float shieldAmount)
    {
        currentShields += shieldAmount;
    }

    public void HandleDeath()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            //Switch to spectate mode
        }
        else
        {
            currentRespawnTime = MAX_RESPAWN_TIME;
            //Disable player control and hide model
        }
    }
}
