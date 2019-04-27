/**
    ITCS 4232-001 Group Project
    SpawnPoint.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float safeCheckRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsSafeToSpawn()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, safeCheckRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].tag=="Player")
            {
                return false;
            }
            i++;
        }
        return true;
    }
}
