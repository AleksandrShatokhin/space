using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBox : MonoBehaviour
{

    BoxCollider bc;
    // Start is called before the first frame update
    void Awake()
    {
        bc = GetComponent<BoxCollider>();
    }

    Vector3 GetRandomPointInBounds()
    {
        Vector3 randomPoint;

        randomPoint.x = Random.Range(bc.bounds.min.x, bc.bounds.max.x);
        randomPoint.y = 0.0f;
        randomPoint.z = Random.Range(bc.bounds.min.z, bc.bounds.max.z);

        return randomPoint;
    }

    public GameObject Spawn(GameObject go)
    {
        Quaternion q = new Quaternion(0,180,0,0);
        return Instantiate(go, GetRandomPointInBounds(), q);
    }
}
