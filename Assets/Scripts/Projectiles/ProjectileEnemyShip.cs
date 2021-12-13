using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyShip : MonoBehaviour
{
    private Rigidbody pr_Rigidbody;

    //Коэффициент силы
    private float forceRate = 1.0f;
    public float damage = 1.0f;

    void Start()
    {
        pr_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        pr_Rigidbody.AddForce(transform.forward * forceRate, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Вызвать смерть из объекта игрока. Не удалять извне
            collision.gameObject.GetComponent<PlayerController>().AddDamage(damage);
            Destroy(gameObject);
            CameraController.shake = true;
        }

        if (collision.gameObject.layer == 6) //слой 6 - это enemy
        {
            //Уничтожается снаряд, астероид или другой вражеский корабль не уничтожается
            Destroy(gameObject);
        }
    }
}
