using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ShowInfoPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text age;
    [SerializeField] private TMP_Text nickName;

    private SaveInfoPlayer.SaveData saveData;

    // Update is called once per frame
    void Update()
    {
        saveData = JsonUtility.FromJson<SaveInfoPlayer.SaveData>(File.ReadAllText(SaveInfoPlayer.FileDataName()));
        age.text = saveData.infoData.age.ToString();
        nickName.text = saveData.infoData.nickName.ToString();
    }
}
