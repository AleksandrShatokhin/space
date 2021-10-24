using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{

    public float rotateSpeed = 0.1f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * rotateSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
