using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //Если это враг, то мы должны подсчитать как уничтоженного
        if(collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<EnemyBase>().Death();
        }

        //Удалить все объекты, которые попадают в коллектор
        Destroy(collision.gameObject);
    }
}
