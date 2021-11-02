using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private Transform targetLookAtPlayer;

    public GameObject leftEnemyGun;
    public GameObject rightEnemyGun;
    public GameObject EnemyShipProjectile;

    private bool leftEnemyUsed = false;
    private Vector3 startPosEnemyShip;
    private Vector3 newPosEnemyShip;
    private float step = 0.0f;


    void Start()
    {
        InvokeRepeating("EnemyShoot", Random.Range(1, 3), Random.Range(1, 5));

        startPosEnemyShip = transform.position;
        newPosEnemyShip = new Vector3(Random.Range(-15, 15), 0, Random.Range(13, 0));

        //понадобилось обратиться к игровому объекту на сцене, так как при забрасывании вражеского корабля в иерархию цель слежения не задается
        targetLookAtPlayer = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        // Первое сырое решение. Отключить логику врага, если игра закончена
        if (GameController.GetInstance().IsGameOver())
        {
            return;
        }
        // проверим относительное положение позиций player и enemyship и после зададим поворот в сторону player
        Vector3 relativePos = targetLookAtPlayer.position - transform.position;
        Quaternion rotationEnemyShip = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotationEnemyShip;
       
        //зададим случайное движение вражеского корабля
        if (step < 1)
        {
            transform.position = Vector3.Lerp(startPosEnemyShip, newPosEnemyShip, step);
            step = step + 0.001f;
        }

        if (step >= 1)
        {
            step = 0;
            startPosEnemyShip = newPosEnemyShip;
            newPosEnemyShip = new Vector3(Random.Range(-15, 15), 0, Random.Range(13, 0));  //координаты пока в пределах конкретных чисел
        }
    }

    void EnemyShoot()
    {
        // Первое сырое решение. Отключить логику врага, если игра закончена
        if (GameController.GetInstance().IsGameOver())
        {
            return;
        }
        // для стрельбы понадобилось проверять в какую сторону повернуты пушки и после чего вызывать снаряд
        Quaternion startRot = leftEnemyGun.transform.rotation;
        Instantiate(EnemyShipProjectile, EnemyGetGun().transform.position, startRot);
    }

    private GameObject EnemyGetGun()  // чередование правой и левой пушек для выстрела
    {
        if (leftEnemyUsed)
        {
            leftEnemyUsed = false;
            return rightEnemyGun;
        } else
        {
            leftEnemyUsed = true;
            return leftEnemyGun;
        }
    }
}
