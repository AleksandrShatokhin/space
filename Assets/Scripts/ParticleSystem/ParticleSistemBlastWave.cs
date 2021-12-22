using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSistemBlastWave : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag != "Player")
        {

            if (other.gameObject.layer == 6)
            {
                other.gameObject.GetComponent<EnemyBase>().Death();
            }
        }
    }

    
    
}