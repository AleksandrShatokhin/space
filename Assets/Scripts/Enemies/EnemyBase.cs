using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, Deathable
{

    public float movementSpeed;

    public GameObject explosionEffect;

    public AudioClip shootSound;
    public AudioClip deathSound;

    public virtual void Death()
    {
        GameController.GetInstance().AddKilledEnemy();

        if (explosionEffect)
        {
            GameObject expolion = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            Destroy(expolion, 2);
            DeathSound();
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


    public void ShootSound()
    {
        GameController.GetInstance().PlaySound(shootSound, .4f);
    }

    public void DeathSound()
    {
        GameController.GetInstance().PlaySound(deathSound, 1.0f);
    }

}
