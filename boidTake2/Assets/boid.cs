using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boid : MonoBehaviour {
    public LayerMask obstacleMask;
    public void movement(List<boid> boids) {

        var direction = Vector3.zero;
        var repulsion = Vector3.zero;
        var centerOfMass = Vector3.zero;
        var matching = Vector3.zero;
        var obstacleDir = Vector3.zero;
        int dirCounter = 0;
        int repCounter = 0;
        int centCounter = 0;

        foreach (boid boidi in boids) {
            if (boidi == this) {
                continue;
            }
            if (Vector3.Distance(boidi.transform.position, this.transform.position) < 5f) {
                repulsion += boidi.transform.position - this.transform.position;
                repCounter += 1;
            }
            if (Vector3.Distance(boidi.transform.position, this.transform.position) < 10f) {
                matching += boidi.transform.forward;
                dirCounter += 1;

                centerOfMass += boidi.transform.position - this.transform.position;
                centCounter += 1;
            }
         }
            if (repCounter != 0) {
                repulsion = repulsion / repCounter;
            }
        repulsion = -repulsion;
            if (centCounter != 0)
            {
                centerOfMass = centerOfMass / centCounter;
            }
        centerOfMass -= this.transform.position;
            if (dirCounter != 0)
            {
                matching = matching / dirCounter;
            }
        if (obstacles())
        {
            Debug.Log("Detect");
            obstacleDir = obstacleRays().normalized;

        }
        direction += (matching.normalized * 0.2f + centerOfMass.normalized * 0.06f + repulsion.normalized * 0.4f + obstacleDir * 50);
        /*if (repulsion != Vector3.zero)
        {
            direction = repulsion;
        }
         if (obstacles())
        {
            Debug.Log("Detect");
            direction = obstacleRays().normalized;

        }*/
        
        if (direction != Vector3.zero) {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(direction), 400 * Time.deltaTime);
            }

         this.transform.position += this.transform.TransformDirection(new Vector3(0, 0, 40)) * Time.deltaTime;
    }

    bool obstacles()
    {
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, 0.27f, this.transform.forward, out hit, 4, obstacleMask)) {
            return true;
        }
        return false;
    }

    Vector3 obstacleRays()
    {
        Vector3[] rayDirections = rays();
        for (int i = 0; i < 300; i++)
        {
            Vector3 newDir = transform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(this.transform.position, newDir);
            if (!Physics.SphereCast(ray, 0.27f, 4, obstacleMask))
            {
                return newDir;
            }
        }

        return this.transform.forward;
    }

    Vector3[] rays()
    {
        var rayList = new Vector3[300];
        for (int i = 0; i < 300; i++)
        {
            float x = Mathf.Sin(Mathf.Acos(1 - (2 * ((float)i / 300)))) * Mathf.Cos((Mathf.PI * 2 * (1 + Mathf.Sqrt(5)) / 2) * i);
            float y = Mathf.Sin(Mathf.Acos(1 - (2 * ((float)i / 300)))) * Mathf.Sin((Mathf.PI * 2 * (1 + Mathf.Sqrt(5)) / 2) * i);
            float z = Mathf.Cos(Mathf.Acos(1 - (2 * ((float)i / 300))));
            rayList[i] = new Vector3(x, y, z);
        }

        return rayList;
    }
}

