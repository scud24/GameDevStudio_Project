/**
    ITCS 4232-001 Group Project
    BasicExplosion.cs
    Purpose: 


    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicExplosion : MonoBehaviour
{

    public float timeToLive;
    public float timeFromStart;
    public bool debugLoop;
    public float debugResetTime;

    public float minScale;
    public float maxScale;
    public float scaleRate;
    public float fadeStartTime;
    public float alphaStart;
    public float alphaEnd;

    public float radius;
    public float force;

    public float baseIndirectDamage;
    public float minIndirectDamage;
    public float indirectDamageFalloffStart;
    public float indirectDamageFalloffEnd;
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("Start");
        maxScale = radius * 2;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider c in colliders)
        {
            if (c.attachedRigidbody == null) continue;
            float targetDist = Vector3.Distance(transform.position, c.transform.position);
            targetDist = Mathf.Clamp(targetDist, indirectDamageFalloffStart, indirectDamageFalloffEnd);
            Debug.Log(indirectDamageFalloffEnd + ", " + indirectDamageFalloffStart);
            float distRatio = (targetDist - indirectDamageFalloffStart) / (indirectDamageFalloffEnd - indirectDamageFalloffStart);
            float appliedForce = Mathf.Lerp(0, force, distRatio);
            c.attachedRigidbody.AddExplosionForce(force, transform.position, radius, 0.2f, ForceMode.Impulse);
            if(c.transform.GetComponentInParent<PlayerStatsManager>())
            {
                Debug.Log("Dist Ratio: " + distRatio);
                float indirectDamage = Mathf.Lerp(minIndirectDamage, baseIndirectDamage, distRatio);
                Debug.Log("Indirect damage: " + indirectDamage);
                c.transform.GetComponentInParent<PlayerStatsManager>().ApplyDamage(indirectDamage);
            }
        }
    }
    private void Awake()
    {
        Debug.Log("Awake"); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scaleRate = (radius - minScale) / timeToLive;
        float currentScale = Mathf.Lerp(minScale, radius, (timeFromStart / timeToLive));
        Vector3 tempScale = new Vector3(currentScale, currentScale, currentScale);
        transform.localScale = tempScale;

        //timeFromStart++;
        float fadeAmount = 0;
        if (timeFromStart > fadeStartTime)
        {
            fadeAmount = (timeFromStart - fadeStartTime) / (timeToLive - fadeStartTime);
        }
        Color tempColor = GetComponent<Renderer>().material.color;
        tempColor.a = Mathf.Lerp(alphaStart, alphaEnd, fadeAmount);
        GetComponent<Renderer>().material.color = tempColor;

        if (timeFromStart > timeToLive)
        {
            if (debugLoop)
            {
                if(timeFromStart > timeToLive+debugResetTime)
                {
                    timeFromStart = 0;
                    Debug.Log(fadeAmount + " " + tempColor.a);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        timeFromStart++;
    }
}
