using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSistemBlastWave : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(other.gameObject);
        }
    }

    
    
}
