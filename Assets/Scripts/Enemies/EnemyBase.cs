using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, Deathable
{

    public float movementSpeed;

    public GameObject explosionEffect;

    public AudioClip shootSound;
    public AudioClip deathSound;
    public AudioClip hitSound;

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
        bool isDead = GetComponent<HealtComponent>().Change(-damage);


        if (!isDead)
        {
            HitSound();
        }
    }


    public void ShootSound()
    {
        GameController.GetInstance().PlaySound(shootSound, .3f);
    }

    public void DeathSound()
    {
        GameController.GetInstance().PlaySound(deathSound, 0.8f);
    }


    public void HitSound()
    {
        GameController.GetInstance().PlaySound(hitSound, 0.5f);
    }

}
