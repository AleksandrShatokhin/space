using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMover : MonoBehaviour
{

    [SerializeField]
    private int side = 1;

    [SerializeField]
    private float speed;

    void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.forward * speed * side * Time.deltaTime);
    }

}
