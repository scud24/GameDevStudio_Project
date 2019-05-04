/**
    ITCS 4232-001 Group Project
    Double Gun.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGun : BasicGun
{

    public float burstWaitTime;
    public float currentBurstWait;
    public int burstShotsRemaining;
    public int MAX_BURST_SHOTS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        if(currentRefireWait>0)
        {
            currentRefireWait--;
        }
        else if(burstShotsRemaining > 0)
        {
            burstShotsRemaining--;
            BurstRefire();
        }
    }

    public override void Fire()
    {
        base.Fire();
        currentBurstWait = burstWaitTime;
        burstShotsRemaining = MAX_BURST_SHOTS;
    }
    public void BurstRefire()
    {
        projectileSpawnLoc = transform.position;
        projectileFireDir = Vector3.Normalize(cameraTarget.transform.position - projectileSpawnLoc);
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
    }
}
