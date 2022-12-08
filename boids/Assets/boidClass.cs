using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidClass : MonoBehaviour
{
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 direction;
    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float maxSpeed;
    [HideInInspector]
    public float minSpeed;
    Transform cachedTransform;
    // Start is called before the first frame update
    void Awake()
    {
        cachedTransform = transform;
    }
    public void Initialize()
    {
        minSpeed = 1;
        speed = 2;
        maxSpeed = 4;
        direction = -cachedTransform.forward;
        position = cachedTransform.position;
        velocity = speed * direction;
    }
    // Update is called once per frame
    public void moveBoid()
    {
        cachedTransform.forward = -Vector3.Normalize(velocity);
        direction = -Vector3.Normalize(velocity);
        if (Mathf.Abs(Vector3.Magnitude(velocity)) > maxSpeed)
        {
            velocity = (velocity / Vector3.Magnitude(velocity)) * maxSpeed;
        }

        if (Mathf.Abs(Vector3.Magnitude(velocity)) < minSpeed)
        {
            velocity = direction * minSpeed;
        }
        cachedTransform.forward = -Vector3.Normalize(velocity);
        cachedTransform.position += Time.deltaTime * velocity;
        position  = cachedTransform.position;
        transform.position = position;
    }

    
}
