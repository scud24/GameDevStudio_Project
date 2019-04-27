/**
    ITCS 4232-001 Group Project
    PlayerCamera.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject targetPlayer;
    public Vector3 playerRot;
    public Vector3 rigRot;
    public Vector3 quat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = targetPlayer.transform.position;
        Vector3 tempRot = transform.rotation.eulerAngles;
        //tempRot.y = targetPlayer.transform.rotation.eulerAngles.y;
        if (targetPlayer.GetComponent<IndependentAimPlayer>())
        {
            tempRot.y = targetPlayer.GetComponent<IndependentAimPlayer>().aimDir.y;
        }
        else
        {
            tempRot.y = targetPlayer.transform.rotation.eulerAngles.y;
        }
        quat = Quaternion.Euler(tempRot).eulerAngles;
        transform.rotation = Quaternion.Euler(tempRot);
        playerRot = targetPlayer.transform.rotation.eulerAngles;
        rigRot = transform.rotation.eulerAngles;
    }
}
