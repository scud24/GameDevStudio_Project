using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public float refireWaitTime;
    public float currentRefireWait;
    public GameObject projectile;
    public float projectileSpeed;
    public Vector3 projectileSpawnLoc;
    public Vector3 projectileFireDir;
    public GameObject playerCamera;
    public GameObject cameraTarget;
    public int playerNum;


    // Start is called before the first frame update
    void Start()
    {
        playerNum = GetComponentInParent<PlayerCharacter>().playerNum;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentRefireWait--;
    }

    public void Fire()
    {
        if (currentRefireWait <= 0)
        {
                projectileSpawnLoc = transform.position;
                projectileFireDir = Vector3.Normalize(cameraTarget.transform.position - projectileSpawnLoc);
                Quaternion initialRotation = Quaternion.LookRotation(projectileFireDir);
                GameObject newProjectile = Instantiate(projectile, projectileSpawnLoc, initialRotation);
                newProjectile.GetComponent<BasicProjectile>().projectileDir = projectileFireDir;
                newProjectile.GetComponent<BasicProjectile>().projectileSpeed = projectileSpeed;
                newProjectile.GetComponent<BasicProjectile>().parentPlayerNum = playerNum;
                Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), GetComponent<Collider>());
                Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), GetComponentInParent<PlayerCharacter>().coll);
                currentRefireWait = refireWaitTime;
        }
    }
}
