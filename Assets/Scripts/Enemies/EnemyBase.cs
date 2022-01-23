using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, Deathable
{

    public float movementSpeed;

    public GameObject explosionEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Death()
    {
        GameController.GetInstance().AddKilledEnemy();

        if (explosionEffect)
        {
            GameObject expolion = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            Destroy(expolion, 2);
        }

        Destroy(gameObject);
    }

    void Deathable.Kill()
    {
        Death();
    }


    public void AddDamage(float damage)
    {
        GetComponent<HealtComponent>().Change(-damage);
    }
}
