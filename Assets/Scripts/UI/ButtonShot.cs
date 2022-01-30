using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShot : MonoBehaviour
{
    public Button shotButton;

    void Start()
    {
        shotButton.onClick.AddListener(ShotButtonClick);
    }

    void ShotButtonClick()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().Shoot();
    }
}
