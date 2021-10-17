using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    SphereCollider sc;
    public float projectileSpeed = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    virtual protected void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
    }


    //abstract protected void OnCollisionEnter(Collision collision);
}
