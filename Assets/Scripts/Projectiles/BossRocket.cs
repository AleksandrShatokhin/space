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
        randomPoint = new Vector3(Random.Range(-40, 40), 0, Random.Range(-20, 10));
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

    // создаем взрыв рокеты
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
            blast = Instantiate(blastBossRocket, transform.position, Quaternion.identity);
            indicator.GetComponent<IndicatorForBossRocket>().DestroyIndicator();
        }

        return blast;
    }

    // создаем индикатор прицеливания рокеты
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
}
