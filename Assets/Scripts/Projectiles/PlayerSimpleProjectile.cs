using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleProjectile : Projectile
{
    // Update is called once per frame
    override protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    override protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layers.Enemy) // слой 6 - это enemy
        {
            //collision.gameObject.GetComponent<EnemyBase>().Death();
            collision.gameObject.GetComponent<EnemyBase>().AddDamage(damage);

            ContactPoint contact = collision.GetContact(0);
            FlaresEffect(contact.point);
        }

        Destroy(gameObject);
    }

}
