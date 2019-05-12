using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public ScoreManager scoreManager;
    public Text livesText;
    public int MAX_INVINCIBILITY_FRAMES = 2;
    public int currentInvincibilityFrames;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<GlobalSpawnManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        if(DEBUG_FLAG_UIAttached)
        {
            healthBarUI.maxValue = MAX_HEALTH;
            shieldBarUI.maxValue = MAX_SHIELDS;
            livesText.text = "Lives: " + currentLives;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentInvincibilityFrames > 0)
        {
            currentInvincibilityFrames--;
        }
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
            if (currentRespawnTime <= 0 && currentLives > 0)
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
        if(currentInvincibilityFrames <= 0)
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
            currentInvincibilityFrames = MAX_INVINCIBILITY_FRAMES;
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
        scoreManager.AddPlayerDeath(playerNum);
        if (currentLives > 0)
        {
            currentRespawnTime = MAX_RESPAWN_TIME;
        }
        else
        {
            scoreManager.MarkPlayerOut(playerNum);
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
