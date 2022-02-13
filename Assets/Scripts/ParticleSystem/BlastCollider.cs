using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCollider : PS_RocketBlast
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) // ���� 6 - ��� enemy
        {
            other.gameObject.GetComponent<EnemyBase>().AddDamage(damage);
        }
    }
}
