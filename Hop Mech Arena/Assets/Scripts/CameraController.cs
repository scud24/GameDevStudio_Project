/**
    ITCS 4232-001 Group Project
    CameraController.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraMoveTarget;
    public Transform cameraLookTarget;
    public Transform spectateCameraMoveTarget;
    public Transform spectateCameraLookTarget;
    public bool isSpectateMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpectateMode)
        {
            transform.position = spectateCameraMoveTarget.position;
            transform.LookAt(spectateCameraLookTarget);
        }
        else
        {
            transform.position = cameraMoveTarget.position;
            transform.LookAt(cameraLookTarget);
        }
    }
}
