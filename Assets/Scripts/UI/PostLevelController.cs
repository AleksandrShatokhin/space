using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class PostLevelController : MonoBehaviour
{

    public Button next;
    public Button restart;
    public Button toMenu;

    public GameObject nextGO;
    public GameObject restartGO;
    public GameObject toMenuGO;


    // Start is called before the first frame update
    void Start()
    {

        next.onClick.AddListener(ToNextLevel);
        restart.onClick.AddListener(Restart);
        toMenu.onClick.AddListener(ToMainMenu);

        //Оставить игру после показа меню
        Time.timeScale = 0;

        if (GameController.GetInstance().IsLevelEnded())
        {
            restartGO.SetActive(false);
        }


        if (GameController.GetInstance().IsGameOver() && !GameController.GetInstance().IsLevelEnded())
        {
            nextGO.SetActive(false);
        }
    }

    void ToNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(Regex.Match(currentScene, @"\d+").Value);

        string nextLevel = $"Level{++levelNumber}";

        //Перед загрузкой новой сцены снять игру с паузы
        Time.timeScale = 1.0f;

        //Сразу задать следующий номер для уровня
        DataStore.SetInt(DataStore.level, GameController.GetInstance().LevelNumber + 1);
        SceneManager.LoadScene(nextLevel);
    }


    void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
