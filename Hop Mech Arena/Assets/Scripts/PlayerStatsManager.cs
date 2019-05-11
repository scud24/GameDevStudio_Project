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
    public HealthBar healthBarUI;
    public HealthBar shieldBarUI;
    public bool DEBUG_FLAG_UIAttached = true;
    public GlobalSpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<GlobalSpawnManager>();
        if(DEBUG_FLAG_UIAttached)
        {
            healthBarUI.maxValue = MAX_HEALTH;
            shieldBarUI.maxValue = MAX_SHIELDS;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentHealth <= 0 && !isDead)
        {
            HandleDeath();
        }
        if (currentShieldChargeDelay <= 0)
        {
            if(currentShields < MAX_SHIELDS && !isDead)
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
        if(DEBUG_FLAG_UIAttached)
        {
            healthBarUI.currentValue = currentHealth;
            shieldBarUI.currentValue = currentShields;
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
        playerCharacter.transform.rotation = Quaternion.identity;
        playerCharacter.transform.position = spawnManager.GetAvailibleSpawnpoint(playerNum);
        spectateRig.SetActive(false);
        rigCamera.GetComponent<CameraController>().isSpectateMode = false;
        
        currentHealth = MAX_HEALTH;
        currentShields = MAX_SHIELDS;
    }
}
