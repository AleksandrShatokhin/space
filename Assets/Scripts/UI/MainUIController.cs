using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
     public TextMeshProUGUI centerText, quantityRocketsText;
    public static bool isPickedUpSlowing = false;
    public static bool isPickedUpShield = false;
    public static bool isPickedUpDisableShot = false;
    public static bool isPickedUpBlastWave = false;
    public static bool isPickedUpRocket = false;
    private Vector3 maxScale = new Vector3(2.0f, 2.0f, 2.0f);
    private Vector3 startTextScale;

    public GameObject childImage;
    private Image imageProjectile;
    public Sprite defaultPr, rocketPr;

    void Start()
    {
        ClearText();

        startTextScale = centerText.transform.localScale;

        // изображение выбранного вида снарядов игроком
        imageProjectile = childImage.GetComponent<Image>();
    }

    void ClearText() // пока сделал метод для очистки текста (использую при старте игры)
    {
        if(centerText.text != null)
        {
            centerText.text = null;
        }
            
    }
    
    void Update()
    {
        ProjectileOnScreen();

        // проверка на получение замедления, 
        // выводим на экран текст по замедлению
        if (isPickedUpSlowing == true)
        IsPickedUpSlowing();

        // проверка на получение щита, 
        // выводим на экран текст по щиту
        if (isPickedUpShield == true)
        IsPickedUpShield();

        // проверка на получение отключения стрельбы, 
        // выводим на экран текст по отключенной стрельбе
        if (isPickedUpDisableShot == true)
        IsPickedUpDisableShot();

        // проверка на получение взрывной волны, 
        // выводим на экран текст по взрывной волне
        if (isPickedUpBlastWave == true)
        IsPickedUpBlastWave();

        // проверка на получение ракет, 
        // выводим на экран текст по количеству подобраных ракет
        if (isPickedUpRocket == true)
        IsPickedUpRocket();
    }

    void IsPickedUpSlowing() //метод для вывода текста на экран при получения замедления
    {
        centerText.text = "Замедление";

        centerText.transform.localScale += new Vector3(2f, 2f, 2f) * Time.deltaTime;
        
        if(centerText.transform.localScale.x > maxScale.x)
        {
            centerText.text = null;
            isPickedUpSlowing = false;
            centerText.transform.localScale = startTextScale;
        }
    }

    void IsPickedUpShield() //метод для вывода текста на экран при получения щита
    {
        centerText.text = "Подобран щит";

        centerText.transform.localScale += new Vector3(2f, 2f, 2f) * Time.deltaTime;
        
        if(centerText.transform.localScale.x > maxScale.x)
        {
            centerText.text = null;
            isPickedUpShield = false;
            centerText.transform.localScale = startTextScale;
        }
    }

    void IsPickedUpDisableShot() //метод для вывода текста на экран при отключения стрельбы
    {
        centerText.text = "Орудия повреждены";

        centerText.transform.localScale += new Vector3(2f, 2f, 2f) * Time.deltaTime;
        
        if(centerText.transform.localScale.x > maxScale.x)
        {
            centerText.text = null;
            isPickedUpDisableShot = false;
            centerText.transform.localScale = startTextScale;
        }
    }

    void IsPickedUpBlastWave() //метод для вывода текста на экран при взрывной волне
    {
        centerText.text = "Взрывная волна";

        centerText.transform.localScale += new Vector3(2f, 2f, 2f) * Time.deltaTime;
        
        if(centerText.transform.localScale.x > maxScale.x)
        {
            centerText.text = null;
            isPickedUpBlastWave = false;
            centerText.transform.localScale = startTextScale;
        }
    }

    public void IsPickedUpRocket() //метод для вывода текста на экран при подборе рокет
    {
        centerText.text = "Добавлены ракеты";

        centerText.transform.localScale += new Vector3(2f, 2f, 2f) * Time.deltaTime;
        
        if(centerText.transform.localScale.x > maxScale.x)
        {
            centerText.text = null;
            isPickedUpRocket = false;
            centerText.transform.localScale = startTextScale;
        }
    }

    void ProjectileOnScreen() // зададим условия выводимого изображения на экран по выбранному виду оружия игроком
    {

        Weapons currentWeapon = GameController.GetInstance().GetPlayer().GetWeapon().id;

        if (currentWeapon == Weapons.Rocket)
        {
            imageProjectile.sprite = rocketPr;
            quantityRocketsText.text = GameController.GetInstance().GetPlayer().GetWeapon().GetBullets().ToString();
        }
        else if(currentWeapon == Weapons.Laser)
        {
            imageProjectile.sprite = defaultPr;
            quantityRocketsText.text = null;
        }
    }
}