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


    //переменные таймера
    private float timerStatr = 10.0f;
    private float timerEnd = 0.0f;
    

    // Start is called before the first frame update
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
        if (Input.GetKeyDown("space"))
        {
            _ = Instantiate(projectile, GetGun().transform.position, Quaternion.identity);
        }
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
        //запустим условный таймер действия дебафа замедления и востановим начальную скорость
        if (speedPlayer == debuffSlowingActions)
        {
            if (timerStatr > 0)
            {
                timerStatr = timerStatr - 0.007f;
                if (timerStatr <= timerEnd)
                speedPlayer = 18.0f;
            }
        }
    }

    //проверим столкновение с обьектом
    void OnCollisionEnter(Collision collision)
    {   
        //проверка столкновения с дебафом замедления
        if (collision.gameObject.tag == "DebuffSlowing")
        {
            speedPlayer = debuffSlowingActions;
            Destroy(collision.gameObject);
        }
    }


    //Правильный путь для смерти игрока. Должны задаваться все необходимые переменные
    //Например, признак конца игры
    public void Death()
    {
        GameController.GetInstance().GameOver();    
        Destroy(this.gameObject);
    }

}
