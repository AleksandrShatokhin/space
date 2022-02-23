using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSlowing : Influencer
{
    private Transform targetPlayer;
    private float distance;
    private Quaternion rotDebuff;

    void Start()
    {
        targetPlayer = GameObject.Find("Player").GetComponent<Transform>();
        rotDebuff = transform.rotation;
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

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.Find("Game").GetComponent<TimerController>().isSlowing == false)
            {
                GameObject.Find("Game").GetComponent<TimerController>().isSlowing = true;
                GameObject.Find("Player").GetComponent<PlayerController>().speedPlayer = 10.0f;
                Destroy(gameObject);
            }
            else
            {
                GameObject.Find("Game").GetComponent<TimerController>().timerStatrForSlowing = 10.0f;
                Destroy(gameObject);
            }
        }
    }Пока убрал данный формат. Тестируем через триггер*/

    protected override void OnTriggerEnter(Collider other)
    {
   
        //проверка столкновения с игроком
        if (other.gameObject.tag == "Player")
        {
            if (TimerController.isSlowing == false)
            {
                TimerController.isSlowing = true;
                PlayerController.speedPlayer = 10.0f;
                Destroy(gameObject);
                MainUIController.isPickedUpSlowing = true; // для вызова тектса на экран игроку
                DoEffect();
            }
            else
            {
                TimerController.timerStatrForSlowing = 10.0f;
                Destroy(gameObject);
            }
            ActivateSound();
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
