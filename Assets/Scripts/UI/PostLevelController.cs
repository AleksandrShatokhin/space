using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class PostLevelController : MonoBehaviour
{

    public Button next;
    public Button toMenu;


    // Start is called before the first frame update
    void Start()
    {

        next.onClick.AddListener(ToNextLevel);
        toMenu.onClick.AddListener(ToMainMenu);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ToNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(Regex.Match(currentScene, @"\d+").Value);
        
        string nextLevel = $"Level{++levelNumber}";
        Debug.Log(nextLevel);
        SceneManager.LoadScene(nextLevel);
    }


    void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
