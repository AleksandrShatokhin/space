using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_RocketBlast : MonoBehaviour
{
    protected float damage = 3.0f;
    [SerializeField] private SphereCollider blastCol;

    void Start()
    {
        blastCol.enabled = false;

        StartCoroutine(Blast());
    }

    void Update()
    {
        Destroy(gameObject, 2);
    }

    IEnumerator Blast()
    {
        yield return new WaitForSeconds(0.1f);
        blastCol.enabled = true;
    }

    //void OnParticleCollision(GameObject collision)
    //{
    //    if (collision.gameObject.layer == 6 && isCollision == false) // слой 6 - это enemy
    //    {
    //        isCollision = true;
    //        collision.gameObject.GetComponent<EnemyBase>().AddDamage(damage);
    //    }
    //}
}
