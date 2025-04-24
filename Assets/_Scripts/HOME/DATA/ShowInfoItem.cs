using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoItem
{
    public static DataItem dataItem = new();

    //Create path file
    public static string FilePath()
    {
        string path = Application.persistentDataPath + "/DataItem" + ".json";
        return path;
    }

    public static void LoadData()
    {
        string dataContent = File.ReadAllText(FilePath());

        dataItem = JsonUtility.FromJson<DataItem>(dataContent);

        HandleLoadData();
    }

    public static void HandleLoadData()
    {
        GameManager.instance.infoItem.LoadData(dataItem.infoItems);
    }

    //public static void SaveData()
    //{
    //    dataItem.infoItems = new DataInfoItem[8];
    //    for (int i = 0; i < 8; i++)
    //    {
    //        dataItem.infoItems[i].name = "Hieu";
    //    }
    //    File.WriteAllText(FilePath(), JsonUtility.ToJson(dataItem, true));
    //}

    [System.Serializable]
    public struct DataItem
    {
        public DataInfoItem[] infoItems;
    }
}
