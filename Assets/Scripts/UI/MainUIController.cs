using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    private string BonusText;
    [SerializeField] private GameObject WindowWithText;

    public TextMeshProUGUI bulletsText, screenText;

    public GameObject player;
    public GameObject childImage;
    private Image imageProjectile, imageButtonSwitch;
    public Sprite defaultPr, rocketPr;

    public Button shotButton;
    public Button switchWeaponButton;
    [SerializeField] private Button pauseButton;

    void Start()
    {
        // изображение выбранного вида снарядов игроком
        imageProjectile = childImage.GetComponent<Image>();

        shotButton.onClick.AddListener(ShotButtonClick);
        switchWeaponButton.onClick.AddListener(SwitchWeaponButton);
        pauseButton.onClick.AddListener(PauseButton);
    }
    
    void Update()
    {
        ProjectileOnScreen();
    }

    // метод вывода на экран. определяем что подобрали и выводим соответствующее
    public string GetCurrentText(int bonusNumber)
    {
        Instantiate(WindowWithText);

        switch (bonusNumber)
        {
            case (int)BonusNumber.BuffShield:
                {
                    BonusText = "Подобран щит";
                    break;
                }

            case (int)BonusNumber.DebuffDisableShot:
                {
                    BonusText = "Орудия повреждены";
                    break;
                }

            case (int)BonusNumber.BuffBlastWave:
                {
                    BonusText = "Взрывная волна";
                    break;
                }

            case (int)BonusNumber.BuffRocket:
                {
                    BonusText = "Добавлены ракеты";
                    break;
                }

            case (int)BonusNumber.DebuffSlowing:
                {
                    BonusText = "Замедление";
                    break;
                }
        }

        return BonusText;
    }
    
    // метод для чтения, табло с текстом по бонусам получает текст, который нужно показать
    public string GetCurrentText()
    {
        return BonusText;
    }

    void ProjectileOnScreen() // зададим условия выводимого изображения на экран по выбранному виду оружия игроком
    {

        Weapons currentWeapon  = GameController.GetInstance().GetPlayer().GetWeapon().id;
        string bullets = GameController.GetInstance().GetPlayer().GetWeapon().GetBullets().ToString();

        if (currentWeapon == Weapons.Rocket)
        {
            imageProjectile.sprite = rocketPr;
            bulletsText.text = bullets;
        }
        else if(currentWeapon == Weapons.Laser)
        {
            imageProjectile.sprite = defaultPr;
            bulletsText.text = bullets;
        }
    }

    void ShotButtonClick()
    {
        player.GetComponent<PlayerController>().Shoot();
    }

    void SwitchWeaponButton()
    {
        player.GetComponent<PlayerController>().SwitchProjectile();
    }

    void PauseButton()
    {
        GameController.GetInstance().PauseModeOn();
    }
}