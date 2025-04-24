using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveInfoPlayer
{
    public static SaveData dataInfo = new();

    public static string FileDataName()
    {
        string fileName = Application.persistentDataPath + "/InfoPlayer" + ".json";
        return fileName;
    }

    public static void Save()
    {
        HandleSaveData();
        File.WriteAllText(FileDataName(), JsonUtility.ToJson(dataInfo,true));
    }

    public static void HandleSaveData()
    {
        GameManager.instance.infoPlayer.SaveData(ref dataInfo.infoData);
    }

    public static void Load()
    {
        string dataContent = File.ReadAllText(FileDataName());

        dataInfo = JsonUtility.FromJson<SaveData>(dataContent);

        Debug.Log(dataContent);
        
        HandleLoadData();
    }

    public static void HandleLoadData()
    {
        GameManager.instance.infoPlayer.LoadData(dataInfo.infoData);
    }

    [System.Serializable]
    public struct SaveData 
    {
        public InfoData infoData;
    }
}
