using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidCalculations : MonoBehaviour
{
    boidClass[] boidList;
    // Start is called before the first frame update
    void Awake()
    {
        boidList = FindObjectsOfType<boidClass>();
        foreach (boidClass boid in boidList)
        {
            Vector3 pos = boid.transform.position + Random.insideUnitSphere * 10;
            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;
            boid.Initialize();
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (boidClass boid in boidList)
        {
            boid.moveBoid();
            boid.velocity += (centerCalc(boid) + stayAway(boid) + matching(boid));
        }
    }
    bool inFront(boidClass boidj, boidClass boid)
    {
        float angle = Vector3.Angle(boidj.transform.forward, boid.position - transform.position);
        if (Mathf.Abs(angle) < 180)
        {
            return true;

        }
        return false;
    }
    public Vector3 centerCalc(boidClass boidj)
    {
        Vector3 centerOfMass = Vector3.zero;
        foreach (boidClass boid in boidList)
        {
            if (boid != boidj && inFront(boidj, boid))
            {
                centerOfMass += boid.position;
            }
        }
        centerOfMass = centerOfMass / (boidList.Length - 1);
        return (centerOfMass - boidj.position) / (100);
    }

    public Vector3 stayAway(boidClass boidj)
    {
        Vector3 repulsion = Vector3.zero;
        foreach (boidClass boid in boidList)
        {
            if (boid != boidj && inFront(boidj, boid))
            {
                if (Mathf.Abs(Vector3.Magnitude(boid.position - boidj.position)) < 1/10)
                {
                    repulsion = repulsion - (boid.position - boidj.position);
                }
            }
        }
        return repulsion;
    }

    public Vector3 matching(boidClass boidj)
    {
        Vector3 matchedVelocity = Vector3.zero;
        foreach (boidClass boid in boidList)
        {
            if (boid != boidj && inFront(boidj, boid))
            {
                matchedVelocity += boid.velocity;
            }
        }

        matchedVelocity = matchedVelocity / (boidList.Length - 1);
        return (matchedVelocity - boidj.velocity);
    }
}
