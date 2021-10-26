using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyShip : MonoBehaviour
{
    private Rigidbody pr_Rigidbody;

    void Start()
    {
        pr_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //transform.localPosition = transform.localPosition + new Vector3(0, 0, 1) * Time.deltaTime;
        //pr_Rigidbody.velocity = -transform.forward * speed;
        pr_Rigidbody.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
        //transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
