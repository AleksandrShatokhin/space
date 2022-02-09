using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSistemBlastWave : MonoBehaviour
{
    [SerializeField] private SphereCollider colBlastWave;

    private void Start()
    {
        //colBlastWave = GetComponentInChildren<SphereCollider>();
        colBlastWave.enabled = false;

        StartCoroutine(CorBlastWave());
    }

    IEnumerator CorBlastWave()
    {
        yield return new WaitForSeconds(2.0f);
        colBlastWave.enabled = true;
    }

    // данный формат потенциально вывести на создание объекта
    void Update()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);

        Destroy(gameObject, 3.0f);
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
