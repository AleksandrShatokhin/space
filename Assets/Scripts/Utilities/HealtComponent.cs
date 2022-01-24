using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtComponent : MonoBehaviour
{

    /** 
     * Для использования компонента Health на объекте нужно реализовать
     * интерфейс Deathable
     */

    public float health;
    private Deathable deathableParent;

    public bool Change(float hp)
    {
        bool isDead = false;

        health += hp;
        
        //Ограничить ввод жизней. Если пытаемся отнять больше, чем есть, то просто обнуляем.
        if (health <= 0)
        {
            GetComponentInParent<Deathable>().Kill();
            isDead = true;
        }

        if (health > 5) //если подобраное сердечко дает больше определенного значения
        {
            health = 5;
        }

        UpdatePlayerHealthBar();

        return isDead;
    }


    public float GetHealth()
    {
        return health;
    }

    public void SetDeathable(Deathable deathable) {
        deathableParent = deathable;
    }


    public void UpdatePlayerHealthBar()
    {
        //Если есть компонент HealthBar в родителе, то надо проставить новое значение
        PlayerController pc = GetComponentInParent<PlayerController>();

        if (!pc)
        {
            return;
        }
        
        pc.healthBar.SetValue(health);

    }
}
