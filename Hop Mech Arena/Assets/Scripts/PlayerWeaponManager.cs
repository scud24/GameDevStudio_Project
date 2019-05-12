using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public int currentWeapon;
    public List<GameObject> playerWeapons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentWeapon = Mathf.Clamp(currentWeapon, 0, playerWeapons.Count);
        for(int i = 0; i < playerWeapons.Count; i++)
        {
            playerWeapons[i].SetActive(i == currentWeapon);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.tag == "Pickup")
        {
            PickupWeapon pw = collider.GetComponentInParent<PickupWeapon>();
            currentWeapon = pw.weaponID;
            pw.HandlePickUp();
        }
    }
    public void FireCurrentWeapon()
    {
        Debug.Log("Attempting fire of weapon: " + currentWeapon);
        playerWeapons[currentWeapon].GetComponent<BasicGun>().Fire();
    }
}
