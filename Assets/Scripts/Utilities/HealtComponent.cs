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
        //Ограничить ввод жизней. Если пытаемся отнять больше, чем есть, то просто обнуляем.
        if (health <= Mathf.Abs(hp) && hp < 0)
        {
            GetComponentInParent<Deathable>().Kill();
            return;
        }

        health += hp;
    }


    public float GetHealth()
    {
        return health;
    }

    public void SetDeathable(Deathable deathable) {
        deathableParent = deathable;
    }

}
