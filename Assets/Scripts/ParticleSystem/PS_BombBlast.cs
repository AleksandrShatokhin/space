using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_BombBlast : MonoBehaviour
{
    private float damage = 2.0f;
    private bool isCollision; //этой переменной ограничу воздействие частиц только первой частицей

    void Start()
    {
        isCollision = false;
    }

    void Update()
    {
        Destroy(transform.parent.gameObject, 1);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player" && isCollision == false)
        {
            isCollision = true;
            other.gameObject.GetComponent<PlayerController>().AddDamage(damage);
        }
    }
}
