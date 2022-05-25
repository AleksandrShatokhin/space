using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMode : MonoBehaviour
{
    [SerializeField] private Button continueButton, restartButton, exitButton;

    private void Start()
    {
        continueButton.onClick.AddListener(ToContinue);
        restartButton.onClick.AddListener(ToRestart);
        exitButton.onClick.AddListener(ToMainMenu);
    }

    void ToContinue()
    {
        GameController.GetInstance().PauseModeOff();
    }

    void ToRestart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
