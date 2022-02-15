using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWaveCollider : MonoBehaviour
{
    private float damage = 3.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {

            if (other.gameObject.layer == 6)
            {
                other.gameObject.GetComponent<EnemyBase>().AddDamage(damage);
            }
        }
    }
}
