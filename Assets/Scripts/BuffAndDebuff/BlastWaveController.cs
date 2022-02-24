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
        // Реализуем базовый метод
        base.OnTriggerEnter(other);

        // индивидуальные действия по игроку
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().StartBlastWave();
            Destroy(gameObject);
            MainUIController.isPickedUpBlastWave = true; // для вызова тектса на экран игроку
        }
    }
}