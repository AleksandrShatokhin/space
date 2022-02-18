using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWaveController : Influencer
{
    private GameObject player;

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            blastWave.Play();
            Destroy(gameObject);
        }
    }Пока убрал данный формат. Тестируем через триггер*/

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        //проверка на столкновение с игроком
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().StartBlastWave();
            Destroy(gameObject);
            MainUIController.isPickedUpBlastWave = true; // для вызова тектса на экран игроку
            ActivateSound();
            DoEffect();
        }

        // проверка на столкновение со всевозможными снарядами
        if(other.gameObject.layer == 8) // слой 8 - это projectile
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        // проверка на столкновение с астероидами
        if(other.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}