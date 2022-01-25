using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Deathable
{


    public AudioClip shotSound;
    public AudioClip engineSound;
    public AudioClip playerExplosion;
    public AudioClip hitPlayer;


    public GameObject leftGun;
    public GameObject rightGun;
    public GameObject particlePlayerExplosion;

    

    private GameController game;

    private Animator animPlayer;

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
    public HealthBarComponent healthBar;

    private GameObject joystick;

    private Transform spawnPosBlastWave;
    public GameObject blastWave;

    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunSwitcher = new Switcher();

        animPlayer = GetComponent<Animator>();

        weapons = new List<Weapon>
        {
            new Weapon(Weapons.Laser, currentProjectile, 5, 5, true),
            new Weapon(Weapons.Rocket, rocketProjectile, 5, 5, false)
        };

        //currentWeapon = new Weapon(currentProjectile, 5, 5, true);
        currentWeapon = weapons[0];

        //Получить ссылку на компонент здоровья, для удобства использования
        health = GetComponent<HealtComponent>();
        //health.SetDeathable(this);

        isRocket = false;
        quantityRockets = 5;


        healthBar.SetValue(health.GetHealth());

        joystick = GameObject.Find("JoyStick");

        spawnPosBlastWave = gameObject.transform.GetChild(2);


        audioSource = GetComponent<AudioSource>();

        audioSource.volume = 0.1f;
        audioSource.clip = engineSound;
        audioSource.Play();
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
        PlayerShipMove();

        joystick.GetComponent<MoveJoyStick>().CheckTouch(this.tilt, speedPlayer);
    }

    void PlayerShipMove()
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

    public void StartBlastWave() // метод запуска взрывной волны (вызывается инфлюенсом)
    {
        Instantiate(blastWave, transform.position, blastWave.transform.rotation);
    }


    public void Shoot()
    {
        //Проверить, что стрельба не отключена
        if (isDisableShot)    
        {
            GameObject.Find("Game").GetComponent<TimerController>().TimerForDisableShot();
            return;
        }    

        //варианты ведения стрельбы
        //Проверить, что есть снаряды в текущем оружии
        if (currentWeapon.GetBullets() == 0)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            _ = Instantiate(currentWeapon.GetProjectile(), GetGun().transform.position, Quaternion.identity);

            // Запускаем анимацию стрельбы
            EnterAnimShot();

            audioSource.volume = 0.2f;

            audioSource.PlayOneShot(shotSound);

            //После спауна пули отнимаем один заряд
            currentWeapon.AddBullets(-1);
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
        GameObject explosion = Instantiate(particlePlayerExplosion, this.transform.position, Quaternion.identity);
        Destroy(explosion, 3);
        GameController.GetInstance().PlaySound(playerExplosion, 2.0f);
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

            if (currentWeapon.GettingBulletsByTime())
            {
                currentWeapon.AddBullets(1);
            }
        }
    }

    void Deathable.Kill()
    {
        
        Death();
    }


    public void AddDamage(float dmg)
    {
        GameController.GetInstance().PlaySound(hitPlayer);
        GetComponent<HealtComponent>().Change(-dmg);
        //healthBar.SetValue(health.GetHealth());
    }

    public void AddHealth(byte hp)
    {
        GetComponent<HealtComponent>().Change(hp);
    }

    void SwitchProjectile()
    {
        // здесь воодим переключение снарядов для выстрела
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            int nextWeaponIndex = (int)currentWeapon.id + 1;

            if (nextWeaponIndex >= weapons.Count)
            {
                currentWeapon = weapons[0];
            }
            else
            {
                currentWeapon = weapons[nextWeaponIndex];
            }

        }

    }

    // функции на выход из анимаций стрельбы пушек (Event на анимации)
    public void ExitAnimShotLG()
    {
        animPlayer.SetBool("isShotLG", false);
    }

    public void ExitAnimShotRG()
    {
        animPlayer.SetBool("isShotRG", false);
    }

    // функция на запуск анимации стрельбы левой и правой пушек
    // сейчас почему то работает странно (изменить LG и RG)
    void EnterAnimShot()
    {
        if (gunSwitcher.GetState() == leftGun)
        {
            animPlayer.SetBool("isShotRG", true);
        }

        if (gunSwitcher.GetState() == rightGun)
        {
            animPlayer.SetBool("isShotLG", true);
        }
    }


    public Weapon GetWeapon() => currentWeapon;
}
