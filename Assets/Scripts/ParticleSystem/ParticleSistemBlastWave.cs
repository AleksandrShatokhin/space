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

        Destroy(gameObject, 2.0f);
        Destroy(colBlastWave, 1.35f);
    }

    private void FixedUpdate()
    {
        if (!colBlastWave)
        {
            return;
        }

        // зададим расширение коллайдера объекта
        if (colBlastWave.radius < 7.0f)
        {
            colBlastWave.radius += 0.1f;
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
