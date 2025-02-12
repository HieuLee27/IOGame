using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{
    private bool isPaused = false;
    public Slider levelBar;
    public GameObject panelSkills;
    public GameObject skillPrefab;

    private void Update()
    {
        Selection();
    }

    private void Selection()
    {
        if (levelBar.value == 1)
        {
            panelSkills.SetActive(true);
            TogglePause();
            levelBar.value = 0;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Dừng toàn bộ game

        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

}
