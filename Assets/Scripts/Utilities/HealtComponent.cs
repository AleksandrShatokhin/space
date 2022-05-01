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

    private float startHealth;
    private Deathable deathableParent;

    void Start(){
        startHealth = health;
    }

    public bool Change(float hp)
    {
        bool isDead = false;

        //Максимальное ХП должно регулироваться для каждого объекта.
        //Поэтому при ограничении используем значение StartHealth
        if (health + hp > startHealth) //если подобраное сердечко дает больше определенного значения
        {
            health = startHealth;
        }
        //Ограничить ввод жизней. Если пытаемся отнять больше, чем есть, то просто обнуляем.
        else if (health + hp <= 0)
        {
            GetComponentInParent<Deathable>().Kill();
            isDead = true;
        }
        else 
        {
            health += hp;
        }

        return isDead;
    }


    public float GetHealth()
    {
        return health;
    }

    public void SetDeathable(Deathable deathable) {
        deathableParent = deathable;
    }


    public float GetStartHealth() => startHealth;
}
