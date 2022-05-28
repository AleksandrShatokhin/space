using UnityEngine;

public class BlastCollider : PS_RocketBlast
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Enemy)
        {
            other.gameObject.GetComponent<EnemyBase>().AddDamage(damage);
        }
    }
}
