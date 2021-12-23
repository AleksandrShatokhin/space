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

    public void Change(float hp)
    {

        health += hp;
        
        //Ограничить ввод жизней. Если пытаемся отнять больше, чем есть, то просто обнуляем.
        if (health <= 0)
        {
            GetComponentInParent<Deathable>().Kill();
        }

        UpdatePlayerHealthBar();
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
