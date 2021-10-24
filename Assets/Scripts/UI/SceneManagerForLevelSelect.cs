using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerForLevelSelect : MonoBehaviour
{
    public Button returnBack;

    void Start()
    {
        returnBack.onClick.AddListener(toMainMenu);
    }

    void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
