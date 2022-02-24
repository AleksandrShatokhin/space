using UnityEngine;
using System.Collections;
using UnityEditor;

public class Influencer : MonoBehaviour
{
    public AudioClip activateSound;
    public AudioClip destroySound;
    public GameObject activateEffect;


    private void Awake()
    {
        if (!destroySound)
        {
            destroySound = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Sounds/S_InfluencerDestroyed.mp3", typeof(AudioClip));
        }
    }

    public void ActivateSound()
    {
        GameController.GetInstance().PlaySound(activateSound, 0.5f);
    }


    public void OnDestroy()
    {
        if (!destroySound)
        {
            return;
        }

        GameController.GetInstance().PlaySound(destroySound, 4);
    }


    public void DoEffect()
    {
        if (activateEffect)
        {
            GameObject effect = Instantiate(activateEffect, this.gameObject.transform.position, Quaternion.identity);
            Destroy(effect, 2);
        }
    }

    public void OnPlayerEffects()
    {
        ActivateSound();
        DoEffect();
    }

    public void OnPlayerContact(Collider other)
    {
        // проверка на столкновение с игроком
        if (other.gameObject.tag == "Player")
        {
            OnPlayerEffects();
        }
    }

    public void OnProjectileContact(Collider other)
    {
        // проверка на столкновение со всевозможными снарядами
        if (other.gameObject.layer == (int)Layers.Projectile)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    public void OnAsteroidContact(Collider other)
    {
        // проверка на столкновение с астероидами
        if (other.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }

    virtual protected void OnTriggerEnter(Collider other)
    {

        OnPlayerContact(other);
        OnProjectileContact(other);
        OnAsteroidContact(other);
    }
}
