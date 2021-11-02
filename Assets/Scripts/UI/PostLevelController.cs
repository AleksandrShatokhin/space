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


    // Start is called before the first frame update
    void Start()
    {

        next.onClick.AddListener(ToNextLevel);
        restart.onClick.AddListener(Restart);
        toMenu.onClick.AddListener(ToMainMenu);
        Time.timeScale = 0;
    }

    void ToNextLevel()
    {
        if (GameController.GetInstance().IsLevelEnded())
        {
            string currentScene = SceneManager.GetActiveScene().name;
            int levelNumber = int.Parse(Regex.Match(currentScene, @"\d+").Value);

            string nextLevel = $"Level{++levelNumber}";
            Debug.Log(nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
    }


    void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Restart()
    {
        if (GameController.GetInstance().IsGameOver() || GameController.GetInstance().IsLevelEnded())
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
