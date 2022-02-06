using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_RocketBlast : MonoBehaviour
{
    private float damage = 3.0f;
    private bool isCollision;

    void Start()
    {
        isCollision = false;
    }

    void Update()
    {
        Destroy(gameObject, 2);
    }

    void OnParticleCollision(GameObject collision)
    {
        if (collision.gameObject.layer == 6 && isCollision == false) // слой 6 - это enemy
        {
            isCollision = true;
            collision.gameObject.GetComponent<EnemyBase>().AddDamage(damage);
        }
    }
}
