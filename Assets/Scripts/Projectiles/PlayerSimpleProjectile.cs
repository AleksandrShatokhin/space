using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleProjectile : Projectile
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    override protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    override protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) // слой 6 - это enemy
        {
            //collision.gameObject.GetComponent<EnemyBase>().Death();
            collision.gameObject.GetComponent<EnemyBase>().AddDamage(damage);

            ContactPoint contact = collision.GetContact(0);
            FlaresEffect(contact.point);
        }

        Destroy(gameObject);
    }

}
