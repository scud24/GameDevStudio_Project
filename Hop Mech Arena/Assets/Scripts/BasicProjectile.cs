/**
    ITCS 4232-001 Group Project
    BasicProjectile.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public Vector3 projectileDir;
    public float projectileSpeed;
    public float timeToLive;
    public float timeFromStart;
    public float projectileGravityScale=1;//Disable UseGravity if using this value
    public bool debugLoop;
    protected Rigidbody rgbd;
    public float directHitDamage;
    public float baseIndirectDamage;
    public float minIndirectDamage;
    public float indirectDamageFalloffStart;
    public float indirectDamageFalloffEnd;
    //public float indirectDamageRadius;
    public float explosionForce;


    public int MAX_BOUNCES;
    public int bouncesRemaining;
    public int MAX_BOUNCE_COOLDOWN;
    public int bounceCooldown;

    public GameObject hitEffectObject;
    public string projectileType;

    
    public int parentPlayerNum;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        bouncesRemaining = MAX_BOUNCES;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rgbd.AddForce(projectileGravityScale * Physics.gravity * rgbd.mass);
        Vector3 tempPos = transform.position;
        tempPos += projectileDir * projectileSpeed * Time.deltaTime;
        rgbd.MovePosition(tempPos);

        if (timeFromStart > timeToLive)
        {
            if (debugLoop)
            {
                timeFromStart = 0;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        timeFromStart++;

        if(bounceCooldown > 0)
        {
            bounceCooldown--;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("exit coll");
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.tag);
        if(collision.transform.tag == "Player" || (bouncesRemaining <=0 && bounceCooldown <= 0))
        {
            if (collision.transform.tag == "Player")
            {
                collision.transform.GetComponentInParent<PlayerStatsManager>().ApplyDamage(directHitDamage);
            }
            if (projectileType == "Explosive")
            {  
                //Create explosion effect. this object will deal the indirect damage
                GameObject explosionEffect = Instantiate(hitEffectObject, transform.position, Quaternion.identity);
                explosionEffect.GetComponent<BasicExplosion>().baseIndirectDamage = baseIndirectDamage;
                explosionEffect.GetComponent<BasicExplosion>().minIndirectDamage = minIndirectDamage;
                explosionEffect.GetComponent<BasicExplosion>().indirectDamageFalloffStart = indirectDamageFalloffStart;
                explosionEffect.GetComponent<BasicExplosion>().indirectDamageFalloffEnd = indirectDamageFalloffEnd;
                Debug.Log("p: " +indirectDamageFalloffEnd + ", " + indirectDamageFalloffStart);
                explosionEffect.GetComponent<BasicExplosion>().force = explosionForce;
                explosionEffect.GetComponent<BasicExplosion>().radius = indirectDamageFalloffEnd;
            }
            Destroy(gameObject);
        }
        else
        {
            if (bounceCooldown <= 0)
            {
                //Debug.Log("Bounce! Remaining: " + bouncesRemaining);
                bouncesRemaining--;
                bounceCooldown = MAX_BOUNCE_COOLDOWN;
            }
            else
            {
                //Debug.Log("ON cooldown");
            }
        }
    }
}
