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
        randomPoint.y = 0.0f;
        randomPoint.z = Random.Range(bc.bounds.min.z, bc.bounds.max.z);

        return randomPoint;
    }



    void Spawn()
    {
        Quaternion q = new Quaternion(0,180,0,0);


        if (timer < Time.time)
        {
            GameObject obj = Instantiate(objectToSpawn, GetRandomPointInBounds(), q);
            Debug.Log(obj.transform.rotation);
            timer += rate;
        }

    }
}
