using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBase
{
    private Animator animatorCamera;

    private Transform targetLookAtPlayer;
    [SerializeField] private GameObject leftGun, rightGun;

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
    }

    void Update()
    {
        LookAtLeftGuns();
        Movement();
    }
    
    // проверим относительное положение позиций player и boss и после зададим поворот бластеров в сторону player
    private void LookAtLeftGuns()
    {
        Vector3 relativePosForLeftGun = targetLookAtPlayer.position - leftGun.transform.position;
        Vector3 relativePosForRightGun = targetLookAtPlayer.position - rightGun.transform.position;

        Quaternion rotationBossLeftGun = Quaternion.LookRotation(relativePosForLeftGun, Vector3.up);
        Quaternion rotationBossRightGun = Quaternion.LookRotation(relativePosForRightGun, Vector3.up);

        leftGun.transform.rotation = rotationBossLeftGun;
        rightGun.transform.rotation = rotationBossRightGun;
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
