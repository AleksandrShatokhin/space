
using UnityEngine;

public class DataStore : MonoBehaviour
{   

    public const string level = "LevelNumber";

   public static void SetInt(string key, int value){
       PlayerPrefs.SetInt(key, value);
   }

   public static int GetInt(string key){
       return PlayerPrefs.GetInt(key);
   }
}
