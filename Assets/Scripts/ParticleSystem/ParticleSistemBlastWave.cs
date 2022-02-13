using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSistemBlastWave : MonoBehaviour
{
    [SerializeField] private SphereCollider colBlastWave;


    // данный формат потенциально вывести на создание объекта
    void Update()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);

        Destroy(gameObject, 3.0f);
    }

    private void FixedUpdate()
    {
        // зададим расширение коллайдера объекта
        if (colBlastWave.radius < 9.9)
        {
            colBlastWave.radius += 0.065f;
        }
    }

    //void OnParticleCollision(GameObject other)
    //{
    //    if (other.gameObject.tag != "Player")
    //    {

    //        if (other.gameObject.layer == 6)
    //        {
    //            other.gameObject.GetComponent<EnemyBase>().Death();
    //        }
    //    }
    //}

}
