using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, Deathable
{
    protected Transform targetLookAtPlayer;

    public float movementSpeed;

    public GameObject explosionEffect;

    public AudioClip shootSound;
    public AudioClip deathSound;
    public AudioClip hitSound;

    public GameObject projectileEnemy;

    //Ожидание перед первым выстрелом
    public float FirstShootWait = 2.0f;

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


    public virtual void AddDamage(float damage)
    {
        HitSound();
        _ = GetComponent<HealtComponent>().Change(-damage);
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

    virtual protected Vector3 GetProjectilePosition()
    {
        return gameObject.transform.position;
    }

    virtual protected Quaternion GetProjectileRotation()
    {
        return Quaternion.identity;
    }

    virtual protected void PlayAnimation() { }

    protected IEnumerator Shooting()
    {
        yield return new WaitForSeconds(FirstShootWait);

        while (true)
        {
            Instantiate(projectileEnemy, GetProjectilePosition(), GetProjectileRotation());

            PlayAnimation();

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));

            ShootSound();
        }
    }

    protected float AngleBetweenBossAndPlayer()
    {
        Vector3 targetPos = targetLookAtPlayer.transform.position;
        targetPos.y = gameObject.transform.position.y;

        Vector3 targetAngle = targetPos - gameObject.transform.position;

        float angleBetween = Vector3.SignedAngle(targetAngle, gameObject.transform.forward, Vector3.up);

        return angleBetween;
    }
}
