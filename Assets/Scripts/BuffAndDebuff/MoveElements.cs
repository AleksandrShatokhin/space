using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElements : MonoBehaviour
{
    private float speed = 5.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
