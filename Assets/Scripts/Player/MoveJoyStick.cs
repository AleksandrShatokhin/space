using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJoyStick : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform joystick, cursor;

    private Vector3 pointA, pointB;

    [SerializeField] private bool isTouchScreen = false;
    public Vector3 direction;

    public void CheckTouch(float tilt, float speed)
    {
        // необходимые переменные для получения позиции мыши (касания) на экране
        Vector3 posMouse = Input.mousePosition;
        Vector3 posMouseOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(posMouse.x, posMouse.y, 5));

        // необходимые переменные для ограничения движения курсора (стика)
        Vector3 offset = pointB - pointA;

        direction = Vector3.ClampMagnitude(offset, 0.6f);

        // при нажатии кнопки мыши (по экрану первое касание)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pointA = posMouseOnScreen;
            joystick.transform.position = pointA;

            // делаем джойстик видимым
            joystick.GetComponent<Image>().enabled = true;
            cursor.GetComponent<Image>().enabled = true;
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
            //player.Translate(direction * speed * Time.deltaTime);
            player.GetComponent<Rigidbody>().velocity = direction * speed;
            player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -tilt);
        }
        else // когда мы отпускаем кнопку мыши (перестаем касаться экрана)
        {
            cursor.transform.position = joystick.transform.position;
            isTouchScreen = false;
            joystick.transform.position = pointA;

            // делаем джойстик невидимым
            joystick.GetComponent<Image>().enabled = false;
            cursor.GetComponent<Image>().enabled = false;
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
