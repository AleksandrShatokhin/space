using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundSpawner : MonoBehaviour
{
    Vector3 scaleBounds;

    void Start()
    {
        scaleBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));

        transform.localScale = scaleBounds;
    }
}
