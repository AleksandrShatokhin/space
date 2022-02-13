using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    SphereCollider sc;
    public float projectileSpeed = 15.0f;
    public float rocketSpeed = 8.0f;
    public float damage = 1.0f;

    public GameObject flaresEffect;
    public float flaresDestroyTime = 0.3f;


    public AudioClip explosionSound;

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


    virtual protected void OnCollisionEnter(Collision collision)
    {
        
    }

    protected void FlaresEffect(Vector3 position)
    {
        //в месте соприкосновения создать эффект искр
        GameObject flares = Instantiate(flaresEffect, position, Quaternion.identity);
        Destroy(flares, flaresDestroyTime);
    }

    //abstract protected void OnCollisionEnter(Collision collision);
}
