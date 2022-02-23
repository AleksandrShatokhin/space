using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher
{
    //Основное свойство переключателя
    private bool state = false;

    //Возращается текущее состояние и меняется на новое
    //Для анализа на внешей стороне 
    public bool GetState()
    {
        bool oldState = state;
        state = !state;
        return oldState;
    }
}
