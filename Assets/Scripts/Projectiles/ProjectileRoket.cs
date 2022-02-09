using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRoket : Projectile
{
    public GameObject blastRocket;
    private Collider colPlayer;

    private void Start()
    {
        // ������� ������������� ������������ � �������, ���� �� ���������� ������ �� ������
        colPlayer = GameObject.Find("Player").GetComponent<Collider>();
        Physics.IgnoreCollision(colPlayer, this.GetComponent<Collider>(), true);
    }

    override protected void FixedUpdate()
    {
        base.Rocket();
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(blastRocket, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
    }
}
