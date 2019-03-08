using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraMoveTarget;
    public Transform cameraLookTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraMoveTarget.position;
        transform.LookAt(cameraLookTarget);
    }
}
