
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    

    public static void SaveLevelFile(LevelFileData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saveData.hds";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static void SaveFromLevelClearedDict()
    {
        LevelFileData newSaveData = Management.Instance.levelData;
        
        for (int i = 0; i < newSaveData.levelnames.Length; i ++)
        {
            newSaveData.levelWonStatus[i] = Management.Instance.levelCleared[newSaveData.levelnames[i]];
        }
        SaveLevelFile(newSaveData);
    }

    public static LevelFileData LoadLevelFile()
    {

        string path = Application.persistentDataPath + "/saveData.hds";


        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelFileData loadData = (LevelFileData) formatter.Deserialize(stream);
            stream.Close();

            return loadData;
        }
        else
        {
            return null;
        }
    }

    public static bool SaveDataExists()
    {
        string path = Application.persistentDataPath + "/saveData.hds";
        Debug.Log(path);

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
