using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffShield : Influencer
{
    public GameObject shield;
    private MainUIController uiController;

    private void Start()
    {
        uiController = GameObject.Find("MainUI").GetComponent<MainUIController>();
    }

    private void ActionShield()
    {
        GameController.GetInstance().GetInvulnerablePlayer(true);
        Destroy(gameObject);
        Instantiate(shield, transform.position, transform.rotation);
        uiController.GetCurrentText((int)BonusNumber.BuffShield); // для вызова тектса на экран игроку
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // Реализуем базовый метод
        base.OnTriggerEnter(other);

        if (GameObject.Find("Player").GetComponent<PlayerController>().Invulnerable())
        {
            GameObject shield = GameObject.Find("ForceShield(Clone)");
            Destroy(shield);
            ActionShield();
            return;
        }

        // индивидуальные действия по игроку
        if (other.gameObject.tag == "Player")
        {
            ActionShield();
        }
    }
}
