using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShield : Influencer
{
    public GameObject shield;
    
    /*void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().isShield = true;
            Destroy(gameObject);
            Instantiate(shield, transform.position, transform.rotation);
        }
    }
    Пока убрал данный формат. Тестируем через триггер
    */

    void OnTriggerEnter(Collider other)
    {
        //проверка на столкновение с игроком
        if(other.gameObject.tag == "Player")
        {
            PlayerController.isShield = true;
            Destroy(gameObject);
            Instantiate(shield, transform.position, transform.rotation);
            MainUIController.isPickedUpShield = true; // для вызова тектса на экран игроку
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
