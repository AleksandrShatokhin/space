using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoyStick : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform joystick, cursor;

    private Vector2 pointA, pointB;
    private float speed = 18.0f;

    [SerializeField] private bool isTouchScreen = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }
    }
}
