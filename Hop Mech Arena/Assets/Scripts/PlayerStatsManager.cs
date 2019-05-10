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
    public float currentRespawnTime;
    public float MAX_RESPAWN_TIME;
    public GameObject playerCharacter;
    public GameObject spectateRig;
    public GameObject rigCamera;
    public bool isDead;
    public int playerNum;
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
                currentShields += chargeRate * Time.deltaTime;
                if(currentShields > MAX_SHIELDS)
                {
                    currentShields = MAX_SHIELDS;
                }
            }
        }
        else
        {
            currentShieldChargeDelay -= Time.deltaTime;
        }


        if (isDead)
        {
            if (currentRespawnTime <= 0)
            {
                HandleRespawn();
            }
            else
            {
                currentRespawnTime -= Time.deltaTime;
            }
        }
    }

    public void ApplyDamage(float damageAmount)
    {
        Debug.Log("Player " + playerNum + " recieves " + damageAmount + " damage");
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
        else
        {
            currentShieldChargeDelay = MAX_CHARGE_DELAY;
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

        playerCharacter.SetActive(false);
        spectateRig.SetActive(true);
        rigCamera.GetComponent<CameraController>().isSpectateMode = true;
        isDead = true;
        if (currentLives > 0)
        {
            currentRespawnTime = MAX_RESPAWN_TIME;
        }
        else
        {
        }
    }

    public void HandleRespawn()
    {
        isDead = false;

        playerCharacter.SetActive(true);
        spectateRig.SetActive(false);
        rigCamera.GetComponent<CameraController>().isSpectateMode = false;
    }
}
