using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider sliderLoad;
    public GameObject LoadingScene;
    public GameObject HomeScene;

    public void LoadScene()
    {
        LoadingScene.SetActive(true);
        HomeScene.SetActive(false);

        StartCoroutine(WaitScript());
        StartCoroutine(LoadLevelASync());
    }

    IEnumerator WaitScript()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator LoadLevelASync()
    {
        AsyncOperation progress = SceneManager.LoadSceneAsync("GameScene");

        while (!progress.isDone)
        {
            float progression = Mathf.Clamp01(progress.progress / 0.9f);

            sliderLoad.value = progression;
            yield return null;
        }
    }
}
