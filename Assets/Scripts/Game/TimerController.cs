using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private float SlowingActions = 10.0f;
    public float timerStatrForSlowing = 10.0f;
    public bool isSlowing = false;
    public float timerStatrForDisableShot = 10.0f;
    public bool isDisable = false;
    private float timerEnd = 0.0f;

    void Start()
    {
        float speed = GameObject.Find("Player").GetComponent<PlayerController>().speedPlayer;
    }

    public void TimerForSlowing() //условный таймер действия дебафа замедления и востановим начальную скорость
    {
        float speed = GameObject.Find("Player").GetComponent<PlayerController>().speedPlayer;

        if (speed == SlowingActions && isSlowing == true)
        {
            if (timerStatrForSlowing > timerEnd)
            {
                timerStatrForSlowing = timerStatrForSlowing - 0.007f;
                if (timerStatrForSlowing <= timerEnd)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().speedPlayer = 18.0f;
                    timerStatrForSlowing = 10.0f;
                    isSlowing = false;
                }
            }
        }
    }

    public void TimerForDisableShot() //условный таймер действия дебафа отключения стрельбы
    {
        bool disable = GameObject.Find("Player").GetComponent<PlayerController>().isDisableShot;

        if (disable == true && isDisable == true)
        {
            if (timerStatrForDisableShot > timerEnd)
            {
                timerStatrForDisableShot = timerStatrForDisableShot - 0.007f;
                if (timerStatrForDisableShot <= timerEnd)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().isDisableShot = false;
                    timerStatrForDisableShot = 10.0f;   
                    isDisable = false; 
                }
            }
        }
    }
}
