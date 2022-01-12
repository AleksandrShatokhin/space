using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundEmemyShip : MonoBehaviour
{
    Vector3 moveBounds;
    public float enemyWidth;
    public MeshRenderer mr;

    void Start()
    {
        moveBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));

        enemyWidth = mr.bounds.size.x / 2;

        moveBounds.x -= enemyWidth;
    }

    void LateUpdate()
    {
        Vector3 boundsTransform = transform.position;

        boundsTransform.x = Mathf.Clamp(transform.position.x, -moveBounds.x, moveBounds.x);
        transform.position = boundsTransform;
    }
}
