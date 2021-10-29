using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public GameObject leftGun;
    public GameObject rightGun;

    private GameController game;

    //Регулировка угла наклона 
    public float tilt = 1.5f;
    bool leftUsed = false;

    public GameObject projectile;
    Rigidbody rb;

    public float speedPlayer = 18.0f;
    float forwardInput = 0;
    float horizontalInput = 0;

    private float debuffSlowingActions = 10.0f;
    public bool isDisableShot = false; //для дебаффа отключения стрельбы

    //переменные таймера для дебаффа замедления
    public float timerStatrForSlowing = 10.0f;
    private float timerEndForSlowing = 0.0f;

    //переменные таймера для дебаффа замедления
    public float timerStatrForDisableShot = 10.0f;
    private float timerEndForDisableShot = 0.0f;

    //переменные для баффа щита
    public bool isShield = false;
    public GameObject shield;
    

    // ----------------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void LateUpdate()
    {
        Shoot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Получить данные по вводу от игркоа
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        ////Переместить игрока 
        //Реализовал новую механику перемещения используя RigidBody игрока
        //При использовании напрямую Transofrm получаю странное поведение при поворотах игрока
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Задать вектор движения и умножить на скорость игрока. 
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speedPlayer;


        //Задать вращение при движении по оси X. Угол наклона регулируется через параметр tilt
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }


    private void Shoot()
    {
        if (Input.GetKeyDown("space") && isDisableShot == false)
        {
            _ = Instantiate(projectile, GetGun().transform.position, Quaternion.identity);
        }
        else TimerForDisableShot();
    }

    private GameObject GetGun()
    {
        if (leftUsed)
        {
            leftUsed = false;
            return rightGun;
        } else
        {
            leftUsed = true;
            return leftGun;
        }
    }

    void Update()
    {
        TimerForSlowing();
    }

    //проверим столкновение с обьектом
    void OnCollisionEnter(Collision collision)
    {   

        //проверка столкновения с дебафом замедления
        if (collision.gameObject.tag == "DebuffSlowing")
        {
            if (speedPlayer == 18.0f)
            {
                speedPlayer = debuffSlowingActions;
                Destroy(collision.gameObject);
            }
            else
            {
                timerStatrForSlowing = 10.0f;
                Destroy(collision.gameObject);
            }
        }

        //проверка столкновения с дебафом отключения стрельбы
        if (collision.gameObject.tag == "DebuffDisableShot")
        {
            if (isDisableShot == false)
            {
                isDisableShot = true;
                Destroy(collision.gameObject);
            }
            else 
            {
                timerStatrForDisableShot = 10.0f;
                Destroy(collision.gameObject);
            }
        }

        //проверка на столкновение с баффом щитом
        if (collision.gameObject.tag == "BuffShield")
        {
            isShield = true;
            Destroy(collision.gameObject);
            Instantiate(shield, transform.position, transform.rotation);
        }
    }


    //Правильный путь для смерти игрока. Должны задаваться все необходимые переменные
    //Например, признак конца игры
    public void Death()
    {
        GameController.GetInstance().GameOver();    
        Destroy(this.gameObject);
    }

    void TimerForSlowing() //условный таймер действия дебафа замедления и востановим начальную скорость
    {
        if (speedPlayer == debuffSlowingActions)
        {
            if (timerStatrForSlowing > 0)
            {
                timerStatrForSlowing = timerStatrForSlowing - 0.007f;
                if (timerStatrForSlowing <= timerEndForSlowing)
                speedPlayer = 18.0f;
                if (timerStatrForSlowing < 0)
                {
                    timerStatrForSlowing = 10.0f;
                }
            }
        }
    }

    void TimerForDisableShot() //условный таймер действия дебафа отключения стрельбы
    {
        if (isDisableShot == true)
        {
            if (timerStatrForDisableShot > 0)
            {
                timerStatrForDisableShot = timerStatrForDisableShot - 0.007f;
                if (timerStatrForDisableShot <= timerEndForDisableShot)
                isDisableShot = false;
                if (timerStatrForDisableShot < 0)
                {
                    timerStatrForDisableShot = 10.0f;
                }            
            }
        }
    }
}
