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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) // слой 6 - это enemy
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }

}
