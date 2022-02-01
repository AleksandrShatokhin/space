using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSistemBlastWave : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // данный формат потенциально вывести на создание объекта
    void Update()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);

        transform.position = player.position;

        Destroy(gameObject, 5.0f);
    }

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
