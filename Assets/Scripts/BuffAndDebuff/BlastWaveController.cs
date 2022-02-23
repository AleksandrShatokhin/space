using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWaveController : Influencer
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        //проверка на столкновение с игроком
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().StartBlastWave();
            Destroy(gameObject);
            MainUIController.isPickedUpBlastWave = true; // для вызова тектса на экран игроку
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