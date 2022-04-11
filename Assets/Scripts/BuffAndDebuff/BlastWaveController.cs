using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWaveController : Influencer
{
    private GameObject player;
    private MainUIController uiController;

    private void Start()
    {
        player = GameObject.Find("Player");
        uiController = GameObject.Find("MainUI").GetComponent<MainUIController>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
    
        // Реализуем базовый метод
        base.OnTriggerEnter(other);

        // индивидуальные действия по игроку
        if (other.gameObject.tag == "Player")
        {
            // для вызова тектса на экран игроку
            uiController.GetCurrentText((int)BonusNumber.BuffBlastWave);
            player.GetComponent<PlayerController>().StartBlastWave();
            Destroy(gameObject);
        }
    }
}