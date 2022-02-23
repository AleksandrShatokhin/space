using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : EnemyBase
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
        //задаем стрельбу вражеского корабля
        StartCoroutine(Shooting());

        startPosEnemyShip = transform.position;
        newPosEnemyShip = new Vector3(Random.Range(-15, 15), 0, Random.Range(13, 0));

        //понадобилось обратиться к игровому объекту на сцене, так как при забрасывании вражеского корабля в иерархию цель слежения не задается
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            targetLookAtPlayer = player.transform;
        }

    }

    protected override Vector3 GetProjectilePosition() => EnemyGetGun().transform.position;
    

    protected override Quaternion GetProjectileRotation() => leftEnemyGun.transform.rotation;


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
