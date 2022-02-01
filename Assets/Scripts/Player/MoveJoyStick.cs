using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJoyStick : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform joystick, cursor;
    
    private float deviationCursor = 0.7f;
    
    private Vector3 pointA, pointB;

    [SerializeField] private bool isTouchScreen = false;
    public Vector3 direction;

    void Start()
    {
        FindOutSize();
    }

    private void FindOutSize()
    {
        if (Screen.height == 1280 && Screen.width == 720)
        {
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 400);
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
        }
        else
        if (Screen.height == 1920 && Screen.width == 1080)
        {
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 550);
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        }
        else
        if (Screen.height == 2160 && Screen.width == 1080)
        {
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 600);
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        }
        else
        if (Screen.height == 2560 && Screen.width == 1440)
        {
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 700);
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);
        }
        else
        if (Screen.height == 2960 && Screen.width == 1440)
        {
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 800);
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);
        }
        else
        {
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            cursor.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        }
    }

    public void CheckTouch(float tilt, float speed)
    {
        // необходимые переменные для получения позиции мыши (касания) на экране
        Vector3 posMouse = Input.mousePosition;
        Vector3 posMouseOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(posMouse.x, posMouse.y, 5));

        // необходимые переменные для ограничения движения курсора (стика)
        Vector3 offset = pointB - pointA;

        direction = Vector3.ClampMagnitude(offset, deviationCursor);

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
