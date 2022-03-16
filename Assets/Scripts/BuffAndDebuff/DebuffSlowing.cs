using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSlowing : Influencer
{
    private Transform targetPlayer;
    private float distance;
    private Quaternion rotDebuff;
    private MainUIController uiController;

    void Start()
    {
        uiController = GameObject.Find("MainUI").GetComponent<MainUIController>();
        GameObject player = GameObject.Find("Player");

        if (player)
        {
            targetPlayer = player.GetComponent<Transform>();
            rotDebuff = transform.rotation;
        }
    }

    void Update()
    {

        if (!targetPlayer)
        {
            return;
        }

        // узнаем дистанцию до игрока и при короткой дистанции заставляем двигаться дебафф на игрока
        distance = Vector3.Distance(transform.position, targetPlayer.transform.position);
        if (distance < 10)
        {
            transform.LookAt(targetPlayer);
            transform.position = transform.position + transform.forward * 1.0f * Time.deltaTime;
        }
        else transform.rotation = rotDebuff; // повернем дебафф в прежнее направление
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // Реализуем базовый метод
        base.OnTriggerEnter(other);

        // индивидуальные действия по игроку
        if (other.gameObject.tag == "Player")
        {
            // для вызова тектса на экран игроку
            uiController.GetCurrentText((int)BonusNumber.DebuffSlowing);

            if (TimerController.isSlowing == false)
            {
                TimerController.isSlowing = true;
                PlayerController.speedPlayer = 10.0f;
                Destroy(gameObject);
            }
            else
            {
                TimerController.timerStatrForSlowing = 10.0f;
                Destroy(gameObject);
            }
        }
    }
}
