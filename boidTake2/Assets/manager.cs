using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class manager : MonoBehaviour
{

    boid[] boidTemp;
    public List<boid> boidList;
    
    // Start is called before the first frame update
    void Start()
    {
        boidList = new List<boid>();
        boidTemp = FindObjectsOfType<boid>();

        foreach (boid boidi in boidTemp)
        {
            Vector3 pos = boidi.transform.position + Random.insideUnitSphere * 10;
            boidi.transform.position = pos;
            boidi.transform.forward = Random.insideUnitSphere;
            boidList.Add(boidi);
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (boid boidi in boidList)
        {
            boidi.movement(boidList);

        }
    }
}
