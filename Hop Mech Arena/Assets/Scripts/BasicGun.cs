/**
    ITCS 4232-001 Group Project
    BasicGun.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public float refireWaitTime;
    public float currentRefireWait;
    public GameObject projectile;
    public float projectileSpeed;
    public Vector3 projectileSpawnLoc;
    public Vector3 projectileFireDir;
    public GameObject playerCamera;
    public GameObject cameraTarget;
    public int playerNum;
    public bool readyToFire;
    public float aimOffset;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<PlayerCharacter>())
        {
            playerNum = GetComponentInParent<PlayerCharacter>().playerNum;
        }
        else if (GetComponentInParent<IndependentAimPlayer>())
        {
            playerNum = GetComponentInParent<IndependentAimPlayer>().playerNum;
        }
        else
        {
            playerNum = -1;
        }
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        projectileSpawnLoc = transform.position;
        projectileFireDir = Vector3.Normalize(cameraTarget.transform.position - projectileSpawnLoc);
        Quaternion initialRotation = Quaternion.LookRotation(projectileFireDir);
        transform.rotation = initialRotation;

        if (currentRefireWait > 0)
        {
            currentRefireWait--;
        }
        else if(!readyToFire)
        {
            readyToFire = true;
        }
    }

    public virtual void Fire()
    {
        if (readyToFire)
        {
            projectileSpawnLoc = transform.position;
            projectileFireDir = Vector3.Normalize(cameraTarget.transform.position - projectileSpawnLoc + new Vector3(0, aimOffset, 0));
            Quaternion initialRotation = Quaternion.LookRotation(projectileFireDir);
            GameObject newProjectile = Instantiate(projectile, projectileSpawnLoc, initialRotation);
            newProjectile.GetComponent<BasicProjectile>().projectileDir = projectileFireDir;
            newProjectile.GetComponent<BasicProjectile>().projectileSpeed = projectileSpeed;
            newProjectile.GetComponent<BasicProjectile>().parentPlayerNum = playerNum;
            Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), GetComponent<Collider>());
            if (GetComponentInParent<PlayerCharacter>())
            {
                Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), GetComponentInParent<PlayerCharacter>().coll);
            }
            else
            {
                Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), GetComponentInParent<IndependentAimPlayer>().coll);
            }
            readyToFire = false;
            currentRefireWait = refireWaitTime;
        }
    }
}
