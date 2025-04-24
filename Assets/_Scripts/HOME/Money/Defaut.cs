using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Defaut : MonoBehaviour
{
    public TMP_Text coin;
    private DataValue dataCoint;
    private GameProgress data;

    private void Start()
    {
        GetValue();
    }

    private void Update()
    {
        UpdateJsonValue();
    }

    public static string FileName()
    {
        string path = Application.persistentDataPath + "/Money.json";
        return path;
    }

    public void GetValue()
    {
        if(!File.Exists(FileName()))
        {
            dataCoint.coin = 0;
            File.WriteAllText(FileName(), JsonUtility.ToJson(dataCoint, true));
        }
        string data = File.ReadAllText(FileName());

        dataCoint = JsonUtility.FromJson<DataValue>(data);
        coin.text = dataCoint.coin.ToString();
    }

    public void UpdateJsonValue()
    {
        dataCoint.coin = int.Parse(coin.text);
        File.WriteAllText(FileName(), JsonUtility.ToJson(dataCoint, true));
    }

    public void ResetJsonValue()
    {
        string filePath = LevelTrasition.FileName();

        if (File.Exists(filePath))
        {
            data.coin = "0";
            data.mana = 0;
            data.chapter = "1";
            File.WriteAllText(filePath, JsonUtility.ToJson(data, true));
        }
    }
}

[System.Serializable]
public struct DataValue 
{
    public int coin;
}
