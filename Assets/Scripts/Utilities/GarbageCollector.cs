using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Удалить все объекты, которые попадают в коллектор
        Destroy(collision.gameObject);
    }
}
