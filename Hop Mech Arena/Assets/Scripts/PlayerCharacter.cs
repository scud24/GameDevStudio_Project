using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;
    public float turnSpeed;
    public Vector3 playerVelocity;
    bool pressedJump = false;
    float jumpVelocity = 0;

    public bool isGrounded;

    public bool turnLeft;
    public bool turnRight;
    public bool stunned;
    public float stunTimeRemaining = 0;
    public GameObject playerBox;


    public int playerNum = 1;

    //public GameObject playerCollider;
    public bool isDummy = false;

    public BasicGun currentWeapon;
    Rigidbody rgbd;
    public Collider coll;

    bool debugDraw = true;

	// Use this for initialization
	void Start () {

        rgbd = gameObject.GetComponent<Rigidbody>();
        playerVelocity = new Vector3();
        coll = playerBox.GetComponent<Collider>();
        currentWeapon = GetComponentInChildren<BasicGun>();
    }

    // Update is called once per frame
    // Update is called once per frame
    void FixedUpdate()
    {
        if(stunTimeRemaining == 0)
        {
            stunned = false;
            //transform.
        }
        else if (stunTimeRemaining > 0)
        {
            stunTimeRemaining--;
        }
        if (!isDummy || stunned)
        {
            // Handle player walking
            WalkHandler();

            //Handle player jumping
            JumpHandler();

            //Handle firing
            GunHandler();
        }
    }

    // Make the player walk according to user input
    void WalkHandler()
    {
        // Set x and z velocities to zero
        rgbd.velocity = new Vector3(0, rgbd.velocity.y, 0);

        // Distance ( speed = distance / time --> distance = speed * time)
        float distance = walkSpeed * Time.deltaTime;

        // Input on x ("Horizontal")
        float hAxis = Input.GetAxis("Horizontal");

        // Input on z ("Vertical")
        float vAxis = Input.GetAxis("Vertical");

        float zAxis = Input.GetAxis("Turn");
        if (zAxis < 0)
        {
            turnLeft = true;
        }
        else
        {
            turnLeft = false;
        }

        if (zAxis > 0)
        {
            turnRight = true;
        }
        else
        {
            turnRight = false;
        }

        // Movement vector
        Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);
        movement = transform.TransformDirection(movement);
        //Debug.Log(transform.TransformDirection(movement));
        // Current position
        Vector3 currPosition = transform.position;

        // New position
        Vector3 newPosition = currPosition + movement;

        // Move the rigid body
        rgbd.MovePosition(newPosition);
        playerVelocity = rgbd.velocity;

        Vector3 tempRotation = rgbd.rotation.eulerAngles;
        tempRotation.y += turnSpeed * zAxis;
        rgbd.MoveRotation(Quaternion.Euler(tempRotation));
    }

    // Check whether the player can jump and make it jump
    void JumpHandler()
    {
        // Jump axis
        float jAxis = Input.GetAxis("Jump");

        // Is grounded
        isGrounded = true;
        isGrounded = CheckGrounded();

        // Check if the player is pressing the jump key
        if (jAxis > 0f)
        {
            // Make sure we've not already jumped on this key press
            if (!pressedJump && isGrounded)
            {
                // We are jumping on the current key press
                pressedJump = true;

                // Jumping vector
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);

                // Make the player jump by adding velocity
                rgbd.velocity = (rgbd.velocity /2) + jumpVector;
            }
        }
        else
        {
            // Update flag so it can jump again if we press the jump key
            pressedJump = false;
        }
    }

    // Check if the object is grounded
    bool CheckGrounded()
    {
        float rayDist = 0.1f;
        // Object size in x
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;

        // Position of the 4 bottom corners of the game object
        // We add 0.01 in Y so that there is some distance between the point and the floor
        Vector3 corner1 = transform.TransformPoint( new Vector3(sizeX / 2, (-sizeY / 2) + 0.01f, sizeZ / 2) +coll.transform.localPosition);
        Vector3 corner2 = transform.TransformPoint(new Vector3(-sizeX / 2, (-sizeY / 2) + 0.01f, sizeZ / 2) + coll.transform.localPosition);
        Vector3 corner3 = transform.TransformPoint(new Vector3(sizeX / 2, (-sizeY / 2) + 0.01f, -sizeZ / 2) + coll.transform.localPosition);
        Vector3 corner4 = transform.TransformPoint(new Vector3(-sizeX / 2, (-sizeY / 2) + 0.01f, -sizeZ / 2) + coll.transform.localPosition);

        // Send a short ray down the cube on all 4 corners to detect ground
        RaycastHit hit1, hit2, hit3, hit4;
        bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), out hit1, rayDist);
        bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), out hit2, rayDist);
        bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), out hit3, rayDist);
        bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), out hit4, rayDist);

        if (debugDraw)
        {
            if(grounded1) Debug.DrawLine(corner1, hit1.point, Color.green);
            else Debug.DrawLine(corner1, corner1 + new Vector3(0, -rayDist, 0), Color.red);
            if (grounded2) Debug.DrawLine(corner2, hit2.point, Color.green);
            else Debug.DrawLine(corner2, corner2 + new Vector3(0, -rayDist, 0), Color.red);
            if (grounded3) Debug.DrawLine(corner3, hit3.point, Color.green);
            else Debug.DrawLine(corner3, corner3 + new Vector3(0, -rayDist, 0), Color.red);
            if (grounded4) Debug.DrawLine(corner4, hit4.point, Color.green);
            else Debug.DrawLine(corner4, corner4 + new Vector3(0, -rayDist, 0), Color.red);

        }
        /*Debug.Log(grounded1 + " " + grounded2 + " " + grounded3 + " " + grounded4);
        Debug.Log(corner1.y + " " + (transform.position + new Vector3(sizeX / 2, (-sizeY / 2) + 0.01f, sizeZ / 2) + coll.transform.localPosition).y);
        if(grounded1 || grounded2 || grounded3 || grounded4)
        {
            Debug.Log(hit1.distance + " " + hit2.distance + " " + hit3.distance + " " + hit4.distance);
        }*/
        // If any corner is grounded, the object is grounded
        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    void GunHandler()
    {
        if(Input.GetAxis("Fire1") > 0 && currentWeapon != null)
        {
            currentWeapon.Fire();
        }
    }
}
