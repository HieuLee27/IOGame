using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPlayer : MonoBehaviour
{
    public InfoData infoData;
    [SerializeField] private TMP_InputField namePlayer;
    [SerializeField] private TMP_InputField agePlayer;
    [SerializeField] private TMP_InputField isMalePlayer;

    public void SaveData(ref InfoData data)
    {
        infoData.nickName = namePlayer.text;
        infoData.age = agePlayer.text;
        infoData.isMale = isMalePlayer.text;
        data = infoData;
    }

    public void LoadData(InfoData data)
    {
        infoData = data;

        namePlayer.text = infoData.nickName;
        agePlayer.text = infoData.age;
        isMalePlayer.text = infoData.isMale;
    }
}

[System.Serializable]
public struct InfoData
{
    public string nickName;
    public string age;
    public string isMale;
}
