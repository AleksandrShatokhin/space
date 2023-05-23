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
        isStopPosition = false;

        //Получить ссылку на босса
        BoxCollider bossCollider = GameController.GetInstance().GetBoss().GetComponent<BoxCollider>();

        // randomPoint = RandomPoint();

        //Если точка еще не сгенерирована или попала в ограничение на боссе, то делаем новую генерацию 
        while(CheckPointInBoxCollider(randomPoint, bossCollider) || randomPoint == new Vector3())
        {
            randomPoint = RandomPoint();
        }
    }

    override protected void FixedUpdate()
    {
        base.BossRocket();

        if (transform.position.y <= destroyRocketPosition.y && isStopPosition)
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
            isStopPosition = true;
        }
    }

    // ������� ��������� ����� � ���� ������ ������
    private Vector3 RandomPoint()
    {
        Vector3 centerPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.y));
        Vector3 screenBounds = GameController.GetInstance().ScreenBound();

        float valueX = Random.Range(centerPoint.x - Random.Range(0, screenBounds.x), centerPoint.x + Random.Range(0, screenBounds.x));
        float valueZ = Random.Range(centerPoint.z - Random.Range(0, screenBounds.z), centerPoint.z + Random.Range(0, screenBounds.z));

        return new Vector3(valueX, 0.0f, valueZ);
    }

    private bool CheckPointInBoxCollider(Vector3 point, BoxCollider box){

        //По сути у нас есть прямоугольник, который мы хотим исключить
        //Поэтому мы просто проверяем координаты X и Z для точки и коллайдера-ограничителя
        if(box.bounds.min.x < point.x && box.bounds.max.x > point.x && 
           box.bounds.min.z < point.z && box.bounds.max.z > point.z)
           {
               return true;
           }
           else 
           {
               return false;
           }

    }

}
