using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_RocketBlast : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 1);
    }

    void OnParticleCollision(GameObject collision)
    {
        if (collision.gameObject.layer == 6) // слой 6 - это enemy
        {
            collision.gameObject.GetComponent<EnemyBase>().Death();
        }
    }
}
