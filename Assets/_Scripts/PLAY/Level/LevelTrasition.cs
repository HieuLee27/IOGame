using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTrasition : MonoBehaviour
{
    public GameProgress progressI;
    public int levelAmount;

    [Space]
    [Header("Fixed UI")]
    public TMP_Text level;
    public TMP_Text coint;
    public Slider sliderMana;

    private DataValue data;

    private void Start()
    {
        UpdateValue();
    }

    public void Transition() //Translate to next level text
    {
        if (level.text == levelAmount.ToString())
        {
            ManagerGame.instance.result = ManagerGame.Results.Win;

            string filePath = Defaut.FileName();
            string dataFile = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<DataValue>(dataFile);
            int realCoin = data.coin;
            realCoin += int.Parse(coint.text);
            data.coin = realCoin;

            File.WriteAllText(filePath, JsonUtility.ToJson(data, true));
            GameObject.Find("BtnNextLevel").gameObject.SetActive(false);
        }
        else
        {
            int levelTmp = int.Parse(level.text);
            levelTmp++;
            level.text = levelTmp.ToString();

            NextLevelScene();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameObject.Find("BtnNextLevel").gameObject.SetActive(false);
        }
    }

    public static string FileName()
    {
        string path = Application.persistentDataPath + "/Level.json";
        return path;
    }

    public void NextLevelScene()
    {
        progressI.coin = coint.text;
        progressI.mana = sliderMana.value;
        progressI.chapter = level.text;

        File.WriteAllText(FileName(), JsonUtility.ToJson(progressI, true));
    }

    public void UpdateValue()
    {
        string data = File.ReadAllText(FileName());
        progressI = JsonUtility.FromJson<GameProgress>(data);

        coint.text = progressI.coin;
        level.text = progressI.chapter;
        sliderMana.value = progressI.mana;
    }
}

[System.Serializable]
public struct GameProgress
{
    public string coin;
    public string chapter;
    public float mana;
}
