using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class LocalizationUtility : MonoBehaviour
{

    //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ru");

    //По умолчанию таблица для строк - Strings. Смотреть в Window->Asset management->Localization tables
    static public string GetLocString(string key, string tableName = "Strings")
    {
        return new LocalizedString(tableName, key).GetLocalizedString();
    }
}
