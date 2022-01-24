using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyBase
{
    private Vector3 startPosEnemyBomber, newPosEnemyBomber;
    private float step = 0.0f;
    private Transform targetLookAtPlayer;
    public GameObject bombPrefab, gun;
    private Animator animShot;

    void Start()
    {
        animShot = GetComponent<Animator>();

        startPosEnemyBomber = transform.position;
        newPosEnemyBomber = new Vector3(Random.Range(-15, 15), 0, Random.Range(13, 0));

        targetLookAtPlayer = GameObject.Find("Player").GetComponent<Transform>();

        //задаем стрельбу вражеского корабля
        InvokeRepeating("BomberShoot", Random.Range(5, 7), Random.Range(2, 5));
    }

    void Update()
    {
        MoveBomber();
    }

    void MoveBomber()
    {
        // проверим относительное положение позиций player и enemyship и после зададим поворот в сторону player
        Vector3 relativePos = targetLookAtPlayer.position - transform.position;
        Quaternion rotationEnemyBomber = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotationEnemyBomber;

        //зададим случайное движение вражеского корабля
        if (step < 1)
        {
            transform.position = Vector3.Lerp(startPosEnemyBomber, newPosEnemyBomber, step);
            step = step + 0.001f;
        }

        if (step >= 1)
        {
            step = 0;
            startPosEnemyBomber = newPosEnemyBomber;
            newPosEnemyBomber = new Vector3(Random.Range(-15, 15), 0, Random.Range(13, 0));  //координаты пока в пределах конкретных чисел
        }
    }

    void BomberShoot()
    {
        // Первое сырое решение. Отключить логику врага, если игра закончена
        if (GameController.GetInstance().IsGameOver())
        {
            return;
        }

        Instantiate(bombPrefab, gun.transform.position, transform.rotation);
        animShot.SetBool("isShot", true); //запустим анимацю отскока чуть назад при выстреле

        ShootSound();
    }

    public void AnimDefault()
    {   // фунция вызывается по Event на анимационном клипе
        // по окончании анимации происходит возват условий в default
        animShot.SetBool("isShot", false);
    }
}
