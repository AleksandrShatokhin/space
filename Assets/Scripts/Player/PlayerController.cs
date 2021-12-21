using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Deathable
{


    public GameObject leftGun;
    public GameObject rightGun;

    private GameController game;

    //Регулировка угла наклона 
    public float tilt = 1.5f;
    //bool leftUsed = false;

    public GameObject currentProjectile, rocketProjectile;
    Rigidbody rb;

    public static float speedPlayer = 18.0f;
    float forwardInput = 0;
    float horizontalInput = 0;

    public static bool isDisableShot = false; //для дебаффа отключения стрельбы

    public static bool isShield = false; // переменные для баффа щита
    public GameObject shield;

    public static bool isRocket;
    public static int quantityRockets;


    private Switcher gunSwitcher;
    private Weapon currentWeapon;

    public List<Weapon> weapons;


    public HealtComponent health;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunSwitcher = new Switcher();

        //weapons = new List<Weapon>
        //{
        //    new Weapon(currentProjectile, 5, 5, true)
        //};

        currentWeapon = new Weapon(currentProjectile, 5, 5, true);


        //Получить ссылку на компонент здоровья, для удобства использования
        health = GetComponent<HealtComponent>();
        //health.SetDeathable(this);
        
        isRocket = false;
        quantityRockets = 5;
    }


    private void Awake()
    {
        StartCoroutine(AddBullets());
    }

    void Update()
    {
        SwitchProjectile();
    }

    private void LateUpdate()
    {
        Shoot();
        GameObject.Find("Game").GetComponent<TimerController>().TimerForSlowing();
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
        //Проверить, что стрельба не отключена
        if (isDisableShot)
        {
            GameObject.Find("Game").GetComponent<TimerController>().TimerForDisableShot();
            return;
        }

        //варианты ведения стрельбы
        switch (isRocket)
        {
            case true: //если выбраны ракеты
                if (Input.GetKeyDown("space") && quantityRockets > 0)
                {
                    Instantiate(rocketProjectile, GetGun().transform.position, transform.rotation);
                    quantityRockets -= 1;
                }
            break;
            case false: //если выбраны стандартные снаряды
                //Проверить, что есть снаряды в текущем оружии
                if (currentWeapon.GetBullets() == 0)
                {
                    return;
                }

                if (Input.GetKeyDown("space"))
                {
                    _ = Instantiate(currentWeapon.GetProjectile(), GetGun().transform.position, Quaternion.identity);

                    //После спауна пули отнимаем один заряд
                    currentWeapon.AddBullets(-1);
                }
            break;
        }
        
    }

    private GameObject GetGun()
    {
        return gunSwitcher.GetState() ? leftGun : rightGun;
    }

    //Правильный путь для смерти игрока. Должны задаваться все необходимые переменные
    //Например, признак конца игры
    public void Death()
    {
        GameController.GetInstance().GameOver();    
        Destroy(this.gameObject);
    }


    // Для теста реализовать в Player'е
    //По идее надо вынести в  GameContoller
    IEnumerator AddBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            currentWeapon.AddBullets(1);
        }
    }

    void Deathable.Kill()
    {
        Death();
    }


    public void AddDamage(float dmg)
    {
        GetComponent<HealtComponent>().Change(-dmg);
    }

    void SwitchProjectile()
    {
        // здесь воодим переключение снарядов для выстрела
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isRocket == false)
            {
                isRocket = true;
            }
            else
            {
                isRocket = false;
            }
        }

        // ограничение по количеству ракет
        if (quantityRockets > 20)
        {
            quantityRockets = 20;
        }
            
    }
}
