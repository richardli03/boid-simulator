using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public boidClass prefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * 10; //The last arg is the spawn radius
            boidClass Boid = Instantiate(prefab);
            Boid.transform.position = pos;
            Boid.transform.forward = Random.insideUnitSphere;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}