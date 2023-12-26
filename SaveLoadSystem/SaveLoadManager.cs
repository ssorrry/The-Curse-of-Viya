using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static void SaveGameProgress(GameProgress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //string filePath = Application.persistentDataPath + "/gameProgress.dat";

        string filePath = Application.persistentDataPath + progress.nameFile;

        using (FileStream fileStream = File.Create(filePath))
        {
            binaryFormatter.Serialize(fileStream, progress);
        }
    }

    public static GameProgress LoadGameProgress(string nameFile)
    {
        string filePath = Application.persistentDataPath + nameFile;

        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                return (GameProgress)binaryFormatter.Deserialize(fileStream);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }
    }
}
