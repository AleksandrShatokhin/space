using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Deathable
{
    //Аудио
    private AudioSource audioSource;
    public AudioClip engineSound;
    public AudioClip playerExplosion;
    public AudioClip hitPlayer;

    public AudioClip blasterSound;
    public AudioClip rocketSound;
    public AudioClip waveSound;

    public GameObject laserProjectile, rocketProjectile;
    public GameObject leftGun;
    public GameObject rightGun;

    //Эффекты
    public GameObject particlePlayerExplosion;

    //Анимация
    private Animator animPlayer;

    //Настройки игрока
    //Регулировка угла наклона 
    public float tilt = 1.5f;
    public static float speedPlayer = 18.0f;
    public static bool isDisableShot = false; //для дебаффа отключения стрельбы
    private bool invulnerable; // контроль неуязвимости

    //Арсенал
    private Switcher gunSwitcher;
    private Weapon currentWeapon;
    public List<Weapon> weapons;

    //Здоровье игрока
    public HealtComponent health;
    public HealthBarComponent healthBar;

    public GameObject blastWave;
    private Rigidbody rb;
    private GameObject joystick;

    [SerializeField]
    private bool godMode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunSwitcher = new Switcher();

        animPlayer = GetComponent<Animator>();

        weapons = new List<Weapon>
        {
            new Weapon(Weapons.Laser, laserProjectile, 8, 8, blasterSound, true),
            new Weapon(Weapons.Rocket, rocketProjectile, 8, 10, rocketSound, false)
        };

        currentWeapon = weapons[0];

        //Получить ссылку на компонент здоровья, для удобства использования
        health = GetComponent<HealtComponent>();


        healthBar.SetValue(health.GetHealth());

        joystick = GameObject.Find("JoyStick");

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
        audioSource.clip = engineSound;
        audioSource.Play();
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchProjectile();
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }
        GameObject.Find("Game").GetComponent<TimerController>().TimerForSlowing();
        GameObject.Find("Game").GetComponent<TimerController>().TimerForDisableShot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerShipMove();
        joystick.GetComponent<MoveJoyStick>().CheckTouch(this.tilt, speedPlayer);
    }

    void PlayerShipMove()
    {
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
        GameController.GetInstance().PlaySound(waveSound, 0.7f);
    }


    public void Shoot()
    {
        //Проверить, что стрельба не отключена
        if (isDisableShot)
        {
            return;
        }

        //варианты ведения стрельбы
        //Проверить, что есть снаряды в текущем оружии
        if (currentWeapon.GetBullets() == 0)
        {
            return;
        }

        _ = Instantiate(currentWeapon.GetProjectile(), GetGun().transform.position, Quaternion.identity);

        // Запускаем анимацию стрельбы
        EnterAnimShot();

        audioSource.volume = 0.2f;

        //Звук оружия брать из самого оружия
        audioSource.PlayOneShot(currentWeapon.GetShootingSound());

        //После спауна пули отнимаем один заряд
        currentWeapon.AddBullets(-1);

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
        UpdateHealthBar();
        Destroy(this.gameObject);
    }

    void Deathable.Kill()
    {
        Death();
    }


    public void AddDamage(float dmg)
    {
        if(godMode){
            return;
        }

        if (!invulnerable)
        {
            GameController.GetInstance().PlaySound(hitPlayer);
            GetComponent<HealtComponent>().Change(-dmg);
            UpdateHealthBar();
        }
    }

    public void AddHealth(byte hp)
    {
        GetComponent<HealtComponent>().Change(hp);
        UpdateHealthBar();
    }

    public void SwitchProjectile()
    {
        // здесь воодим переключение снарядов для выстрела

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

    public bool Invulnerable()
    {
        return invulnerable;
    }

    public bool Invulnerable(bool variable)
    {
        invulnerable = variable;
        return invulnerable;
    }


    public Weapon GetWeapon() => currentWeapon;


    void UpdateHealthBar(){ 
            healthBar.SetValue(health.GetHealth());
        
    }
}
