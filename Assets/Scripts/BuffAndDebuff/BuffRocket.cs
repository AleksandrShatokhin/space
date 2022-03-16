using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffRocket : Influencer
{
    private int quantityRt;
    private MainUIController uiController;

    void Start()
    {
        uiController = GameObject.Find("MainUI").GetComponent<MainUIController>();
        quantityRt = Random.Range(2, 5);
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        //В списке всего оружия найти ссылку на Ракетницу и добавить снаряды
        GameController.GetInstance().GetPlayer().weapons.Find(weapon => weapon.id == Weapons.Rocket).AddBullets(quantityRt);

        // Реализуем базовый метод
        base.OnTriggerEnter(other);

        // индивидуальные действия по игроку
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //MainUIController.isPickedUpRocket = true; // для вызова тектса на экран игроку
            uiController.GetCurrentText((int)BonusNumber.BuffRocket);

        }
    }
}
