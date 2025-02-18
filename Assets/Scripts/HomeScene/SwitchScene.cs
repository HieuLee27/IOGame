using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject storyPanel;
    private bool isInfoOpen = false;

    [SerializeField] private GameObject loadScene;
    [SerializeField] private Slider sliderLoad;

    public AudioSource source;
    public AudioClip fantasy;

    private void Start()
    {
        source.PlayOneShot(fantasy);
    }

    public void OpenInfo()
    {
        isInfoOpen = !isInfoOpen;
        infoPanel.SetActive(isInfoOpen);
    }

    public void OpenStory()
    {
        isInfoOpen = !isInfoOpen;
        storyPanel.SetActive(isInfoOpen);
    }

    public void SwitchPlayScene()
    {
        loadScene.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        sliderLoad.value = Mathf.Clamp01(async.progress / 0.9f);
        SceneManager.LoadScene(1);
    }
}
