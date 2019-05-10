/**
    ITCS 4232-001 Group Project
    SpectatorCameraController.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 3/31/19
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCameraController : MonoBehaviour
{
    public float moveSpeed;
    public float horizontalAimSpeed;//degrees/second?
    public float verticalAimSpeed;

    public Vector3 currentRotation;

    Rigidbody rgbd;
    public int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        if (!(rgbd = gameObject.GetComponent<Rigidbody>()))
        {
            rgbd = gameObject.GetComponentInChildren<Rigidbody>();
            currentRotation = rgbd.rotation.eulerAngles;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Input on x ("Horizontal")
        float hAxis = Input.GetAxis("HorizontalP" + playerNum);
        // Input on z ("Vertical")
        float vAxis = Input.GetAxis("VerticalP" + playerNum);
        float lookHAxis = Input.GetAxis("AimHorizontalP" + playerNum);
        float lookVAxis = Input.GetAxis("AimVerticalP" + playerNum);
        float elevationAxis = Input.GetAxis("SpectateElevationP" + playerNum);

        // Distance ( speed = distance / time --> distance = speed * time)
        float distance = moveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(hAxis * distance, elevationAxis * distance, vAxis * distance);
        movement = transform.TransformDirection(movement);

        // Current position
        Vector3 currPosition = transform.position;

        // New position
        Vector3 newPosition = currPosition + movement;

        // Move the rigid body
        rgbd.MovePosition(newPosition);

        //Vector3 tempRotation = rgbd.rotation.eulerAngles;
        //tempRotation.x += verticalAimSpeed * lookVAxis;
        //tempRotation.y += horizontalAimSpeed * lookHAxis;


        currentRotation.x += verticalAimSpeed * lookVAxis * Time.deltaTime;
        currentRotation.y += horizontalAimSpeed * lookHAxis * Time.deltaTime;
        rgbd.MoveRotation(Quaternion.Euler(currentRotation));
    }
}
