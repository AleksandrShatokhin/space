using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Enum;


public enum BossStages
{
    Stage1 = 1,
    Stage2,
    Stage3
}
public class BossController : EnemyBase
{
    private Animator animatorCamera;

    [SerializeField] private GameObject leftBlasterGun, rightBlasterGun;
    [SerializeField] private GameObject spawnLeftBlasterProjectile, spawnRightBlasterProjectile;
    [SerializeField] private GameObject spawnLeftBombProjectile, spawnRightBombProjectile;
    [SerializeField] private GameObject spawnLeftRocketProjectile, spawnRightRocketProjectile;
    [SerializeField] private GameObject blasterProjectile, bombProjectile, rocketProjectile;
    private bool isShotLeftBlasterGun, isShotRightBlasterGun;

    private float firstDelay;

    private Vector3 startPositionBoss, newPositionBoss;
    private float step = 0.0f;

    private Vector3 betweenStagePositionBoss = new Vector3(0.0f, -10.0f, 28.0f);

    [SerializeField]
    private BossStages stage;
    private int stageCount;
    private float hpOnOneStage;

    [SerializeField]
    private float timeToNextStage = 30.0f;


    // Звуки для босса, так как у него отдельная модель с оружиями
    [SerializeField]
    private AudioClip blasterSound;
    [SerializeField]
    private AudioClip bombSound;
    [SerializeField]
    private AudioClip launcherSound;

    [SerializeField]
    private AudioClip bossStartExplosion;

    private bool bossReadyToReturn = false;


    //Стрельба вкл/выкл
    private bool canShoot = true;

    public bool IsReadyToReturn() => bossReadyToReturn;

    void Start()
    {
        // stage = BossStages.Stage1;
        stageCount = GetNames(typeof(BossStages)).Length;
        hpOnOneStage = GetComponent<HealtComponent>().GetHealth() / stageCount;

        firstDelay = Random.Range(0.5f, 3);

        // �������� ��������� ������
        animatorCamera = Camera.main.GetComponent<Animator>();
        GameController.GetInstance().SetBossMode();

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

    private void Movement()
    {
        if (GameController.GetInstance().IsBossMode())
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
        else
        {
            newPositionBoss = betweenStagePositionBoss;

            if (step < 1)
            {
                transform.position = Vector3.Lerp(base.GetCurrentPositionBoss(), newPositionBoss, step);
                step = step + 0.001f;
            }

            if (step >= 1)
            {
                transform.position = newPositionBoss;
            }
        }
    }

    // �������� ������������� ��������� ������� player � boss � ����� ������� ������� ��������� � ������� player
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

    // ������� ������� ����������� �������� �����
    private void BlasterRotationBounds()
    {
        float minAngleLeft = 35.0f, maxAngleLeft = 195.0f;
        float minAngleRight = 160.0f, maxAngleRight = 325.0f;

        // ������������ �������� ����� �����
        if (leftBlasterGun.transform.eulerAngles.y > maxAngleLeft)
        {
            leftBlasterGun.transform.rotation = Quaternion.Euler(leftBlasterGun.transform.rotation.x, maxAngleLeft, leftBlasterGun.transform.rotation.z);
        }

        if (leftBlasterGun.transform.eulerAngles.y < minAngleLeft)
        {
            leftBlasterGun.transform.rotation = Quaternion.Euler(leftBlasterGun.transform.rotation.x, minAngleLeft, leftBlasterGun.transform.rotation.z);
        }

        // ������� ������� ��� ��������
        if (leftBlasterGun.transform.eulerAngles.y < maxAngleLeft && leftBlasterGun.transform.eulerAngles.y > minAngleLeft)
        {
            isShotLeftBlasterGun = true;
        }
        else
        {
            isShotLeftBlasterGun = false;
        }

        // ������������ �������� ������ �����
        if (rightBlasterGun.transform.eulerAngles.y > maxAngleRight)
        {
            rightBlasterGun.transform.rotation = Quaternion.Euler(rightBlasterGun.transform.rotation.x, maxAngleRight, rightBlasterGun.transform.rotation.z);
        }

        if (rightBlasterGun.transform.eulerAngles.y < minAngleRight)
        {
            rightBlasterGun.transform.rotation = Quaternion.Euler(rightBlasterGun.transform.rotation.x, minAngleRight, rightBlasterGun.transform.rotation.z);
        }

        // ������� ������� ��� ��������
        if (rightBlasterGun.transform.eulerAngles.y < maxAngleRight && rightBlasterGun.transform.eulerAngles.y > minAngleRight)
        {
            isShotRightBlasterGun = true;
        }
        else
        {
            isShotRightBlasterGun = false;
        }
    }

    // �������� ������ ��������
    IEnumerator ShotLeftBlasterGun()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {

            if (!canShoot)
            {
                continue;
            }

            if (isShotLeftBlasterGun && GameController.GetInstance().IsBossMode())
            {
                Instantiate(blasterProjectile, spawnLeftBlasterProjectile.transform.position, spawnLeftBlasterProjectile.transform.rotation);
                GameController.GetInstance().PlaySound(blasterSound);
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    // �������� ������� ��������
    IEnumerator ShotRightBlasterGun()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (!canShoot)
            {
                continue;
            }

            if (isShotRightBlasterGun && GameController.GetInstance().IsBossMode())
            {
                Instantiate(blasterProjectile, spawnRightBlasterProjectile.transform.position, spawnRightBlasterProjectile.transform.rotation);
                GameController.GetInstance().PlaySound(blasterSound);
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    // ����� �������� (����� ��� �� �������� �� � ����� �����)
    private void BossShooting()
    {
        // ��������� �������� �� ���������
        StartCoroutine(ShotLeftBlasterGun());
        StartCoroutine(ShotRightBlasterGun());

        // ��������� �������� �������
        StartCoroutine(ShotLeftBombGun());
        StartCoroutine(ShotRightBombGun());

        StartCoroutine(ShootingRocket());

        if (stage == BossStages.Stage2)
        {
            return;
        }

        if (stage == BossStages.Stage3)
        {
            return;
        }

    }

    // �������� ����� �����
    IEnumerator ShotLeftBombGun()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {   

            if(!canShoot){
                continue;
            }

            if (stage >= BossStages.Stage2)
            {
                if (AngleBetweenBossAndPlayer() > -180 && AngleBetweenBossAndPlayer() < -100 && GameController.GetInstance().IsBossMode())
                {
                    Instantiate(bombProjectile, spawnLeftBombProjectile.transform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    // �������� ������ �����
    IEnumerator ShotRightBombGun()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {   

            if(!canShoot){
                continue;
            }

            if (stage >= BossStages.Stage2)
            {
                if (AngleBetweenBossAndPlayer() < 180 && AngleBetweenBossAndPlayer() > 100 && GameController.GetInstance().IsBossMode())
                {
                    Instantiate(bombProjectile, spawnRightBombProjectile.transform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    // собираем левые и правые и рганизовываем порядок стрельбы рокетами по зоне
    IEnumerator ShootingRocket()
    {
        while (true)
        {   
            if(!canShoot){
                continue;
            }

            if (stage >= BossStages.Stage3 && GameController.GetInstance().IsBossMode())
            {
                StartCoroutine(ShotLeftRocketGun());
                StartCoroutine(ShotRightRocketGun());
            }

            yield return new WaitForSeconds(Mathf.Lerp(5, 10, Random.value));
        }
    }

    // цикл стрельбы из левого лаунчера рокетами
    IEnumerator ShotLeftRocketGun()
    {
        int maxCount = 5;
        int currentCount = 0;

        while (currentCount < maxCount)
        {
            GameController.GetInstance().PlaySound(launcherSound, 0.6f);
            currentCount += 1;
            Instantiate(rocketProjectile, spawnLeftRocketProjectile.transform.position, rocketProjectile.transform.rotation);

            yield return new WaitForSeconds(0.3f);
        }
    }

    // цикл стрельбы из правого лаунчера рокетами
    IEnumerator ShotRightRocketGun()
    {
        int maxCount = 5;
        int currentCount = 0;

        while (currentCount < maxCount)
        {
            currentCount += 1;
            Instantiate(rocketProjectile, spawnRightRocketProjectile.transform.position, rocketProjectile.transform.rotation);

            yield return new WaitForSeconds(0.3f);
        }
    }

    //������� �������� ���������� �������


    private void NextStage()
    {
        if (stage == BossStages.Stage3)
        {
            return;
        }

        stage += 1;
    }

    private void ChangeBossStageProcess()
    {
        float hp = GetComponent<HealtComponent>().GetHealth();
        float startHp = GetComponent<HealtComponent>().GetStartHealth();

        if (startHp - hp >= hpOnOneStage * (int)stage)
        {
            NextStage();
            GameController.GetInstance().SetBossModeOff();
            SetCurrentPositionBoss(this.transform.position);
            StartCoroutine(SetBossModeAfterPause());
        }
    }

    public override void AddDamage(float damage)
    {
        base.AddDamage(damage);
        ChangeBossStageProcess();
    }


    protected IEnumerator SetBossModeAfterPause()
    {
        yield return new WaitForSeconds(timeToNextStage);
        bossReadyToReturn = true;

        //Подождать смерти последнего противника из волны
        while (GameController.GetInstance().IsLastEnemyAlive())
        {
            Debug.Log("Ready to return");
            yield return new WaitForSeconds(1.5f);
        }

        GameController.GetInstance().SetBossMode();
        bossReadyToReturn = false;
    }




    public override void Death()
    {   
        canShoot = false;

        if (bossStartExplosion)
        {
            GameController.GetInstance().PlaySound(bossStartExplosion, 8.0f);
        }

        if (explosionEffect)
        {
            DeathSound();
            GameObject expolion = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            Destroy(expolion, 3);
            
        }

        Destroy(gameObject);
    }
}
