using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffRocket : Influencer
{
    private int quantityRt;

    void Start()
    {
        quantityRt = Random.Range(2, 5);
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        //В списке всего оружия найти ссылку на Ракетницу и добавить снаряды
        GameController.GetInstance().GetPlayer().weapons.Find(weapon => weapon.id == Weapons.Rocket).AddBullets(quantityRt);

        // проверка на столкновение с Игроком
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            MainUIController.isPickedUpRocket = true; // для вызова тектса на экран игроку
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
