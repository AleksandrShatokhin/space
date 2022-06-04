using UnityEngine;

public class BlastCollider : PS_RocketBlast
{
    private bool isDamageBoss = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Enemy)
        {
            if (other.gameObject.tag == "Boss")
            {
                if (!isDamageBoss)
                {
                    other.gameObject.GetComponent<EnemyBase>().AddDamage(damage);
                    isDamageBoss = true;
                }
            }
            else
            {
                other.gameObject.GetComponent<EnemyBase>().AddDamage(damage);
            }
        }
    }
}
