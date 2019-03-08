using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpeedometer : MonoBehaviour
{
    public float averageSpeed;
    public Vector3 averageVelocity;
    public Vector3 startPos;
    public Vector3 currentPos;
    public Vector3 distanceTravelled;
    public float timeFromStart;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPos = transform.position;
        distanceTravelled = currentPos - startPos;
        averageVelocity = distanceTravelled / timeFromStart;
        averageSpeed = averageVelocity.magnitude;
        timeFromStart++;
    }
}
