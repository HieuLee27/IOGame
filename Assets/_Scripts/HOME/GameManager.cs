using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = new();
    public InfoPlayer infoPlayer;
    public InfoItem infoItem;
    private void Awake()
    {
        instance = this;
    }

    public void Save()
    {
        SaveInfoPlayer.Save();
    }

    public void Load()
    {
        SaveInfoPlayer.Load();
    }

    public void LoadItem()
    {
        ShowInfoItem.LoadData();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
