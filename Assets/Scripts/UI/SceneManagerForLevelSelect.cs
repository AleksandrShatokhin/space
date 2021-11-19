using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerForLevelSelect : MonoBehaviour
{
    public Button returnBack;
    public Button level1;
    public Button level2;
    public Button level3;

    void Start()
    {
        returnBack.onClick.AddListener(toMainMenu);
        level1.onClick.AddListener(toLevel1);
        level2.onClick.AddListener(toLevel2);
        level3.onClick.AddListener(toLevel3);
    }

    void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void toLevel1()
    {
        SceneManager.LoadScene("level1");
    }

    void toLevel2()
    {
        SceneManager.LoadScene("level2");
    }

    void toLevel3()
    {
        SceneManager.LoadScene("level3");
    }
}
