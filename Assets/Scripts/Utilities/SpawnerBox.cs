using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBox : MonoBehaviour
{
    float timer;
    float rate = 3.0f;
    public GameObject objectToSpawn;

    BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        timer = Time.time + rate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Spawn();
    }



    Vector3 GetRandomPointInBounds()
    {
        Vector3 randomPoint;

        randomPoint.x = Random.Range(bc.bounds.min.x, bc.bounds.max.x);
        randomPoint.y = Random.Range(bc.bounds.min.y, bc.bounds.max.y);
        randomPoint.z = Random.Range(bc.bounds.min.z, bc.bounds.max.z);

        return randomPoint;
    }



    void Spawn()
    {
        Quaternion q = Quaternion.identity;
        q.y = -180;

        if (timer < Time.time)
        {
            Instantiate(objectToSpawn, GetRandomPointInBounds(), q);
            timer += rate;
        }

    }
}
