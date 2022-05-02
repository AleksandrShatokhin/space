using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocket : Projectile
{
    private Vector3 randomPoint;
    private Vector3 destroyRocketPosition = new Vector3(0, 0, 0);

    [SerializeField] private GameObject blastBossRocket;
    [SerializeField] private GameObject indicatorTarget;
    private GameObject indicator;

    private bool isStopPosition;

    void Start()
    {
        RandomPoint();
    }

    override protected void FixedUpdate()
    {
        base.BossRocket();

        if (transform.position.y <= destroyRocketPosition.y)
        {
            BlastBossRocket();
            Destroy(this.gameObject);
        }
    }

    // ������� ����� ������
    private GameObject BlastBossRocket()
    {
        GameObject blast;
        bool isBlast = false;

        if (isBlast)
        {
            return null;
        }
        else 
        {
            isBlast = true;
            GameController.GetInstance().PlaySound(explosionSound, 0.6f);
            blast = Instantiate(blastBossRocket, transform.position, Quaternion.identity);
            indicator.GetComponent<IndicatorForBossRocket>().DestroyIndicator();
        }

        return blast;
    }

    // ������� ��������� ������������ ������
    private GameObject Indicator()
    {
        bool isIndicator = false;

        if (isIndicator)
        {
            return null;
        }
        else
        {
            isIndicator = true;
            indicator = Instantiate(indicatorTarget, randomPoint, indicatorTarget.transform.rotation);
        }

        return indicator;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StoperBossRocket")
        {
            transform.LookAt(randomPoint);
            Indicator();
        }
    }

    // ������� ��������� ����� � ���� ������ ������
    private Vector3 RandomPoint()
    {
        Vector3 centerPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.y));
        Vector3 screenBounds = GameController.GetInstance().ScreenBound();

        float valueX = Random.Range(centerPoint.x - Random.Range(0, screenBounds.x), centerPoint.x + Random.Range(0, screenBounds.x));
        float valueZ = Random.Range(centerPoint.z - Random.Range(0, screenBounds.z), centerPoint.z + Random.Range(0, screenBounds.z));

        randomPoint = new Vector3(valueX, 0.0f, valueZ);

        return randomPoint = new Vector3(valueX, 0.0f, valueZ);
    }
}
