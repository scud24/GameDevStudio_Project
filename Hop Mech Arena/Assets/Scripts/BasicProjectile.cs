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

    public GameObject hitEffectObject;
    public string projectileType;

    public int parentPlayerNum;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rgbd.AddForce(projectileGravityScale * Physics.gravity * rgbd.mass);
        Vector3 tempPos = transform.position;
        tempPos += projectileDir * projectileSpeed;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(projectileType == "Explosive")
        {
            Instantiate(hitEffectObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
