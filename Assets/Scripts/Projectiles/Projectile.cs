using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    SphereCollider sc;
    public float projectileSpeed = 15.0f;
    public float rocketSpeed = 8.0f;


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

    virtual protected void Rocket() //если игроком выбраны ракеты
    {
        transform.Translate(Vector3.forward * Time.deltaTime * rocketSpeed);
    }


    //abstract protected void OnCollisionEnter(Collision collision);
}
