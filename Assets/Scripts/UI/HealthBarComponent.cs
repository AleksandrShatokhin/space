using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour
{

    public Slider slider;


    public void SetValue(float health)
    {
        slider.SetValueWithoutNotify(health);
    }
}
