using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBase
{
    private Animator animatorCamera;

    private Transform targetLookAtPlayer;
    [SerializeField] private GameObject leftBlasterGun, rightBlasterGun;
    [SerializeField] private GameObject spawnLeftBlasterProjectile, spawnRightBlasterProjectile;
    [SerializeField] private GameObject blasterProjectile;
    private bool isShotLeftBlasterGun, isShotRightBlasterGun;

    private Vector3 startPositionBoss, newPositionBoss;
    private float step = 0.0f;

    void Start()
    {
        // вызываем отдаление камеры
        animatorCamera = Camera.main.GetComponent<Animator>();
        animatorCamera.SetBool("isPlus", true);

        targetLookAtPlayer = GameObject.Find("Player").GetComponent<Transform>();

        startPositionBoss = transform.position;
        newPositionBoss = new Vector3(Random.Range(-10, 10), 0, Random.Range(6, 10));

        BossShooting();
    }

    void Update()
    {
        LookAtGuns();
        Movement();
    }
    
    // проверим относительное положение позиций player и boss и после зададим поворот бластеров в сторону player
    private void LookAtGuns()
    {
        Vector3 relativePosForLeftGun = targetLookAtPlayer.position - leftBlasterGun.transform.position;
        Vector3 relativePosForRightGun = targetLookAtPlayer.position - rightBlasterGun.transform.position;

        Quaternion rotationBossLeftGun = Quaternion.LookRotation(relativePosForLeftGun, Vector3.up);
        Quaternion rotationBossRightGun = Quaternion.LookRotation(relativePosForRightGun, Vector3.up);

        leftBlasterGun.transform.rotation = rotationBossLeftGun;
        rightBlasterGun.transform.rotation = rotationBossRightGun;

        AngleBounds();
    }

    // зададим условия ограничений движения пушек
    private void AngleBounds()
    {
        float minAngleLeft = 35.0f, maxAngleLeft = 195.0f;
        float minAngleRight = 160.0f, maxAngleRight = 325.0f;

        // ограничиваем движение левой пушки
        if (leftBlasterGun.transform.eulerAngles.y > maxAngleLeft)
        {
            leftBlasterGun.transform.rotation = Quaternion.Euler(leftBlasterGun.transform.rotation.x, maxAngleLeft, leftBlasterGun.transform.rotation.z);
        }

        if (leftBlasterGun.transform.eulerAngles.y < minAngleLeft)
        {
            leftBlasterGun.transform.rotation = Quaternion.Euler(leftBlasterGun.transform.rotation.x, minAngleLeft, leftBlasterGun.transform.rotation.z);
        }

        // добавим условие для стрельбы
        if (leftBlasterGun.transform.eulerAngles.y < maxAngleLeft && leftBlasterGun.transform.eulerAngles.y > minAngleLeft)
        {
            isShotLeftBlasterGun = true;
        }
        else
        {
            isShotLeftBlasterGun = false;
        }

        // ограничиваем движение правой пушки
        if (rightBlasterGun.transform.eulerAngles.y > maxAngleRight)
        {
            rightBlasterGun.transform.rotation = Quaternion.Euler(rightBlasterGun.transform.rotation.x, maxAngleRight, rightBlasterGun.transform.rotation.z);
        }

        if (rightBlasterGun.transform.eulerAngles.y < minAngleRight)
        {
            rightBlasterGun.transform.rotation = Quaternion.Euler(rightBlasterGun.transform.rotation.x, minAngleRight, rightBlasterGun.transform.rotation.z);
        }

        // добавим условие для стрельбы
        if (rightBlasterGun.transform.eulerAngles.y < maxAngleRight && rightBlasterGun.transform.eulerAngles.y > minAngleRight)
        {
            isShotRightBlasterGun = true;
        }
        else
        {
            isShotRightBlasterGun = false;
        }
    }

    // корутина левого бластера
    IEnumerator ShotLeftBlasterGun()
    {
        float firstDelay = Random.Range(1, 3);

        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (isShotLeftBlasterGun)
            {
                Instantiate(blasterProjectile, spawnLeftBlasterProjectile.transform.position, spawnLeftBlasterProjectile.transform.rotation);
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    // корутина правого бластера
    IEnumerator ShotRightBlasterGun()
    {
        float firstDelay = Random.Range(1, 3);

        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (isShotRightBlasterGun)
            {
                Instantiate(blasterProjectile, spawnRightBlasterProjectile.transform.position, spawnRightBlasterProjectile.transform.rotation);
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    // метод стрельбы (думаю как то собирать всё в одном месте)
    private void BossShooting()
    {
        StartCoroutine(ShotLeftBlasterGun());
        StartCoroutine(ShotRightBlasterGun());
    }

    //зададим движение вражеского корабля
    private void Movement()
    { 
        if (step < 1)
        {
            transform.position = Vector3.Lerp(startPositionBoss, newPositionBoss, step);
            step = step + 0.001f;
        }

        if (step >= 1)
        {
            step = 0;
            startPositionBoss = newPositionBoss;
            newPositionBoss = new Vector3(Random.Range(-10, 10), 0, Random.Range(6, 10));
        }
    }
}
