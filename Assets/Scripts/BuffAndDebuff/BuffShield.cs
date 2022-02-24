using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShield : Influencer
{
    public GameObject shield;

    protected override void OnTriggerEnter(Collider other)
    {
        // Реализуем базовый метод
        base.OnTriggerEnter(other);

        if (PlayerController.isShield)
        {
            Destroy(gameObject);
            return;
        }

        // индивидуальные действия по игроку
        if (other.gameObject.tag == "Player")
        {
            

            PlayerController.isShield = true;
            Destroy(gameObject);
            Instantiate(shield, transform.position, transform.rotation);
            MainUIController.isPickedUpShield = true; // для вызова тектса на экран игроку
        }
    }
}
