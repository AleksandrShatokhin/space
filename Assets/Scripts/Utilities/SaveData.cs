using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveData
{
    public int levelNumber;
}

public class SaveGameData
{
    public static void SaveLevelNumber()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + "/DataLevel.dat");
        SaveData saveData = new SaveData();

        saveData.levelNumber = GameController.LevelNumber;
        binaryFormatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public static bool TutorialComlete()
    {
        bool isTutorialComlete;

        if (File.Exists(Application.persistentDataPath + "/DataLevel.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/DataLevel.dat", FileMode.Open);
            SaveData saveData = (SaveData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            if (saveData.levelNumber != 0)
            {
                isTutorialComlete = true;
            }
            else
            {
                isTutorialComlete = false;
            }    
        }
        else
        {
            isTutorialComlete = false;
        }

        return isTutorialComlete;
    }
}