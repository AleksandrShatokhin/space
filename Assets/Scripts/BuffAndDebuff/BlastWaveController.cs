using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWaveController : MonoBehaviour
{
    public ParticleSystem blastWave;
    
    void Start()
    {
        blastWave = GameObject.Find("Player").GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            blastWave.Play();
            Destroy(gameObject);
        }
    }
}
//GetComponentInChildren