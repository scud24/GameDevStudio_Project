using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public int weaponID;
    public float MAX_RESPAWN_WAIT;
    public float currentRespawnWait;
    public bool isActive = true;
    public GameObject pickupObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentRespawnWait <= 0 && !isActive)
        {
            isActive = true;
            pickupObject.SetActive(true);
        }
        else
        {
            currentRespawnWait = currentRespawnWait - Time.deltaTime;
        }
    }

    public void HandlePickUp()
    {

        isActive = false;
        pickupObject.SetActive(false);
        currentRespawnWait = MAX_RESPAWN_WAIT;
    }
}
