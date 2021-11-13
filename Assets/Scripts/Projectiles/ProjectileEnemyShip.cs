using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyShip : MonoBehaviour
{
    private Rigidbody pr_Rigidbody;

    void Start()
    {
        pr_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        pr_Rigidbody.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            //Вызвать смерть из объекта игрока. Не удалять извне
            collision.gameObject.GetComponent<PlayerController>().Death();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            //Уничтожается снаряд, астероид или другой вражеский корабль не уничтожается
            Destroy(gameObject);
        }
    }
}