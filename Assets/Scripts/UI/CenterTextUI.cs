using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CenterTextUI : MonoBehaviour
{
    private Vector3 maxScale = new Vector3(2.0f, 2.0f, 2.0f);
    [SerializeField] private GameObject goText;
    private TextMeshProUGUI text;
    private MainUIController uiController;

    private void Start()
    {
        uiController = GameObject.Find("MainUI").GetComponent<MainUIController>();

        text = goText.GetComponent<TextMeshProUGUI>();
        text.text = uiController.GetCurrentText();
    }

    void Update()
    {
        ShowText();
    }

    void ShowText()
    {
        goText.transform.localScale += new Vector3(2f, 2f, 2f) * Time.deltaTime;

        if (goText.transform.localScale.x > maxScale.x)
        {
            Destroy(this.gameObject);
        }
    }
}
