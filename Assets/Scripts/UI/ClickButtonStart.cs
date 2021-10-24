using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickButtonStart : MonoBehaviour
{
    public Button firstLavel;
    public Button levelSelect;

    void Start()
    {
        firstLavel.onClick.AddListener(toFirstLavel);
        levelSelect.onClick.AddListener(toLevelSelection);
    }

    void toFirstLavel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void toLevelSelection()
    {
        SceneManager.LoadScene("LevelSelectionMenu");
    }
}
