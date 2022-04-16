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
    [SerializeField] private GameObject blasterProjectile, bombProjectile;
    private bool isShotLeftBlasterGun, isShotRightBlasterGun;

    private Vector3 startPositionBoss, newPositionBoss;
    private float step = 0.0f;

    private float firstDelay;

    [SerializeField]
    private BossStages stage;
    private int stageCount;
    private float hpOnOneStage;


    void Start()
    {
        stage = BossStages.Stage1;
        stageCount = GetNames(typeof(BossStages)).Length;
        hpOnOneStage = GetComponent<HealtComponent>().GetHealth() / stageCount;

        firstDelay = Random.Range(0.5f, 3);

        // �������� ��������� ������
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
            if (isShotLeftBlasterGun)
            {
                Instantiate(blasterProjectile, spawnLeftBlasterProjectile.transform.position, spawnLeftBlasterProjectile.transform.rotation);
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
            if (isShotRightBlasterGun)
            {
                Instantiate(blasterProjectile, spawnRightBlasterProjectile.transform.position, spawnRightBlasterProjectile.transform.rotation);
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

            if (stage >= BossStages.Stage2)
            {



                if (AngleBetweenBossAndPlayer() > -180 && AngleBetweenBossAndPlayer() < -100)
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
            if (stage >= BossStages.Stage2)
            {

                if (AngleBetweenBossAndPlayer() < 180 && AngleBetweenBossAndPlayer() > 100)
                {
                    Instantiate(bombProjectile, spawnRightBombProjectile.transform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(Mathf.Lerp(1, 3, Random.value));
        }
    }

    //������� �������� ���������� �������
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

        Debug.Log(startHp - hp);
        Debug.Log(hpOnOneStage * (int)stage);

        if (startHp - hp >= hpOnOneStage * (int)stage)
        {
            NextStage();
        }

        Debug.Log(stage);
    }

    public override void AddDamage(float damage)
    {
        base.AddDamage(damage);
        ChangeBossStageProcess();
    }

}
