using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoyStick : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform joystick, cursor;

    private Vector3 pointA, pointB;
    private float speed = 18.0f;

    [SerializeField] private bool isTouchScreen = false;
    public Vector3 direction;

    public void CheckTouch(float tilt)
    {
        // необходимые переменные для получения позиции мыши (касания) на экране
        Vector3 posMouse = Input.mousePosition;
        Vector3 posMouseOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(posMouse.x, posMouse.y, 5));

        // необходимые переменные для ограничения движения курсора (стика)
        Vector3 offset = pointB - pointA;

        direction = Vector3.ClampMagnitude(offset, 0.7f);

        // при нажатии кнопки мыши (по экрану первое касание)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pointA = posMouseOnScreen;
            joystick.transform.position = pointA;
        }

        // при удерживании нажатой кнопки мыши (при удержании нажания на экран)
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isTouchScreen = true;
            pointB = posMouseOnScreen;
            cursor.transform.position = pointB;

            // задем допустимую дистанцию движения курсора (стика)
            cursor.transform.position = new Vector3(pointA.x + direction.x, joystick.transform.position.y, pointA.z + direction.z);

            // придаем движение игроку
            player.Translate(direction * speed * Time.deltaTime);
        }
        else // когда мы отпускаем кнопку мыши (перестаем касаться экрана)
        {
            cursor.transform.position = joystick.transform.position;
            isTouchScreen = false;
            joystick.transform.position = pointA;
        }
    }

    public bool IsTouchScreen()
    {
        return isTouchScreen;
    }

    public Vector3 Direction()
    {
        return direction;
    }    
}
