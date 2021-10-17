using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    //Границы перемещения игрока на экране
    Vector3 moveBounds;
    public float playerWidth;
    public float playerHeight;
    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        //Используя ScreenToWorldPoint. размеры окна и позицию по высоте камеры формируем границы движения игрока
        moveBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));
        
        playerWidth = mr.bounds.size.x / 2;
        playerHeight = mr.bounds.size.z / 2;

        moveBounds.x -= playerWidth;
        moveBounds.z -= playerHeight;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Сохранить текущее положение корабля
        Vector3 boundsTransform = transform.position;

        //Ограничиваем возможное перемещение по экрану через функцию Clamp и устанавливаем новое положение
        boundsTransform.x = Mathf.Clamp(transform.position.x, -moveBounds.x, moveBounds.x);
        boundsTransform.z = Mathf.Clamp(transform.position.z, -moveBounds.z, moveBounds.z);
        transform.position = boundsTransform;
    }
}
