using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBase
{
    private Animator animatorCamera;

    [SerializeField] private GameObject leftBlasterGun, rightBlasterGun;
    [SerializeField] private GameObject spawnLeftBlasterProjectile, spawnRightBlasterProjectile;
    [SerializeField] private GameObject spawnLeftBombProjectile, spawnRightBombProjectile;
    [SerializeField] private GameObject blasterProjectile, bombProjectile;
    private bool isShotLeftBlasterGun, isShotRightBlasterGun;

    private Vector3 startPositionBoss, newPositionBoss;
    private float step = 0.0f;

    private float firstDelay;

    void Start()
    {
        firstDelay = Random.Range(0.5f, 3);

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
        LookAtBlasterGuns();
        Movement();
    }

    // проверим относительное положение позиций player и boss и после зададим поворот бластеров в сторону player
    private void LookAtBlasterGuns()
    {
        Vector3 relativePosForLeftGun = targetLookAtPlayer.position - leftBlasterGun.transform.position;
        Vector3 relativePosForRightGun = targetLookAtPlayer.position - rightBlasterGun.transform.position;

        Quaternion rotationBossLeftGun = Quaternion.LookRotation(relativePosForLeftGun, Vector3.up);
        Quaternion rotationBossRightGun = Quaternion.LookRotation(relativePosForRightGun, Vector3.up);

        leftBlasterGun.transform.rotation = rotationBossLeftGun;
        rightBlasterGun.transform.rotation = rotationBossRightGun;

        BlasterRotationBounds();
    }

    // зададим условия ограничений движения пушек
    private void BlasterRotationBounds()
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
        // запускаем стрельбу из бластеров
        StartCoroutine(ShotLeftBlasterGun());
        StartCoroutine(ShotRightBlasterGun());

        // запускаем стрельбу бомбами
        StartCoroutine(ShotLeftBombGun());
        StartCoroutine(ShotRightBombGun());
    }

    // корутина левой бомбы
    IEnumerator ShotLeftBombGun()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (AngleBetweenBossAndPlayer() > -180 && AngleBetweenBossAndPlayer() < -100)
            {
                Instantiate(bombProjectile, spawnLeftBombProjectile.transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    // корутина правой бомбы
    IEnumerator ShotRightBombGun()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (AngleBetweenBossAndPlayer() < 180 && AngleBetweenBossAndPlayer() > 100)
            {
                Instantiate(bombProjectile, spawnRightBombProjectile.transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
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
