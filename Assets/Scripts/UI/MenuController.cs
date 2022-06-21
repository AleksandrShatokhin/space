using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject continueGO;
    [SerializeField]
    private GameObject newGameGO;
    [SerializeField]
    private GameObject exitGO;

    [SerializeField]
    private Button continueBtn;
    [SerializeField]
    private Button newGameBtn;
    [SerializeField]
    private Button exitBtn;

    private void Start()
    {
        continueBtn.onClick.AddListener(Continue);
        newGameBtn.onClick.AddListener(StartNewGame);
        exitBtn.onClick.AddListener(ExitGame);
    }
    void Continue()
    {

    }

    void StartNewGame()
    {
        if (SaveGameData.TutorialComlete())
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }


    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
