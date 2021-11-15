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
    //bool leftUsed = false;

    public GameObject currentProjectile;
    Rigidbody rb;

    public float speedPlayer = 18.0f;
    float forwardInput = 0;
    float horizontalInput = 0;

    public bool isDisableShot = false; //для дебаффа отключения стрельбы

    public bool isShield = false; // переменные для баффа щита
    public GameObject shield;



    private Switcher gunSwitcher;
    private Weapon currentWeapon;

    public List<Weapon> weapons;
    

    // ----------------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunSwitcher = new Switcher();

        //weapons = new List<Weapon>
        //{
        //    new Weapon(currentProjectile, 5, 5, true)
        //};

        currentWeapon = new Weapon(currentProjectile, 5, 5, true);
        
    }


    private void Awake()
    {
        StartCoroutine(AddBullets());
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
            return;
        }


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
        else
        {
            GameObject.Find("Game").GetComponent<TimerController>().TimerForDisableShot();
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
}
