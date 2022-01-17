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
        // ����������� ���������� ��� ��������� ������� ���� (�������) �� ������
        Vector3 posMouse = Input.mousePosition;
        Vector3 posMouseOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(posMouse.x, posMouse.y, 5));

        // ����������� ���������� ��� ����������� �������� ������� (�����)
        Vector3 offset = pointB - pointA;

        direction = Vector3.ClampMagnitude(offset, 0.7f);

        // ��� ������� ������ ���� (�� ������ ������ �������)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pointA = posMouseOnScreen;
            joystick.transform.position = pointA;
        }

        // ��� ����������� ������� ������ ���� (��� ��������� ������� �� �����)
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isTouchScreen = true;
            pointB = posMouseOnScreen;
            cursor.transform.position = pointB;

            // ����� ���������� ��������� �������� ������� (�����)
            cursor.transform.position = new Vector3(pointA.x + direction.x, joystick.transform.position.y, pointA.z + direction.z);

            // ������� �������� ������
            player.Translate(direction * speed * Time.deltaTime);
        }
        else // ����� �� ��������� ������ ���� (��������� �������� ������)
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
